// src/useLogger.ts
import { useContext } from "react";
import { LoggerContext } from "./LoggerContext";
import { LogLevel } from "./LogLevel";
import {Logger} from "./Logger";
import {LoggingAdapter} from "./LoggingAdapter";

export function useLogger(): Logger {
    const loggingAdapters = useContext(LoggerContext);
    const log = (level: LogLevel, message: TemplateStringsArray,
                 ...properties: string[]): void => {
        loggingAdapters.forEach((logger) => logger.log(level, message, ...properties));
    };
    const logAllAdapter: LoggingAdapter = {
        log,
        name: "All"
    };
    return new Logger(logAllAdapter);
}
