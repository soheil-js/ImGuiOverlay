using System.Numerics;
using Hexa.NET.ImGui;

namespace ImGuiOverlayDemo.UI;

public static class Theme
{
    public static void Apply()
    {
        var style = ImGui.GetStyle();
        var colors = style.Colors;

        // ============================================================
        // GLOBAL STYLE
        // ============================================================

        style.Alpha = 1.0f;

        style.WindowPadding = new Vector2(14, 14);
        style.FramePadding = new Vector2(10, 7);
        style.CellPadding = new Vector2(8, 6);
        style.ItemSpacing = new Vector2(9, 8);
        style.ItemInnerSpacing = new Vector2(7, 6);

        style.IndentSpacing = 20.0f;
        style.ScrollbarSize = 11.0f;
        style.GrabMinSize = 10.0f;

        // Rounded / Premium
        style.WindowRounding = 12.0f;
        style.ChildRounding = 10.0f;
        style.FrameRounding = 7.0f;
        style.PopupRounding = 10.0f;
        style.ScrollbarRounding = 8.0f;
        style.GrabRounding = 7.0f;
        style.TabRounding = 8.0f;

        // Borders
        style.WindowBorderSize = 1.0f;
        style.ChildBorderSize = 1.0f;
        style.PopupBorderSize = 1.0f;
        style.FrameBorderSize = 1.0f;
        style.TabBorderSize = 0.0f;

        // ============================================================
        // PALETTE
        // ============================================================

        // Background
        Vector4 bg = new(0.025f, 0.028f, 0.045f, 0.96f);
        Vector4 panel = new(0.045f, 0.050f, 0.075f, 0.88f);
        Vector4 panel2 = new(0.060f, 0.065f, 0.095f, 0.92f);

        // Border
        Vector4 border = new(0.15f, 0.17f, 0.25f, 0.65f);
        Vector4 borderHot = new(0.30f, 0.42f, 0.75f, 0.85f);

        // Cyber blue
        Vector4 blue = new(0.25f, 0.55f, 1.00f, 1.00f);
        Vector4 blueHot = new(0.38f, 0.68f, 1.00f, 1.00f);
        Vector4 blueSoft = new(0.18f, 0.35f, 0.65f, 0.45f);

        // Cyber purple
        Vector4 purple = new(0.58f, 0.32f, 1.00f, 1.00f);
        Vector4 purpleSoft = new(0.40f, 0.20f, 0.75f, 0.40f);

        // Text
        Vector4 text = new(0.92f, 0.94f, 1.00f, 1.00f);
        Vector4 textDim = new(0.48f, 0.52f, 0.62f, 1.00f);

        // ============================================================
        // WINDOW
        // ============================================================

        colors[(int)ImGuiCol.WindowBg] = bg;
        colors[(int)ImGuiCol.ChildBg] = panel;
        colors[(int)ImGuiCol.PopupBg] = new Vector4(0.035f, 0.040f, 0.065f, 0.98f);
        colors[(int)ImGuiCol.Border] = border;
        colors[(int)ImGuiCol.BorderShadow] = new Vector4(0, 0, 0, 0);

        // ============================================================
        // TEXT
        // ============================================================

        colors[(int)ImGuiCol.Text] = text;
        colors[(int)ImGuiCol.TextDisabled] = textDim;

        // ============================================================
        // TITLE BAR
        // ============================================================

        colors[(int)ImGuiCol.TitleBg] = new Vector4(0.025f, 0.028f, 0.045f, 1.00f);
        colors[(int)ImGuiCol.TitleBgActive] = new Vector4(0.055f, 0.060f, 0.095f, 1.00f);
        colors[(int)ImGuiCol.TitleBgCollapsed] = new Vector4(0.025f, 0.028f, 0.045f, 0.80f);

        // ============================================================
        // FRAME / INPUT
        // ============================================================

        colors[(int)ImGuiCol.FrameBg] = new Vector4(0.065f, 0.075f, 0.110f, 0.90f);
        colors[(int)ImGuiCol.FrameBgHovered] = new Vector4(0.095f, 0.115f, 0.175f, 1.00f);
        colors[(int)ImGuiCol.FrameBgActive] = new Vector4(0.115f, 0.140f, 0.215f, 1.00f);

        // ============================================================
        // BUTTON
        // ============================================================

        colors[(int)ImGuiCol.Button] = new Vector4(0.065f, 0.075f, 0.115f, 0.95f);
        colors[(int)ImGuiCol.ButtonHovered] = new Vector4(0.12f, 0.18f, 0.30f, 1.00f);
        colors[(int)ImGuiCol.ButtonActive] = new Vector4(0.18f, 0.30f, 0.52f, 1.00f);

        // ============================================================
        // CHECKBOX / SLIDER
        // ============================================================

        colors[(int)ImGuiCol.CheckMark] = blue;
        colors[(int)ImGuiCol.SliderGrab] = blue;
        colors[(int)ImGuiCol.SliderGrabActive] = blueHot;

        // ============================================================
        // HEADER
        // ============================================================

        colors[(int)ImGuiCol.Header] = new Vector4(0.075f, 0.090f, 0.135f, 0.95f);
        colors[(int)ImGuiCol.HeaderHovered] = new Vector4(0.12f, 0.20f, 0.34f, 0.95f);
        colors[(int)ImGuiCol.HeaderActive] = new Vector4(0.18f, 0.30f, 0.52f, 1.00f);

        // ============================================================
        // TABS
        // ============================================================

        colors[(int)ImGuiCol.Tab] = new Vector4(0.055f, 0.065f, 0.100f, 1.00f);
        colors[(int)ImGuiCol.TabHovered] = new Vector4(0.16f, 0.25f, 0.45f, 1.00f);
        colors[(int)ImGuiCol.TabSelected] = new Vector4(0.11f, 0.20f, 0.38f, 1.00f);
        colors[(int)ImGuiCol.TabSelectedOverline] = blue;
        colors[(int)ImGuiCol.TabDimmed] = new Vector4(0.045f, 0.052f, 0.080f, 1.00f);
        colors[(int)ImGuiCol.TabDimmedSelected] = new Vector4(0.075f, 0.12f, 0.22f, 1.00f);
        colors[(int)ImGuiCol.TabDimmedSelectedOverline] = blueSoft;

        // ============================================================
        // SCROLLBAR
        // ============================================================

        colors[(int)ImGuiCol.ScrollbarBg] = new Vector4(0.018f, 0.020f, 0.032f, 0.85f);
        colors[(int)ImGuiCol.ScrollbarGrab] = new Vector4(0.11f, 0.14f, 0.21f, 1.00f);
        colors[(int)ImGuiCol.ScrollbarGrabHovered] = new Vector4(0.20f, 0.28f, 0.45f, 1.00f);
        colors[(int)ImGuiCol.ScrollbarGrabActive] = blue;

        // ============================================================
        // SEPARATOR
        // ============================================================

        colors[(int)ImGuiCol.Separator] = new Vector4(0.13f, 0.15f, 0.22f, 0.70f);
        colors[(int)ImGuiCol.SeparatorHovered] = new Vector4(0.25f, 0.40f, 0.70f, 0.85f);
        colors[(int)ImGuiCol.SeparatorActive] = blue;

        // ============================================================
        // RESIZE GRIP
        // ============================================================

        colors[(int)ImGuiCol.ResizeGrip] = new Vector4(0.10f, 0.13f, 0.20f, 0.30f);
        colors[(int)ImGuiCol.ResizeGripHovered] = new Vector4(0.22f, 0.38f, 0.68f, 0.70f);
        colors[(int)ImGuiCol.ResizeGripActive] = blue;

        // ============================================================
        // TABLE
        // ============================================================

        colors[(int)ImGuiCol.TableHeaderBg] = new Vector4(0.065f, 0.075f, 0.115f, 1.00f);
        colors[(int)ImGuiCol.TableBorderStrong] = new Vector4(0.16f, 0.19f, 0.28f, 1.00f);
        colors[(int)ImGuiCol.TableBorderLight] = new Vector4(0.10f, 0.12f, 0.18f, 1.00f);
        colors[(int)ImGuiCol.TableRowBg] = new Vector4(0, 0, 0, 0);
        colors[(int)ImGuiCol.TableRowBgAlt] = new Vector4(0.06f, 0.07f, 0.10f, 0.35f);

        // ============================================================
        // NAVIGATION
        // ============================================================

        colors[(int)ImGuiCol.NavWindowingHighlight] = blue;
        colors[(int)ImGuiCol.NavWindowingHighlight] = new Vector4(0.55f, 0.65f, 0.90f, 0.70f);
        colors[(int)ImGuiCol.NavWindowingDimBg] = new Vector4(0.015f, 0.020f, 0.035f, 0.70f);

        // ============================================================
        // DRAG & DROP
        // ============================================================

        colors[(int)ImGuiCol.DragDropTarget] = new Vector4(0.35f, 0.65f, 1.00f, 0.95f);

        // ============================================================
        // MODAL
        // ============================================================

        colors[(int)ImGuiCol.ModalWindowDimBg] = new Vector4(0.01f, 0.015f, 0.03f, 0.78f);
    }
}