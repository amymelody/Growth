using UnityEngine;
using System.Collections;

public class Prefabs 
{
    private static string empty_object_path = "Prefabs/prefab_empty_object";
    public static GameObject Empty_Object { get { return (Resources.Load(empty_object_path) as GameObject); } }

    private static string explosion_rainbow_small_path = "Prefabs/Effects/prefab_explosion_rainbow_small";
    public static GameObject Explosion_Rainbow_Small { get { return (Resources.Load(explosion_rainbow_small_path) as GameObject); } }

    private static string explosion_rainbow_large_path = "Prefabs/Effects/prefab_explosion_rainbow_large";
    public static GameObject Explosion_Rainbow_Large { get { return (Resources.Load(explosion_rainbow_large_path) as GameObject); } }
}
