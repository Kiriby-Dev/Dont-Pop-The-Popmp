using UnityEngine;

public class OndaClick : MonoBehaviour
{
    [SerializeField] private GameObject prefab; // Prefab que deseas instanciar
    [SerializeField] private float tiempoAnimacion = 0.6f;

    void Update()
    {
        // Detecta si se hace clic izquierdo
        if (Input.GetMouseButtonDown(0)) // 0 es clic izquierdo, 1 es clic derecho
        {
            SpawnPrefabAtCursor();
        }
    }

    private void SpawnPrefabAtCursor()
    {
        if (prefab != null)
        {
            // Obtén la posición del cursor en pantalla
            Vector3 mousePosition = Input.mousePosition;

            // Convierte la posición del cursor a coordenadas del mundo
            mousePosition.z = 10f; // Ajusta la profundidad (distancia desde la cámara)
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            // Instancia el prefab en la posición calculada
            GameObject spawnedPrefab = Instantiate(prefab, worldPosition, Quaternion.identity); // Usa Quaternion.identity para rotación

            // Destruye el prefab instanciado después de 1 segundo
            Destroy(spawnedPrefab, tiempoAnimacion);

            Debug.Log("Prefab instanciado en: " + worldPosition);
        }
        else
        {
            Debug.LogWarning("Prefab no asignado en el Inspector.");
        }
    }
}