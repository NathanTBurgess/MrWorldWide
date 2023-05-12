export interface StructuredLogDefinition {
    messageTemplate: string;
    properties: Record<string, string>;
}

export function parseTemplateLikeString(str: string): string[] {
    const regex = /\$\{([^}]+)\}/g;
    let match;
    let lastIndex = 0;
    const result: string[] = [];

    while ((match = regex.exec(str)) !== null) {
        if (match.index !== lastIndex) {
            result.push(str.substring(lastIndex, match.index));
        }

        if(result.length === 0){
            result.push('');
        }
        result.push(match[1]);
        lastIndex = match.index + match[0].length;
    }

    if (lastIndex !== str.length) {
        result.push(str.substring(lastIndex));
    }
    return result;
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
