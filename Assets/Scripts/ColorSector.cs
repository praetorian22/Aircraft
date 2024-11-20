using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorSector : MonoBehaviour
{
    public Color32 colorRed;
    public Color32 colorGreen;
    public Color32 colorEmpty;

    public List<Image> sectors_0_2_5up = new List<Image>();
    public List<Image> sectors_0_2_5down = new List<Image>();
    public List<Image> sectors_2_5_5up = new List<Image>();
    public List<Image> sectors_2_5_5down = new List<Image>();
    public List<Image> sectors_5_10up = new List<Image>();
    public List<Image> sectors_5_10down = new List<Image>();
    public List<Image> sectors_10_15up = new List<Image>();
    public List<Image> sectors_10_15down = new List<Image>();
    public List<Image> sectors_15_20up = new List<Image>();
    public List<Image> sectors_15_20down = new List<Image>();
    public List<Image> sectors_20_25up = new List<Image>();
    public List<Image> sectors_20_25down = new List<Image>();
    public List<Image> sectors_25_30up = new List<Image>();
    public List<Image> sectors_25_30down = new List<Image>();

    public void SetMonitorVerticalSpeed(bool up)
    {
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
}
