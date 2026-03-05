using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllAllyGliss : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    private RotateAirCraft rotateAirCraft;
    private MoveAirCraft moveAirCraft;
    [SerializeField] private float defaultRotation;
    [SerializeField] private float defaultSpeed;
    [SerializeField] private Explosion explosion;
    [SerializeField] private GameObject fPoint;
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private GameObject pointStartPosition;
    [SerializeField] private Glissada glissada;
    private Coroutine inAirPortCoro;

    public Action inAirPortEvent;
    private void Awake()
    {
        rotateAirCraft = GetComponent<RotateAirCraft>();
        moveAirCraft = GetComponent<MoveAirCraft>();
        rotateAirCraft = GetComponent<RotateAirCraft>();
        rotateAirCraft.changeAngleEvent += uiManager.TranslateAngleArrow3;
    }
    private void OnEnable()
    {
        soundManager.SetAudioSource(gameObject.GetComponent<AudioSource>());
        inAirPortEvent += InAirPort;
    }
    private void OnDisable()
    {
        inAirPortEvent -= InAirPort;
    }
    private void Update()
    {
        uiManager.SetHight(((fPoint.transform.position.y + 2.61f) * 500 / 3.1f) + 300);
        if (uiManager.Hight1 <= 0)
        {
            uiManager.SetHight(0);
        }
        float volume = (float)(1 - (uiManager.Hight1 * 0.0016));
        if (volume < 0.3f) volume = 0.3f;
        if (volume > 1f) volume = 1f;
        soundManager.SetVolume(volume);        
    }
    private void SetStartPosition(float value)
    {
        gameObject.transform.position = pointStartPosition.transform.position;        
    }
    private void InAirPort()
    {
        if (inAirPortCoro != null) StopCoroutine(inAirPortCoro);
        inAirPortCoro = StartCoroutine(InAirPortCoro());        
    }
    private IEnumerator InAirPortCoro()
    {
        while (true)
        {
            rotateAirCraft.InAirPort(defaultRotation, 6);            
            yield return null;            
        }
    }
    public void NewGlissSimulation()
    {
        uiManager.NewGlissSimulation();
        glissada.ResetSimulation();
        if (inAirPortCoro != null) StopCoroutine(inAirPortCoro);
        rotateAirCraft.NewSimulation(defaultRotation, 3);
        moveAirCraft.SetSpeed(0f);        
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
        moveAirCraft.MoveStoppingLow();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject.tag != "Gliss" && collision.gameObject.tag != "AirPort")
        {
            explosion.ExplosionCallGliss();
        }
        if (collision != null && collision.gameObject.tag == "AirPort")
        {
            InAirPort();
            moveAirCraft.MoveStoppingHight();
        }
    }
}
