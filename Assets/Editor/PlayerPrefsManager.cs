using UnityEngine;
using UnityEditor;

public class PlayerPrefsManager : Editor
{
    private const int MAX_NIVELES = 5; // Cambia esto según la cantidad de niveles de tu juego

    [MenuItem("Tools/Reset PlayerPrefs")]
    public static void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("PlayerPrefs reseteados.");
    }

    [MenuItem("Tools/Unlock All Levels")]
    public static void UnlockAllLevels()
    {
        for (int i = 0; i <= MAX_NIVELES; i++)
        {
            PlayerPrefs.SetInt("Unlocked" + i, 1); // Desbloquea el nivel i
        }
        PlayerPrefs.Save();

        Debug.Log("Todos los niveles han sido desbloqueados.");
    }
}


