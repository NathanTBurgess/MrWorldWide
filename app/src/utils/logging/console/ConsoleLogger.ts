import {LogLevel} from "../LogLevel";
import {LoggingAdapter} from "../LoggingAdapter";
import {object} from "yup";

export interface ConsoleLoggerConfiguration {
    logLevel?: LogLevel;
}

// Utility function to format the timestamp
function formatTimestamp(date: Date): string {
    return date.toISOString().substr(11, 8);
}

// Utility function to format the log level
function formatLogLevel(level: LogLevel): string {
    switch (level) {
        case LogLevel.Trace:
            return "TRC";
        case LogLevel.Debug:
            return "DBG";
        case LogLevel.Information:
            return "INF";
        case LogLevel.Warning:
            return "WRN";
        case LogLevel.Error:
            return "ERR";
        case LogLevel.Critical:
            return "CRT";
        case LogLevel.None:
            return "N/A";

    }
}

function styleMessage(messageTemplateArray: string[], templateProperties: {[key: string]: string}): string {
    let styledMessage = "";
    let templated = true;
    for (let i = 0; i < messageTemplateArray.length; i++) {
        if(templated){
            styledMessage += messageTemplateArray[i];
        }
        else{
            const key = messageTemplateArray[i];
            styledMessage += templateProperties[key];
        }

        if (i < messageTemplateArray.length - 1) {
            styledMessage += "%c";
        }
        templated = !templated;
    }

    return styledMessage;
}

const baseStyle = "font-weight: bold;";
const defaultStyle = `${baseStyle} background: inherit; color: inherit;`;

// Utility function to style the console log output
function consoleLogLevelStyle(level: LogLevel): string {

    if (level === LogLevel.Error || level === LogLevel.Critical) {
        return `${baseStyle} background: red;`;
    }
    if (level == LogLevel.Warning) {
        return `${baseStyle} color: yellow;`;
    }
    if (level == LogLevel.Debug) {
        return `${baseStyle} color: #D3D3D3;`;
    }

    return `${baseStyle} color: inherit;`;
}

function consoleArgumentStyle(isNumber: boolean): string {

    if (isNumber) {
        return `${baseStyle} color: #CBC3E3;`;
    }

    return `${baseStyle} color: teal;`;
}

export default class ConsoleLogger implements LoggingAdapter {
    private readonly currentLogLevel: LogLevel;
    constructor(private readonly configuration: ConsoleLoggerConfiguration) {
        this.currentLogLevel = configuration.logLevel ?? LogLevel.Debug;
    }

    name = "Console";


    log(level: LogLevel, messageTemplateArray: string[], templateVals: { [p: string]: string }): void {
        if (level < this.currentLogLevel) {
            return;
        }
        const timestamp = formatTimestamp(new Date());
        const levelString = formatLogLevel(level);
        const message = styleMessage(messageTemplateArray, templateVals);
        console.log(
            `[${timestamp} %c${levelString}%c] ${message}`,
            ...[
                consoleLogLevelStyle(level),
                defaultStyle,
                ...Object.keys(templateVals).flatMap((prop) => [
                    consoleArgumentStyle(!isNaN(parseFloat(prop))),
                    defaultStyle
                ])
            ]
        );
    }
}