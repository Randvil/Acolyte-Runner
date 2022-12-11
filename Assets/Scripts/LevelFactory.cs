using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFactory : MonoBehaviour
{
    [Header("Coins and Bonuses")]

    [SerializeField]
    private int coinsAndBonusesNumber;

    [SerializeField]
    private float previousCoinOrBonusPosition;

    [SerializeField]
    private float minDistanceBetweenCoinOrBonus;

    [SerializeField]
    private float maxDistanceBetweenCoinOrBonus;

    [SerializeField]
    private float bonusProbability;

    [SerializeField]
    private GameObject coinPrefab;

    [SerializeField]
    private GameObject[] bonusesPrefabs;


    [Header("Platforms")]

    [SerializeField]
    private Transform player;

    [SerializeField]
    private GameObject platformPrefab;

    [SerializeField]
    private int minPlatformNumberInChunk;

    [SerializeField]
    private int maxPlatformNumberInChunk;

    [SerializeField]
    private int chunkNumber;

    [SerializeField]
    private float distanceBetweenPlatforms;

    [SerializeField]
    private float minPlatformHeight;

    [SerializeField]
    private float maxPlatformHeight;

    [SerializeField]
    private float platformHeightStep;

    [SerializeField]
    private float distanceBetweenChunks;

    private float previousPlatformPositionX = 10f;
    private float previousPlatformPositionY;
    private float newYPosition;

    [Header("Background")]

    [SerializeField]
    private GameObject background;

    [SerializeField]
    private float backgroundWidth;

    [SerializeField]
    private float backgroundYPosition;

    private int backgroundNumber;
    private List<GameObject> backgrounds = new();

    private void Update()
    {
        if (player.transform.position.x > previousPlatformPositionX - 20f) BuildPlatformChunk();
        if (player.transform.position.x > previousCoinOrBonusPosition - 20f) SpawnCoinsAndBonuses();

        if (player.transform.position.x > (backgroundNumber - 1) * backgroundWidth)
        {
            backgrounds.Add(Instantiate(background, new Vector3(backgroundWidth * backgroundNumber, backgroundYPosition, 0f), Quaternion.identity));
            if (backgroundNumber > 1) Destroy(backgrounds[backgroundNumber - 2]);

            backgroundNumber += 1;
        }
    }

    private void SpawnCoinsAndBonuses()
    {
        for (int i = 0; i < coinsAndBonusesNumber; i++)
        {
            float newXPositon = previousCoinOrBonusPosition + Random.Range(minDistanceBetweenCoinOrBonus, maxDistanceBetweenCoinOrBonus);
            if (Random.Range(0f, 100f) < bonusProbability) Instantiate(bonusesPrefabs[Random.Range(0, bonusesPrefabs.Length)], new Vector3(newXPositon, 0f, 0f), Quaternion.identity);
            else Instantiate(coinPrefab, new Vector3(newXPositon, 0f, 0f), Quaternion.identity);
            previousCoinOrBonusPosition = newXPositon;
        }
    }

    private void CreatePlatforms()
    {
        for (int i = 0; i < Random.Range(minPlatformNumberInChunk, maxPlatformNumberInChunk + 1); i++)
        {
            float newXPositon = previousPlatformPositionX + distanceBetweenPlatforms;
            if (i == 0)
            {
                newYPosition = 2;
            }
            else
            {
                if (previousPlatformPositionY < maxPlatformHeight && previousPlatformPositionY > minPlatformHeight)
                {
                    newYPosition = previousPlatformPositionY + Mathf.Sign(Random.Range(-1f, 1f)) * platformHeightStep;
                }
                else if (previousPlatformPositionY == minPlatformHeight)
                {
                    newYPosition = previousPlatformPositionY + platformHeightStep;
                }
                else if (previousPlatformPositionY == maxPlatformHeight)
                {
                    newYPosition = previousPlatformPositionY - platformHeightStep;
                }
            }

            GameObject platform = Instantiate(platformPrefab, new Vector3(newXPositon, newYPosition, 0f), Quaternion.identity);
            Instantiate(coinPrefab, new Vector3(platform.transform.position.x - 1f, platform.transform.position.y + 1f, 0f), Quaternion.identity);
            Instantiate(coinPrefab, new Vector3(platform.transform.position.x + 1f, platform.transform.position.y + 1f, 0f), Quaternion.identity);
            previousPlatformPositionX = newXPositon;
            previousPlatformPositionY = newYPosition;
        }
    }

    private void BuildPlatformChunk()
    {
        CreatePlatforms();
        previousPlatformPositionX += distanceBetweenChunks;
    }
}