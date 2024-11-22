using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

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
    [SerializeField] private List<Image> sectors_5_10up = new List<Image>();
    [SerializeField] private List<Image> sectors_5_10down = new List<Image>();
    [SerializeField] private List<Image> sectors_10_15up = new List<Image>();
    [SerializeField] private List<Image> sectors_10_15down = new List<Image>();
    [SerializeField] private List<Image> sectors_15_20up = new List<Image>();
    [SerializeField] private List<Image> sectors_15_20down = new List<Image>();
    [SerializeField] private List<Image> sectors_20_25up = new List<Image>();
    [SerializeField] private List<Image> sectors_20_25down = new List<Image>();
    [SerializeField] private List<Image> sectors_25_30up = new List<Image>();
    [SerializeField] private List<Image> sectors_25_30down = new List<Image>();

    [SerializeField] private GameObject arrow;
    [SerializeField] private float angleNow;

    [SerializeField] private Image figureObject;
    [SerializeField] private List<Sprite> figures = new List<Sprite>();
    [SerializeField] private Sprite redSquare;
    [SerializeField] private Sprite emptySquare;
    [SerializeField] private Button SPSVOptionButton;
    [SerializeField] private List<Sprite> SPSVButtonImage = new List<Sprite>();
    private Dictionary<typeButtonSPSV, Sprite> SPSVButtonImageDict = new Dictionary<typeButtonSPSV, Sprite>();

    private Dictionary<int, List<Image>> sectorDict = new Dictionary<int, List<Image>>();

    private bool allIndicatorsOff;    
    private figure nowFigure;

    private typeButtonSPSV sPSVNow;

    public Action<typeButtonSPSV> changeTypeSPVSEvent;
    private void Awake()
    {
        SPSVButtonImageDict.Add(typeButtonSPSV.√Œ“, SPSVButtonImage[0]);
        SPSVButtonImageDict.Add(typeButtonSPSV.TA, SPSVButtonImage[1]);
        SPSVButtonImageDict.Add(typeButtonSPSV.T_RA, SPSVButtonImage[2]);
    }

    public void NewSimulation()
    {
        sPSVNow = typeButtonSPSV.T_RA;
        SPSVButtonPressed();        
        TraficTrafic_ClearOfConflict();
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
        if (allIndicatorsOff) return;
        if (up)
        {
            SetSector025upEmty();
            SetSector255upEmty();
            SetSector510upEmty();
            SetSector1015upEmty();
            SetSector1520upEmty();
            SetSector2025upEmty();
            SetSector2530upEmty();

            SetSector025downRed();
            SetSector255downRed();
            SetSector510downRed();
            SetSector1015downRed();
            SetSector1520downRed();
            SetSector2025downRed();
            SetSector2530downRed();
        }
        else
        {
            SetSector025downEmty();
            SetSector255downEmty();
            SetSector510downEmty();
            SetSector1015downEmty();
            SetSector1520downEmty();
            SetSector2025downEmty();
            SetSector2530downEmty();

            SetSector025upRed();
            SetSector255upRed();
            SetSector510upRed();
            SetSector1015upRed();
            SetSector1520upRed();
            SetSector2025upRed();
            SetSector2530upRed();
        }
    }
    public void TraficTrafic_ClearOfConflict()
    {
        if (allIndicatorsOff) return;
        SetSector025upEmty();
        SetSector255upEmty();
        SetSector510upEmty();
        SetSector1015upEmty();
        SetSector1520upEmty();
        SetSector025upEmty();
        SetSector2025upEmty();
        SetSector2530upEmty();
        SetSector025downEmty();
        SetSector255downEmty();
        SetSector510downEmty();
        SetSector1015downEmty();
        SetSector1520downEmty();
        SetSector025downEmty();
        SetSector2025downEmty();
        SetSector2530downEmty();
    }

    public void ClimbClimb_IncreaseClimb_CrossingClimb(float angle)
    {
        if (allIndicatorsOff) return;
        if (angle == 355)
        {
            SetSector025upRed();
            SetSector255upGreen();
            SetSector510upEmty();
            SetSector1015upEmty();
            SetSector1520upEmty();
            SetSector2025upEmty();
            SetSector2530upEmty();

            SetSector025downRed();
            SetSector255downRed();
            SetSector510downRed();
            SetSector1015downRed();
            SetSector1520downRed();
            SetSector2025downRed();
            SetSector2530downRed();
        }
        if (angle == 350)
        {
            SetSector025upRed();
            SetSector255upRed();
            SetSector510upGreen();
            SetSector1015upEmty();
            SetSector1520upEmty();
            SetSector2025upEmty();
            SetSector2530upEmty();

            SetSector025downRed();
            SetSector255downRed();
            SetSector510downRed();
            SetSector1015downRed();
            SetSector1520downRed();
            SetSector2025downRed();
            SetSector2530downRed();
        }
        if (angle == 345)
        {
            SetSector025upRed();
            SetSector255upRed();
            SetSector510upRed();
            SetSector1015upGreen();
            SetSector1520upEmty();
            SetSector2025upEmty();
            SetSector2530upEmty();

            SetSector025downRed();
            SetSector255downRed();
            SetSector510downRed();
            SetSector1015downRed();
            SetSector1520downRed();
            SetSector2025downRed();
            SetSector2530downRed();
        }
        if (angle == 340)
        {
            SetSector025upRed();
            SetSector255upRed();
            SetSector510upRed();
            SetSector1015upRed();
            SetSector1520upGreen();
            SetSector2025upEmty();
            SetSector2530upEmty();

            SetSector025downRed();
            SetSector255downRed();
            SetSector510downRed();
            SetSector1015downRed();
            SetSector1520downRed();
            SetSector2025downRed();
            SetSector2530downRed();
        }
        if (angle == 335)
        {
            SetSector025upRed();
            SetSector255upRed();
            SetSector510upRed();
            SetSector1015upRed();
            SetSector1520upRed();
            SetSector2025upGreen();
            SetSector2530upEmty();

            SetSector025downRed();
            SetSector255downRed();
            SetSector510downRed();
            SetSector1015downRed();
            SetSector1520downRed();
            SetSector2025downRed();
            SetSector2530downRed();
        }
        if (angle == 330)
        {
            SetSector025upRed();
            SetSector255upRed();
            SetSector510upRed();
            SetSector1015upRed();
            SetSector1520upRed();
            SetSector2025upRed();
            SetSector2530upGreen();

            SetSector025downRed();
            SetSector255downRed();
            SetSector510downRed();
            SetSector1015downRed();
            SetSector1520downRed();
            SetSector2025downRed();
            SetSector2530downRed();
        }
    }
    public void DescentDescent_IncreaseDescent_CrossingDescent(float angle)
    {
        if (allIndicatorsOff) return;
        if (angle == 365)
        {
            SetSector025downRed();
            SetSector255downGreen();
            SetSector510downEmty();
            SetSector1015downEmty();
            SetSector1520downEmty();
            SetSector2025downEmty();
            SetSector2530downEmty();

            SetSector025upRed();
            SetSector255upRed();
            SetSector510upRed();
            SetSector1015upRed();
            SetSector1520upRed();
            SetSector2025upRed();
            SetSector2530upRed();
        }
        if (angle == 370)
        {
            SetSector025downRed();
            SetSector255downRed();
            SetSector510downGreen();
            SetSector1015downEmty();
            SetSector1520downEmty();
            SetSector2025downEmty();
            SetSector2530downEmty();

            SetSector025upRed();
            SetSector255upRed();
            SetSector510upRed();
            SetSector1015upRed();
            SetSector1520upRed();
            SetSector2025upRed();
            SetSector2530upRed();
        }
        if (angle == 375)
        {
            SetSector025downRed();
            SetSector255downRed();
            SetSector510downRed();
            SetSector1015downGreen();
            SetSector1520downEmty();
            SetSector2025downEmty();
            SetSector2530downEmty();

            SetSector025upRed();
            SetSector255upRed();
            SetSector510upRed();
            SetSector1015upRed();
            SetSector1520upRed();
            SetSector2025upRed();
            SetSector2530upRed();
        }
        if (angle == 380)
        {
            SetSector025downRed();
            SetSector255downRed();
            SetSector510downRed();
            SetSector1015downRed();
            SetSector1520downGreen();
            SetSector2025downEmty();
            SetSector2530downEmty();

            SetSector025upRed();
            SetSector255upRed();
            SetSector510upRed();
            SetSector1015upRed();
            SetSector1520upRed();
            SetSector2025upRed();
            SetSector2530upRed();
        }
        if (angle == 385)
        {
            SetSector025downRed();
            SetSector255downRed();
            SetSector510downRed();
            SetSector1015downRed();
            SetSector1520downRed();
            SetSector2025downGreen();
            SetSector2530downEmty();

            SetSector025upRed();
            SetSector255upRed();
            SetSector510upRed();
            SetSector1015upRed();
            SetSector1520upRed();
            SetSector2025upRed();
            SetSector2530upRed();
        }
        if (angle == 390)
        {
            SetSector025downRed();
            SetSector255downRed();
            SetSector510downRed();
            SetSector1015downRed();
            SetSector1520downRed();
            SetSector2025downRed();
            SetSector2530downGreen();

            SetSector025upRed();
            SetSector255upRed();
            SetSector510upRed();
            SetSector1015upRed();
            SetSector1520upRed();
            SetSector2025upRed();
            SetSector2530upRed();
        }
    }

    public void Climb_ClimbNow()
    {
        if (allIndicatorsOff) return;
        SetSector025upRed();
        SetSector255upRed();
        SetSector510upRed();
        SetSector1015upRed();
        SetSector1520upRed();
        SetSector2025upRed();
        SetSector2530upGreen();

        SetSector025downRed();
        SetSector255downRed();
        SetSector510downRed();
        SetSector1015downRed();
        SetSector1520downRed();
        SetSector2025downRed();
        SetSector2530downRed();
    }
    public void Descent_DescentNow()
    {
        if (allIndicatorsOff) return;
        SetSector025downRed();
        SetSector255downRed();
        SetSector510downRed();
        SetSector1015downRed();
        SetSector1520downRed();
        SetSector2025downRed();
        SetSector2530downGreen();

        SetSector025upRed();
        SetSector255upRed();
        SetSector510downRed();
        SetSector1015upRed();
        SetSector1520upRed();
        SetSector2025upRed();
        SetSector2530upRed();
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

    private void SetSector510upRed()
    {
        SetSectorsColor(sectors_5_10up, colorRed);
    }
    private void SetSector510upGreen()
    {
        SetSectorsColor(sectors_5_10up, colorGreen);
    }
    private void SetSector510upEmty()
    {
        SetSectorsColor(sectors_5_10up, colorEmpty);
    }
    private void SetSector510downRed()
    {
        SetSectorsColor(sectors_5_10down, colorRed);
    }
    private void SetSector510downGreen()
    {
        SetSectorsColor(sectors_5_10down, colorGreen);
    }
    private void SetSector510downEmty()
    {
        SetSectorsColor(sectors_5_10down, colorEmpty);
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

    private void SetSector2025upRed()
    {
        SetSectorsColor(sectors_20_25up, colorRed);
    }
    private void SetSector2025upGreen()
    {
        SetSectorsColor(sectors_20_25up, colorGreen);
    }
    private void SetSector2025upEmty()
    {
        SetSectorsColor(sectors_20_25up, colorEmpty);
    }
    private void SetSector2025downRed()
    {
        SetSectorsColor(sectors_20_25down, colorRed);
    }
    private void SetSector2025downGreen()
    {
        SetSectorsColor(sectors_20_25down, colorGreen);
    }
    private void SetSector2025downEmty()
    {
        SetSectorsColor(sectors_20_25down, colorEmpty);
    }

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
        allIndicatorsOff = true;
        SetSector025upEmty();
        SetSector255upEmty();
        SetSector510upEmty();
        SetSector1015upEmty();
        SetSector1520upEmty();
        SetSector025upEmty();
        SetSector2025upEmty();
        SetSector2530upEmty();
        SetSector025downEmty();
        SetSector255downEmty();
        SetSector510downEmty();
        SetSector1015downEmty();
        SetSector1520downEmty();
        SetSector025downEmty();
        SetSector2025downEmty();
        SetSector2530downEmty();

    }
    public void AllIndicatorsOn()
    {
        allIndicatorsOff = false;
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
    public void SPSVButtonPressed()
    {
        NextTypeSPSV();
        SPSVOptionButton.gameObject.GetComponent<Image>().sprite = SPSVButtonImageDict[sPSVNow];
        changeTypeSPVSEvent?.Invoke(sPSVNow);
    }
    private void NextTypeSPSV()
    {
        if (sPSVNow == typeButtonSPSV.√Œ“)
        {
            sPSVNow = typeButtonSPSV.TA;
        }
        else
        {
            if (sPSVNow == typeButtonSPSV.TA)
            {
                sPSVNow = typeButtonSPSV.T_RA;
            }
            else
            {
                if (sPSVNow == typeButtonSPSV.T_RA)
                {
                    sPSVNow = typeButtonSPSV.√Œ“;
                }
            }
        }
    }    
}

public enum typeButtonSPSV
{
    T_RA,
    TA,
    √Œ“,
    TEST,
}

