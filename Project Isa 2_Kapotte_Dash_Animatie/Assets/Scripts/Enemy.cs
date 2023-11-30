using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
  public float speed;
  Transform player;

  public int health;

  void Start ()
    {
       // player = FindObjectOfType<CharacterController2D>().transform;
    }
  

  void Update()
  {
     //transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
  }

  void OnTriggerEnter2D(Collider2D other)
  {
      if ( other.tag == "Projectile")
      {
        Debug.Log("Raakt Projectile aan");
          TakeDamage(other.GetComponent<Projectile>().damage);
         
      }
  }

  void TakeDamage(int damageAmount)
  {
    health -= damageAmount;
    if (health <= 0)
    {
      Destroy(gameObject);
      
    }
  }

}
//PlayerController