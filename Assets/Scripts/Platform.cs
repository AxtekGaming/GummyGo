using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [Range(0f, 5f)] [SerializeField] float currentSpeed = 1f;

    bool alreadyCountedScore = false;
    GameObject currentPlatform;

    private Rigidbody2D myRb;

    private void OnEnable()
    {
        myRb = this.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        myRb.velocity = -transform.up * currentSpeed;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        float otherobject = collision.gameObject.GetComponent<Rigidbody2D>().velocity.y;
        bool fromAbove = false;                       
        var character = FindObjectOfType<Character>();

        if (otherobject < Mathf.Epsilon && tag == "Platform") 
        {
            character.HasCharComeFromAbove();
            currentPlatform = collision.gameObject;
            fromAbove = true;
        }
        else if (otherobject < Mathf.Epsilon && tag == "StartingPlatform")
        {
            character.CharOnStartingPlatform();
            currentPlatform = collision.gameObject;
            fromAbove = true;
        }

        if (fromAbove && collision.gameObject.tag == "Player")
        {
            character.transform.parent = transform;
            character.GetComponent<Rigidbody2D>().isKinematic = true;
            FindObjectOfType<Character>().IsGroundedToPlatform();
        }

        if (fromAbove && !alreadyCountedScore && tag == "Platform")
        {
            Score scoreScript = FindObjectOfType<Score>();
            scoreScript.AddToScore();
            FindObjectOfType<GameController>().IncreaseGameSpeed(scoreScript.GetScore());
            if (PlayerPrefsController.GetMasterSFX() != 0)
            {
                GetComponent<AudioSource>().Play();
            }
            alreadyCountedScore = true;
        }
    }
    
    public void ResetPlatformScoreCounter()
    {
        alreadyCountedScore = false;
    }

    public GameObject TheCurrentPlatform()
    {
        return currentPlatform;
    }
    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }

    public void SetCurrentSpeed(float theSpeed)
    {
        currentSpeed = theSpeed;
    }

}
