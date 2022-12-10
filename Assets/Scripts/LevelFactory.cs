using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFactory : MonoBehaviour
{
    [SerializeField]
    private float levelWidth; //100

    [Header("Objects")]

    [SerializeField]
    private float minDistanceBetweenObjects; //4

    [SerializeField]
    private float maxDistanceBetweenObjects; //10

    [Header("Obstacles")]

    [SerializeField]
    private GameObject obstaclePrefab;

    /*[SerializeField]
    private int obstacleNumber;

    [SerializeField]
    private float previousObstaclePosition;

    [SerializeField]
    private float minDistanceBetweenObstacles;

    [SerializeField]
    private float maxDistanceBetweenObstacles;*/


    [Header("Coins")]

    [SerializeField]
    private GameObject coinPrefab;

    /*[SerializeField]
    private int coinNumber;

    [SerializeField]
    private float previousCoinPosition;

    [SerializeField]
    private float distanceBetweenCoins;

    [SerializeField]
    private int coinHeight;*/

    [Header("Platforms")]

    [SerializeField]
    private GameObject platformPrefab;

    /*[SerializeField]
    private int minPlatformNumber;

    [SerializeField]
    private int maxPlatformNumber;

    [SerializeField]
    private float previousPlatformPositionX;

    [SerializeField]
    private float previousPlatformPositionY;*/

    [SerializeField]
    private float minDistanceBetweenPlatforms; //4

    [SerializeField]
    private float maxDistanceBetweenPlatforms; //6

    [SerializeField]
    private float minPlatformHeight; //2

    [SerializeField]
    private float maxPlatformHeight; //10

    [SerializeField]
    private float platformHeightDifferencePos; //2

    [SerializeField]
    private float platformHeightDifferenceNeg; //-2

    private string PLATFORM_TAG = "Platform";

    private void Start()
    {
        GeneratePlatforms();
        CreateObjectsOnGround();
        CreateObjectsOnPlatforms();
    }

    private void GeneratePlatforms()
    {
        //random x position for first platform
        float posX = Random.Range(0, levelWidth);
        //start y so player can jump on it
        float posY = 2f;
        var diff = new List<float>() { platformHeightDifferencePos, platformHeightDifferenceNeg };
        while (posX < levelWidth)
        {
            Instantiate(platformPrefab, new Vector3(posX, posY, 0f), Quaternion.identity);

            if (posY < maxPlatformHeight && posY > minPlatformHeight)
            {
                posY += diff[Random.Range(0, diff.Count)];
            }
            else if (posY == minPlatformHeight)
            {
                posY += platformHeightDifferencePos;
            }
            else if (posY == maxPlatformHeight)
            {
                posY += platformHeightDifferenceNeg;
            }

            posX += Random.Range(minDistanceBetweenPlatforms, maxDistanceBetweenPlatforms);
        }
    }

    private void CreateObjectsOnGround()
    {
        var objects = new List<GameObject>() { coinPrefab, obstaclePrefab };
        float posX = 6f; //can be changed to random position for first object
        while (posX < levelWidth)
        {
            Instantiate(objects[Random.Range(0, objects.Count)], new Vector3(posX, Random.Range(0, 2), 0f), Quaternion.identity);
            posX += Random.Range(minDistanceBetweenObjects, maxDistanceBetweenObjects); ;
        }
    }

    private void CreateObjectsOnPlatforms()
    {
        GameObject[] platforms;
        platforms = GameObject.FindGameObjectsWithTag(PLATFORM_TAG);

        var objects = new List<GameObject>() { coinPrefab, obstaclePrefab };

        foreach (GameObject plat in platforms)
        {
            //i hope x position is always the half of game object :(
            float half = GetObjectWidth(plat) / 2;
            float posX = plat.transform.position.x - half;
            while (posX < plat.transform.position.x + half) 
            {
                Instantiate(objects[Random.Range(0, objects.Count)], 
                    new Vector3(posX, plat.transform.position.y + 2, 0f), Quaternion.identity);
                posX += 2;//Random.Range(minDistanceBetweenObjects, maxDistanceBetweenObjects);
            }
        }

    }

    //getting width from box collider size and object scale
    private float GetObjectWidth(GameObject gameObject)
    {
        BoxCollider boxCollider = gameObject.GetComponent<BoxCollider>();
        return boxCollider.size.x * gameObject.transform.localScale.x;
    }



}
