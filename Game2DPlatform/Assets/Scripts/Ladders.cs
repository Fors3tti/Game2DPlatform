using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladders : MonoBehaviour
{
    private GatherInput gI;
    private PlayerMoveControls pMC;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gI = collision.GetComponent<GatherInput>();
        pMC = collision.GetComponent<PlayerMoveControls>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (gI.tryToClimb)
        {
            pMC.onLadders = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        pMC.ExitLadders();
    }
}
