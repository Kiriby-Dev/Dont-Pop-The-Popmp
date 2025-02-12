using UnityEngine;
using UnityEditor;

public class PlayerPrefsResetter : Editor
{
    [MenuItem("Tools/Reset PlayerPrefs")]
    public static void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("PlayerPrefs reseteados");
    }
}

