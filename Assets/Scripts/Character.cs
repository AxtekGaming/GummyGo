using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    //General
    bool isGamePaused = false;
    Vector2 currentBallPos;
    private Animator myAnimator;
    private AudioSource myAudioSource;
    private TrailRenderer trail;
    bool characterIsFalling = false;

    [SerializeField] GameObject traileffect;
    [SerializeField] GameObject trajSpawnSpot;
    GameController myGameControllerScript; 

    //----- Caching
    Rigidbody2D myRigidBody;

    //Particle Effect
    [Header("Parctile Effect")]
    [SerializeField] GameObject particleSpawnPos;
    [SerializeField] GameObject dustEffect;

    //Character Movement
    [Header("Character Movement")]
    [SerializeField] float forceFactor = 2f;
    [SerializeField] float deadzoneSwipe = 100.0f;
    Vector2 startTouchPos, endTouchPos, forceAtPlayer;
    bool screenHasBeenClicked = false;
    bool isInDeadzone; //DO NOT DELETE, this is used
    float characterScale = 0.9f; //this is for switching direction
    float sqrDeadzone;

    //Restricting Movement through Clamping
    [Header("Movement Boundaries")]
    [SerializeField] float minX = -0.5f;
    [SerializeField] float maxX = 0.5f;
    [SerializeField] float minY = 0f;
    [SerializeField] float maxY = -2f;
    float theXForce;
    float theYForce;

    //Pause Button Protection
    [Header("Protecting Pause Button")]
    [SerializeField] float yPadding = 4.2f;

    //Trajectory 
    [Header("Trajectory Varirables")]
    [SerializeField] float gravityVar = 1f;
    [SerializeField] GameObject trajectoryDot;
    [SerializeField] int amountOfDots = 20;
    GameObject[] allTrajetoryDots;
    bool dotsAreSpawned;

    //Allowing to Jump when grounded to a Platform
    bool fromAbove = true;
    bool charIsGrounded = false; 


    private void Start()
    {
        myGameControllerScript = FindObjectOfType<GameController>();
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myAudioSource = GetComponent<AudioSource>();
        trail = traileffect.GetComponent<TrailRenderer>();
        allTrajetoryDots = new GameObject[amountOfDots];
        sqrDeadzone = deadzoneSwipe * deadzoneSwipe;
        SettingGravity();
    }

    private void Update()
    {
        if (!isGamePaused)
        {
            Movement();
            ActivateFallingAnimation();
            IsGroundedToPlatform();
        }
        else
        {
            return;
        }

    }

    private void ActivateFallingAnimation()
    {
        if (!characterIsFalling)
        {
            return;
        } 
          else if (myRigidBody.velocity.y < 0)
            {
                myAnimator.SetTrigger("isFalling");
                trail.emitting = false; 
                characterIsFalling = false;
            }
    }

    private void Movement()
    {
        if (fromAbove && (charIsGrounded)) 
        {
            if (Input.touchCount > 0) 
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began) 
                {
                    startTouchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position); 
                    screenHasBeenClicked = true;
                    myGameControllerScript.DisableGameStartCanvas();
                }

            }

            if (Input.touchCount == 1) 
            {
                
                 if (screenHasBeenClicked && startTouchPos.y <= yPadding)
                   {
                     endTouchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                     endTouchPos.x = endTouchPos.x / 2f; 
                     endTouchPos.y = endTouchPos.y / 1.05f; 
                     forceAtPlayer = endTouchPos - startTouchPos;
                     FlipSprite(forceAtPlayer);

                    if (!IsInDeadZone()) 
                       {
                          if (!dotsAreSpawned && startTouchPos.y <= yPadding)
                            {
                              myAnimator.SetTrigger("jumpReady");
                              dotsAreSpawned = true;

                            for (int i = 0; i < amountOfDots; i++) 
                                {
                                  GameObject trajectoryDot = ObjectPooler.SharedInstance.GetPooledObject("Trajectory");
                                  float counter = i;

                                if (trajectoryDot != null)
                                {
                                    trajectoryDot.transform.parent = gameObject.transform;
                                    trajectoryDot.transform.localScale = new Vector2(0.65f - counter / 25, 0.65f - counter / 25);
                                    
                                    Color tempDotColour = trajectoryDot.GetComponent<SpriteRenderer>().color;
                                    tempDotColour.a = 0.95f - counter * 0.05f;
                                    trajectoryDot.GetComponent<SpriteRenderer>().color = tempDotColour;
                                    
                                    trajectoryDot.SetActive(true);
                                    allTrajetoryDots[i] = trajectoryDot;
                                }
                            } 
                        }

                         forceAtPlayer.x = Mathf.Clamp(forceAtPlayer.x, minX, maxX);
                         forceAtPlayer.y = Mathf.Clamp(forceAtPlayer.y, minY, maxY);
                       }


                    if (dotsAreSpawned && (IsInDeadZone() || forceAtPlayer.y > -maxY)) 
                    {
                            CharRestingOnPlatform();
                    }
                        

                     for (int i = 0; i < amountOfDots; i++)
                       {
                         if (allTrajetoryDots[i] != null)
                           {
                             allTrajetoryDots[i].transform.position = CalculatePositionOfDot(i * 0.1f); 
                           }
                      }
                } 
             }

            if (Input.touchCount > 0) 
            {
                if (Input.GetTouch(0).phase == TouchPhase.Ended) 
                {
                    if (screenHasBeenClicked && startTouchPos.y <= yPadding)
                    {
                        theXForce = -forceAtPlayer.x * forceFactor;
                        theYForce = -forceAtPlayer.y * forceFactor;

                        if (dotsAreSpawned)
                        {
                            myRigidBody.velocity = new Vector2(theXForce, theYForce);
                            myAnimator.SetTrigger("isFlying");

                            if (PlayerPrefsController.GetMasterSFX() != 0)
                            {
                                myAudioSource.Play();
                            }

                            for (int i = 0; i < amountOfDots; i++)
                            {
                                allTrajetoryDots[i].SetActive(false);
                            }
                            fromAbove = false;
                            screenHasBeenClicked = false;
                            characterIsFalling = true;
                            trail.emitting = true;
                            transform.parent = null;
                            myRigidBody.isKinematic = false;
                            myRigidBody.bodyType = RigidbodyType2D.Dynamic;
                            charIsGrounded = false;
                            dotsAreSpawned = false;
                        }
                    }

                }
            }    
        } 
    }


    void SettingGravity() 
    {
        Physics2D.gravity = new Vector2(0, -9.81f * gravityVar);
    }


    private void CharRestingOnPlatform()
    {
        for (int i = 0; i < amountOfDots; i++)
        {
            allTrajetoryDots[i].SetActive(false);
        }
        forceAtPlayer = Vector2.zero;
        dotsAreSpawned = false;
        myAnimator.SetTrigger("onPlatform");
    }

    private void FlipSprite(Vector2 whichDirection)
    {
        bool playerFacingHorizontal = whichDirection.x > 0;

        if (playerFacingHorizontal)
        {
            transform.localScale = new Vector2(characterScale * -1, characterScale);
        }
        else
        {
            transform.localScale = new Vector2(characterScale, characterScale);
        }
    }

    private Vector2 CalculatePositionOfDot(float elapsedTime)
    {
        currentBallPos = new Vector2(trajSpawnSpot.transform.position.x, trajSpawnSpot.transform.position.y);
        return currentBallPos + new Vector2(-forceAtPlayer.x * forceFactor, -forceAtPlayer.y * forceFactor) * elapsedTime + 0.5f * Physics2D.gravity * elapsedTime * elapsedTime; 
    }


    public bool IsInDeadZone()
    {
        if (forceAtPlayer.sqrMagnitude <= sqrDeadzone) 
        {
            return isInDeadzone = true;
        }
        else return isInDeadzone = false;
    }


    public void IsGroundedToPlatform()
    {
        charIsGrounded = true;
    }


    public void HasCharComeFromAbove()
    {
        fromAbove = true;
        if (charIsGrounded) 
        {
            myAnimator.SetTrigger("onPlatform");
            Instantiate(dustEffect, particleSpawnPos.transform.position, Quaternion.identity);
        }
    }

  
    public void CharOnStartingPlatform()
    {
        fromAbove = true;
        if (charIsGrounded)
        {
            myAnimator.SetTrigger("onStartingPlatform");
        }
    }

    public int GetAmountOfTrajDots()
    {
        return amountOfDots;
    }

    public void PauseTheGame()
    {
        isGamePaused = true;
    }

    public void ResumeTheGame() 
    {
        isGamePaused = false;
    }


}
