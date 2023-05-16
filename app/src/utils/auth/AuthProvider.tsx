import React, { ReactNode, useEffect, useState } from "react";
import { AuthContext } from "./AuthContext";
import SilentRefresh from "./SilentRefresh";
import AxiosConfig from "./AxiosConfig";
import {AuthorizationsApi} from "../../api/AuthorizationsApi";
import {ClaimsIdentity} from "./claims/ClaimsIdentity";
import {TokenContextProps} from "./TokenProvider";
import {CustomEventMethods} from "../events/CustomEventMethods";
export interface User {
    name: string;
    id: string;
    email: string;
}
export interface UserManagerEvents {
    addUserLoaded(callbackFn: EventListenerOrEventListenerObject): void;
    removeUserLoaded(callbackFn: EventListenerOrEventListenerObject): void;
    addUserSignedOut(callbackFn: EventListenerOrEventListenerObject):void;
    removeUserSignedOut(callbackFn: EventListenerOrEventListenerObject):void;
    addAccessTokenExpiring(callbackFn: EventListenerOrEventListenerObject): void;
    removeAccessTokenExpiring(callbackFn: EventListenerOrEventListenerObject): void;
}
export class UserManager {
    private user: User | null = null;
    private userLoadedEventMethods = new CustomEventMethods("userLoaded");
    private userSignedOutEventMethods= new CustomEventMethods("userSignedOut");
    private accessTokenExpiringEventMethods= new CustomEventMethods("tokenExpiring");
    public get events(): UserManagerEvents {
        return {
            addUserLoaded: this.userLoadedEventMethods.addListener,
            removeUserLoaded: this.userLoadedEventMethods.removeListener,
            addUserSignedOut: this.userSignedOutEventMethods.addListener,
            removeUserSignedOut: this.userSignedOutEventMethods.removeListener,
            addAccessTokenExpiring: this.accessTokenExpiringEventMethods.addListener,
            removeAccessTokenExpiring: this.accessTokenExpiringEventMethods.removeListener
        }
    }
    constructor(private readonly tokenProps: TokenContextProps) {
        const identity = tokenProps.getIdentity();
        if(identity){
            this.updateUserFromClaimsIdentity(identity);
        }
    }
    async handleGoogleSso(idToken: string): Promise<User>{
        const {accessToken} = await AuthorizationsApi.authorizeGoogleSignin(idToken);
        this.tokenProps.setToken(accessToken);
        const identity = this.tokenProps.getIdentity();
        if(identity === null){
            throw new Error("Unexpected behavior in user manager. Claim Identity does not have a value so no user could be stored");
        }
        return this.updateUserFromClaimsIdentity(identity);
    }
    private updateUserFromClaimsIdentity(claims: ClaimsIdentity): User{
        const {id, name, email} = claims;
        if(!id){
            throw new Error("Unable to handle provided Claims Identity as a user ID could not be determined");
        }
        this.user = {
            id,
            name: name ?? "Unknown",
            email: email ?? "Unknown"
        }
        this.userLoadedEventMethods.dispatch({detail: this.user});
        return this.user;
    }
}

function AuthProvider({ children, ...userManagerSettings }: UserManagerSettings & { children: ReactNode }) {
    const [userManager] = useState(
        new UserManager({
            ...userManagerSettings,
            accessTokenExpiringNotificationTimeInSeconds: 600,
            automaticSilentRenew: false,
        }),
    );
    const [authenticationState, setAuthenticationState] = useState<AuthenticationState>("Initializing");
    const [user, setUser] = useState<User | null | undefined>(undefined);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState();
    useEffect(() => {
        if (user === undefined) {
            return;
        }
        setAuthenticationState(user != null && !user.expired ? "Authenticated" : "Unauthenticated");
    }, [user]);
    useEffect(() => {
        userManager
            .getUser()
            .then((user) => {
                setUser(user);
            })
            .catch((e) => setError(e))
            .finally(() => setLoading(false));
        userManager.events.addUserSignedOut(handleUserSignedOut);
        return () => {
            userManager.events.removeUserLoaded(handleUserLoaded);
        };
    }, []);

    async function handleUserLoaded() {
        const loadedUser = await userManager.getUser();
        setUser(loadedUser);
    }

    // function handleUserSignedIn() {
    //     setIsAuthenticated(true);
    // }

    function handleUserSignedOut() {
        setUser(null);
    }

    const state = {
        userManager,
        user: user ?? null,
        isAuthenticated: authenticationState === "Authenticated",
        loading,
        error,
    };
    return (
        <AuthContext.Provider value={state}>
            <AxiosConfig />
            <SilentRefresh />
            {children}
        </AuthContext.Provider>
    );
}

export default React.memo(AuthProvider);
