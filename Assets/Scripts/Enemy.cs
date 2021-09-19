using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Transform player;
    [SerializeField] int health = 5;
    [SerializeField] float speed = 1;

    private void Start()
    {
        player = FindObjectOfType<Player>().transform;
    }

    private void Update()
    {
        Vector2 direction = player.position - transform.position;
        // direction.normalized * evita disminución de velocidad
        transform.position += (Vector3) direction.normalized * Time.deltaTime * speed; 
    }

    public void TakeDamage()
    {
        health--;
        if (health < 1)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().TakeDamage();
            //Destroy(gameObject);
        }
    }

}
