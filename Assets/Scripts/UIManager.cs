using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour
{
    [SerializeField] Button controllUpButton;
    [SerializeField] Button controllDownButton;

    [SerializeField] private Color32 colorRed;
    [SerializeField] private Color32 colorGreen;
    [SerializeField] private Color32 colorEmpty;

    [SerializeField] private List<Image> sectors_0_2_5up = new List<Image>();
    [SerializeField] private List<Image> sectors_0_2_5down = new List<Image>();
    [SerializeField] private List<Image> sectors_2_5_5up = new List<Image>();
    [SerializeField] private List<Image> sectors_2_5_5down = new List<Image>();    
    [SerializeField] private List<Image> sectors_5_7_5up = new List<Image>();
    [SerializeField] private List<Image> sectors_5_7_5down = new List<Image>();
    [SerializeField] private List<Image> sectors_7_5_10up = new List<Image>();
    [SerializeField] private List<Image> sectors_7_5_10down = new List<Image>();
    [SerializeField] private List<Image> sectors_10_15up = new List<Image>();
    [SerializeField] private List<Image> sectors_10_15down = new List<Image>();
    [SerializeField] private List<Image> sectors_15_20up = new List<Image>();
    [SerializeField] private List<Image> sectors_15_20down = new List<Image>();
    [SerializeField] private List<Image> sectors_20_225up = new List<Image>();
    [SerializeField] private List<Image> sectors_20_225down = new List<Image>();
    //[SerializeField] private List<Image> sectors_25_30up = new List<Image>();
    //[SerializeField] private List<Image> sectors_25_30down = new List<Image>();

    [SerializeField] private GameObject arrow;
    [SerializeField] private float angleNow;

    [SerializeField] private Image figureObject;
    [SerializeField] private List<Sprite> figures = new List<Sprite>();
    [SerializeField] private Sprite redSquare;
    [SerializeField] private Sprite emptySquare;    
    [SerializeField] private Image SPSVOptionImage;
    [SerializeField] private List<Sprite> SPSVButtonImage = new List<Sprite>();
    [SerializeField] private GameObject SPSVButtonTestImage;

    [SerializeField] private Button exitButton;
    [SerializeField] private Button airButton;
    [SerializeField] private Button groundButton;


    [SerializeField] private GameObject variometrIndicationOff;
    private Dictionary<typeButtonSPSV, Sprite> SPSVButtonImageDict = new Dictionary<typeButtonSPSV, Sprite>();

    private Dictionary<int, List<Image>> sectorDict = new Dictionary<int, List<Image>>();
    [SerializeField] private List<Text> textDisplay_1 = new List<Text>();
    [SerializeField] private List<Text> textDisplay_2 = new List<Text>();
    [SerializeField] private List<Text> textDisplay_3 = new List<Text>();
    private string TA;
    private string ACS;
    private string AS;
    private string GOT;
    private string FID;
    private float timer;
    private float timerZnak;
    private Coroutine timerDisp1Coro;
    private int indexSelectorDisp_1;
    private typeInputDisp1 tpInputDisp1;
    private List<string> fidAlpha = new List<string>();
    private bool znakButtonPress;

    private bool SVS;
    private bool AZN;
    private bool OTV;
    private typeInputDisp3 tpInputDisp3;
    private int indexSPS;
    private List<string> VV = new List<string>() { "ÕŒ–Ã", "¬€ÿ≈", "Õ»∆≈"};
    private int indexVV;
    private List<string> ABS = new List<string>() { "Œ“Õ", "¿¡—"};
    private int indexABS;
    private List<string> TRF = new List<string>() { "¿¬“Œ", " ¬ À" };
    private int indexTRF;
    private int dist;
    private int minH;
    private int maxH;
    private int indexSelectorDisp_3;

    [SerializeField] private List<GameObject> greenButtons = new List<GameObject>();
    private int indexGreenButtonList;

    private figure nowFigure;
    private SceneManager sceneManager;

    private typeButtonSPSV sPSVNow;
    private Coroutine testPressCoro;
    private Coroutine airSceneCoro;
    private Coroutine znakPressCoro;

    public Action<typeButtonSPSV> changeTypeSPVSEvent;
    public Action airButtonPressEvent;
    private void Awake()
    {
        SPSVButtonImageDict.Add(typeButtonSPSV.√Œ“, SPSVButtonImage[0]);
        SPSVButtonImageDict.Add(typeButtonSPSV.TA, SPSVButtonImage[1]);
        SPSVButtonImageDict.Add(typeButtonSPSV.T_RA, SPSVButtonImage[2]);
        sceneManager = GetComponent<SceneManager>();
        fidAlpha = new List<string> { "0", "1","2", "3", "4", "5", "6", "7", "8", "9",
        "A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U",
        "V","W","X","Y","Z"};
    }

    public void PressAirButton()
    {
        exitButton.interactable = false;
        airButton.interactable = false;
        groundButton.interactable = false;
        if (airSceneCoro != null) StopCoroutine(airSceneCoro);
        airSceneCoro = StartCoroutine(AirSceneCoro());
    }

    private IEnumerator AirSceneCoro()
    {
        yield return new WaitForSeconds(1f);
        sceneManager.SetScene(scenes.airGame);
        yield return null;
        airButtonPressEvent?.Invoke();
    }
    private void ResetSPSV()
    {
        TA = "TA  2000";
        ACS = "ACS 2000";
        AS = "AS  2000";
        GOT = "√Œ“ 2000";
        FID = "00000000";                
        tpInputDisp1 = typeInputDisp1.empty;
        SPSVButtonPressed(2);
        textDisplay_2[3].text = " ";
        textDisplay_2[4].text = " ";
        textDisplay_2[5].text = " ";
        textDisplay_2[1].text = "1";
        textDisplay_2[7].text = "1";
        tpInputDisp3 = typeInputDisp3.def;
        indexVV = 0;
        indexABS = 0;
        indexTRF = 0;
        dist = 100;
        minH = 50;
        maxH = 100;
        indexSPS = 0;
        indexGreenButtonList = 0;
        textDisplay_3[indexSelectorDisp_3].GetComponent<Animator>().SetTrigger("Off");
        textDisplay_1[indexSelectorDisp_1].GetComponent<Animator>().SetTrigger("Off");
        ChangeSPS();
    }
    public void NewAirSimulation()
    {
        //SPSVButtonPressed((int)typeButtonSPSV.√Œ“);        
        TraficTrafic_ClearOfConflict();
        ResetSPSV();
        //if (testPressCoro != null) StopCoroutine(testPressCoro);
        //SPSVButtonTestImage.SetActive(false);
        angleNow = 360;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleNow));
    }

    public void ChangeFigura(figure nowFigure)
    {
        figureObject.sprite = figures[(int)nowFigure];
        this.nowFigure = nowFigure;
    }


    public void SetMonitorVerticalSpeed(bool up)
    {
        if (up)
        {
            SetSector025upGreen();
            SetSector255upEmty();
            SetSector575upEmty();
            SetSector7510upEmty();
            SetSector1015upEmty();
            SetSector1520upEmty();
            SetSector20225upEmty();
            //SetSector2530upEmty();

            SetSector025downRed();
            SetSector255downRed();
            SetSector575downRed();
            SetSector7510downRed();
            SetSector1015downRed();
            SetSector1520downRed();
            SetSector20225downRed();
            //SetSector2530downRed();
        }
        else
        {
            SetSector025downGreen();
            SetSector255downEmty();
            SetSector575downEmty();
            SetSector7510downEmty();
            SetSector1015downEmty();
            SetSector1520downEmty();
            SetSector20225downEmty();
            //SetSector2530downEmty();

            SetSector025upRed();
            SetSector255upRed();
            SetSector575upRed();
            SetSector7510upRed();
            SetSector1015upRed();
            SetSector1520upRed();
            SetSector20225upRed();
            //SetSector2530upRed();
        }
    }
    public void TraficTrafic_ClearOfConflict()
    {        
        SetSector025upEmty();
        SetSector255upEmty();
        SetSector575upEmty();
        SetSector7510upEmty();
        SetSector1015upEmty();
        SetSector1520upEmty();        
        SetSector20225upEmty();
        //SetSector2530upEmty();
        SetSector025downEmty();
        SetSector255downEmty();
        SetSector575downEmty();
        SetSector7510downEmty();
        SetSector1015downEmty();
        SetSector1520downEmty();        
        SetSector20225downEmty();
        //SetSector2530downEmty();
    }

    public void ClimbClimb_IncreaseClimb_CrossingClimb(float angle)
    {
        if (angle == 350)
        {
            SetSector025upRed();
            SetSector255upRed();
            SetSector575upRed();
            SetSector7510upGreen();
            SetSector1015upEmty();
            SetSector1520upEmty();
            SetSector20225upEmty();
            //SetSector2530upEmty();

            SetSector025downRed();
            SetSector255downRed();
            SetSector575downRed();
            SetSector7510downRed();
            SetSector1015downRed();
            SetSector1520downRed();
            SetSector20225downRed();
            //SetSector2530downRed();
        }
        if (angle == 345)
        {
            SetSector025upRed();
            SetSector255upRed();
            SetSector575upRed();
            SetSector7510upRed();
            SetSector1015upGreen();
            SetSector1520upEmty();
            SetSector20225upEmty();
            //SetSector2530upEmty();

            SetSector025downRed();
            SetSector255downRed();
            SetSector575downRed();
            SetSector7510downRed();
            SetSector1015downRed();
            SetSector1520downRed();
            SetSector20225downRed();
            //SetSector2530downRed();
        }
    }
    public void DescentDescent_IncreaseDescent_CrossingDescent(float angle)
    {
        if (angle == 370)
        {
            SetSector025downRed();
            SetSector255downRed();
            SetSector575downRed();
            SetSector7510downGreen();
            SetSector1015downEmty();
            SetSector1520downEmty();
            SetSector20225downEmty();
            //SetSector2530downEmty();

            SetSector025upRed();
            SetSector255upRed();
            SetSector575upRed();
            SetSector7510upRed();
            SetSector1015upRed();
            SetSector1520upRed();
            SetSector20225upRed();
            //SetSector2530upRed();
        }
        if (angle == 375)
        {
            SetSector025downRed();
            SetSector255downRed();
            SetSector575downRed();
            SetSector7510downRed();
            SetSector1015downGreen();
            SetSector1520downEmty();
            SetSector20225downEmty();
            //SetSector2530downEmty();

            SetSector025upRed();
            SetSector255upRed();
            SetSector575upRed();
            SetSector7510upRed();
            SetSector1015upRed();
            SetSector1520upRed();
            SetSector20225upRed();
            //SetSector2530upRed();
        }
    }

    public void Climb_ClimbNow()
    {
        SetSector025upRed();
        SetSector255upRed();
        SetSector575upRed();
        SetSector7510upGreen();
        SetSector1015upEmty();
        SetSector1520upEmty();
        SetSector20225upEmty();
        //SetSector2530upEmty();

        SetSector025downRed();
        SetSector255downRed();
        SetSector575downRed();
        SetSector7510downRed();
        SetSector1015downRed();
        SetSector1520downRed();
        SetSector20225downRed();
        //SetSector2530downRed();
    }
    public void Descent_DescentNow()
    {
        SetSector025downRed();
        SetSector255downRed();
        SetSector575downRed();
        SetSector7510downGreen();
        SetSector1015downEmty();
        SetSector1520downEmty();
        SetSector20225downEmty();
        //SetSector2530downEmty();

        SetSector025upRed();
        SetSector255upRed();
        SetSector575upRed();
        SetSector7510upRed();
        SetSector1015upRed();
        SetSector1520upRed();
        SetSector20225upRed();
        //SetSector2530upRed();
    }
    public void Maintain(float angle)
    {
        if (angle > 370)
        {
            SetSector025downRed();
            SetSector255downRed();
            SetSector575downRed();
            SetSector7510downRed();
            SetSector1015downGreen();
            SetSector1520downGreen();
            SetSector20225downGreen();
            //SetSector2530downEmty();

            SetSector025upRed();
            SetSector255upRed();
            SetSector575upRed();
            SetSector7510upRed();
            SetSector1015upRed();
            SetSector1520upRed();
            SetSector20225upRed();
            //SetSector2530upRed();
        }
        if (angle < 350)
        {
            SetSector025upRed();
            SetSector255upRed();
            SetSector575upRed();
            SetSector7510upRed();
            SetSector1015upGreen();
            SetSector1520upGreen();
            SetSector20225upGreen();
            //SetSector2530downEmty();

            SetSector025downRed();
            SetSector255downRed();
            SetSector575downRed();
            SetSector7510downRed();
            SetSector1015downRed();
            SetSector1520downRed();
            SetSector20225downRed();
            //SetSector2530upRed();
        }
    }

    private void SetSectorsColor(List<Image> list, Color32 color)
    {
        foreach (Image image in list)
        {
            image.color = color;
        }
    }

    private void SetSector025upRed()
    {
        SetSectorsColor(sectors_0_2_5up, colorRed);
    }
    private void SetSector025upGreen()
    {
        SetSectorsColor(sectors_0_2_5up, colorGreen);
    }
    private void SetSector025upEmty()
    {
        SetSectorsColor(sectors_0_2_5up, colorEmpty);
    }
    private void SetSector025downRed()
    {
        SetSectorsColor(sectors_0_2_5down, colorRed);
    }
    private void SetSector025downGreen()
    {
        SetSectorsColor(sectors_0_2_5down, colorGreen);
    }
    private void SetSector025downEmty()
    {
        SetSectorsColor(sectors_0_2_5down, colorEmpty);
    }

    private void SetSector255upRed()
    {
        SetSectorsColor(sectors_2_5_5up, colorRed);
    }
    private void SetSector255upGreen()
    {
        SetSectorsColor(sectors_2_5_5up, colorGreen);
    }
    private void SetSector255upEmty()
    {
        SetSectorsColor(sectors_2_5_5up, colorEmpty);
    }
    private void SetSector255downRed()
    {
        SetSectorsColor(sectors_2_5_5down, colorRed);
    }
    private void SetSector255downGreen()
    {
        SetSectorsColor(sectors_2_5_5down, colorGreen);
    }
    private void SetSector255downEmty()
    {
        SetSectorsColor(sectors_2_5_5down, colorEmpty);
    }

    private void SetSector575upRed()
    {
        SetSectorsColor(sectors_5_7_5up, colorRed);
    }
    private void SetSector575upGreen()
    {
        SetSectorsColor(sectors_5_7_5up, colorGreen);
    }
    private void SetSector575upEmty()
    {
        SetSectorsColor(sectors_5_7_5up, colorEmpty);
    }
    private void SetSector575downRed()
    {
        SetSectorsColor(sectors_5_7_5down, colorRed);
    }
    private void SetSector575downGreen()
    {
        SetSectorsColor(sectors_5_7_5down, colorGreen);
    }
    private void SetSector575downEmty()
    {
        SetSectorsColor(sectors_5_7_5down, colorEmpty);
    }
    private void SetSector7510upRed()
    {
        SetSectorsColor(sectors_7_5_10up, colorRed);
    }
    private void SetSector7510upGreen()
    {
        SetSectorsColor(sectors_7_5_10up, colorGreen);
    }
    private void SetSector7510upEmty()
    {
        SetSectorsColor(sectors_7_5_10up, colorEmpty);
    }
    private void SetSector7510downRed()
    {
        SetSectorsColor(sectors_7_5_10down, colorRed);
    }
    private void SetSector7510downGreen()
    {
        SetSectorsColor(sectors_7_5_10down, colorGreen);
    }
    private void SetSector7510downEmty()
    {
        SetSectorsColor(sectors_7_5_10down, colorEmpty);
    }
    private void SetSector1015upRed()
    {
        SetSectorsColor(sectors_10_15up, colorRed);
    }
    private void SetSector1015upGreen()
    {
        SetSectorsColor(sectors_10_15up, colorGreen);
    }
    private void SetSector1015upEmty()
    {
        SetSectorsColor(sectors_10_15up, colorEmpty);
    }
    private void SetSector1015downRed()
    {
        SetSectorsColor(sectors_10_15down, colorRed);
    }
    private void SetSector1015downGreen()
    {
        SetSectorsColor(sectors_10_15down, colorGreen);
    }
    private void SetSector1015downEmty()
    {
        SetSectorsColor(sectors_10_15down, colorEmpty);
    }

    private void SetSector1520upRed()
    {
        SetSectorsColor(sectors_15_20up, colorRed);
    }
    private void SetSector1520upGreen()
    {
        SetSectorsColor(sectors_15_20up, colorGreen);
    }
    private void SetSector1520upEmty()
    {
        SetSectorsColor(sectors_15_20up, colorEmpty);
    }
    private void SetSector1520downRed()
    {
        SetSectorsColor(sectors_15_20down, colorRed);
    }
    private void SetSector1520downGreen()
    {
        SetSectorsColor(sectors_15_20down, colorGreen);
    }
    private void SetSector1520downEmty()
    {
        SetSectorsColor(sectors_15_20down, colorEmpty);
    }

    private void SetSector20225upRed()
    {
        SetSectorsColor(sectors_20_225up, colorRed);
    }
    private void SetSector20225upGreen()
    {
        SetSectorsColor(sectors_20_225up, colorGreen);
    }
    private void SetSector20225upEmty()
    {
        SetSectorsColor(sectors_20_225up, colorEmpty);
    }
    private void SetSector20225downRed()
    {
        SetSectorsColor(sectors_20_225down, colorRed);
    }
    private void SetSector20225downGreen()
    {
        SetSectorsColor(sectors_20_225down, colorGreen);
    }
    private void SetSector20225downEmty()
    {
        SetSectorsColor(sectors_20_225down, colorEmpty);
    }
    /*
    private void SetSector2530upRed()
    {
        SetSectorsColor(sectors_25_30up, colorRed);
    }
    private void SetSector2530upGreen()
    {
        SetSectorsColor(sectors_25_30up, colorGreen);
    }
    private void SetSector2530upEmty()
    {
        SetSectorsColor(sectors_25_30up, colorEmpty);
    }
    private void SetSector2530downRed()
    {
        SetSectorsColor(sectors_25_30down, colorRed);
    }
    private void SetSector2530downGreen()
    {
        SetSectorsColor(sectors_25_30down, colorGreen);
    }
    private void SetSector2530downEmty()
    {
        SetSectorsColor(sectors_25_30down, colorEmpty);
    }
    */
    private void Update()
    {
        arrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleNow));
    }

    public void TranslateAngleArrow(float angle)
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

    public void AllIndicatorsOff()
    {
        variometrIndicationOff.SetActive(true);  
    }
    public void AllIndicatorsOn()
    {
        variometrIndicationOff.SetActive(false);
    }
    public void FiguraOff()
    {
        figureObject.color = new Color32(255, 255, 255, 0);
    }
    public void FiguraOn()
    {
        figureObject.color = new Color32(255, 255, 255, 255);
    } 
    public void RedSquareOff()
    {
        figures[(int)figure.redSquare] = emptySquare;
        ChangeFigura(nowFigure);
    }
    public void RedSquareOn()
    {
        figures[(int)figure.redSquare] = redSquare;
        ChangeFigura(nowFigure);
    }
    public void SPSVButtonPressed(int tButtonSPSV)
    {
        if (tpInputDisp1 == typeInputDisp1.empty)
        {
            sPSVNow = (typeButtonSPSV)tButtonSPSV;
            if (sPSVNow == typeButtonSPSV.√Œ“)
            {
                SetGotSetting();
            }
            if (sPSVNow == typeButtonSPSV.ACS)
            {
                SetACSSetting();
            }
            if (sPSVNow == typeButtonSPSV.AS)
            {
                SetASSetting();
            }
            if (sPSVNow == typeButtonSPSV.TA)
            {
                SetTASetting();
            }
            if (znakButtonPress)
            {
                textDisplay_1[3].text = "*";
                textDisplay_1[3].GetComponent<Animator>().SetTrigger("On");
            }
            else
            {
                textDisplay_1[3].text = " ";
                textDisplay_1[3].GetComponent<Animator>().SetTrigger("Off");
                if (znakPressCoro != null) StopCoroutine(znakPressCoro);
            }
        }
        
        /*
        if ((int)sPSVNow < 3)
        {
            SPSVOptionImage.GetComponent<Image>().sprite = SPSVButtonImageDict[sPSVNow];
            changeTypeSPVSEvent?.Invoke(sPSVNow);
        }
        else
        {
            TestPress();
        }
        */
    }
    private void SetTASetting()
    {
        StringToDisplay(1, TA);
    }
    private void SetGotSetting()
    {
        StringToDisplay(1, GOT);
    }
    private void SetACSSetting()
    {
        StringToDisplay(1, ACS);
    }
    private void SetASSetting()
    {
        StringToDisplay(1, AS);
    }

    private void StringToDisplay(int numDisplay, string str)
    {
        if (numDisplay == 1)
        {
            textDisplay_1[0].text = str[0].ToString();
            textDisplay_1[1].text = str[1].ToString();
            textDisplay_1[2].text = str[2].ToString();
            textDisplay_1[3].text = str[3].ToString();
            textDisplay_1[4].text = str[4].ToString();
            textDisplay_1[5].text = str[5].ToString();
            textDisplay_1[6].text = str[6].ToString();
            textDisplay_1[7].text = str[7].ToString();
        }
        if (numDisplay == 3)
        {
            textDisplay_3[0].text = str[0].ToString();
            textDisplay_3[1].text = str[1].ToString();
            textDisplay_3[2].text = str[2].ToString();
            textDisplay_3[3].text = str[3].ToString();
            textDisplay_3[4].text = str[4].ToString();
            textDisplay_3[5].text = str[5].ToString();
            textDisplay_3[6].text = str[6].ToString();
            textDisplay_3[7].text = str[7].ToString();
        }
    }
    private void DisplayToString(int numDisplay, ref string str)
    {
        if (numDisplay == 1)
        {
            str = textDisplay_1[0].text + textDisplay_1[1].text + textDisplay_1[2].text + textDisplay_1[3].text +
                textDisplay_1[4].text + textDisplay_1[5].text + textDisplay_1[6].text + textDisplay_1[7].text;            
        }
    }
    public void PressKODButton()
    {
        if (tpInputDisp1 == typeInputDisp1.empty)
        {
            if (!znakButtonPress)
            {
                textDisplay_1[3].text = " ";
                textDisplay_1[3].GetComponent<Animator>().SetTrigger("Off");
                if (znakPressCoro != null) StopCoroutine(znakPressCoro);
            }
            tpInputDisp1 = typeInputDisp1.kod;
            if (tpInputDisp3 == typeInputDisp3.r2 || tpInputDisp3 == typeInputDisp3.r3)
            {
                textDisplay_3[indexSelectorDisp_3].GetComponent<Animator>().SetTrigger("Off");
            }
            StringToDisplay(1, "A   " + textDisplay_1[4].text + textDisplay_1[5].text + textDisplay_1[6].text + textDisplay_1[7].text);
            indexSelectorDisp_1 = 4;
            textDisplay_1[indexSelectorDisp_1].GetComponent<Animator>().SetTrigger("On");
            if (timerDisp1Coro != null)
            {
                StopCoroutine(timerDisp1Coro);
            }
            timerDisp1Coro = StartCoroutine(TimerDisp1Coro());
        }
        
    }
    public void PressFidButton()
    {
        if (tpInputDisp1 == typeInputDisp1.empty)
        {
            textDisplay_1[3].text = " ";
            textDisplay_1[3].GetComponent<Animator>().SetTrigger("Off");
            if (znakPressCoro != null) StopCoroutine(znakPressCoro);
            
            tpInputDisp1 = typeInputDisp1.fid;
            if (tpInputDisp3 == typeInputDisp3.r2 || tpInputDisp3 == typeInputDisp3.r3)
            {
                textDisplay_3[indexSelectorDisp_3].GetComponent<Animator>().SetTrigger("Off");
            }
            StringToDisplay(1, FID);
            indexSelectorDisp_1 = 0;
            textDisplay_1[indexSelectorDisp_1].GetComponent<Animator>().SetTrigger("On");
            if (timerDisp1Coro != null)
            {
                StopCoroutine(timerDisp1Coro);
            }
            timerDisp1Coro = StartCoroutine(TimerDisp1Coro());
        }
    }
    public void PressUpButton()
    {
        if (tpInputDisp1 == typeInputDisp1.kod)
        {
            if (int.Parse(textDisplay_1[indexSelectorDisp_1].text) < 7)
            {
                textDisplay_1[indexSelectorDisp_1].text = (int.Parse(textDisplay_1[indexSelectorDisp_1].text) + 1).ToString();
            }
            else
                textDisplay_1[indexSelectorDisp_1].text = "0";
            timer = 0;
        }
        else
        {
            if (tpInputDisp1 == typeInputDisp1.fid)
            {
                int index = fidAlpha.IndexOf(textDisplay_1[indexSelectorDisp_1].text);
                if (index < fidAlpha.Count - 1)
                {
                    textDisplay_1[indexSelectorDisp_1].text = fidAlpha[index + 1];
                }
                else
                {
                    textDisplay_1[indexSelectorDisp_1].text = fidAlpha[0];
                }
                timer = 0;
            }
            else
            {
                if (tpInputDisp3 == typeInputDisp3.r1)
                {
                    if (dist < 200) dist += 25;
                    ChangeSPS();
                }
                if (tpInputDisp3 == typeInputDisp3.r2 || tpInputDisp3 == typeInputDisp3.r3)
                {
                    
                    if (int.Parse(textDisplay_3[indexSelectorDisp_3].text) < 9)
                        textDisplay_3[indexSelectorDisp_3].text = (int.Parse(textDisplay_3[indexSelectorDisp_3].text) + 1).ToString();
                    else
                        textDisplay_3[indexSelectorDisp_3].text = "0";
                    if (tpInputDisp3 == typeInputDisp3.r2)
                    {
                        maxH = int.Parse(textDisplay_3[2].text + textDisplay_3[3].text + textDisplay_3[4].text);
                        if (maxH > 127)
                        {
                            textDisplay_3[2].text = "1";
                            textDisplay_3[3].text = "2";
                            textDisplay_3[4].text = "7";
                            maxH = 127;
                        }
                        if (maxH < 27)
                        {
                            textDisplay_3[2].text = "0";
                            textDisplay_3[3].text = "2";
                            textDisplay_3[4].text = "7";
                            maxH = 27;
                        }
                    }
                    if (tpInputDisp3 == typeInputDisp3.r3)
                    {
                        minH = int.Parse(textDisplay_3[2].text + textDisplay_3[3].text + textDisplay_3[4].text);
                        if (minH > 127)
                        {
                            textDisplay_3[2].text = "1";
                            textDisplay_3[3].text = "2";
                            textDisplay_3[4].text = "7";
                            minH = 127;
                        }
                        if (minH < 27)
                        {
                            textDisplay_3[2].text = "0";
                            textDisplay_3[3].text = "2";
                            textDisplay_3[4].text = "7";
                            minH = 27;
                        }
                    }
                }
                if (tpInputDisp3 == typeInputDisp3.r4)
                {
                    if (indexTRF < 1)
                        indexTRF++;
                    else
                        indexTRF = 0;
                    StringToDisplay(3, "“–‘ " + TRF[indexTRF]);
                }            
            }
        }
        
    }
    public void PressDownButton()
    {
        if (tpInputDisp1 == typeInputDisp1.kod)
        {
            if (int.Parse(textDisplay_1[indexSelectorDisp_1].text) > 0)
            {
                textDisplay_1[indexSelectorDisp_1].text = (int.Parse(textDisplay_1[indexSelectorDisp_1].text) - 1).ToString();
            }
            else
                textDisplay_1[indexSelectorDisp_1].text = "7";
            timer = 0;
        }
        else
        {
            if (tpInputDisp1 == typeInputDisp1.fid)
            {
                int index = fidAlpha.IndexOf(textDisplay_1[indexSelectorDisp_1].text);
                if (index > 0)
                {
                    textDisplay_1[indexSelectorDisp_1].text = fidAlpha[index - 1];
                }
                else
                {
                    textDisplay_1[indexSelectorDisp_1].text = fidAlpha[fidAlpha.Count - 1];
                }
                timer = 0;
            }
            else
            {
                if (tpInputDisp3 == typeInputDisp3.r1)
                {
                    if (dist > 0) dist -= 25;
                    ChangeSPS();
                }
                if (tpInputDisp3 == typeInputDisp3.r2 || tpInputDisp3 == typeInputDisp3.r3)
                {
                    if (int.Parse(textDisplay_3[indexSelectorDisp_3].text) > 0)
                    {
                        textDisplay_3[indexSelectorDisp_3].text = (int.Parse(textDisplay_3[indexSelectorDisp_3].text) - 1).ToString();
                    }                        
                    else
                    {
                        textDisplay_3[indexSelectorDisp_3].text = "9";
                    }

                    if (tpInputDisp3 == typeInputDisp3.r2)
                    {
                        maxH = int.Parse(textDisplay_3[2].text + textDisplay_3[3].text + textDisplay_3[4].text);
                        if (maxH > 127)
                        {
                            textDisplay_3[2].text = "1";
                            textDisplay_3[3].text = "2";
                            textDisplay_3[4].text = "7";
                            maxH = 127;
                        }
                        if (maxH < 27)
                        {
                            textDisplay_3[2].text = "0";
                            textDisplay_3[3].text = "2";
                            textDisplay_3[4].text = "7";
                            maxH = 27;
                        }
                    }
                    if (tpInputDisp3 == typeInputDisp3.r3)
                    {
                        minH = int.Parse(textDisplay_3[2].text + textDisplay_3[3].text + textDisplay_3[4].text);
                        if (minH > 127)
                        {
                            textDisplay_3[2].text = "1";
                            textDisplay_3[3].text = "2";
                            textDisplay_3[4].text = "7";
                            minH = 127;
                        }
                        if (minH < 27)
                        {
                            textDisplay_3[2].text = "0";
                            textDisplay_3[3].text = "2";
                            textDisplay_3[4].text = "7";
                            minH = 27;
                        }
                    }
                }
                if (tpInputDisp3 == typeInputDisp3.r4)
                {
                    if (indexTRF > 0)
                        indexTRF--;
                    else
                        indexTRF = 1;
                    StringToDisplay(3, "“–‘ " + TRF[indexTRF]);
                }
            }
        }
        
    }
    public void PressLeftButton()
    {
        if (tpInputDisp1 == typeInputDisp1.kod)
        {
            textDisplay_1[indexSelectorDisp_1].GetComponent<Animator>().SetTrigger("Off");
            if (indexSelectorDisp_1 > 4)
                indexSelectorDisp_1 = indexSelectorDisp_1 - 1;
            else indexSelectorDisp_1 = 7;
            textDisplay_1[indexSelectorDisp_1].GetComponent<Animator>().SetTrigger("On");
            timer = 0;
        }
        else
        {
            if (tpInputDisp1 == typeInputDisp1.fid)
            {
                textDisplay_1[indexSelectorDisp_1].GetComponent<Animator>().SetTrigger("Off");
                if (indexSelectorDisp_1 > 0)
                    indexSelectorDisp_1 = indexSelectorDisp_1 - 1;
                else indexSelectorDisp_1 = 7;
                textDisplay_1[indexSelectorDisp_1].GetComponent<Animator>().SetTrigger("On");
                timer = 0;
            }
            else
            {
                if (tpInputDisp3 == typeInputDisp3.r2 || tpInputDisp3 == typeInputDisp3.r3)
                {
                    textDisplay_3[indexSelectorDisp_3].GetComponent<Animator>().SetTrigger("Off");
                    if (indexSelectorDisp_3 > 2)
                        indexSelectorDisp_3 = indexSelectorDisp_3 - 1;
                    else indexSelectorDisp_3 = 4;
                    textDisplay_3[indexSelectorDisp_3].GetComponent<Animator>().SetTrigger("On");
                }
            }
        }
        
    }
    public void PressRightButton()
    {
        if (tpInputDisp1 == typeInputDisp1.kod)
        {
            textDisplay_1[indexSelectorDisp_1].GetComponent<Animator>().SetTrigger("Off");
            if (indexSelectorDisp_1 < 7)
                indexSelectorDisp_1 = indexSelectorDisp_1 + 1;
            else indexSelectorDisp_1 = 4;
            textDisplay_1[indexSelectorDisp_1].GetComponent<Animator>().SetTrigger("On");
            timer = 0;
        }
        else
        {
            if (tpInputDisp1 == typeInputDisp1.fid)
            {
                textDisplay_1[indexSelectorDisp_1].GetComponent<Animator>().SetTrigger("Off");
                if (indexSelectorDisp_1 < 7)
                    indexSelectorDisp_1 = indexSelectorDisp_1 + 1;
                else indexSelectorDisp_1 = 0;
                textDisplay_1[indexSelectorDisp_1].GetComponent<Animator>().SetTrigger("On");
                timer = 0;
            }
            else
            {
                if (tpInputDisp3 == typeInputDisp3.r2 || tpInputDisp3 == typeInputDisp3.r3)
                {
                    textDisplay_3[indexSelectorDisp_3].GetComponent<Animator>().SetTrigger("Off");
                    if (indexSelectorDisp_3 < 4)
                        indexSelectorDisp_3 = indexSelectorDisp_3 + 1;
                    else indexSelectorDisp_3 = 2;
                    textDisplay_3[indexSelectorDisp_3].GetComponent<Animator>().SetTrigger("On");
                }
            }
        }
        
    }
    public void PressVVODButton()
    {
        if (tpInputDisp1 == typeInputDisp1.kod)
        {
            if (sPSVNow == typeButtonSPSV.√Œ“)
            {
                GOT = "√Œ“ " + textDisplay_1[4].text + textDisplay_1[5].text + textDisplay_1[6].text + textDisplay_1[7].text;
                SetGotSetting();
            }
            if (sPSVNow == typeButtonSPSV.ACS)
            {
                ACS = "ACS " + textDisplay_1[4].text + textDisplay_1[5].text + textDisplay_1[6].text + textDisplay_1[7].text;
                SetACSSetting();
            }
            if (sPSVNow == typeButtonSPSV.AS)
            {
                AS = "AS  " + textDisplay_1[4].text + textDisplay_1[5].text + textDisplay_1[6].text + textDisplay_1[7].text;
                SetASSetting();
            }
            if (sPSVNow == typeButtonSPSV.TA)
            {
                TA = "TA  " + textDisplay_1[4].text + textDisplay_1[5].text + textDisplay_1[6].text + textDisplay_1[7].text;
                SetTASetting();
            }
            textDisplay_1[indexSelectorDisp_1].GetComponent<Animator>().SetTrigger("Off");
            tpInputDisp1 = typeInputDisp1.empty;
            if (tpInputDisp3 == typeInputDisp3.r2 || tpInputDisp3 == typeInputDisp3.r3)
            {
                textDisplay_3[indexSelectorDisp_3].GetComponent<Animator>().SetTrigger("On");
            }
            if (timerDisp1Coro != null) StopCoroutine(timerDisp1Coro);
            timerDisp1Coro = null;
            timer = 100;
        }
        if (tpInputDisp1 == typeInputDisp1.fid)
        {
            DisplayToString(1, ref FID);
            if (sPSVNow == typeButtonSPSV.√Œ“)
            {
                SetGotSetting();
            }
            if (sPSVNow == typeButtonSPSV.ACS)
            {
                SetACSSetting();
            }
            if (sPSVNow == typeButtonSPSV.AS)
            {
                SetASSetting();
            }
            if (sPSVNow == typeButtonSPSV.TA)
            {
                SetTASetting();
            }            
            textDisplay_1[indexSelectorDisp_1].GetComponent<Animator>().SetTrigger("Off");
            tpInputDisp1 = typeInputDisp1.empty;
            if (tpInputDisp3 == typeInputDisp3.r2 || tpInputDisp3 == typeInputDisp3.r3)
            {
                textDisplay_3[indexSelectorDisp_3].GetComponent<Animator>().SetTrigger("On");
            }
            if (timerDisp1Coro != null) StopCoroutine(timerDisp1Coro);
            timerDisp1Coro = null;
            timer = 100;
        }
        if (znakButtonPress)
        {
            textDisplay_1[3].text = "*";
            textDisplay_1[3].GetComponent<Animator>().SetTrigger("On");
        }
        else
        {
            textDisplay_1[3].text = " ";
            textDisplay_1[3].GetComponent<Animator>().SetTrigger("Off");
            if (znakPressCoro != null) StopCoroutine(znakPressCoro);
        }
    }
    private IEnumerator TimerDisp1Coro()
    {
        timer = 0f;
        while (timer < 10f)
        {
            yield return new WaitForSeconds(0.1f);
            timer += 0.1f;
        }
        if (sPSVNow == typeButtonSPSV.√Œ“)
        {
            SetGotSetting();
        }
        if (sPSVNow == typeButtonSPSV.ACS)
        {
            SetACSSetting();
        }
        if (sPSVNow == typeButtonSPSV.AS)
        {
            SetASSetting();
        }
        if (sPSVNow == typeButtonSPSV.TA)
        {
            SetTASetting();
        }
        textDisplay_1[indexSelectorDisp_1].GetComponent<Animator>().SetTrigger("Off");
        timerDisp1Coro = null;
        tpInputDisp1 = typeInputDisp1.empty;
        if (tpInputDisp3 == typeInputDisp3.r2 || tpInputDisp3 == typeInputDisp3.r3)
        {
            textDisplay_3[indexSelectorDisp_3].GetComponent<Animator>().SetTrigger("On");
        }
        if (znakButtonPress)
        {
            textDisplay_1[3].text = "*";
            textDisplay_1[3].GetComponent<Animator>().SetTrigger("On");
        }
        else
        {
            textDisplay_1[3].text = " ";
            textDisplay_1[3].GetComponent<Animator>().SetTrigger("Off");
            if (znakPressCoro != null) StopCoroutine(znakPressCoro);
        }
    }
    public void PressZnakButton()
    {
        if (tpInputDisp1 == typeInputDisp1.empty)
        {
            znakButtonPress = !znakButtonPress;
            if (znakButtonPress)
            {
                textDisplay_1[3].text = "*";
                textDisplay_1[3].GetComponent<Animator>().SetTrigger("On");
            }
            else
            {
                if (znakPressCoro != null) StopCoroutine(znakPressCoro);
                znakPressCoro = StartCoroutine(ZnakPressCoro());
            }
        }        
    }
    private IEnumerator ZnakPressCoro()
    {
        timerZnak = 0;
        while (timerZnak < 18)
        {
            yield return new WaitForSeconds(0.1f);
            timerZnak += 0.1f;
        }
        textDisplay_1[3].text = " ";
        textDisplay_1[3].GetComponent<Animator>().SetTrigger("Off");
    }
    public void PressSVSButton()
    {
        SVS = !SVS;
        if (SVS)
           textDisplay_2[7].text = "2";
        else
           textDisplay_2[7].text = "1";        
    }
    public void PressAZNButton()
    {
        AZN = !AZN;
        if (AZN)
        {
            textDisplay_2[3].text = "A";
            textDisplay_2[4].text = "«";
            textDisplay_2[5].text = "Õ";
        }            
        else
        {
            textDisplay_2[3].text = " ";
            textDisplay_2[4].text = " ";
            textDisplay_2[5].text = " ";
        }
    }
    public void PressOTVButton()
    {
        OTV = !OTV;
        if (OTV)
        {
            textDisplay_2[1].text = "2";            
        }
        else
        {
            textDisplay_2[1].text = "1";            
        }
    }
    public void PressSPSButton()
    {
        if (indexSPS < 4)
            indexSPS++;
        else
            indexSPS = 0;
        ChangeSPS();
    }
    public void PressVV()
    {
        if (tpInputDisp3 == typeInputDisp3.def)
        {
            if (indexVV < 2)
                indexVV++;
            else
                indexVV = 0;
            ChangeSPS();
        }
        
    }
    public void PressABS()
    {
        if (tpInputDisp3 == typeInputDisp3.def)
        {
            if (indexABS < 1)
                indexABS++;
            else
                indexABS = 0;
            ChangeSPS();
        }
            
    }
    
    private void ChangeSPS()
    {
        switch (indexSPS)
        {
            case 0:
                {
                    textDisplay_3[indexSelectorDisp_3].GetComponent<Animator>().SetTrigger("Off");
                    tpInputDisp3 = typeInputDisp3.def;
                    string s;
                    s = VV[indexVV] + " " + ABS[indexABS];
                    StringToDisplay(3, s);                    
                    break;
                }
            case 1:
                {
                    textDisplay_3[indexSelectorDisp_3].GetComponent<Animator>().SetTrigger("Off");
                    tpInputDisp3 = typeInputDisp3.r1;
                    string s = "        ";
                    if (dist < 10) s = "ƒ   " + dist.ToString() + " " + "ÃÃ";
                    if (dist < 100 && dist > 10) s = "ƒ  " + dist.ToString() + " " + "ÃÃ";
                    if (dist >= 100) s = "ƒ " + dist.ToString() + " " + "ÃÃ";
                    StringToDisplay(3, s);
                    break;
                }
            case 2:
                {
                    if (indexSelectorDisp_3 != 2) textDisplay_3[indexSelectorDisp_3].GetComponent<Animator>().SetTrigger("Off");
                    tpInputDisp3 = typeInputDisp3.r2;
                    indexSelectorDisp_3 = 2;
                    string s = "A     FL";
                    StringToDisplay(3, s);
                    string s2 = maxH.ToString();
                    if (tpInputDisp1 == typeInputDisp1.empty)
                        textDisplay_3[indexSelectorDisp_3].GetComponent<Animator>().SetTrigger("On");
                    if (maxH >= 100)
                    {
                        textDisplay_3[2].text = s2[0].ToString();
                        textDisplay_3[3].text = s2[1].ToString();
                        textDisplay_3[4].text = s2[2].ToString();
                    }
                    else
                    {
                        if (maxH >= 10)
                        {
                            textDisplay_3[2].text = "0";
                            textDisplay_3[3].text = s2[0].ToString();
                            textDisplay_3[4].text = s2[1].ToString();
                        }
                        else
                        {
                            textDisplay_3[2].text = "0";
                            textDisplay_3[3].text = "0";
                            textDisplay_3[4].text = s2[0].ToString();
                        }
                    }                    
                    break;
                }
            case 3:
                {
                    if (indexSelectorDisp_3 != 2) textDisplay_3[indexSelectorDisp_3].GetComponent<Animator>().SetTrigger("Off");
                    tpInputDisp3 = typeInputDisp3.r3;
                    indexSelectorDisp_3 = 2;
                    string s = "B     FL";
                    StringToDisplay(3, s);
                    string s2 = minH.ToString();
                    if (tpInputDisp1 == typeInputDisp1.empty)
                        textDisplay_3[indexSelectorDisp_3].GetComponent<Animator>().SetTrigger("On");
                    if (minH >= 100)
                    {
                        textDisplay_3[2].text = s2[0].ToString();
                        textDisplay_3[3].text = s2[1].ToString();
                        textDisplay_3[4].text = s2[2].ToString();
                    }
                    else
                    {
                        if (minH >= 10)
                        {
                            textDisplay_3[2].text = "0";
                            textDisplay_3[3].text = s2[0].ToString();
                            textDisplay_3[4].text = s2[1].ToString();
                        }
                        else
                        {
                            textDisplay_3[2].text = "0";
                            textDisplay_3[3].text = "0";
                            textDisplay_3[4].text = s2[0].ToString();
                        }
                    }
                    break;
                }
            case 4:
                {
                    textDisplay_3[indexSelectorDisp_3].GetComponent<Animator>().SetTrigger("Off");
                    tpInputDisp3 = typeInputDisp3.r4;
                    StringToDisplay(3, "“–‘ " + TRF[indexTRF]);
                    break;
                }
        }
    }
    
    public void TestPress()
    {
        if (testPressCoro != null) StopCoroutine(testPressCoro);
        testPressCoro = StartCoroutine(TestPressCoro());
    }
    
    private IEnumerator TestPressCoro()
    {
        indexGreenButtonList = 0;
        while (indexGreenButtonList < greenButtons.Count - 5)
        {
            greenButtons[indexGreenButtonList].SetActive(true);
            yield return new WaitForSeconds(0.2f);
            greenButtons[indexGreenButtonList].SetActive(false);
            indexGreenButtonList++;
        }
        greenButtons[indexGreenButtonList + 4].SetActive(true);
        greenButtons[indexGreenButtonList + 3].SetActive(true);
        greenButtons[indexGreenButtonList + 2].SetActive(true);
        greenButtons[indexGreenButtonList + 1].SetActive(true);
        yield return new WaitForSeconds(0.2f);
        greenButtons[indexGreenButtonList + 4].SetActive(false);
        greenButtons[indexGreenButtonList + 3].SetActive(false);
        greenButtons[indexGreenButtonList + 2].SetActive(false);
        greenButtons[indexGreenButtonList + 1].SetActive(false);
    }
    public void PressButtonAndGreenLight(GameObject light)
    {
        StartCoroutine(LightCoro(light));
    }
    private IEnumerator LightCoro(GameObject gameObject)
    {
        gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(0.05f);
        gameObject.SetActive(false);
    }
}

public enum typeButtonSPSV
{
    T_RA,
    TA,
    √Œ“,
    TEST,
    ACS,
    AS,
}
public enum typeInputDisp1
{
    kod,
    fid,
    empty,
}
public enum typeInputDisp3
{
    def,
    r1,
    r2,
    r3,
    r4,
}

