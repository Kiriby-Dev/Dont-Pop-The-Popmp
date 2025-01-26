using UnityEngine;

public class MovimientoBotones : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float amplitud = 20f;
    [SerializeField] private float alturabase = 0f;
    [SerializeField] private GameObject boton;
    RectTransform rectTransform;
    private void Start()
    {
        rectTransform = boton.GetComponent<RectTransform>();
    }
    void Update()
    {
        Vector3 pos = new Vector3 (rectTransform.localPosition.x, (amplitud * Mathf.Sin(speed * Time.unscaledTime)), rectTransform.localPosition.z);
        rectTransform.localPosition = pos;
    }
}
