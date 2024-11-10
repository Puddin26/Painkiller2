using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class MG5Manager1
{
    public static GameObject MG5CurrentGameobject1 = null;
    public static bool MG5CanDraw1 = true;
    public static SpriteRenderer[] Part11 = GameObject.Find("Part11").transform.GetComponentsInChildren<SpriteRenderer>();
    public static SpriteRenderer[] Part22 = GameObject.Find("Part22").transform.GetComponentsInChildren<SpriteRenderer>();
    public static SpriteRenderer[] Part33 = GameObject.Find("Part33").transform.GetComponentsInChildren<SpriteRenderer>();
}
