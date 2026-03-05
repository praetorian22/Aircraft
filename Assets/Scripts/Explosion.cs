using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public GameObject explosionPrefab;
    public GameObject enemy;
    public GameObject ally;
    public GameObject allyGround;
    public GameObject allyGliss;
    public GameObject enemyExpP;
    public GameObject allyExpP;
    public GameObject allyGroundExpP;
    public GameObject allyGlissExpP;

    public GameObject allyDeadPrefab;
    public GameObject allyGroundDeadPrefab;
    public GameObject enemyDeadPrefab;

    public void ExplosionCall()
    {
        enemy.SetActive(false);
        ally.SetActive(false);
        GameObject explosion1 = Instantiate(explosionPrefab);
        GameObject explosion2 = Instantiate(explosionPrefab);
        GameObject allyDead = Instantiate(allyDeadPrefab);
        GameObject enemyDead = Instantiate(enemyDeadPrefab);
        explosion1.transform.position = enemyExpP.transform.position;
        explosion2.transform.position = allyExpP.transform.position;
        enemyDead.transform.position = enemy.transform.position;
        allyDead.transform.position = ally.transform.position;
        enemyDead.transform.rotation = enemy.transform.rotation;
        allyDead.transform.rotation = ally.transform.rotation;

        Destroy(enemyDead, 5f);
        Destroy(allyDead, 5f);
        Destroy(explosion1, 5f);
        Destroy(explosion2, 5f);
    }
    public void ExplosionCallGround()
    {
        allyGround.SetActive(false);
        GameObject explosion1 = Instantiate(explosionPrefab);        
        GameObject allyDead = Instantiate(allyGroundDeadPrefab);        
        explosion1.transform.position = allyGroundExpP.transform.position;
        allyDead.transform.position = allyGround.transform.position;
        allyDead.transform.rotation = allyGround.transform.rotation;

        Destroy(allyDead, 5f);
        Destroy(explosion1, 5f);
    }
    public void ExplosionCallGliss()
    {
        allyGliss.SetActive(false);
        GameObject explosion1 = Instantiate(explosionPrefab);
        GameObject allyDead = Instantiate(allyGroundDeadPrefab);
        explosion1.transform.position = allyGlissExpP.transform.position;
        allyDead.transform.position = allyGliss.transform.position;
        allyDead.transform.rotation = allyGliss.transform.rotation;

        Destroy(allyDead, 5f);
        Destroy(explosion1, 5f);
    }
}
