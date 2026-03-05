using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DistanceToGear : MonoBehaviour
{
    private RaycastHit2D hit;
    private ControlAllyGround host;
    private void Awake()
    {
        host = gameObject.transform.parent.gameObject.GetComponent<ControlAllyGround>();
    }
    private void FixedUpdate()
    {
        hit = Physics2D.Raycast(transform.position, host.gameObject.transform.right);

        if (hit.collider != null && hit.collider.tag == "Gora")
        {
            host.SetDistToGear(Vector2.Distance(transform.position, hit.point));
        }
        else
        {
            host.SetDistToGear(10000f);
        }
    }
}
