import {LoggingAdapter} from "./LoggingAdapter";
import {LogLevel} from "./LogLevel";
import {parseTemplateLikeString} from "./loggingHelpers";

export class Logger {
    constructor(private readonly adapter: LoggingAdapter) {
    }
    debug(messageTemplate: string, properties: {[key: string]: string}): void{
        const templateLike = parseTemplateLikeString(messageTemplate);
        this.adapter.log(LogLevel.Debug, templateLike, properties);
    }
    info(messageTemplate: string, properties: {[key: string]: string}): void{
        const templateLike = parseTemplateLikeString(messageTemplate);
        this.adapter.log(LogLevel.Information, templateLike, properties);
    }

    warn(messageTemplate: string, properties: {[key: string]: string}): void{
        const templateLike = parseTemplateLikeString(messageTemplate);
        this.adapter.log(LogLevel.Warning, templateLike, properties);
    }

    error(messageTemplate: string, properties: {[key: string]: string}): void{
        const templateLike = parseTemplateLikeString(messageTemplate);
        this.adapter.log(LogLevel.Error, templateLike, properties);
    }

    critical(messageTemplate: string, properties: {[key: string]: string}): void{
        const templateLike = parseTemplateLikeString(messageTemplate);
        this.adapter.log(LogLevel.Critical, templateLike, properties);
    }

}