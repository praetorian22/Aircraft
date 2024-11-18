using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllSimulation : MonoBehaviour
{
    public bool isPause;
    public GameObject pointAirEnemy;
    public GameObject pointAirAlly;
    public GameObject airEnemy;
    public GameObject airAlly;
    public void PressPause()
    {
        if (!isPause)
        {
            Time.timeScale = 0f;
            isPause = true;
        }
        else
        {
            Time.timeScale = 1f;
            isPause = false;
        }
    }
    public void ResetSimulation()
    {
        airEnemy.SetActive(true);
        airAlly.SetActive(true);
        airEnemy.transform.position = pointAirEnemy.transform.position;
        airAlly.transform.position = pointAirAlly.transform.position;
        airEnemy.GetComponent<RotateAirCraft>().NewSimulation();
        airAlly.GetComponent<RotateAirCraft>().NewSimulation();
        airAlly.GetComponent<ControlAlly>().NewSimulation();
    }
}
