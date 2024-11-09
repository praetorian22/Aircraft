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

    private void Start()
    {
        angleNow = targetAngleList[6];
        angleTarget = targetAngleList[6];
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

    public void PressUp()
    {

    }

}
