using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAirCraft : MonoBehaviour
{
    private float speed;
    [SerializeField] private bool ally;    

    private void Update()
    {
        if (ally) transform.Translate(Vector3.left * speed * Time.deltaTime);
        else transform.Translate(Vector3.right * speed * Time.deltaTime);        
    }

    public void NewSimulation(float speed)
    {
        this.speed = speed;
    }

    public void UPSpeed(float val)
    {
        speed += val;
    }
}
