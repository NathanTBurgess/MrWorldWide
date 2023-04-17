export interface StructuredLogDefinition {
    messageTemplate: string;
    properties: Record<string, string>;
}

export function structureLog(
    message: TemplateStringsArray,
    ...propertyValues: string[]
): StructuredLogDefinition {
    const propertyNames = message
        .reduce((acc, str, index) => {
            return (
                acc +
                (index > 0
                    ? `{property${index - 1}}`
                    : str.replace(/\$/g, () => "$$$"))
            );
        }, "")
        .split(/[^{]*{\s*([^}\s]+)\s*}[^{]*/)
        .filter((_, index) => index % 2 === 1);

    const properties: Record<string, string> = {};
    propertyNames.forEach((name, index) => {
        properties[name] = propertyValues[index];
    });

    const messageTemplate = message.raw.reduce((acc, str, index) => {
        return (
            acc +
            (index > 0
                ? `{${propertyNames[index - 1]}}`
                : str.replace(/\$/g, () => "$$$"))
        );
    }, "");

    return {
        messageTemplate,
        properties,
    };
}
