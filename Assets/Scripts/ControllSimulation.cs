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

    private Coroutine testPressCoro;
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

    private void Start()
    {
        ResetSimulation();
    }

    public void ResetSimulation()
    {
        airEnemy.SetActive(true);
        airAlly.SetActive(true);
        airEnemy.transform.position = pointAirEnemy.transform.position;
        airAlly.transform.position = pointAirAlly.transform.position;
        airEnemy.GetComponent<ControllEnemy>().NewSimulation();        
        airAlly.GetComponent<ControlAlly>().NewSimulation();
        GOTPress();
    }    

    public void GOTPress()
    {
        UIManager.Instance.SPSVButtonPressed(typeButtonSPSV.√Œ“);
        UIManager.Instance.AllIndicatorsOff();
        UIManager.Instance.FiguraOff();
        SoundManager.Instance.SoundOff();
    }
    public void TAPress()
    {
        UIManager.Instance.SPSVButtonPressed(typeButtonSPSV.TA);
        UIManager.Instance.AllIndicatorsOff();
        UIManager.Instance.FiguraOn();
        UIManager.Instance.RedSquareOff();
        SoundManager.Instance.SoundOn();
        SoundManager.Instance.TaSPSVOn();
    }
    public void T_RAPress()
    {
        UIManager.Instance.SPSVButtonPressed(typeButtonSPSV.T_RA);
        UIManager.Instance.AllIndicatorsOn();
        UIManager.Instance.FiguraOn();
        UIManager.Instance.RedSquareOn();
        SoundManager.Instance.SoundOn();
        SoundManager.Instance.TaSPSVOff();
    }
    public void TestPress()
    {
        if (testPressCoro == null) testPressCoro = StartCoroutine(TestPressCoro());
    }

    private IEnumerator TestPressCoro()
    {
        yield return new WaitForSeconds(5f);
        UIManager.Instance.SPSVTestButtonOn();
        yield return new WaitForSeconds(1f);
        UIManager.Instance.SPSVTestButtonOff();
        testPressCoro = null;
    }
}
