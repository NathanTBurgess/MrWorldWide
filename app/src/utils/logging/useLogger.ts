// src/useLogger.ts
import { useContext } from "react";
import { LoggerContext } from "./LoggerContext";
import { LogLevel } from "./LogLevel";
import {Logger} from "./Logger";
import {LoggingAdapter} from "./LoggingAdapter";
import {parseTemplateLikeString} from "./loggingHelpers";

export function useLogger(): Logger {
    const loggingAdapters = useContext(LoggerContext);
    const log = (level: LogLevel, messageTemplate: string, properties: {[key: string]: string}): void => {
        loggingAdapters.forEach((logger) => logger.log(level, parseTemplateLikeString(messageTemplate), properties));
    };
    const logAllAdapter: LoggingAdapter = {
        log: (message)=> console.log(message),
        name: "All"
    };
    return new Logger(logAllAdapter);
}
