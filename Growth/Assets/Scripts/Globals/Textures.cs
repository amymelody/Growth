using UnityEngine;
using System.Collections;

public class Textures
{
    private static string cursor_base_texture_path = "Images/img_cursor_base";
    public static Texture2D Cursor_Base_Texture { get { return (Resources.Load(cursor_base_texture_path) as Texture2D); } }
}
