using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerUpBehaviour : MonoBehaviour
{
    public float puRespawn = 7;

   public void HidePU()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;

        StartCoroutine(DelayRespawn()); // Start of timer
    }

    IEnumerator DelayRespawn()
    {
        yield return new WaitForSeconds(puRespawn);
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<CircleCollider2D>().enabled = true;
    }
}
