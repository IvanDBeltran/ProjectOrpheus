using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierDetector : MonoBehaviour
{

    public bool isTouchingBarrier;
    public GameObject barrierBeingTouched;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Barrier")
        {
            isTouchingBarrier = true;
            barrierBeingTouched = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Barrier")
        {
            isTouchingBarrier = false;
            barrierBeingTouched = null;
        }
    }
}
