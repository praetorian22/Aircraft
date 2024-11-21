using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceDP : MonoBehaviour
{
    [SerializeField] private float minForce;
    [SerializeField] private float maxForce;
    [SerializeField] private Vector3 force;

    private void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(force * Random.Range(minForce, maxForce), ForceMode2D.Impulse);
    }
}
