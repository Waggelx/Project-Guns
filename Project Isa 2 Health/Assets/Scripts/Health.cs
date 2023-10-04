using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;
    public int heartNumber;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private void Update()
    {
        if (health > heartNumber)
        {
            health = heartNumber;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if(i < health) {
                hearts[i].sprite = fullHeart;
            } else
            {
                hearts[i].sprite =emptyHeart;
            }

            if(i < heartNumber)
            {
                hearts[i].enabled = true;
            }   else
            {
                hearts[i].enabled = false;
            }
        }
    }
}
