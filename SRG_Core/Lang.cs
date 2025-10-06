using System.Text.Json;

namespace Serger.SRG_Core;

public class Lang
{

    private static readonly JsonSerializerOptions Options = new()
    {
        PropertyNameCaseInsensitive = true
    };
    
    // System messages
    public string Error { get; set; } = "Error";
    public string Success { get; set; } = "Success";
    public string LanguageLoaded { get; set; } = "Language loaded";
    public string SergerStarted { get; set; } = "Serger started";

    // Config messages
    public string ConfigLoaded { get; set; } = "Config loaded successfully";
    public string ConfigSaved { get; set; } = "Config saved successfully";
    public string ConfigNotFound { get; set; } = "Config file not found, created default";
    public string ConfigLoadError { get; set; } = "Error loading configuration";
    public string ConfigSaveError { get; set; } = "Error saving configuration";

    // Monitor messages
    public string MonitorUp { get; set; } = "UP";
    public string MonitorDown { get; set; } = "DOWN";
    public string MonitorExecuteError { get; set; } = "Error executing monitor";
    public string MonitorsLoaded { get; set; } = "monitors loaded from";
    public string MonitorsSaved { get; set; } = "monitors saved to";
    public string MonitorLoadError { get; set; } = "Error loading monitor configuration";
    public string MonitorSaveError { get; set; } = "Error saving monitor configuration";
    public string MonitorConfigNotFound { get; set; } = "Monitor config file not found";

    // Monitor types
    public string PingMonitor { get; set; } = "Ping";
    public string HttpMonitor { get; set; } = "HTTP";
    public string SocketMonitor { get; set; } = "Socket";

    // UI strings for SergerGUI
    public string Welcome { get; set; } = "Welcome to Serger!";
    public string SergerSettings { get; set; } = "Serger Settings";
    public string NewMonitor { get; set; } = "New monitor";
    public string Monitoring { get; set; } = "Monitoring:";
    public string LastCheck { get; set; } = "Last check: {0:G}";
    public string Edit { get; set; } = "Edit";
    public string Remove { get; set; } = "Remove";
    public string Application { get; set; } = "Application";
    public string ExitSerger { get; set; } = "Exit Serger";
    public string Monitor { get; set; } = "Monitor";
    public string CreateNew { get; set; } = "Create new…";
    
    // Monitor Editor strings
    public string MonitorEditor { get; set; } = "Monitor Editor";
    public string ConfigureMonitor { get; set; } = "Configure monitor";
    public string Type { get; set; } = "Type";
    public string Host { get; set; } = "Host";
    public string Uri { get; set; } = "URI";
    public string IntervalMs { get; set; } = "Interval (ms)";
    public string TimeoutMs { get; set; } = "Timeout (ms)";
    public string Port { get; set; } = "Port";
    public string ValidCodes { get; set; } = "Valid codes";
    public string ValidCodesExample { get; set; } = "e.g. 200,201";
    public string BodyRegex { get; set; } = "Body regex";
    public string Ok { get; set; } = "OK";
    public string Cancel { get; set; } = "Cancel";
    
    // Monitor Item View strings
    public string Interval { get; set; } = "Interval";
    public string Timeout { get; set; } = "Timeout";
    public string Codes { get; set; } = "Codes";
    public string Any { get; set; } = "any";
    
    // Notification strings
    public string Down { get; set; } = "Down";
    public string Up { get; set; } = "Up";
    public string IsDown { get; set; } = "is down";
    public string IsBackOnline { get; set; } = "is back online";
    
    // Error messages
    public string UnknownMonitorType { get; set; } = "Unknown monitor type";

    public static Lang LoadLang(string langCode)
    {
        // Prefer per-user app data Langs directory
        var langsDir = CorePaths.GetLangsDirectory();
        var candidatePaths = new[]
        {
            Path.Combine(langsDir, $"{langCode}.json"),
            Path.Combine(AppContext.BaseDirectory ?? string.Empty, "Langs", $"{langCode}.json")
        };

        string? langFile = null;
        foreach (var p in candidatePaths)
        {
            if (File.Exists(p)) { langFile = p; break; }
        }

        if (langFile == null)
        {
            Console.WriteLine($"Language file not found for '{langCode}'. Using default language.");
            return new Lang();
        }

        try
        {
            var jsonLang = File.ReadAllText(langFile);
            var loadedLang = JsonSerializer.Deserialize<Lang>(jsonLang, Options);
            if (loadedLang != null)
            {
                return loadedLang;
            }
            Console.WriteLine("Failed to deserialize language file. Using default language.");
            return new Lang();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading language file: {ex.Message}. Using default language.");
            return new Lang();
        }
    }
    
}
