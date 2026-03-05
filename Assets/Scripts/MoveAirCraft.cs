using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAirCraft : MonoBehaviour
{
    private float speed;
    [SerializeField] private bool ally;
    private Coroutine stopLowCoro;
    private Coroutine stopHighCoro;

    private void Update()
    {
        if (ally) transform.Translate(Vector3.left * speed * Time.deltaTime);
        else transform.Translate(Vector3.right * speed * Time.deltaTime);        
    }
    public void UPSpeed(float val)
    {
        speed += val;
    }
    public void SetSpeed(float speed)
    {
        NormalMove(speed);
    }
    public void MoveStoppingLow()
    {
        if (stopLowCoro != null) StopCoroutine(stopLowCoro);
        stopLowCoro = StartCoroutine(StopLowCoro());
    }
    private IEnumerator StopLowCoro()
    {
        while (true)
        {
            speed -= 0.005f;
            if (speed <= 0.15f)
            {
                speed = 0.15f;
                break;
            }
            yield return new WaitForSeconds(1f);
        }
    }
    public void MoveStoppingHight()
    {
        if (stopHighCoro != null) StopCoroutine(stopHighCoro);
        if (stopLowCoro != null) StopCoroutine(stopLowCoro);
        stopHighCoro = StartCoroutine(StopHightCoro());
    }
    private IEnumerator StopHightCoro()
    {
        while (true)
        {
            speed -= 0.015f;
            if (speed <= 0)
            {
                speed = 0;
                break;
            }
            yield return new WaitForSeconds(1f);
        }
    }
    public void NormalMove(float speed)
    {
        if (stopHighCoro != null) StopCoroutine(stopHighCoro);
        if (stopLowCoro != null) StopCoroutine(stopLowCoro);
        this.speed = speed;
    }
}
