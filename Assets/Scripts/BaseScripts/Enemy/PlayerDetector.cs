using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{

    public bool playerIsInRange;

    private void OnTriggerStay2D(Collider2D collision)
    {
        print(collision.tag);
        if (collision.tag == "Player" || collision.tag == "PlayerHealth")
        {
            playerIsInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "PlayerHealth")
        {
            playerIsInRange = false;
        }
    }
}
