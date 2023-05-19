import React, {createContext, ReactNode, useContext, useEffect, useState} from 'react';
import {useSessionStorage} from "../hooks";
import {ClaimsIdentity} from "./claims/ClaimsIdentity";
import jwt, {JwtPayload} from "jsonwebtoken";
import {Claim} from "./claims/Claim";
import {IdentityChangedEvent, TokenExpiringEvent} from "../events";


export interface TokenProviderProps {
    expiryThreshold: number
}

// Define the shape of the context
export interface TokenContextProps {
    setToken: (accessToken: string) => void;
    token: string | null;
    identity: ClaimsIdentity | null;
    clear: () => void;
}

const TokenContext = createContext<TokenContextProps | undefined>(undefined);

const DEFAULT_EXPIRY_THRESHOLD = 5 * 60 * 1000; // 5 minutes

function TokenProvider({children, expiryThreshold = DEFAULT_EXPIRY_THRESHOLD}: TokenProviderProps & {
    children: (ReactNode | ReactNode[])
}) {
    const [token, setTokenInternal] = useSessionStorage('access_token');
    const [identity, setIdentity] = useState<ClaimsIdentity | null>(null);
    const [expiry, setExpiry] = useState<number | null>(null);

    // When the token changes, parse and set the claims
    useEffect(() => {
        if (token) {
            const decoded = jwt.decode(token, {json: true});
            if (decoded === null) {
                throw new Error("Unable to decode the provided JSON web token");
            }
            handlePayload(decoded);
        } else {
            setIdentity(null);
            setExpiry(null);
        }
    }, [token]);

    function handlePayload(token: JwtPayload): void {
        const claims = Object.keys(token).map(key => new Claim(key, token[key].toString()));
        const identity = new ClaimsIdentity(claims);
        setIdentity(identity);
        IdentityChangedEvent.dispatch({detail: identity});
        const exp = token.exp ? token.exp * 1000 : null;
        setExpiry(exp); // Convert to milliseconds
    }

    // Monitor for token expiration
    useEffect(() => {
        if (token && expiry) {
            const timeoutId = setTimeout(() => {
                // Broadcast a 'tokenExpiring' event
                TokenExpiringEvent.dispatch({detail: token});
            }, Math.max(0, expiry - Date.now() - expiryThreshold));
            return () => clearTimeout(timeoutId);
        }
    }, [expiry, expiryThreshold, token]);

    const setToken = (accessToken: string) => {
        const decoded = jwt.decode(accessToken, {json: true});
        if (decoded === null) {
            throw new Error("Unable to decode the provided JSON web token");
        }
        setTokenInternal(accessToken);
        handlePayload(decoded);
    };

    const clear = () => {
        setTokenInternal(null);
    }


    return (
        <TokenContext.Provider value={{setToken, token, identity, clear}}>
            {children}
        </TokenContext.Provider>
    );
}

// Custom hook to use the TokenContext
export const useToken = () => {
    const context = useContext(TokenContext);
    if (context === undefined) {
        throw new Error('useToken must be used within a TokenProvider');
    }
    return context;
};

export default React.memo(TokenProvider);
