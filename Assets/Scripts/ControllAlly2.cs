using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllAlly2 : MonoBehaviour
{
    public GameObject ghostAlly;
    public GameObject ghostEnemy;
    public GameObject ally;
    public GameObject enemy;
    public ContorllCollision contorllCollision;

    public float expansionSpeed;
    public float expansionSpeedDelta;
    public RotateAirCraft rotateAirCraft;
    public GameObject enemyAirCraft;
    public RotateAirCraft enemyRotateAirCraft;

    public figure nowFigure;
    public float time;
    public Coroutine timeCoro;
    public bool notDangerous;
    public bool targetUp;    
    public float distanceAir;

    public bool changeTargetFlag;
    public Animator animator;

    private void OnEnable()
    {
        contorllCollision.collisionDetect += CollisionDetect;
    }

    private void OnDisable()
    {
        contorllCollision.collisionDetect -= CollisionDetect;
    }

    public void StartGhost()
    {
        ghostAlly.transform.position = ally.transform.position;
        ghostEnemy.transform.position = enemy.transform.position;
        ghostAlly.transform.rotation = ally.transform.rotation;
        ghostEnemy.transform.rotation = enemy.transform.rotation;
    }

    public void CollisionDetect()
    {
        ghostAlly.transform.position = new Vector3(100f, 100f, 0f);
        ghostEnemy.transform.position = new Vector3(-100f, -100f, 0f);
        

        if (distanceAir < 6 && !changeTargetFlag)
        {
            ChangeTarget();
            changeTargetFlag = true;
        }
        SetTargetUp(targetUp);
        //if (!SetTargetUp(targetUp)) ChangeTarget();
    }

    public void NewSimulation()
    {
        nowFigure = figure.emptyDiamond;
        time = 30f;
        expansionSpeed = 0f;
        animator.SetTrigger("Start");
    }

    private void Start()
    {
        enemyRotateAirCraft = enemyAirCraft.GetComponent<RotateAirCraft>();
        NewSimulation();
        if (timeCoro != null) StopCoroutine(timeCoro);
        timeCoro = StartCoroutine(TimeCoroutine());
    }

    public IEnumerator TimeCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            time -= 0.1f;
            if (time < 30 && time > 25) nowFigure = figure.emptyDiamond;
            if (time <= 25 && time > 20 && nowFigure != figure.whiteDiamond)
            {
                nowFigure = figure.whiteDiamond; 
            }
            if (time <= 20 && time > 15) nowFigure = figure.yelowCircle;
            if (time <= 15 && !notDangerous) nowFigure = figure.redSquare;
            if (notDangerous)
            {
                animator.SetTrigger("Stop");
                nowFigure = figure.emptyDiamond;
            }
            distanceAir = Mathf.Abs(enemyAirCraft.transform.position.x) + Mathf.Abs(gameObject.transform.position.x);

            if ((int)nowFigure > 0)
            {
                StartGhost();
            }
        }
    }

    public void StopTarget()
    {
        rotateAirCraft.angleTarget = rotateAirCraft.angleNow;
    }

    public bool SetTargetUp(bool up)
    {
        if (up)
        {
            targetUp = true;
            return rotateAirCraft.NextAngleOne(0.5f);
        }
        else
        {
            targetUp = false;
            return rotateAirCraft.PrewAngleOne(0.5f);
        }
    }
    public void ChangeTarget()
    {
        if (targetUp) SetTargetUp(false);
        else SetTargetUp(true);
    }
}
