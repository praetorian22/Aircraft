using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllEnemy : MonoBehaviour
{
    private RotateAirCraft rotateAirCraft;
    private MoveAirCraft moveAirCraft;
    [SerializeField] private float defaultRotation;
    [SerializeField] private float defaultSpeed;


    private void Awake()
    {
        rotateAirCraft = GetComponent<RotateAirCraft>();
        moveAirCraft = GetComponent<MoveAirCraft>();
    }

    public void NewSimulation()
    {
        rotateAirCraft.NewSimulation(defaultRotation, 6);
        moveAirCraft.SetSpeed(defaultSpeed);
    }

    public void PressUp()
    {
        rotateAirCraft.NextAngle();
    }
    public void PressDown()
    {
        rotateAirCraft.PrewAngle();
    }
}
