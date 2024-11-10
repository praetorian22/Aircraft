using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAirCraft : MonoBehaviour
{
    public float speedRotation;
    public int upDown;
    public List<float> targetAngleList;
    public float angleNow;
    public float angleTarget;
    public int index;

    public bool MoveUp => angleNow > 360;
    public bool MoveDown => angleNow < 360;
    public bool MoveForward => angleNow == 360;

    public bool AngleLimit => angleNow == angleTarget;

    private void Start()
    {
        angleNow = targetAngleList[6];
        angleTarget = targetAngleList[6];
    }

    public void NewSimulation()
    {
        angleNow = targetAngleList[6];
        angleTarget = targetAngleList[6];
        index = 6;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleNow));
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
    } 

    public bool NextAngle(int val = 1)
    {
        if (index == 12) return false;
        if (index + val < 12)
        {
            index += val;
            angleTarget = targetAngleList[index];
            return true;
        }
        else
        {
            index = 12;
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
        if (angleTarget == 390) return false;
        if (angleTarget + val < 390)
        {
            angleTarget += val;
            return true;
        }
        else
        {
            angleTarget = 390;
            return true;
        }
    }
    public bool PrewAngleOne(float val = 1)
    {
        if (angleTarget == 330) return false;
        if (angleTarget - val > 330)
        {
            angleTarget -= val;            
            return true;
        }
        else
        {
            angleTarget = 330;            
            return true;
        }
    }

    public void AngleZero()
    {
        index = 6;
        angleTarget = targetAngleList[index];
    }
}
