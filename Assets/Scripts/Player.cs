using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float h;
    float v;
    Vector3 moveDirection;
    [SerializeField] float speed = 3;
    [SerializeField] Transform aim;
    [SerializeField] Camera camera;
    Vector2 facingDirection;
    [SerializeField] Transform bulletPrefab;
    bool gunLoaded = true;
    [SerializeField] float fireRate = 1;
    [SerializeField] int health = 5;

    // Start is called before the first frame update
    void Start()
    {
        print("Hola");
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxis("Horizontal"); // a, d, tecla izquierda y derecha
        v = Input.GetAxis("Vertical");
        moveDirection.x = h;
        moveDirection.y = v;

        transform.position += moveDirection * Time.deltaTime * speed;

        // Aim movement
        facingDirection = camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        aim.position = transform.position + (Vector3)facingDirection.normalized;

        if (Input.GetMouseButton(0) && gunLoaded) 
        {
            gunLoaded = false;
            float angle = Mathf.Atan2(facingDirection.y, facingDirection.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Instantiate(bulletPrefab, transform.position, targetRotation);
            StartCoroutine(ReloadGun());
        } 

    }
    IEnumerator ReloadGun()
    {
        yield return new WaitForSeconds(1/ fireRate);
        gunLoaded = true;
    }

    public void TakeDamage()
    {
        health--;
        if (health < 1)
        {
            Destroy(gameObject);
        }
    }


}
