using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    public GameObject prefab; // Arrastra tu prefab aqu� desde el editor.
    public string uniqueName = "UniquePrefab"; // Cambia este nombre para identificar el objeto.

    private void Awake()
    {
        // Busca un objeto con el mismo nombre �nico en la escena.
        GameObject existingObject = GameObject.Find(uniqueName);

        // Si no existe, instancia el prefab y le asigna el nombre �nico.
        if (existingObject == null)
        {
            GameObject newObject = Instantiate(prefab);
            newObject.name = uniqueName; // Aseg�rate de que el nombre sea �nico.
        }
    }
}

