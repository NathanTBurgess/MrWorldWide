// src/SeqLogProvider.tsx
import {Logger as SeqLoggingLogger} from "seq-logging";
import {LogLevel} from "../LogLevel";
import {structureLog} from "../loggingHelpers";
import {LoggingAdapter} from "../LoggingAdapter";

interface SeqLoggerConfiguration {
    serverUrl: string;
    apiKey?: string;
    logLevel?: LogLevel;
    onError?: (e: Error) => void;
}

export default class SeqLogger implements LoggingAdapter {
    private readonly seqLogger: SeqLoggingLogger;
    private readonly currentLogLevel: LogLevel;

    constructor(private readonly configuration: SeqLoggerConfiguration) {
        this.seqLogger = new SeqLoggingLogger({
            serverUrl: configuration.serverUrl,
            apiKey: configuration.apiKey,
            onError: configuration.onError ?? ((e) => {
                console.error("Seq logging error:", e);
            })
        });
        this.currentLogLevel = configuration.logLevel ?? LogLevel.Debug;
    }


    name = "Seq";

    log(level: LogLevel, rawMessage: TemplateStringsArray,
        ...properties: string[]): void {
        if (level < this.currentLogLevel) {
            return;
        }
        const structuredLog = structureLog(rawMessage, ...properties)
        this.seqLogger.emit({
            timestamp: new Date(),
            level: level.toString(),
            messageTemplate: structuredLog.messageTemplate,
            properties: structuredLog.properties
        })
    }
}
