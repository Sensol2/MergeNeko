using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EditorMenu
{
    [MenuItem("Custom/Reset PlayerPrefs")]
    public static void resetPlayerPrefs() 
    {
        PlayerPrefs.DeleteAll();
    }
    [MenuItem("Custom/motherroad")]
    public static void motherroad()
    {
        DataManager.instance.SetGem(100000);
    }
}
