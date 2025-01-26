using UnityEngine;
using UnityEngine.EventSystems;

public class OndaClick : MonoBehaviour
{
    [SerializeField] private GameObject prefab; // Prefab que deseas instanciar
    [SerializeField] private float tiempoAnimacion = 0.6f;
    private MenuPausa menuPausa; // Referencia al script de pausa

    private void Start()
    {
        // Busca el script MenuPausa en la escena
        menuPausa = FindFirstObjectByType<MenuPausa>();
    }

    void Update()
    {
        // Verifica si el juego no est� pausado y si el clic no es sobre la UI
        if (!menuPausa.IsPaused() && Input.GetMouseButtonDown(0) && !IsPointerOverUI())
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
        GameObject spawnedPrefab = Instantiate(prefab, worldPosition, Quaternion.identity);

        // Destruye el prefab instanciado despu�s de un tiempo
        Destroy(spawnedPrefab, tiempoAnimacion);
    }

    private bool IsPointerOverUI()
    {
        // Retorna true si el cursor est� sobre un elemento de la interfaz de usuario
        return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
    }
}
