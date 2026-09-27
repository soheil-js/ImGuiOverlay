using System.Collections.Concurrent;
using System.Runtime.InteropServices;

namespace ImGuiOverlayDemo.Input;

/// <summary>
/// A lightweight global keyboard hook that works independently of window focus.
///
/// Why is this necessary?
/// Our overlay is click-through: when the mouse is not over an ImGui widget,
/// all keyboard/mouse input goes directly to the underlying application
/// (such as a game or any other window), and the overlay receives no keyboard
/// messages at all. To capture a toggle key (such as Insert) in all states,
/// the only reliable approach is a system-wide low-level keyboard hook.
///
/// Important technical detail: a low-level hook must be installed on a thread
/// that has its own message loop (GetMessage/DispatchMessage); otherwise, the
/// callback will not be invoked. That's why this class creates a dedicated
/// thread for the hook.
/// </summary>
public static class GlobalHotkey
{
    private const int WH_KEYBOARD_LL = 13;
    private const int WM_KEYDOWN = 0x0100;
    private const int WM_SYSKEYDOWN = 0x0104;

    private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

    [StructLayout(LayoutKind.Sequential)]
    private struct KBDLLHOOKSTRUCT
    {
        public int vkCode;
        public int scanCode;
        public int flags;
        public int time;
        public IntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct MSG
    {
        public IntPtr hwnd;
        public uint message;
        public IntPtr wParam;
        public IntPtr lParam;
        public uint time;
        public int ptX;
        public int ptY;
    }

    [DllImport("user32.dll", SetLastError = true)]
    private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool UnhookWindowsHookEx(IntPtr hhk);

    [DllImport("user32.dll")]
    private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
    private static extern IntPtr GetModuleHandle(string? lpModuleName);

    [DllImport("user32.dll")]
    private static extern bool GetMessage(out MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax);

    [DllImport("user32.dll")]
    private static extern bool TranslateMessage(ref MSG lpMsg);

    [DllImport("user32.dll")]
    private static extern IntPtr DispatchMessage(ref MSG lpMsg);

    // Keep a reference to this delegate; otherwise, the GC may collect it
    // while the hook is still referencing it from native code, causing
    // random crashes.
    private static LowLevelKeyboardProc? _proc;
    private static IntPtr _hookHandle = IntPtr.Zero;
    private static readonly ConcurrentDictionary<int, Action> _bindings = new();
    private static Thread? _hookThread;

    /// <summary>
    // Binds a global key to an action. The action runs on the hook's internal
    // thread, so any state it modifies must be thread-safe.
    // (In this project, AppState.IsVisible is volatile for this reason.)
    /// </summary>
    public static void Register(Keys key, Action onPressed)
    {
        _bindings[(int)key] = onPressed;

        if (_hookThread != null)
        {
            return; // The hook thread is already running; only the binding was added.
        }

        _hookThread = new Thread(() =>
        {
            _proc = HookCallback;
            using var currentProcess = System.Diagnostics.Process.GetCurrentProcess();
            using var mainModule = currentProcess.MainModule;
            _hookHandle = SetWindowsHookEx(WH_KEYBOARD_LL, _proc, GetModuleHandle(mainModule?.ModuleName), 0);

            // This message loop allows the hook to actually receive callbacks.
            while (GetMessage(out var msg, IntPtr.Zero, 0, 0))
            {
                TranslateMessage(ref msg);
                DispatchMessage(ref msg);
            }
        })
        {
            IsBackground = true,
            Name = "GlobalHotkeyHookThread"
        };
        _hookThread.Start();
    }

    public static void Unregister(Keys key) => _bindings.TryRemove((int)key, out _);

    public static void Shutdown()
    {
        if (_hookHandle != IntPtr.Zero)
        {
            UnhookWindowsHookEx(_hookHandle);
            _hookHandle = IntPtr.Zero;
        }
    }

    private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
    {
        if (nCode >= 0 && (wParam == (IntPtr)WM_KEYDOWN || wParam == (IntPtr)WM_SYSKEYDOWN))
        {
            var data = Marshal.PtrToStructure<KBDLLHOOKSTRUCT>(lParam);
            if (_bindings.TryGetValue(data.vkCode, out var action))
            {
                action();
            }
        }

        return CallNextHookEx(_hookHandle, nCode, wParam, lParam);
    }
}
