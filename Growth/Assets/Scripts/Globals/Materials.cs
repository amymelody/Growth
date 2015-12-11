using UnityEngine;
using System.Collections;

public class Materials
{
    private static string outline_path = "Materials/mat_outline";
    public static Material Outline { get { return (Resources.Load(outline_path) as Material); } }
}
