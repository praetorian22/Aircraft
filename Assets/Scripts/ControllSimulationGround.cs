using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllSimulationGround : MonoBehaviour
{
    [SerializeField] private GameObject airAllyGround;
    private bool isPause;
    [SerializeField] private GameObject pointAirAlly;    

    private UIManager uIManager;

    private SoundManager soundManager;
    private void Awake()
    {
        soundManager = GetComponent<SoundManager>();
        uIManager = GetComponent<UIManager>();        
        uIManager.groundButtonPressEvent += StartGroundScene;
        uIManager.changeTypeSPVSEvent += ChangeSPSVOption;
    }
    public void StartGroundScene()
    {
        ResetGroundSimulation();
    }
    public void ResetGroundSimulation()
    {
        uIManager.SetSliderPositionZero();        
        airAllyGround.SetActive(true);
        airAllyGround.transform.position = pointAirAlly.transform.position;
        airAllyGround.GetComponent<ControlAllyGround>().NewGroundSimulation();

    }    
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
    public void GOTPress()
    {
        //uIManager.AllIndicatorsOff();
        //uIManager.FiguraOff();
        soundManager.SoundOff();
    }
    public void TAPress()
    {
        //uIManager.AllIndicatorsOff();
        //uIManager.FiguraOn();
        //uIManager.RedSquareOff();
        soundManager.SoundOn();
        soundManager.TaSPSVOn();
    }
    public void T_RAPress()
    {
        //uIManager.AllIndicatorsOn();
        //uIManager.FiguraOn();
        //uIManager.RedSquareOn();
        soundManager.SoundOn();
        soundManager.TaSPSVOff();
    }
    private void ChangeSPSVOption(typeButtonSPSV typeButtonSPSV)
    {
        switch (typeButtonSPSV)
        {
            case typeButtonSPSV.√Œ“:
                {
                    GOTPress();
                    break;
                }
            case typeButtonSPSV.TA:
                {
                    TAPress();
                    break;
                }
            case typeButtonSPSV.T_RA:
                {
                    T_RAPress();
                    break;
                }
        }
    }
}
