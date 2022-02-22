using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] float minSpawnDelay = 1f;
    [SerializeField] float maxSpawnDelay = 5f;
    [SerializeField] GameObject startPlatform;
    [SerializeField] GameObject landingPlatform;
    [SerializeField] List<GameObject> spawningPoints;
    [SerializeField] List<GameObject> landingSpawnPoints;
    [SerializeField] Sprite[] spritesImage;
    [SerializeField] GameObject spawnVisualEffect;

    [SerializeField] bool spawn = false;

    [SerializeField] float spawnEffectYPos = 0.22f;
    int rand;
    private int lastColour;
    private int secondLastColour;
    bool hasStarted = false;
    GameObject lastPlatformSpawned;


    private void Start()
    {
        SetStartPlatformColoursAndSpawn();
    }


    private void Update()
    {
        StartTheSpawn();
    }

    private void StartTheSpawn()
    {
        if (hasStarted)
        {
            return;
        }
        else if (spawn)
        {
            StartCoroutine(BeginSpawning());
            hasStarted = true;
        }
    }

    IEnumerator BeginSpawning()
    {
        while (spawn)
        {
            yield return StartCoroutine(SpawnAPlatform());
        }
    }

    private IEnumerator SpawnAPlatform()
    {
        var platformIndex = UnityEngine.Random.Range(0, spawningPoints.Count);
        var currentPlatform = spawningPoints[platformIndex];

        if (currentPlatform != lastPlatformSpawned)
        {
            GameObject platform = ObjectPooler.SharedInstance.GetPooledObject("Platform");
            SpriteRenderer currentPlatColour = platform.GetComponent<SpriteRenderer>();

            if (platform != null)
            {
                platform.transform.position = currentPlatform.transform.position;
                platform.transform.rotation = currentPlatform.transform.rotation;
                platform.SetActive(true);

                if(PlayerPrefsController.GetMasterSFX() != 0)
                {
                    GetComponent<AudioSource>().Play();
                }

                var adjustSpawnEffectPos = new Vector2(currentPlatform.transform.position.x, currentPlatform.transform.position.y - spawnEffectYPos);
                Instantiate(spawnVisualEffect, adjustSpawnEffectPos, Quaternion.identity);
                currentPlatColour.sprite = spritesImage[GetRandom(0, spritesImage.Length)];
            }

            yield return new WaitForSeconds(UnityEngine.Random.Range(minSpawnDelay, maxSpawnDelay));
            lastPlatformSpawned = currentPlatform; 
        }

        else
        {
            yield return new WaitForSeconds(0);
        }

    }

    private void SetStartPlatformColoursAndSpawn()
    {
        rand = UnityEngine.Random.Range(0, spritesImage.Length);
        startPlatform.GetComponent<SpriteRenderer>().sprite = spritesImage[GetRandom(0, spritesImage.Length)];
        landingPlatform.GetComponent<SpriteRenderer>().sprite = spritesImage[GetRandom(0, spritesImage.Length)];

        var landingSpawnSpot = landingSpawnPoints[UnityEngine.Random.Range(0, landingSpawnPoints.Count)];
        landingPlatform.transform.position = landingSpawnSpot.transform.position;
    }

    int GetRandom(int min, int max)
    {
        int rand = UnityEngine.Random.Range(min, max);
        while (rand == lastColour || rand == secondLastColour)
            rand = UnityEngine.Random.Range(min, max);
        secondLastColour = lastColour;
        lastColour = rand;

        return rand;
    }

    public void SetSpawnToSpawning()
    {
        spawn = true;
    }

}