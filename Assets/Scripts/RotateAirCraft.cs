using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RotateAirCraft : MonoBehaviour
{
    private float speedRotation;
    [SerializeField] private GameObject backPoint;
    [SerializeField] private GameObject forwardPoint;

    [SerializeField] private int upDown;
    [SerializeField] private List<float> targetAngleList;
    [SerializeField] private float angleNow;
    [SerializeField] private float angleTarget;
    [SerializeField] private int index;
    
    public bool MoveUp => angleNow > 360;
    public bool MoveDown => angleNow < 360;
    public bool MoveForward => angleNow == 360;

    public bool AngleLimit => angleNow == angleTarget;

    public Action<float> changeAngleEvent;
    public float AngleTaret => angleTarget;

    public bool Horizont => index == (targetAngleList.Count - 1) / 2;

    public float vertPositionB => backPoint.transform.position.y;
    public float vertPositionF => forwardPoint.transform.position.y;

    private void Start()
    {
        angleNow = targetAngleList[(targetAngleList.Count - 1)];
        angleTarget = targetAngleList[(targetAngleList.Count - 1)];
    }

    public void NewSimulation(float speedRotation, int defAngleIndex)
    {
        angleNow = targetAngleList[defAngleIndex];
        angleTarget = targetAngleList[defAngleIndex];
        index = defAngleIndex;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleNow));
        this.speedRotation = speedRotation;
    }

    private void Update()
    {
        if (Mathf.Abs(angleNow - angleTarget) > 0.1f)
        {
            if (angleNow > angleTarget) angleNow -= speedRotation * Time.deltaTime;
            else angleNow += speedRotation * Time.deltaTime;
            
        }
        else 
            angleNow = angleTarget;        
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleNow));        
        changeAngleEvent?.Invoke(angleNow);
    } 
    public void SetAngle(float val = 360)
    {
        angleTarget = val;
        index = targetAngleList.IndexOf(angleTarget);
    }
    public bool NextAngle(int val = 1)
    {
        if (index == (targetAngleList.Count - 1)) return false;
        if (index + val < (targetAngleList.Count - 1))
        {
            index += val;
            angleTarget = targetAngleList[index];
            return true;
        }
        else
        {
            index = targetAngleList.Count - 1;
            angleTarget = targetAngleList[index];
            return true;
        }
    }
    public bool PrewAngle(int val = 1)
    {
        if (index == 0) return false;
        if (index - val > 0)
        {
            index -= val;
            angleTarget = targetAngleList[index];
            return true;
        }
        else
        {
            index = 0;
            angleTarget = targetAngleList[index];
            return true;
        }
    }
    public bool NextAngleOne(float val = 1)
    {
        if (angleTarget == targetAngleList[targetAngleList.Count - 1]) return false;
        if (angleTarget + val < targetAngleList[targetAngleList.Count - 1])
        {
            angleTarget += val;
            return true;
        }
        else
        {
            angleTarget = targetAngleList[targetAngleList.Count - 1];
            return true;
        }
    }
    public bool PrewAngleOne(float val = 1)
    {
        if (angleTarget == targetAngleList[0]) return false;
        if (angleTarget - val > targetAngleList[0])
        {
            angleTarget -= val;            
            return true;
        }
        else
        {
            angleTarget = targetAngleList[0];            
            return true;
        }
    }

    public void AngleZero()
    {
        index = (targetAngleList.Count - 1) / 2;
        angleTarget = targetAngleList[index];
    }

    public void UpSpeedRotation(float val)
    {
        speedRotation += val;
    }
}
