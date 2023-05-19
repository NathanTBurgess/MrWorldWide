namespace MrWorldwide.WebApi.Infrastructure.Configuration;

public static class ConfigurationUtility
{
    public static string ParseSectionName<TOptions>()
    {
        const string suffix = "Options";
        var optionName = typeof(TOptions).Name;
        if (!optionName.EndsWith(suffix, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException(
                "Invalid use of BindOptions. Options types must conform to the {NAME}Options naming convention");
        }

        var optionsCharacterCount = suffix.Length;
        var configSection = optionName.Remove(optionName.Length - optionsCharacterCount, optionsCharacterCount);
        if (string.IsNullOrWhiteSpace(configSection))
        {
            throw new InvalidOperationException("Unable to parse config section, insufficient characters");
        }

        return configSection;
    }
}