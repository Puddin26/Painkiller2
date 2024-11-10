using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class MG5Manager
{
    public static GameObject MG5CurrentGameobject = null;
    public static bool MG5CanDraw = true;
    public static SpriteRenderer[] Part1 = GameObject.Find("Part1").transform.GetComponentsInChildren<SpriteRenderer>();
    public static SpriteRenderer[] Part2 = GameObject.Find("Part2").transform.GetComponentsInChildren<SpriteRenderer>();
    public static SpriteRenderer[] Part3 = GameObject.Find("Part3").transform.GetComponentsInChildren<SpriteRenderer>();
}
