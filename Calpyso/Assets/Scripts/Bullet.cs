using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]private int damage;
 private void OnCollisionEnter2D(Collision2D other) {
    if (other.gameObject.tag=="Player")
    {
        other.gameObject.GetComponent<Health>().TakeDamage(damage,gameObject.GetComponent<Collider2D>());
        Destroy(gameObject);
    }

 }
 private void OnTriggerEnter2D(Collider2D other) {
     if (other.gameObject.tag=="Player")
    {
        other.gameObject.GetComponent<Health>().TakeDamage(damage,gameObject.GetComponent<Collider2D>());
        Destroy(gameObject);
    }
 }
}
