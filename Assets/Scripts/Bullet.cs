using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 20;
    public float damage = 0.1f;

    private void Start()
    {
        Destroy(gameObject, 0.7f);
    }

    void Update()
    {
        transform.Translate(transform.forward * Time.deltaTime * speed, Space.World);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.name);

        Enemy enemy  = collision.gameObject.GetComponentInParent<Enemy>();
        if (!enemy)
            return;

        enemy.Damage(damage);
        Destroy(gameObject);
    }
}
