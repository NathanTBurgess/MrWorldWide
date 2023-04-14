import React from "react";
import AppRoutes from "./App.Routes";
import {GoogleOAuthProvider} from "@react-oauth/google";

function App() {
    // const oidcEnvironmentConfig = useOidcEnvironmentConfiguration();
    return (
        // <AuthProvider {...oidcEnvironmentConfig}>
        <GoogleOAuthProvider clientId={""}>
            <AppRoutes/>
        </GoogleOAuthProvider>
        ///*</AuthProvider>*/
    );
}

export default App;
