using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ContorllCollision : MonoBehaviour
{
    public Action collisionDetect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collisionDetect?.Invoke();
    } 
}
