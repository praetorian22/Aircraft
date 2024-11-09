using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAirCraft : MonoBehaviour
{
    public float speed;
    public bool ally;

    private void Update()
    {
        if (ally) transform.Translate(Vector3.left * speed * Time.deltaTime);
        else transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}
