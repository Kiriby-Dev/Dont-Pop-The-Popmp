using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class Mina : MonoBehaviour
{

    private float timer = 0;
    private SpriteRenderer spriteRenderer;
    private UnityEngine.Rendering.Universal.Light2D luz;
    private AudioSource audioSource;
    private GameObject player;
    bool sonido=false;
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
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindWithTag("Player");
        sonido = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 1 && timer < 2)
        {
            spriteRenderer.sprite = minaPrendida;
            luz.enabled = true;
            if (!sonido)
            {
                audioSource.Play();
                sonido=true;
            }
        }
        else if (timer > 2)
        {
            spriteRenderer.sprite = minaApagada;
            timer = 0;
            luz.enabled = false;
            sonido = false;
        }

        Vector3 pos = new Vector3(transform.position.x, (amplitud * Mathf.Sin(speed * Time.unscaledTime) + alturabase), transform.position.z);
        transform.position = pos;
        Vector3 pos_jugador = player.transform.position;
        float distancia = (pos - pos_jugador).magnitude;
        if (distancia < 10)
        {
            audioSource.volume = 2 / distancia;
        }
        else
        {
            audioSource.volume = 0;
        }
    }
}
