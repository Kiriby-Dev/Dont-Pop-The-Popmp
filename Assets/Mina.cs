using UnityEngine;

public class Mina : MonoBehaviour
{

    private float timer = 0;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite minaApagada;
    [SerializeField] private Sprite minaPrendida;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float amplitud = 0.25f;
    [SerializeField] private float alturabase = 0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 0.5 && timer < 1)
        {
            spriteRenderer.sprite = minaPrendida;
        }
        else if (timer > 1)
        {
            spriteRenderer.sprite = minaApagada;
            timer = 0;
        }

        Vector3 pos = new Vector3(transform.position.x, (amplitud * Mathf.Sin(speed * Time.unscaledTime) + alturabase), transform.position.z);
        transform.position = pos;
    }
}
