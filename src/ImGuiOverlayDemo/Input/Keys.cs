namespace ImGuiOverlayDemo.Input;

/// <summary>
/// A small subset of Windows Virtual-Key Codes, so we don't have to reference
/// the entire System.Windows.Forms assembly just for an enum.
/// Full list: https://learn.microsoft.com/windows/win32/inputdev/virtual-key-codes
/// </summary>
public enum Keys
{
    Insert = 0x2D,
    Delete = 0x2E,
    Home = 0x24,
    End = 0x23,
    F1 = 0x70,
    F2 = 0x71,
    F3 = 0x72,
    F4 = 0x73,
}
