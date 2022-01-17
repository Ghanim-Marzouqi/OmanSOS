namespace OmanSOS.Mobile.Utilities;

public static class ColorConfig
{
    public const string PRIMARY = "#383B64";
    public const string SECONDARY = "#F7A400";
    public const string ACCENT = "";
    public const string SUCCESS = "#06D79C";
    public const string INFO = "";
    public const string WARNING = "";
    public const string ERROR = "";
    public const string BACKGROUND = "#FFFFFF";
    public const string APPBAR_BACKGROUND = "#383B64";
    public const string DRAWER_BACKGROUND = "#FFFFFF";
    public const string DRAWER_TEXT = "#000000B3";
}

public static class TableStyleConfig
{
    public const string HEADER = "background-color: #3A9EFD; color: white; font-weight: bold";
}

public static class ExpansionStyleConfig
{
    public const string EXPANSION_PANEL = "background-color: #EBF1F4";
}

public static class ColorConvertor
{
    public static MudBlazor.Color Convert(string colorCode)
    {
        return colorCode switch
        {
            "PRIMARY" => MudBlazor.Color.Primary,
            "SECONDARY" => MudBlazor.Color.Secondary,
            "INFO" => MudBlazor.Color.Info,
            "SUCCESS" => MudBlazor.Color.Success,
            "WARNING" => MudBlazor.Color.Warning,
            "ERROR" => MudBlazor.Color.Error,
            "DARK" => MudBlazor.Color.Dark,
            _ => MudBlazor.Color.Default,
        };
    }
}

public static class BrowserStorage
{
    public const string LOCAL_STORAGE = "localStorage";
    public const string SESSION_STORAGE = "sessionStorage";
}

public static class AuthorizationType
{
    public const string Bearer = "Bearer";
    public const string Cache = "Cache";
}
