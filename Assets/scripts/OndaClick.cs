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
         // Obt�n la posici�n del cursor en pantalla
         Vector3 mousePosition = Input.mousePosition;

         // Convierte la posici�n del cursor a coordenadas del mundo
         mousePosition.z = 10f; // Ajusta la profundidad (distancia desde la c�mara)
         Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

         // Instancia el prefab en la posici�n calculada
         GameObject spawnedPrefab = Instantiate(prefab, worldPosition, Quaternion.identity); // Usa Quaternion.identity para rotaci�n

         // Destruye el prefab instanciado despu�s de 1 segundo
         Destroy(spawnedPrefab, tiempoAnimacion);
    }
}