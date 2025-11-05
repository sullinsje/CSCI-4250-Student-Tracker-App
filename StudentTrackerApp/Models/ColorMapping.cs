using System.Collections.Generic;
namespace StudentTrackerApp.Models;
public static class ColorMapping
{
    public static readonly Dictionary<string, RoleColors> RoleColorMap = new Dictionary<string, RoleColors>(StringComparer.OrdinalIgnoreCase)
    {
        {
            "Student", new RoleColors
            {
                Primary = "#4a90e2",
                Hover = "#357abd",
                ShadowRgb = "74, 144, 226",
                GradientStart = "#4a90e2",
                GradientEnd = "#2c5aa0"
            }
        },
        {
            "Teacher", new RoleColors
            {
                Primary = "#e74c3c",
                Hover = "#c0392b",
                ShadowRgb = "231, 76, 60",
                GradientStart = "#e74c3c",
                GradientEnd = "#c0392b"
            }
        }
    };
}