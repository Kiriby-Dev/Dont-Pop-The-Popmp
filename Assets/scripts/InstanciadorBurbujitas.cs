using UnityEngine;

public class GeneradorBurbujitas : MonoBehaviour
{
    [SerializeField] private GameObject prefabBurbujita; // Prefab de la burbujita
    [SerializeField] private RectTransform menuPausaPanel; // Referencia al panel de pausa
    [SerializeField] private float tiempoEntreBurbujas = 1f; // Tiempo entre la creación de burbujas
    [SerializeField] private int maxBurbujas = 10; // Máximo de burbujas existentes al mismo tiempo

    private int burbujasActivas = 0; // Contador de burbujas activas
    private float tiempoActual = 0f; // Contador de tiempo para la creación de burbujas

    void Update()
    {
        // Incrementa el contador de tiempo (independientemente de la escala del tiempo)
        tiempoActual += Time.unscaledDeltaTime;

        // Verifica si es momento de crear una nueva burbujita
        if (tiempoActual >= tiempoEntreBurbujas && burbujasActivas < maxBurbujas)
        {
            GenerarBurbujita();
            tiempoActual = 0f; // Reinicia el contador de tiempo
        }
    }

    private void GenerarBurbujita()
    {
        // Genera una posición aleatoria dentro del rango especificado
        float posicionX = Random.Range(-970f, 970f);
        Vector3 posicion = new Vector3(posicionX, -565f, 0f);

        // Instancia la burbujita como hija del panel
        GameObject nuevaBurbujita = Instantiate(prefabBurbujita, menuPausaPanel);
        nuevaBurbujita.GetComponent<RectTransform>().anchoredPosition = posicion;

        // Incrementa el contador de burbujas activas
        burbujasActivas++;

        // Asigna un evento para reducir el contador cuando la burbujita sea destruida
        nuevaBurbujita.GetComponent<DestruirBurbujita>().OnBurbujitaDestruida += ReducirContador;
    }

    private void ReducirContador()
    {
        burbujasActivas--;
    }
}

