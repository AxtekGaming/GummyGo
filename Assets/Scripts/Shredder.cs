using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject otherObject = collision.gameObject;

       if (otherObject.GetComponent<Character>())
       {
            FindObjectOfType<AdMobAds>().TriggerAdvertDeathCounter(); 
            Destroy(collision.gameObject); 
        }
       else if (otherObject.transform.Find("Character"))
        {
            FindObjectOfType<AdMobAds>().TriggerAdvertDeathCounter(); 
            Destroy(collision.gameObject);
        }
    }
}

