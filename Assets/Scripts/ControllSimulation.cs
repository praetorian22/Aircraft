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
        uIManager.SPSVButtonPressed(typeButtonSPSV.√Œ“);
        uIManager.AllIndicatorsOff();
        uIManager.FiguraOff();
        soundManager.SoundOff();
    }
    public void TAPress()
    {
        uIManager.SPSVButtonPressed(typeButtonSPSV.TA);
        uIManager.AllIndicatorsOff();
        uIManager.FiguraOn();
        uIManager.RedSquareOff();
        soundManager.SoundOn();
        soundManager.TaSPSVOn();
    }
    public void T_RAPress()
    {
        uIManager.SPSVButtonPressed(typeButtonSPSV.T_RA);
        uIManager.AllIndicatorsOn();
        uIManager.FiguraOn();
        uIManager.RedSquareOn();
        soundManager.SoundOn();
        soundManager.TaSPSVOff();
    }
    public void TestPress()
    {
        if (testPressCoro == null) testPressCoro = StartCoroutine(TestPressCoro());
    }

    private IEnumerator TestPressCoro()
    {
        yield return new WaitForSeconds(5f);
        uIManager.SPSVTestButtonOn();
        yield return new WaitForSeconds(1f);
        uIManager.SPSVTestButtonOff();
        testPressCoro = null;
    }
}
