import React, {ReactNode, useEffect, useState} from "react";
import {AuthContext} from "./AuthContext";
import SilentRefresh from "./SilentRefresh";
import AxiosConfig from "./AxiosConfig";
import {UserManager} from "./UserManager";
import {useToken} from "./TokenProvider";
import {useLogger} from "../logging";
import {AuthState} from "./AuthState";
import {User} from "./User";
import {CustomEventArgs} from "../events";

function AuthProvider({children}: { children: ReactNode }) {
    const tokenContext = useToken();
    const [userManager] = useState(new UserManager(tokenContext, useLogger(UserManager)));
    const user = userManager.user;
    const state: AuthState = user ?
        {userManager, isAuthenticated: true, user: user} :
        {userManager, isAuthenticated: false};
    // useEffect(()=>{
    //     userManager.events.addUserLoaded(handleUserLoaded);
    //     userManager.events.addUserSignedOut(handleUserSignedOut);
    //     return () =>{
    //         userManager.events.removeUserLoaded(handleUserLoaded);
    //         userManager.events.removeUserSignedOut(handleUserSignedOut);
    //     }
    // },[]);

    // function handleUserLoaded({detail}: CustomEventArgs<User>){
    //     setUser(detail);
    // }
    // function handleUserSignedOut() {
    //     setUser(null);
    // }
    return (
        <AuthContext.Provider value={state}>
            <AxiosConfig/>
            <SilentRefresh/>
            {children}
        </AuthContext.Provider>
    );
}

export default React.memo(AuthProvider);
