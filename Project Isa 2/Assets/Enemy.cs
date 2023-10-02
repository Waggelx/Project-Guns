using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player; // Referentie naar de speler
    public GameObject kogelsPrefab; // Het prefab van de kogels
    public Transform firePoint; // Het punt waar de kogels worden afgevuurd
    public float moveSpeed = 2.0f; // Snelheid van de vijand
    public float vuurSnelheid = 2.0f; // Snelheid van de kogels
    public float maxDistance = 5.0f; // Maximale afstand tot de speler

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Start het schieten zodra het spel begint
        InvokeRepeating("SchietKogel", 0f, 1f / vuurSnelheid);
    }

    private void Update()
    {
        if (player != null)
        {
            // Bereken de afstand tussen de vijand en de speler
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer > maxDistance)
            {
                // Beweeg de vijand naar de speler met een beperkte snelheid
                Vector3 direction = (player.position - transform.position).normalized;
                rb.velocity = direction * moveSpeed;
            }
            else
            {
                // De vijand is dichtbij genoeg om te schieten, stop de beweging
                rb.velocity = Vector2.zero;
            }
        }
        else
        {
            // De speler is afwezig, stop de vijand
            rb.velocity = Vector2.zero;
        }
    }

    private void SchietKogel()
    {
        if (player != null)
        {
            // Creëer een nieuw projectiel (kogels) op het vuurpunt van de vijand
            GameObject newProjectile = Instantiate(kogelsPrefab, firePoint.position, Quaternion.identity);

            // Bereken de richting naar de speler
            Vector3 directionToPlayer = (player.position - firePoint.position).normalized;

            // Voeg kracht toe aan het projectiel om het naar de speler te laten bewegen
            Rigidbody2D projectileRb = newProjectile.GetComponent<Rigidbody2D>();
            projectileRb.velocity = directionToPlayer * vuurSnelheid;

            // Vernietig het projectiel na een bepaalde tijd (bijvoorbeeld 3 seconden)
            Destroy(newProjectile, 3f);
        }
    }
}