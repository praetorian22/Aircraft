using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glissada : MonoBehaviour
{
    [SerializeField] SoundManager soundManager;
    [SerializeField] UIManager uiManager;
    private bool inGlissada;
    private Coroutine coroutine;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        inGlissada = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        inGlissada = false;
    }
    private void OnDisable()
    {
        if (coroutine != null) StopCoroutine(coroutine);
    }
    public void ResetSimulation()
    {
        if (coroutine != null) StopCoroutine(coroutine);
        coroutine = StartCoroutine(GlissCheCkCoro());
    }
    private IEnumerator GlissCheCkCoro()
    {
        while(true)
        {
            yield return new WaitForSeconds(1.5f);
            if (!inGlissada)
            {
                soundManager.ClideSlope();
            }
        }
    }
}
