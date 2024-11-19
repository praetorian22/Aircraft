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

    public figure nowFigure;
    public float time;
    public Coroutine timeCoro;
    public bool notDangerous;
    public bool targetUp;
    public bool changeTargetFlag;
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
        changeTargetFlag = false;
        notDangerous = false;
        rotateAirCraft.speedRotation = 3;
        if (timeCoro != null) StopCoroutine(timeCoro);
        timeCoro = StartCoroutine(TimeCoroutine());
        animator.SetTrigger("Start");
        soundManager.NewSimulation();
        soundManager.SoundTraffic_Traffic();
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
                        SetTarget(true);                        
                        soundManager.SoundDescent_Descent();
                        colorSector.DescentDescent_IncreaseDescent(rotateAirCraft.angleTarget);
                    }                       
                    else
                    {
                        SetTarget(false);
                        SetTarget(false);
                        soundManager.SoundClimb_Climb();
                        colorSector.ClimbClimb_IncreaseClimb(rotateAirCraft.angleTarget);
                    }                        
                    
                }
                else
                {
                    if (enemyRotateAirCraft.MoveDown)
                    {
                        SetTarget(false);
                        soundManager.SoundClimb_Climb();
                        colorSector.ClimbClimb_IncreaseClimb(rotateAirCraft.angleTarget);
                    }
                    else
                    {
                        if (enemyRotateAirCraft.MoveUp)
                        {
                            SetTarget(true);
                            soundManager.SoundDescent_Descent();
                            colorSector.DescentDescent_IncreaseDescent(rotateAirCraft.angleTarget);
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
            if (notDangerous)
            {
                nowFigure = figure.emptyDiamond;                
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
                float expansionSpeedNow = Mathf.Abs(enemyAirCraft.transform.position.y - transform.position.y);
                expansionSpeedDelta = expansionSpeed - expansionSpeedNow;
                expansionSpeed = expansionSpeedNow;

                List<float> dist = new List<float>();
                dist.Add(Mathf.Abs(rotateAirCraft.vertPositionB - enemyRotateAirCraft.vertPositionB));
                dist.Add(Mathf.Abs(rotateAirCraft.vertPositionF - enemyRotateAirCraft.vertPositionB));
                dist.Add(Mathf.Abs(rotateAirCraft.vertPositionB - enemyRotateAirCraft.vertPositionF));
                dist.Add(Mathf.Abs(rotateAirCraft.vertPositionF - enemyRotateAirCraft.vertPositionF));
                vertDist = dist.Min();
                if (vertDist > 1f && rotateAirCraft.index != 6)
                {
                    SetTargetZero();
                    soundManager.SoundVertical_Speed();
                    colorSector.SetMonitorVerticalSpeed(targetUp);
                }
                distanceAir = Mathf.Abs(enemyAirCraft.transform.position.x) + Mathf.Abs(gameObject.transform.position.x);
                /*
                if (vertDist < 1f && rotateAirCraft.AngleLimit && expansionSpeedDelta >= 0)
                {
                    if (distanceAir > 4f)
                    {
                        if (rotateAirCraft.angleNow <= enemyRotateAirCraft.angleNow)
                        {
                            //if (targetUp)
                            //{
                                if (enemyRotateAirCraft.angleNow - rotateAirCraft.angleNow > 5)
                                {
                                    rotateAirCraft.PrewAngle();
                                }
                                else
                                {
                                    rotateAirCraft.NextAngle();
                                }
                            //}                            
                        }
                        else
                        {
                            if (rotateAirCraft.angleNow >= enemyRotateAirCraft.angleNow)
                            {
                                //if (!targetUp)
                                //{
                                    if (rotateAirCraft.angleNow - enemyRotateAirCraft.angleNow > 5)
                                    {
                                        rotateAirCraft.NextAngle();                                        
                                    }
                                    else
                                    {
                                        rotateAirCraft.PrewAngle();
                                    }
                                //}
                            }                            
                        }

                    }
                }
                */
                if (expansionSpeedDelta >= -0.0005f && rotateAirCraft.AngleLimit)
                {
                    if (!SetTarget(targetUp) && !changeTargetFlag && distanceAir > 4f && expansionSpeedDelta > -0.005f)
                    {
                        ChangeTarget();
                        changeTargetFlag = true;
                        rotateAirCraft.speedRotation += 4;
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
            if (next && rotateAirCraft.angleTarget > 370 && !changeTargetFlag)
            {
                soundManager.SoundIncrease_Descent();
                colorSector.DescentDescent_IncreaseDescent(rotateAirCraft.angleTarget);
            }
        }
        else
        {
            targetUp = false;
            next = rotateAirCraft.PrewAngle();
            if (next && rotateAirCraft.angleTarget < 350 && !changeTargetFlag)
            {
                soundManager.SoundIncrease_Climp();
                colorSector.ClimbClimb_IncreaseClimb(rotateAirCraft.angleTarget);
            }
        }
        return next;
    }
    public void ChangeTarget()
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