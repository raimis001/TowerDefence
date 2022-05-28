using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Wave
{
    public Enemy[] prefabs;
    public int spawnCount;

    public float delay;
    public float delayStart;

    public float startPercent => startTime / delayStart;

    float spawnTime;
    int count;
    float startTime;

    public readonly List<Enemy> enemys = new List<Enemy>();

    public void Start()
    {
        startTime = delayStart;
    }

    public bool Update()
    {

        if (startTime > 0)
        {
            startTime -= Time.deltaTime;
            if (startTime <= 0)
                spawnTime = delay;

            return true;
        }



        if (count >= spawnCount) 
            return enemys.Count > 0;

        if (spawnTime > 0)
        {
            spawnTime -= Time.deltaTime;
            return true;
        }

        spawnTime = delay;

        int id = Random.Range(0, prefabs.Length);

        Enemy enemy = GameObject.Instantiate(prefabs[id]);

        enemys.Add(enemy);
        count++;

        return true;
    }

    public void RemoveEnemy(Enemy enemy)
    {
        enemys.Remove(enemy);
    }
}

public class Game : MonoBehaviour
{
    public static Game instance;
    public static int money = 100;

    public Image startProgress;

    public List<Wave> waves;

    Wave currentWave => waves[waveID];
    int waveID = 0; 

    internal bool endGame = false;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        currentWave.Start();
    }

    private void Update()
    {
        if (endGame)
            return;

        if (!currentWave.Update())
        {
            NextWave();
            return;
        }

        startProgress.fillAmount = currentWave.startPercent;
    }

    private void NextWave()
    {
        waveID++;
        endGame = waves.Count <= waveID;
        if (endGame)
            return;

        currentWave.Start();
    }

    public void RemoveEnemy(Enemy enemy)
    {
        if (endGame)
            return;

        currentWave.RemoveEnemy(enemy);
    }

    public static IEnumerable<Enemy> EnemyList()
    {
        foreach (Enemy enemy in instance.currentWave.enemys)
            yield return enemy;
    }
}
