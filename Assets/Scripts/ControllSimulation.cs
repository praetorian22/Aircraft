using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllSimulation : MonoBehaviour
{
    private bool isPause;
    [SerializeField] private GameObject pointAirEnemy;
    [SerializeField] private GameObject pointAirAlly;
    [SerializeField] private GameObject airEnemy;
    [SerializeField] private GameObject airAlly;

    private Coroutine testPressCoro;
    private UIManager uIManager;
    private SoundManager soundManager;
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

    private void Awake()
    {
        soundManager = GetComponent<SoundManager>();
        uIManager = GetComponent<UIManager>();
        uIManager.changeTypeSPVSEvent += ChangeSPSVOption;
    }

    private void Start()
    {
        ResetSimulation();
    }

    private void Update()
    {
        if (Input.GetKey("escape"))  
        {
            Application.Quit();    
        }
    }

    public void ResetSimulation()
    {
        airEnemy.SetActive(true);
        airAlly.SetActive(true);
        airEnemy.transform.position = pointAirEnemy.transform.position;
        airAlly.transform.position = pointAirAlly.transform.position;
        airEnemy.GetComponent<ControllEnemy>().NewSimulation();        
        airAlly.GetComponent<ControlAlly>().NewSimulation();        
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

    public void GOTPress()
    {
        uIManager.AllIndicatorsOff();
        uIManager.FiguraOff();        
        soundManager.SoundOff();
    }
    public void TAPress()
    {
        uIManager.AllIndicatorsOff();        
        uIManager.FiguraOn();  
        uIManager.RedSquareOff();
        soundManager.SoundOn();
        soundManager.TaSPSVOn();
    }
    public void T_RAPress()
    {
        uIManager.AllIndicatorsOn();
        uIManager.FiguraOn();
        uIManager.RedSquareOn();
        soundManager.SoundOn();
        soundManager.TaSPSVOff();
    }    
}
