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
    [SerializeField] private GameObject enemyAirCraft;
    
    private void Awake()
    {
        moveAirCraft = GetComponent<MoveAirCraft>();
        rotateAirCraft = GetComponent<RotateAirCraft>();
        rotateAirCraft.changeAngleEvent += UIManager.Instance.TranslateAngleArrow;
        enemyRotateAirCraft = enemyAirCraft.GetComponent<RotateAirCraft>();               
    }

    

    public void NewSimulation()
    {
        nowFigure = figure.emptyDiamond;
        UIManager.Instance.ChangeFigura(nowFigure);
        time = maxTime_timeEmptyDiamond;
        expansionSpeed = 0f;
        stopMovementChange = false;
        notDangerous = false;
        rotateAirCraft.NewSimulation(defaultRotation);        
        moveAirCraft.NewSimulation(defaultSpeed);
        SoundManager.Instance.NewSimulation();
        UIManager.Instance.NewSimulation();
        if (timeCoro != null) StopCoroutine(timeCoro);
        timeCoro = StartCoroutine(TimeCoroutine());
        animator.SetTrigger("Start");        
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
                UIManager.Instance.ChangeFigura(nowFigure);
            }
            if (timeChangeDiamond >= time && time > timeChangeCircle && nowFigure != figure.whiteDiamond)
            {
                nowFigure = figure.whiteDiamond;
                UIManager.Instance.ChangeFigura(nowFigure);
                if (enemyRotateAirCraft.MoveForward)
                {
                    int random = Random.Range(0, 2);
                    if (random == 0)
                    {
                        SetTarget(true);
                        SoundManager.Instance.SoundDescent_Descent();
                        UIManager.Instance.DescentDescent_IncreaseDescent_CrossingDescent(rotateAirCraft.AngleTaret);
                    }                       
                    else
                    {
                        SetTarget(false);
                        SoundManager.Instance.SoundClimb_Climb();
                        UIManager.Instance.ClimbClimb_IncreaseClimb_CrossingClimb(rotateAirCraft.AngleTaret);
                    }                        
                    
                }
                else
                {
                    if (enemyRotateAirCraft.MoveDown)
                    {
                        SetTarget(false);
                        SoundManager.Instance.SoundClimb_Climb();
                        UIManager.Instance.ClimbClimb_IncreaseClimb_CrossingClimb(rotateAirCraft.AngleTaret);
                    }
                    else
                    {
                        if (enemyRotateAirCraft.MoveUp)
                        {
                            SetTarget(true);
                            SoundManager.Instance.SoundDescent_Descent();
                            UIManager.Instance.DescentDescent_IncreaseDescent_CrossingDescent(rotateAirCraft.AngleTaret);
                        }
                    }
                }
                
            }
            if (timeChangeCircle >= time && time > timeChangeSquare)
            {
                nowFigure = figure.yelowCircle;
                UIManager.Instance.ChangeFigura(nowFigure);
            }
            if (timeChangeSquare >= time && !notDangerous)
            {
                nowFigure = figure.redSquare;
                UIManager.Instance.ChangeFigura(nowFigure);
            }
            if (notDangerous && nowFigure != figure.emptyDiamond)
            {
                nowFigure = figure.emptyDiamond;
                UIManager.Instance.ChangeFigura(nowFigure);
                SoundManager.Instance.SoundClear_Of_Conflict();
                UIManager.Instance.TraficTrafic_ClearOfConflict();
            }

            if (enemyAirCraft.transform.position.x > gameObject.transform.position.x)
            {
                notDangerous = true;
                if (minDefaultVertical < gameObject.transform.position.y && gameObject.transform.position.y < maxDefaultVertical)
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
                if (vertDist >= safeDistanceVertical && !rotateAirCraft.Horizont && rotateAirCraft.AngleLimit)
                {
                    SetTargetZero();
                    SoundManager.Instance.SoundVertical_Speed();
                    UIManager.Instance.SetMonitorVerticalSpeed(!targetUp);
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
                        if (minDistanceClimb_Descent_Now < distanceAir && distanceAir < maxDistanceClimb_Descent_Now)
                        {
                            Climb_Descent_Now();
                            stopMovementChange = true;
                            rotateAirCraft.UpSpeedRotation(upRotationForStopControll);
                        }
                        else
                        {
                            if (minDistanceClimb_Descent_Crossing <= distanceAir && distanceAir < maxDistanceClimb_Descent_Crossing)
                            {
                                SetTargetCrossing();
                                rotateAirCraft.UpSpeedRotation(upRotationForStopControll);
                                stopMovementChange = true;
                            }
                            else
                            {
                                if (distanceAir <= minDistanceStopControll)
                                {
                                    moveAirCraft.UPSpeed(upSpeedForStopControll);
                                    stopMovementChange = true;
                                }
                            }
                        }
                    }
                }
                
            }            
        }
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
                SoundManager.Instance.SoundIncrease_Descent();
                UIManager.Instance.DescentDescent_IncreaseDescent_CrossingDescent(rotateAirCraft.AngleTaret);
            }                    
        }
        else
        {
            targetUp = false;
            next = rotateAirCraft.PrewAngle();
            if (next)
            {
                SoundManager.Instance.SoundIncrease_Climp();
                UIManager.Instance.ClimbClimb_IncreaseClimb_CrossingClimb(rotateAirCraft.AngleTaret);
            }                    
        }        
    }
    public void SetTargetCrossing()
    {
        if (!targetUp)
        {
            targetUp = true;
            rotateAirCraft.SetAngle(rotateAirCraft.AngleTaret + crossingAngleChange);
            SoundManager.Instance.SoundCrossing_Descent();
            UIManager.Instance.DescentDescent_IncreaseDescent_CrossingDescent(rotateAirCraft.AngleTaret);
        }
        else
        {
            targetUp = false;
            rotateAirCraft.SetAngle(rotateAirCraft.AngleTaret - crossingAngleChange);
            SoundManager.Instance.SoundCrossing_Climb();
            UIManager.Instance.ClimbClimb_IncreaseClimb_CrossingClimb(rotateAirCraft.AngleTaret);
        }
    }
    public void Climb_Descent_Now()
    {
        if (targetUp)
        {
            targetUp = false;
            rotateAirCraft.SetAngle(criticalAngleClimb);
            SoundManager.Instance.SoundClimb_Climp_Now();
            UIManager.Instance.Climb_ClimbNow();
        }
        else
        {
            targetUp = true;
            rotateAirCraft.SetAngle(criticalAngleDescent);
            SoundManager.Instance.SoundDescent_Descent_Now();
            UIManager.Instance.Descent_DescentNow();
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