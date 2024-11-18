using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateArrow : MonoBehaviour
{
    public GameObject arrow;   
    public float angleNow;    
    
    public void NewSimulation()
    {
        angleNow = 360;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleNow));
    }

    private void Start()
    {
        angleNow = 360;
    }

    private void Update()
    {
        arrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleNow));
    }

    public void TranslateAngle(float angle)
    {
        if (angle < 360f && angle >= 355f)
        {
            angleNow = 360 - (360 - angle) * 12;
        }
        else
        {
            if (angle < 355f && angle >= 350f)
            {
                angleNow = 300 - (355 - angle) * 8;
            }
            else
            {
                if (angle < 350f && angle >= 345f)
                {
                    angleNow = 260 - (350 - angle) * 6;
                }
                else
                {
                    if (angle < 345f && angle >= 330f)
                    {
                        angleNow = 230 - (345 - angle) * 3;
                    }
                    else
                    {
                        if (angle > 360f && angle <= 365f)
                        {
                            angleNow = 360 + (angle - 360) * 12;
                        }
                        else
                        {
                            if (angle > 365f && angle <= 370f)
                            {
                                angleNow = 420 + (angle - 365) * 8;
                            }
                            else
                            {
                                if (angle > 370f && angle <= 375f)
                                {
                                    angleNow = 460 + (angle - 370) * 6;
                                }
                                else
                                {
                                    if (angle > 375f && angle <= 390f)
                                    {
                                        angleNow = 490 + (angle - 375) * 3;
                                    }
                                }
                            }
                        }
                    }
                    
                }
            }
        }
    }
}
