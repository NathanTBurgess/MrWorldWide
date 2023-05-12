import {LogLevel} from "./LogLevel";

export interface LoggingAdapter {
    name: string;
    log(level: LogLevel, messageTemplateArray: string[], templateProperties: {[key: string]: string}): void;
}