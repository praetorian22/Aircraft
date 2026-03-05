using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ControlAllyGround : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    private RotateAirCraft rotateAirCraft;
    private MoveAirCraft moveAirCraft;
    private Animator animator;
    private float distToGear;
    [SerializeField] private float defaultRotation;
    [SerializeField] private float defaultSpeed;
    [SerializeField] private Explosion explosion;
    [SerializeField] private GameObject fPoint;
    [SerializeField] private SoundManager soundManager;
    private bool dead;
    private bool shassi;
    private Coroutine timeCoro;
    private bool heightDanger;
    [SerializeField] private float timeReaction;

    public void SetDistToGear(float value)
    {
        distToGear = value;
    }


    private void Awake()
    {
        rotateAirCraft = GetComponent<RotateAirCraft>();
        moveAirCraft = GetComponent<MoveAirCraft>();
        rotateAirCraft = GetComponent<RotateAirCraft>();
        animator = GetComponent<Animator>();
        rotateAirCraft.changeAngleEvent += uiManager.TranslateAngleArrow2;
    }
    private void OnEnable()
    {
        soundManager.SetAudioSource(gameObject.GetComponent<AudioSource>());
        uiManager.changeSliderEvent += SetStartPosition;
        uiManager.shassiEvent += ShassiPress;
    }
    private void OnDisable()
    {
        uiManager.changeSliderEvent -= SetStartPosition;
        uiManager.shassiEvent -= ShassiPress;
    }
    private void Update()
    {
        uiManager.SetHight(((fPoint.transform.position.y + 2.61f) * 500 / 3.0f) + 300);
        if (uiManager.Hight <= 0)
        {
            uiManager.SetHight(0);            
        }
    }
    
    private void SetStartPosition(float value)
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x,
            -2.6f + value * 3.0f, gameObject.transform.position.z);
        //uiManager.SetHight(300 + value * 4200);
        //300 800 500
        //0.6 -2.5 3.1
        // 0 - 0  500 3.1
    }    
    public void NewGroundSimulation()
    {
        dead = false;
        uiManager.NewGroundSimulation();
        rotateAirCraft.NewSimulation(defaultRotation, 6);
        moveAirCraft.SetSpeed(0f);
        if (timeCoro != null)
            StopCoroutine(timeCoro);
        timeCoro = StartCoroutine(TimeCoro());
    }
    private IEnumerator TimeCoro()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeReaction);
            if (((uiManager.Hight < 300 && !shassi) || (uiManager.Hight < 150 && shassi)) && !heightDanger)
            {
                heightDanger = true;
                if (uiManager.Hight < 300 && !shassi) TerrainTerrain();
                if (uiManager.Hight < 150 && shassi) Too_Low_Gear();
                yield return new WaitForSeconds(timeReaction);
            }
            else
            {
                if ((uiManager.Hight >= 150 && shassi) || uiManager.Hight >= 300)
                {
                    {
                        heightDanger = false;
                    }
                }
            }
            if (rotateAirCraft.AngleSlinkRate)
            {
                SlinkRate();
            }
            if (rotateAirCraft.AnglePullUp || heightDanger)
            {                
                if (rotateAirCraft.AnglePullUp) PullUp();
                else
                {
                    if (!shassi) Too_Low_Terrain();
                    else Too_Low_Gear();
                }
            }
            if (distToGear <= 4.5f && distToGear > 3.5f)
            {
                CautionTerrain();
            }
            if (distToGear <= 3.5f)
            {
                TerrainTerrainPullUp();
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
    public void PressUp()
    {
        rotateAirCraft.NextAngle();
    }
    public void PressDown()
    {
        rotateAirCraft.PrewAngle();
    }
    public void PressPlayButton()
    {
        moveAirCraft.SetSpeed(defaultSpeed);
    }

    public void SlinkRate()
    {
        soundManager.SoundSlinkRate();            
    }
    public void PullUp()
    {
        soundManager.SoundPullUp();
    }
    public void TerrainTerrain()
    {
        soundManager.Terrain_Terrain();
    }
    public void Too_Low_Terrain()
    {
        soundManager.Too_Low_Terrain();
    }
    public void Too_Low_Gear()
    {
        soundManager.Too_Low_Gear();
    }
    public void CautionTerrain()
    {
        soundManager.CautionTerrain();
    }
    public void TerrainTerrainPullUp()
    {
        soundManager.TerrainTerrainPullUp();
    }
    public void ShassiPress()
    {
        if (!shassi)
        {
            shassi = true;
            animator.SetTrigger("Son");
        }
        else
        {
            shassi = false;
            animator.SetTrigger("Soff");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        explosion.ExplosionCallGround();
    }
}
