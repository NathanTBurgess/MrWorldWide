import React from "react";
import AppRoutes from "./App.Routes";
import {GoogleOAuthProvider} from "@react-oauth/google";

function App() {

    const clientId = process.env["REACT_APP_GOOGLE_CLIENT_ID"] ?? "";
    return (
        <GoogleOAuthProvider clientId={clientId}>
            <AppRoutes/>
        </GoogleOAuthProvider>
    );
}

export default App;
