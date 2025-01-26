using UnityEngine;
using UnityEngine.Audio;

public class Mina : MonoBehaviour
{

    private float timer = 0;
    private SpriteRenderer spriteRenderer;
    private UnityEngine.Rendering.Universal.Light2D luz;
    [SerializeField] private Sprite minaApagada;
    [SerializeField] private Sprite minaPrendida;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float amplitud = 0.25f;
    [SerializeField] private float alturabase = 0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        luz = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 0.5 && timer < 1)
        {
            spriteRenderer.sprite = minaPrendida;
            luz.enabled = true;
        }
        else if (timer > 1)
        {
            spriteRenderer.sprite = minaApagada;
            timer = 0;
            luz.enabled = false;
        }

        Vector3 pos = new Vector3(transform.position.x, (amplitud * Mathf.Sin(speed * Time.unscaledTime) + alturabase), transform.position.z);
        transform.position = pos;
    }
}
