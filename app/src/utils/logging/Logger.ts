import {LoggingAdapter} from "./LoggingAdapter";
import {LogLevel} from "./LogLevel";
import {parseTemplateLikeString} from "./loggingHelpers";
import {ProblemDetails} from "../../domain/models";
import {isProblemDetails} from "../guards";

export type ErrorTypes = Error | ProblemDetails;

export interface ILogger {
    debug(messageTemplate: string, properties?: { [key: string]: string }): void,

    info(messageTemplate: string, properties?: { [key: string]: string }): void,

    warn(messageTemplate: string, properties?: { [key: string]: string }): void,

    error(error: ErrorTypes, messageTemplate?: string, properties?: { [key: string]: string }): void,
}

export interface StructuredLog {
    messageTemplate: string,
    properties: { [key: string]: string },
}

export class Logger implements ILogger {
    constructor(private readonly adapter: LoggingAdapter) {
    }

    debug(messageTemplate: string, properties: { [key: string]: string } = {}): void {
        const templateLike = parseTemplateLikeString(messageTemplate);
        this.adapter.log(LogLevel.Debug, templateLike, properties);
    }

    info(messageTemplate: string, properties: { [key: string]: string } = {}): void {
        const templateLike = parseTemplateLikeString(messageTemplate);
        this.adapter.log(LogLevel.Information, templateLike, properties);
    }

    warn(messageTemplate: string, properties: { [key: string]: string } = {}): void {
        const templateLike = parseTemplateLikeString(messageTemplate);
        this.adapter.log(LogLevel.Warning, templateLike, properties);
    }

    error(error: ErrorTypes, messageTemplate = "", properties: { [key: string]: string }= {}): void {
        const structuredErrorLog = this.formatErrorType(error);
        const templateLike = parseTemplateLikeString(messageTemplate ?? '' + '\n' + structuredErrorLog.messageTemplate);
        this.adapter.log(LogLevel.Error, templateLike, {...properties, ...structuredErrorLog.properties});
    }

    private formatErrorType(error: ErrorTypes) {
        if (isProblemDetails(error)) {
            return this.formatProblemDetails(error);
        }
        return this.formatJavascriptError(error);
    }

    private formatJavascriptError(jsError: Error): StructuredLog {
        return {messageTemplate: jsError.toString(), properties: {}};
    }

    private formatProblemDetails(apiError: ProblemDetails): StructuredLog {
        let messageTemplate = "{title}: {status}.\n{detail}";
        if(apiError.error){
            messageTemplate += "\nAdditional details have been provided\n{exception}: {message}\n{stackTrace}";
        }
        return {
            messageTemplate,
            properties: {
                title: apiError.title,
                status: apiError.status.toString(),
                detail: apiError.detail,
                exception: apiError.error?.name ?? "n/a",
                message: apiError.error?.message ?? "n/a",
                stackTrace: apiError.error?.stackTrace ?? "n/a"
            }
        }
    }
}