import {LoggingAdapter} from "./LoggingAdapter";
import {LogLevel} from "./LogLevel";

export class Logger {
    constructor(private readonly adapter: LoggingAdapter) {
    }
    debug(message: TemplateStringsArray,
          ...properties: string[]): void{
        this.adapter.log(LogLevel.Debug, message, ...properties);
    }

    info(message: TemplateStringsArray,
         ...properties: string[]): void{
        this.adapter.log(LogLevel.Information, message, ...properties);
    }

    warn(message: TemplateStringsArray,
         ...properties: string[]): void{
        this.adapter.log(LogLevel.Warning, message, ...properties);
    }

    error(message: TemplateStringsArray,
          ...properties: string[]): void{
        this.adapter.log(LogLevel.Error, message, ...properties);
    }

    critical(message: TemplateStringsArray,
             ...properties: string[]): void{
        this.adapter.log(LogLevel.Critical, message, ...properties);
    }
}