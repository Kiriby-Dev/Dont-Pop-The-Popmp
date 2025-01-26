using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovimientoBurbuja : MonoBehaviour
{
    [Header("Parametros de movimiento")]
    [SerializeField] private float pushForce = 10f; // Magnitud de la fuerza aplicada al empujar
    [SerializeField] private float rotationSpeed = 1f; // Velocidad de rotacion de la burbuja
    [SerializeField] private float maxSpeed = 15f; // Velocidad máxima de la burbuja
    [SerializeField] private float maxRotation = 10f; // Rotación máxima de la burbuja
    [SerializeField] private float maxDistance = 8f; // Distancia máxima en que el click tiene efecto
    [SerializeField] private float waterResistance = 2f; // Resistencia del agua
    [SerializeField] private float gravedad = -0.05f; // Fuerza con la que sube la burbuja

    [Header("Mejoras de movimiento")]
    [SerializeField] private float speedIncrease = 2f;
    [SerializeField] private float rotationIncrease = 2f;

    [Header("Sprites de burbuja")]
    [SerializeField] private Sprite mediumBubble;
    [SerializeField] private Sprite bigBubble;
    [SerializeField] private Sprite goldenBubble;

    private int currentSize = 1;
    private bool movable = true;
    private Rigidbody2D burbuja;
    private CircleCollider2D burbujaCollider;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.enabled = false;
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
        burbuja = GetComponent<Rigidbody2D>();
        burbujaCollider = GetComponent<CircleCollider2D>();
        GameObject respawnObject = GameObject.FindGameObjectWithTag("Respawn");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        burbuja.gravityScale = gravedad;
        setWaterResistance();
        PlayerPrefs.SetInt("CantidadBurbujas", 0);
        PlayerPrefs.Save();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && movable) // Botón izquierdo del mouse
        {
            // Obtener la posición del clic
            Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickPosition.z = 0f;

            // Obtener la posición actual del objeto
            Vector3 objectPosition = transform.position;

            Vector3 direction = objectPosition - clickPosition;

            float distance = direction.magnitude;

            if (distance < maxDistance) 
            { 
                // Aplicar fuerza al Rigidbody2D en la dirección opuesta
                burbuja.AddForce(direction.normalized * pushForce / direction.magnitude, ForceMode2D.Impulse);
                burbuja.angularVelocity = rotationSpeed / distance;
            }
        }

        // Limitar la velocidad máxima del Rigidbody2D
        if (burbuja.linearVelocity.magnitude > maxSpeed)
        {
            burbuja.linearVelocity = burbuja.linearVelocity.normalized * maxSpeed;
        }

        if (burbuja.angularVelocity > maxRotation)
        {
            burbuja.angularVelocity = maxRotation;
        }
    }

    public void removeWaterResistance()
    {
        burbuja.linearDamping = 0f;
    }

    public void setWaterResistance()
    {
        burbuja.linearDamping = waterResistance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BurbujaChica"))
        {
            agrandarBurbuja();
            int cantBurbujas = PlayerPrefs.GetInt("CantidadBurbujas", 0);
            cantBurbujas++;
            PlayerPrefs.SetInt("CantidadBurbujas", cantBurbujas);
            PlayerPrefs.Save();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Obstaculo"))
        {
           resetStage();
        }
    }

    public void resetStage()
    {
        movable = false;
        burbuja.linearVelocity = Vector2.zero;
        burbuja.angularVelocity = 0f;
        burbuja.gravityScale = 0f;
        if (animator != null)
        {
            animator.enabled = true;
            animator.SetTrigger("Explode");
        }
    }

    public void OnExplosionEnd()
    {
        int cantIntentos = PlayerPrefs.GetInt("CantidadIntentos", 0);
        cantIntentos++;
        PlayerPrefs.SetInt("CantidadIntentos", cantIntentos);
        PlayerPrefs.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void agrandarBurbuja()
    {   
        switch (currentSize)
        {
            case 1:
                spriteRenderer.sprite = mediumBubble;
                burbujaCollider.radius =0.45f;
                pushForce += speedIncrease;
                maxSpeed += speedIncrease;
                rotationSpeed += rotationIncrease;
                maxRotation += rotationIncrease;
                currentSize++;
                break;
            case 2: 
                spriteRenderer.sprite = bigBubble;
                burbujaCollider.radius = 0.54f;
                pushForce += speedIncrease;
                maxSpeed += speedIncrease;
                rotationSpeed += rotationIncrease;
                maxRotation += rotationIncrease;
                currentSize++;
                break;
            case 3:
                spriteRenderer.sprite = goldenBubble;
                pushForce += speedIncrease;
                maxSpeed += speedIncrease;
                rotationSpeed += rotationIncrease;
                maxRotation += rotationIncrease;
                break;
            default:
                break;
        }
    }
}
