using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EditorMenu
{
    [MenuItem("Cheat/Reset PlayerPrefs")]
    public static void resetPlayerPrefs() 
    {
        PlayerPrefs.DeleteAll();
    }
    [MenuItem("Cheat/motherroad")]
    public static void motherroad()
    {
        DataManager.instance.SetGem(4609);
    }

    [MenuItem("Cheat/GetAllItems")]
    public static void GetAllItems()
    {
        DataManager.instance.GetAllItemsCheat();
    }
}
