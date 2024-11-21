using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public GameObject explosionPrefab;
    public GameObject enemy;
    public GameObject ally;
    
    public GameObject allyDeadPrefab;
    public GameObject enemyDeadPrefab;

    public void ExplosionCall()
    {
        enemy.SetActive(false);
        ally.SetActive(false);
        GameObject explosion1 = Instantiate(explosionPrefab);
        GameObject explosion2 = Instantiate(explosionPrefab);
        GameObject allyDead = Instantiate(allyDeadPrefab);
        GameObject enemyDead = Instantiate(enemyDeadPrefab);
        explosion1.transform.position = enemy.transform.position;
        explosion2.transform.position = ally.transform.position;
        enemyDead.transform.position = enemy.transform.position;
        allyDead.transform.position = ally.transform.position;
        enemyDead.transform.rotation = enemy.transform.rotation;
        allyDead.transform.rotation = ally.transform.rotation;

        Destroy(enemyDead, 5f);
        Destroy(allyDead, 5f);
        Destroy(explosion1, 5f);
        Destroy(explosion2, 5f);
    }
}
