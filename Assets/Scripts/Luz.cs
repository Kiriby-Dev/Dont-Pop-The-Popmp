using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Luz : MonoBehaviour
{
    [SerializeField] private Light2D globalLight;
    [SerializeField] private Light2D player;
    [SerializeField] private GameObject enterLight;
    [SerializeField] private float intensityIncrease = 1.0f;
    [SerializeField] private float radiusIncrease = 1.0f;
    [SerializeField] private string hexColor = "#020202"; // Color en formato hexadecimal
    [SerializeField] private GameObject minasCueva;
    private Collider2D triggerCollider;

    private void Awake()
    {
        triggerCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            minasCueva.SetActive(true);
            if (ColorUtility.TryParseHtmlString(hexColor, out Color parsedColor))
            {
                globalLight.color = parsedColor; // Cambiar el color de la luz
                enterLight.SetActive(true);
                player.intensity += intensityIncrease;
                player.pointLightOuterRadius += radiusIncrease;
            }
            else
            {
                Debug.LogWarning("El color hexadecimal no es válido: " + hexColor);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            triggerCollider.isTrigger = false;
        }
    }
}
