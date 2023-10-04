using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kogels : MonoBehaviour
{
    public float levensduur = 3.0f; // Levensduur van de kogels (in seconden)

    private void Start()
    {
        // Start een timer om de kogels na een bepaalde tijd te vernietigen
        Destroy(gameObject, levensduur);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Controleer of de kogels de speler raken
        if (other.CompareTag("Player"))
        {
            // Voeg hier eventueel schade toe aan de speler als dat nodig is

            // Vernietig de kogels als ze de speler raken
            Destroy(gameObject);
        }
    }
}