import React from "react";
import {Route, Routes} from "react-router-dom";
import MainLayout from "./components/Layouts/MainLayout";
import NotFound from "./components/Errors/NotFound";
import Login from "./components/Login/Login";
import CardContentLayout from "./components/Layouts/CardContentLayout";
import {Typography} from "@mui/material";

export const MrWorldWideRoutes = {
    home: "/",
    login: "login"
}

function AppRoutes() {
    return (
        <Routes>
            <Route element={<MainLayout/>}>
                <Route path={"/"}>
                    <Route index element={
                        <CardContentLayout>
                            <Typography variant="h4" gutterBottom>
                                Where in the world is Jackson?
                            </Typography>
                            <Typography>
                                 Find out here soon.
                            </Typography>
                        </CardContentLayout>}/>
                </Route>
                <Route path={MrWorldWideRoutes.login} element={<Login/>}/>
                <Route path={"*"} element={<NotFound/>}/>
            </Route>
        </Routes>
    );
}

export default React.memo(AppRoutes);
