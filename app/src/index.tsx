import React from "react";
import {createRoot} from 'react-dom/client';
import {BrowserRouter} from "react-router-dom";
import "./index.css";
import App from "./App";
import reportWebVitals from "./reportWebVitals";
import * as serviceWorkerRegistration from "./serviceWorkerRegistration";
import {CssBaseline, ThemeProvider} from "@mui/material";
import {defaultTheme} from "./themes/defaultTheme";
import { AdapterMoment } from '@mui/x-date-pickers/AdapterMoment';
import {SnackbarProvider} from "./utils/snackbar";
import ModalProvider from "./utils/modal/ModalProvider";
import {LocalizationProvider} from "@mui/x-date-pickers";
import LoggingProvider, {LoggerTypes} from "./utils/logging/LoggingProvider";
import ConsoleLogger from "./utils/logging/console/ConsoleLogger";
import {LogLevel} from "./utils/logging";
import SeqLogger from "./utils/logging/seq/SeqLogger";
import { isDevelopment } from "./utils/env";

const baseUrl = document.getElementsByTagName("base")[0].getAttribute("href");
const rootElement = document.getElementById("root");
// eslint-disable-next-line @typescript-eslint/no-non-null-assertion
const root = createRoot(rootElement!);
const logLevel: LogLevel = isDevelopment() ? LogLevel.Debug : LogLevel.Information;
const loggers: LoggerTypes[] = [
    new ConsoleLogger({logLevel}),
    {
        logger: new SeqLogger({
            logLevel,
            serverUrl: "http://seq:80"
        }),
        devOnly: true
    }
]
root.render(
    <React.StrictMode>
        <LoggingProvider loggers={loggers}>
            <LocalizationProvider dateAdapter={AdapterMoment} adapterLocale={"en-us"}>
                <ThemeProvider theme={defaultTheme}>
                    <CssBaseline/>
                    <BrowserRouter basename={baseUrl ?? undefined}>
                        <SnackbarProvider>
                            <ModalProvider>
                                <App/>
                            </ModalProvider>
                        </SnackbarProvider>
                    </BrowserRouter>
                </ThemeProvider>
            </LocalizationProvider>
        </LoggingProvider>
    </React.StrictMode>
);

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://cra.link/PWA
serviceWorkerRegistration.unregister();
// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
