using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    public GameObject prefab; // Arrastra tu prefab aquí desde el editor.
    public string uniqueName = "UniquePrefab"; // Cambia este nombre para identificar el objeto.

    private void Awake()
    {
        // Busca un objeto con el mismo nombre único en la escena.
        GameObject existingObject = GameObject.Find(uniqueName);

        // Si no existe, instancia el prefab y le asigna el nombre único.
        if (existingObject == null)
        {
            GameObject newObject = Instantiate(prefab);
            newObject.name = uniqueName; // Asegúrate de que el nombre sea único.
        }
    }
}

