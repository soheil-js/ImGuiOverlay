
using ImGuiOverlayDemo;

Console.Title = "ImGui Overlay Demo";
Console.WriteLine("Starting overlay demo...");
Console.WriteLine("Press INSERT to show/hide the settings panel.");
Console.WriteLine("Use the panel's 'Close Overlay' button, or Ctrl+C here, to exit.");

using var overlay = new SettingsOverlay();

Console.CancelKeyPress += (_, e) =>
{
    e.Cancel = true;
    overlay.Close();
};

await overlay.Run();

Console.WriteLine("Overlay closed.");
