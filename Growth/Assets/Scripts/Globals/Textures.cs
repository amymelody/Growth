using UnityEngine;
using System.Collections;

public class Textures
{
    private static string cursor_base_texture_path = "Images/img_cursor_base";
    public static Texture2D Cursor_Base_Texture { get { return (Resources.Load(cursor_base_texture_path) as Texture2D); } }

    private static string cursor_red_texture_path = "Images/img_cursor_red";
    public static Texture2D Cursor_Red_Texture { get { return (Resources.Load(cursor_red_texture_path) as Texture2D); } }

    private static string cursor_blue_texture_path = "Images/img_cursor_blue";
    public static Texture2D Cursor_Blue_Texture { get { return (Resources.Load(cursor_blue_texture_path) as Texture2D); } }
}
