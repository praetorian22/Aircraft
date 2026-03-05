using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePosition : MonoBehaviour
{
    public GameObject target;

    private void Update()
    {
        gameObject.transform.position = target.transform.position;
    }
}
