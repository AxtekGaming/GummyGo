using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformShredder : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject otherObject = collision.gameObject;

        if (otherObject.tag == "Platform")
        {
            otherObject.SetActive(false);
            otherObject.GetComponent<Platform>().ResetPlatformScoreCounter();
        }

        if (otherObject.tag == "StartingPlatform")
        {
            otherObject.SetActive(false);
        }
    }
}
