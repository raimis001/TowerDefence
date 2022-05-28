using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float radius = 3;
    public Bullet bullet;
    public float bulletDelay = 0.35f;

    public Transform rotor;
    public Transform bulletPosition;

    public Renderer baseRender;
    public Material[] materials;

    public int price = 20;
   
    float bulletTime = 0;

    internal bool canPlace = true;
    internal bool active = false;

    private void Update()
    {
        if (!active)
            return;

        if (Game.instance.endGame)
            return;

        Enemy enemy = null;

        float dist = Mathf.Infinity;
        foreach (Enemy e in Game.EnemyList())
        {
            float d = Vector3.Distance(transform.position, e.transform.position);
            if (d > radius)
                continue;

            if (d > dist)
                continue;

            dist = d;
            enemy = e;
        }

        if (!enemy)
            return;

        Vector3 delta = enemy.transform.position - rotor.position;
        rotor.rotation = Quaternion.LookRotation(delta, Vector3.up);
        Vector3 euler = rotor.eulerAngles;
        euler.x = -6;
        euler.z = 0;

        rotor.eulerAngles = euler;
        if (bulletTime < bulletDelay)
        {
            bulletTime += Time.deltaTime;
            return;
        }

        bulletTime = 0;
        Shot();

    }

    void Shot()
    {
        Bullet b = Instantiate(bullet);
        b.transform.position = bulletPosition.position;
        b.transform.rotation = bulletPosition.rotation;
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (active)
            return;

        if (!other.CompareTag("Turret"))
            return;

        canPlace = false;
        baseRender.material = materials[1];
    }

    private void OnTriggerExit(Collider other)
    {
        if (active)
            return;

        canPlace = true;
        baseRender.material = materials[0];
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
       // Gizmos.DrawWireSphere(transform.position, radius);
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.up, radius);
    }
}
