using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;
    PathController controller;

    public float speed = 3;
    public float hp = 1;

    public int reward = 5;

    bool isLive = true;

    int currentPoint = 0;

    private void Start()
    {
        controller = GameObject.FindObjectOfType<PathController>();

        transform.position = controller.GetPoint(0).position;
        transform.rotation = controller.GetPoint(0).rotation;
        target = controller.GetPoint(1);
        currentPoint = 1;
    }

    private void Update()
    {
        if (target == null)
            return;

        if (!isLive)
            return;

        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * speed);
        if (Vector3.Distance(transform.position, target.position) < 0.001f)
        {
            transform.position = target.position;
            transform.rotation = target.rotation;

            if (target.CompareTag("Palace"))
            {
                Destroy(gameObject);
                isLive = false;
                Game.instance.RemoveEnemy(this);
                return;
            }

            currentPoint++;
            target = controller.GetPoint(currentPoint);
        }
    }

    public void Damage(float damage)
    {
        if (!isLive)
            return;

        hp -= damage;

        if (hp > 0)
            return;

        isLive = false;
        Game.instance.RemoveEnemy(this);
        Destroy(gameObject);

        Game.money += reward;
    }

}
