import {LogLevel} from "./LogLevel";

export interface LoggingAdapter {
    name: string;
    log(level: LogLevel, message: TemplateStringsArray,
        ...properties: string[]): void
}