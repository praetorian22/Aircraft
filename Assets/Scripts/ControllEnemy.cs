using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllEnemy : MonoBehaviour
{
    public RotateAirCraft rotateAirCraft;

    private void Start()
    {
        rotateAirCraft = GetComponent<RotateAirCraft>();
    }

    public void PressUp()
    {
        if (rotateAirCraft.index < 12)
        {
            rotateAirCraft.index += 1;
            rotateAirCraft.angleTarget = rotateAirCraft.targetAngleList[rotateAirCraft.index];
        }
    }
    public void PressDown()
    {
        if (rotateAirCraft.index > 0)
        {
            rotateAirCraft.index -= 1;
            rotateAirCraft.angleTarget = rotateAirCraft.targetAngleList[rotateAirCraft.index];
        }
    }
}
