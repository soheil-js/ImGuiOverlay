using System.Numerics;
using ClickableTransparentOverlay;
using Hexa.NET.ImGui;

namespace ImGuiOverlayDemo.Panels;

public sealed class SettingsPanel
{
    private static readonly string[] ThemeNames = { "Dark", "Light", "Classic" };

    // Temporary buffer for InputText; since the string inside AppState may also be
    // read from elsewhere, we don't work with it directly every frame unless
    // its value has actually changed.
    private string _playerNameBuffer = string.Empty;
    private bool _bufferInitialized;

    public void Draw(AppState state, Overlay overlayHost)
    {
        if (!_bufferInitialized)
        {
            _playerNameBuffer = state.PlayerName;
            _bufferInitialized = true;
        }

        ImGui.SetNextWindowSize(new Vector2(480, 380), ImGuiCond.FirstUseEver);
        ImGui.SetNextWindowPos(new Vector2(60, 60), ImGuiCond.FirstUseEver);

        bool isVisible = state.IsVisible;
        if (!ImGui.Begin("Settings", ref isVisible, ImGuiWindowFlags.NoCollapse))
        {
            ImGui.End();
            state.IsVisible = isVisible;
            return;
        }

        DrawHeader(state);
        ImGui.Separator();

        if (ImGui.BeginTabBar("SettingsTabs"))
        {
            if (ImGui.BeginTabItem("General"))
            {
                state.ActiveTab = SettingsTab.General;
                DrawGeneralTab(state);
                ImGui.EndTabItem();
            }

            if (ImGui.BeginTabItem("Appearance"))
            {
                state.ActiveTab = SettingsTab.Appearance;
                DrawAppearanceTab(state);
                ImGui.EndTabItem();
            }

            if (ImGui.BeginTabItem("Hotkeys"))
            {
                state.ActiveTab = SettingsTab.Hotkeys;
                DrawHotkeysTab();
                ImGui.EndTabItem();
            }

            if (ImGui.BeginTabItem("About"))
            {
                state.ActiveTab = SettingsTab.About;
                DrawAboutTab();
                ImGui.EndTabItem();
            }

            ImGui.EndTabBar();
        }

        ImGui.Separator();
        if (ImGui.Button("Close Overlay", new Vector2(140, 0)))
        {
            overlayHost.Close();
        }

        ImGui.End();

        // If the user clicked the panel's X button, isVisible will be set to false here.
        state.IsVisible = isVisible;
    }

    private static void DrawHeader(AppState state)
    {
        var (lastFrameMs, frameCount) = state.GetPerfSnapshot();
        ImGui.TextDisabled($"Frame #{frameCount}  |  last render: {lastFrameMs:0.00} ms");
    }

    private void DrawGeneralTab(AppState state)
    {
        ImGui.Checkbox("Enable Feature X", ref state.EnableFeatureX);
        ImGui.Checkbox("Enable Notifications", ref state.EnableNotifications);
        ImGui.SliderFloat("Update Interval (s)", ref state.UpdateIntervalSeconds, 0.1f, 5.0f, "%.1f");

        ImGui.Spacing();
        if (ImGui.InputText("Player Name", ref _playerNameBuffer, 64))
        {
            state.PlayerName = _playerNameBuffer;
        }
    }

    private static void DrawAppearanceTab(AppState state)
    {
        ImGui.SliderFloat("UI Scale", ref state.UiScale, 0.75f, 2.0f, "%.2fx");
        ImGui.ColorEdit4("Accent Color", ref state.AccentColor);
        ImGui.Combo("Theme", ref state.ThemeIndex, ThemeNames, ThemeNames.Length);

        ImGui.TextDisabled("UI Scale currently only affects new windows opened after the change.");
    }

    private static void DrawHotkeysTab()
    {
        ImGui.Text($"Toggle overlay visibility: {AppState.ToggleHotkeyLabel}");
        ImGui.Spacing();
        ImGui.TextWrapped(
            "This hotkey is caught by a global low-level keyboard hook, " +
            "because a click-through overlay does not receive normal input " +
            "while the mouse is not hovering it.");
    }

    private static void DrawAboutTab()
    {
        ImGui.TextWrapped("Sample settings overlay built with ClickableTransparentOverlay.Hexa + Hexa.NET.ImGui.");
        ImGui.Text("Version 1.0.0");
    }
}
