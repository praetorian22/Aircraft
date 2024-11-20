using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ControlAlly : MonoBehaviour
{
    public float expansionSpeed;
    public float expansionSpeedDelta;
    public RotateAirCraft rotateAirCraft;
    public GameObject enemyAirCraft;
    public RotateAirCraft enemyRotateAirCraft;
    public RotateArrow rotateArrow;
    public SoundManager soundManager;
    public ColorSector colorSector;
    public MoveAirCraft moveAirCraft;

    public figure nowFigure;
    public float time;
    public Coroutine timeCoro;
    public bool notDangerous;
    public bool targetUp;
    public bool stopMovementChange;
    public float distanceAir;

    public Animator animator;
    public Image figureObject;
    public List<Sprite> figures = new List<Sprite>();

    public float vertDist;
    private void Start()
    {
        rotateArrow = GetComponent<RotateArrow>();
        soundManager = GetComponent<SoundManager>();
        colorSector = GetComponent<ColorSector>();
        moveAirCraft = GetComponent<MoveAirCraft>();
        rotateAirCraft.changeAngleEvent += rotateArrow.TranslateAngle;
        enemyRotateAirCraft = enemyAirCraft.GetComponent<RotateAirCraft>();
        NewSimulation();        
    }

    

    public void NewSimulation()
    {
        nowFigure = figure.emptyDiamond;
        figureObject.sprite = figures[(int)nowFigure];
        time = 30f;
        expansionSpeed = 0f;
        stopMovementChange = false;
        notDangerous = false;
        rotateAirCraft.speedRotation = 3;
        if (timeCoro != null) StopCoroutine(timeCoro);
        timeCoro = StartCoroutine(TimeCoroutine());
        animator.SetTrigger("Start");
        soundManager.NewSimulation();
        soundManager.SoundTraffic_Traffic();
        colorSector.TraficTrafic_ClearOfConflict();
    }

    public IEnumerator TimeCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            time -= 0.01f;
            if (time < 30 && time > 25)
            {
                nowFigure = figure.emptyDiamond;                
            }
            if (time <= 25 && time > 20 && nowFigure != figure.whiteDiamond)
            {
                nowFigure = figure.whiteDiamond;
                if (enemyRotateAirCraft.MoveForward)
                {
                    int random = Random.Range(0, 2);
                    if (random == 0)
                    {
                        SetTarget(true);                                         
                        soundManager.SoundDescent_Descent();
                        colorSector.DescentDescent_IncreaseDescent_CrossingDescent(rotateAirCraft.angleTarget);
                    }                       
                    else
                    {
                        SetTarget(false);                        
                        soundManager.SoundClimb_Climb();
                        colorSector.ClimbClimb_IncreaseClimb_CrossingClimb(rotateAirCraft.angleTarget);
                    }                        
                    
                }
                else
                {
                    if (enemyRotateAirCraft.MoveDown)
                    {
                        SetTarget(false);
                        soundManager.SoundClimb_Climb();
                        colorSector.ClimbClimb_IncreaseClimb_CrossingClimb(rotateAirCraft.angleTarget);
                    }
                    else
                    {
                        if (enemyRotateAirCraft.MoveUp)
                        {
                            SetTarget(true);
                            soundManager.SoundDescent_Descent();
                            colorSector.DescentDescent_IncreaseDescent_CrossingDescent(rotateAirCraft.angleTarget);
                        }
                    }
                }
                
            }
            if (time <= 20 && time > 15)
            {
                nowFigure = figure.yelowCircle;
            }
            if (time <= 15 && !notDangerous)
            {
                nowFigure = figure.redSquare;
            }
            if (notDangerous && nowFigure != figure.emptyDiamond)
            {
                nowFigure = figure.emptyDiamond;
                soundManager.SoundClear_Of_Conflict();
                colorSector.TraficTrafic_ClearOfConflict();
            }

            if (enemyAirCraft.transform.position.x > gameObject.transform.position.x)
            {
                notDangerous = true;
                if (gameObject.transform.position.y > -1.5f && gameObject.transform.position.y < -0.5f)
                {
                    SetTargetZero();
                }
                else
                {
                    if (!targetUp)
                    {
                        rotateAirCraft.NextAngle();
                    }
                    else
                    {
                        rotateAirCraft.PrewAngle();
                    }
                }                
            }

            if ((int)nowFigure > 0)
            {
                

                List<float> dist = new List<float>();
                dist.Add(Mathf.Abs(rotateAirCraft.vertPositionB - enemyRotateAirCraft.vertPositionB));
                dist.Add(Mathf.Abs(rotateAirCraft.vertPositionF - enemyRotateAirCraft.vertPositionB));
                dist.Add(Mathf.Abs(rotateAirCraft.vertPositionB - enemyRotateAirCraft.vertPositionF));
                dist.Add(Mathf.Abs(rotateAirCraft.vertPositionF - enemyRotateAirCraft.vertPositionF));
                vertDist = dist.Min();
                if (vertDist >= 1f && rotateAirCraft.index != 6 && rotateAirCraft.AngleLimit)
                {
                    SetTargetZero();
                    soundManager.SoundVertical_Speed();
                    colorSector.SetMonitorVerticalSpeed(!targetUp);
                }

                float expansionSpeedNow = vertDist;
                expansionSpeedDelta = expansionSpeed - expansionSpeedNow;
                expansionSpeed = expansionSpeedNow;

                distanceAir = Mathf.Abs(enemyAirCraft.transform.position.x) + Mathf.Abs(gameObject.transform.position.x);
                if (expansionSpeedDelta >= -0.0003f && rotateAirCraft.AngleLimit && vertDist < 0.9f)
                {
                    SetTarget(targetUp);
                    if (!stopMovementChange && expansionSpeedDelta > -0.005f)
                    {
                        if (distanceAir > 5.5f && distanceAir < 6.5f)
                        {
                            Climb_Descent_Now();
                            stopMovementChange = true;
                            rotateAirCraft.speedRotation += 4;
                        }
                        else
                        {
                            if (distanceAir >= 7.5f)
                            {
                                //SetTarget(targetUp);                                
                                //stopMovementChange = true;
                            }
                            else
                            {
                                if (distanceAir >= 6.5f && distanceAir < 7.5f)
                                {
                                    SetTargetCrossing();
                                    rotateAirCraft.speedRotation += 4;
                                    stopMovementChange = true;
                                }
                                else
                                {
                                    if (distanceAir <= 4.5f)
                                    {
                                        moveAirCraft.speed += 0.1f;
                                        stopMovementChange = true;
                                    }
                                }                                
                            }
                        }
                    }
                }
                
            }
            figureObject.sprite = figures[(int)nowFigure];
        }
    }

    public void StopTarget()
    {
        rotateAirCraft.angleTarget = rotateAirCraft.angleNow;
    }

    public void SetTargetZero()
    {
        rotateAirCraft.AngleZero();
    }

    public bool SetTarget(bool up)
    {
        bool next;
        if (up)
        {
            targetUp = true;
            next = rotateAirCraft.NextAngle();
            if (next)
            {
                soundManager.SoundIncrease_Descent();
                colorSector.DescentDescent_IncreaseDescent_CrossingDescent(rotateAirCraft.angleTarget);
            }                    
        }
        else
        {
            targetUp = false;
            next = rotateAirCraft.PrewAngle();
            if (next)
            {
                soundManager.SoundIncrease_Climp();
                colorSector.ClimbClimb_IncreaseClimb_CrossingClimb(rotateAirCraft.angleTarget);
            }                    
        }
        return next;
    }
    public void SetTargetCrossing()
    {
        if (!targetUp)
        {
            targetUp = true;
            rotateAirCraft.SetAngle(rotateAirCraft.angleTarget + 30);
            soundManager.SoundCrossing_Descent();
            colorSector.DescentDescent_IncreaseDescent_CrossingDescent(rotateAirCraft.angleTarget);
        }
        else
        {
            targetUp = false;
            rotateAirCraft.SetAngle(rotateAirCraft.angleTarget - 30);
            soundManager.SoundCrossing_Climb();
            colorSector.ClimbClimb_IncreaseClimb_CrossingClimb(rotateAirCraft.angleTarget);
        }
    }
    public void Climb_Descent_Now()
    {
        if (targetUp)
        {
            targetUp = false;
            rotateAirCraft.SetAngle(330);
            soundManager.SoundClimb_Climp_Now();
            colorSector.Climb_ClimbNow();
        }
        else
        {
            targetUp = true;
            rotateAirCraft.SetAngle(390);
            soundManager.SoundDescent_Descent_Now();
            colorSector.Descent_DescentNow();
        }
    }
    public void Climb_Descent_Crossing()
    {
        if (targetUp)
        {
            SetTarget(false);
            soundManager.SoundClimb_Climp_Now();
            colorSector.Climb_ClimbNow();
        }
        else
        {
            SetTarget(true);
            soundManager.SoundDescent_Descent_Now();
            colorSector.Descent_DescentNow();
        }
    }

}

public enum figure
{
    emptyDiamond,
    whiteDiamond,
    yelowCircle,
    redSquare,
}