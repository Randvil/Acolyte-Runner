using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFactory : MonoBehaviour
{
    [Header("Obstacles")]

    [SerializeField]
    private GameObject obstaclePrefab;

    [SerializeField]
    private int obstacleNumber;

    [SerializeField]
    private float previousObstaclePosition;

    [SerializeField]
    private float minDistanceBetweenObstacles;

    [SerializeField]
    private float maxDistanceBetweenObstacles;


    [Header("Coins")]

    [SerializeField]
    private GameObject coinPrefab;

    [SerializeField]
    private int coinNumber;

    [SerializeField]
    private int coinNumberGround;

    [SerializeField]
    private float previousCoinPosition;

    [SerializeField]
    private float distanceBetweenCoins;

    [SerializeField]
    private int coinHeight;

    [Header("Platforms")]

    [SerializeField]
    private GameObject platformPrefab;

    [SerializeField]
    private int platformNumberNear;

    [SerializeField]
    private int platformNumberAll;

    [SerializeField]
    private float previousPlatformPositionX;

    [SerializeField]
    private float previousPlatformPositionY;

    [SerializeField]
    private float distanceBetweenPlatforms;

    [SerializeField]
    private float minPlatformHeight;

    [SerializeField]
    private float maxPlatformHeight;

    [SerializeField]
    private float newYPosition;

    [SerializeField]
    private float platformHeightDifferencePos;

    [SerializeField]
    private float platformHeightDifferenceNeg;

    private string GROUND_TAG = "Ground";
    private string PLATFORM_TAG = "Platform";
    private string OBSTACLE_TAG = "Obstacle";

    private void Start()
    {        
        CreateObstacles();
        SetAllPlatformsAndCoinsBetweenThem();
        CreateCoinsOnPlatforms();
        //CreateCoinsOnGround();

        /*for (int i = 0; i < coinNumber; i++)
        {
            float newXPositon = previousCoinPosition + Random.Range(minDistanceBetweenCoins, maxDistanceBetweenCoins);
            GameObject ground = FindGroundInRange(newXPositon, Random.Range(minDistanceBetweenCoins, maxDistanceBetweenCoins));
            float newYPosition = ground.transform.position.y + coinHeight;
            Instantiate(coinPrefab, new Vector3(newXPositon, newYPosition, 0f), Quaternion.identity);
            previousCoinPosition = newXPositon;
        }*/

    }

    private void CreateObstacles()
    {
        for (int i = 0; i < obstacleNumber; i++)
        {
            float newXPositon = previousObstaclePosition + Random.Range(minDistanceBetweenObstacles, maxDistanceBetweenObstacles);
            Instantiate(obstaclePrefab, new Vector3(newXPositon, Random.Range(0, 2), 0f), Quaternion.identity);
            previousObstaclePosition = newXPositon;
        }
    }

    private void CreatePlatforms()
    {
        for (int i = 0; i < platformNumberNear; i++)
        {
            var diff = new List<float>() { platformHeightDifferencePos, platformHeightDifferenceNeg };
            float newXPositon = previousPlatformPositionX + distanceBetweenPlatforms;
            if (i == 0)
            {
                newYPosition = 2;
                Instantiate(platformPrefab, new Vector3(newXPositon, newYPosition, 0f), Quaternion.identity);
                previousPlatformPositionX = newXPositon;
                previousPlatformPositionY = newYPosition;
            } else
            {
                if (previousPlatformPositionY < maxPlatformHeight && previousPlatformPositionY > minPlatformHeight)
                {
                    newYPosition = previousPlatformPositionY + diff[Random.Range(0, diff.Count)];
                }
                else if (previousPlatformPositionY == minPlatformHeight)
                {
                    newYPosition = previousPlatformPositionY + platformHeightDifferencePos;
                }
                else if (previousPlatformPositionY == maxPlatformHeight)
                {
                    newYPosition = previousPlatformPositionY + platformHeightDifferenceNeg;
                }
                Instantiate(platformPrefab, new Vector3(newXPositon, newYPosition, 0f), Quaternion.identity);
                previousPlatformPositionX = newXPositon;
                previousPlatformPositionY = newYPosition;
            }
        }
    }

    private bool ObstacleCheck(float x)
    {
        GameObject[] obstacles;
        obstacles = GameObject.FindGameObjectsWithTag(OBSTACLE_TAG);
        bool result = false;
        foreach (GameObject ob in obstacles)
        {
            if (x > ob.transform.position.x - 1 && x < ob.transform.position.x + 1)
                result = true;
        }
        return result;
    }

    private void SetAllPlatformsAndCoinsBetweenThem()
    {
        for (int i = 0; i < platformNumberAll; i++)
        {
            CreatePlatforms();
            previousPlatformPositionX += 50;

            //creating coins between platforms
            for (int j = 0; j < coinNumberGround; j++)
            {
                if (j == 0)
                {
                    float newXPositon = previousPlatformPositionX + distanceBetweenCoins - 45;
                    Instantiate(coinPrefab, new Vector3(newXPositon, 0f, 0f), Quaternion.identity);
                    previousCoinPosition = newXPositon;
                } else {
                    float newXPositon = previousCoinPosition + distanceBetweenCoins;
                    Instantiate(coinPrefab, new Vector3(newXPositon, 0f, 0f), Quaternion.identity);
                    previousCoinPosition = newXPositon;
                }

            }
        }
    }

    private void CreateCoinsOnPlatforms()
    {
        GameObject[] platforms;
        platforms = GameObject.FindGameObjectsWithTag(PLATFORM_TAG);
        foreach (GameObject plat in platforms)
        {
            Instantiate(coinPrefab, new Vector3(plat.transform.position.x, plat.transform.position.y + 2, 0f), Quaternion.identity);
        }
    }



}
