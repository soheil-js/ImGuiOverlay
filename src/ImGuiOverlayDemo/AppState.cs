namespace ImGuiOverlayDemo;

public enum SettingsTab
{
    General,
    Appearance,
    Hotkeys,
    About
}

/// <summary>
/// All application data that is independent of ImGui. No other part of the
/// application should modify the fields of this class without considering
/// thread safety, except for fields that are only read and written inside
/// Render() (the render thread), such as UI settings that are modified only
/// by the user through the panel.
/// </summary>
public sealed class AppState
{
    private readonly object _perfLock = new();

    // ---------- Visibility / global ----------
    // This field is written by both the render thread (when the user clicks the
    // panel's X button) and the keyboard hook thread (when the hotkey is pressed).
    // A bool has atomic reads/writes in C# on supported platforms, so no lock is
    // needed for this simple case.
    public volatile bool IsVisible = true;

    public SettingsTab ActiveTab = SettingsTab.General;

    // ---------- General tab ----------
    public bool EnableFeatureX = true;
    public bool EnableNotifications = false;
    public float UpdateIntervalSeconds = 1.0f;
    public string PlayerName = "Player1";

    // ---------- Appearance tab ----------
    public float UiScale = 1.0f;
    public System.Numerics.Vector4 AccentColor = new(0.26f, 0.59f, 0.98f, 1.00f);
    public int ThemeIndex = 0; // 0 = Dark, 1 = Light, 2 = Classic

    // ---------- Hotkeys tab (نمایشی) ----------
    public const string ToggleHotkeyLabel = "Insert";

    // ---------- Perf snapshot: example of data that may come from another thread ----------
    // These two fields are kept private and are accessed only through lock-protected
    // methods because they may also be updated by a separate background thread
    // (e.g., a thread performing actual heavy work), not just the render thread.
    private double _lastFrameTimeMs;
    private int _frameCount;

    public void ReportFrameTime(double ms)
    {
        lock (_perfLock)
        {
            _lastFrameTimeMs = ms;
            _frameCount++;
        }
    }

    public (double lastFrameMs, int frameCount) GetPerfSnapshot()
    {
        lock (_perfLock)
        {
            return (_lastFrameTimeMs, _frameCount);
        }
    }
}
