using UnityEngine;

public class DestruirBurbujita : MonoBehaviour
{
    private RectTransform rectTransform;
    [SerializeField] private float velocidad = 100f; // Velocidad de movimiento

    public delegate void BurbujitaDestruidaHandler();
    public event BurbujitaDestruidaHandler OnBurbujitaDestruida;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        // Mueve la burbujita hacia arriba
        Vector3 movimiento = new Vector3(0, velocidad * Time.unscaledDeltaTime, 0);
        rectTransform.localPosition += movimiento;

        // Destruye la burbujita si supera el límite superior
        if (rectTransform.anchoredPosition.y > 565f)
        {
            // Llama al evento antes de destruir
            OnBurbujitaDestruida?.Invoke();
            Destroy(gameObject);
        }
    }
}
