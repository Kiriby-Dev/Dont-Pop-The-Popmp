using System;
using UnityEngine;

public class MovimientoBurbuja : MonoBehaviour
{
    [SerializeField] private float pushForce = 10f; // Magnitud de la fuerza aplicada al empujar
    [SerializeField] private float maxSpeed = 15f; // Velocidad máxima de la burbuja
    [SerializeField] private float maxDistance = 8f; // Distancia máxima en que el click tiene efecto
    [SerializeField] private float waterResistance = 2f; // Resistencia del agua
    [SerializeField] private float gravedad = -0.05f; // Fuerza con la que sube la burbuja

    [SerializeField] private Rigidbody2D burbuja;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        burbuja = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        burbuja.gravityScale = gravedad;
        setWaterResistance();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Botón izquierdo del mouse
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
            }
        }

        // Limitar la velocidad máxima del Rigidbody2D
        if (burbuja.linearVelocity.magnitude > maxSpeed)
        {
            burbuja.linearVelocity = burbuja.linearVelocity.normalized * maxSpeed;
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
}
