using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public GameObject explosionPrefab;
    public GameObject enemy;
    public GameObject ally;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        enemy.SetActive(false);
        ally.SetActive(false);
        GameObject explosion1 = Instantiate(explosionPrefab);
        GameObject explosion2 = Instantiate(explosionPrefab);
        explosion1.transform.position = enemy.transform.position;
        explosion2.transform.position = ally.transform.position;
    }
}
