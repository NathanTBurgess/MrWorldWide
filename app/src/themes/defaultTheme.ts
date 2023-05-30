import {createTheme} from "@mui/material";

export const defaultTheme = createTheme({
    palette: {
        background: {
            default: "#f5f5f5", // Lighter background color
        },
        primary: {
            main: "#283593", // Darker primary color
            light: "#C5CAE9",
            dark: "#303F9F",
            contrastText: "#FFFFFF",
        },
        secondary: {
            main: "#ffffff",
            light: "#ffffff",
            dark: "#ffffff",
            contrastText: "#FFFFFF",
        },
        error: {
            main: "#f44336",
        },
        info: {
            main: "#2196f3",
        },
        success: {
            main: "#4caf50",
        },
        warning: {
            main: "#ff9800",
        }
    },
    typography: {
        fontSize: 12,
        fontFamily: "Arial",
        body1: {
            fontWeight: 300,
            fontSize: "14px",
            lineHeight: "16px",
        },
    }

});
