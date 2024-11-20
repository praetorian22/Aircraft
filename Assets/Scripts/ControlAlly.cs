using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ControlAlly : MonoBehaviour
{
    private float expansionSpeed;
    private float expansionSpeedDelta;

    private RotateAirCraft rotateAirCraft;
    private RotateArrow rotateArrow;
    private SoundManager soundManager;
    private ColorSector colorSector;
    private MoveAirCraft moveAirCraft;
    private RotateAirCraft enemyRotateAirCraft;

    private figure nowFigure;
    private float time;
    private Coroutine timeCoro;
    private bool notDangerous;
    private bool targetUp;
    private bool stopMovementChange;
    private float distanceAir;

    private float vertDist;

    [SerializeField] private float minDistanceClimb_Descent_Now;
    [SerializeField] private float maxDistanceClimb_Descent_Now;
    [SerializeField] private float minDistanceClimb_Descent_Crossing;
    [SerializeField] private float maxDistanceClimb_Descent_Crossing;
    [SerializeField] private float minDistanceStopControll;

    [SerializeField] private float upSpeedForStopControll;
    [SerializeField] private float upRotationForStopControll;
    [SerializeField] private float defaultSpeed;
    [SerializeField] private float defaultRotation;

    [SerializeField] private float safeDistanceVertical;
    [SerializeField] private float dangerDistanceVertical;

    [SerializeField] private float dangerExpansionSpeed;

    [SerializeField] private int crossingAngleChange;
    [SerializeField] private int criticalAngleClimb;
    [SerializeField] private int criticalAngleDescent;

    [SerializeField] private float timeReactionAlly;

    [SerializeField] private float maxTime_timeEmptyDiamond;
    [SerializeField] private float timeChangeDiamond;
    [SerializeField] private float timeChangeCircle;
    [SerializeField] private float timeChangeSquare;

    [SerializeField] private float minDefaultVertical;
    [SerializeField] private float maxDefaultVertical;

    [SerializeField] private Animator animator;
    [SerializeField] private Image figureObject;
    [SerializeField] private List<Sprite> figures = new List<Sprite>();

    [SerializeField] private GameObject enemyAirCraft;
    
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
        time = maxTime_timeEmptyDiamond;
        expansionSpeed = 0f;
        stopMovementChange = false;
        notDangerous = false;
        rotateAirCraft.speedRotation = defaultRotation;
        moveAirCraft.speed = defaultSpeed;
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
            yield return new WaitForSeconds(timeReactionAlly);
            time -= timeReactionAlly;
            if (maxTime_timeEmptyDiamond < time && time > timeChangeDiamond)
            {
                nowFigure = figure.emptyDiamond;                
            }
            if (timeChangeDiamond <= time && time > timeChangeCircle && nowFigure != figure.whiteDiamond)
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
            if (timeChangeCircle <= time && time > timeChangeSquare)
            {
                nowFigure = figure.yelowCircle;
            }
            if (timeChangeSquare <= time && !notDangerous)
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
                if (gameObject.transform.position.y > minDefaultVertical && gameObject.transform.position.y < maxDefaultVertical)
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
                if (vertDist >= safeDistanceVertical && rotateAirCraft.index != 6 && rotateAirCraft.AngleLimit)
                {
                    SetTargetZero();
                    soundManager.SoundVertical_Speed();
                    colorSector.SetMonitorVerticalSpeed(!targetUp);
                }

                float expansionSpeedNow = vertDist;
                expansionSpeedDelta = expansionSpeed - expansionSpeedNow;
                expansionSpeed = expansionSpeedNow;

                distanceAir = Mathf.Abs(enemyAirCraft.transform.position.x) + Mathf.Abs(gameObject.transform.position.x);
                if (expansionSpeedDelta >= dangerExpansionSpeed && rotateAirCraft.AngleLimit && vertDist < dangerDistanceVertical)
                {
                    SetTarget(targetUp);
                    if (!stopMovementChange)
                    {
                        if (minDistanceClimb_Descent_Now > distanceAir && distanceAir < maxDistanceClimb_Descent_Now)
                        {
                            Climb_Descent_Now();
                            stopMovementChange = true;
                            rotateAirCraft.speedRotation += upRotationForStopControll;
                        }
                        else
                        {
                            if (minDistanceClimb_Descent_Crossing <= distanceAir && distanceAir < maxDistanceClimb_Descent_Crossing)
                            {
                                SetTargetCrossing();
                                rotateAirCraft.speedRotation += upRotationForStopControll;
                                stopMovementChange = true;
                            }
                            else
                            {
                                if (distanceAir <= minDistanceStopControll)
                                {
                                    moveAirCraft.speed += upSpeedForStopControll;
                                    stopMovementChange = true;
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

    public void SetTarget(bool up)
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
    }
    public void SetTargetCrossing()
    {
        if (!targetUp)
        {
            targetUp = true;
            rotateAirCraft.SetAngle(rotateAirCraft.angleTarget + crossingAngleChange);
            soundManager.SoundCrossing_Descent();
            colorSector.DescentDescent_IncreaseDescent_CrossingDescent(rotateAirCraft.angleTarget);
        }
        else
        {
            targetUp = false;
            rotateAirCraft.SetAngle(rotateAirCraft.angleTarget - crossingAngleChange);
            soundManager.SoundCrossing_Climb();
            colorSector.ClimbClimb_IncreaseClimb_CrossingClimb(rotateAirCraft.angleTarget);
        }
    }
    public void Climb_Descent_Now()
    {
        if (targetUp)
        {
            targetUp = false;
            rotateAirCraft.SetAngle(criticalAngleClimb);
            soundManager.SoundClimb_Climp_Now();
            colorSector.Climb_ClimbNow();
        }
        else
        {
            targetUp = true;
            rotateAirCraft.SetAngle(criticalAngleDescent);
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