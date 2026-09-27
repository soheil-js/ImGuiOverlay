using System.Diagnostics;
using ClickableTransparentOverlay;
using ImGuiOverlayDemo.Input;
using ImGuiOverlayDemo.Panels;
using ImGuiOverlayDemo.UI;

namespace ImGuiOverlayDemo;

/// <summary>
/// This is the class that is actually rendered on screen. It is intentionally
/// kept small: there is no UI logic here, only the connection between
/// AppState / SettingsPanel / Theme / GlobalHotkey.
/// </summary>
public sealed class SettingsOverlay : Overlay
{
    private readonly AppState _state = new();
    private readonly SettingsPanel _settingsPanel = new();
    private readonly Stopwatch _frameWatch = new();

    public SettingsOverlay() : base("Settings Overlay Demo", 1920, 1080)
    {
        // For an overlay, FPSLimit is usually better than VSync because it isn't tied
        // to the refresh rate of the underlying application (e.g., a game).
        this.VSync = false;
        this.FPSLimit = 60;
    }

    protected override async Task PostInitialized()
    {
        // The D3D device is ready here, so this is the right place to apply the style
        // and register the global hotkey (not in the constructor).
        Theme.Apply();

        GlobalHotkey.Register(Keys.Insert, () =>
        {
            _state.IsVisible = !_state.IsVisible;
        });

        await Task.CompletedTask;
    }

    protected override void Render()
    {
        _frameWatch.Restart();

        if (_state.IsVisible)
        {
            _settingsPanel.Draw(_state, this);
        }

        _frameWatch.Stop();
        _state.ReportFrameTime(_frameWatch.Elapsed.TotalMilliseconds);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            GlobalHotkey.Shutdown();
        }

        base.Dispose(disposing);
    }
}
