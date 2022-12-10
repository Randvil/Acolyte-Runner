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
    private float previousCoinPosition;

    [SerializeField]
    private float minDistanceBetweenCoins;

    [SerializeField]
    private float maxDistanceBetweenCoins;

    private void Start()
    {        
        for (int i = 0; i < obstacleNumber; i++)
        {
            float newXPositon = previousObstaclePosition + Random.Range(minDistanceBetweenObstacles, maxDistanceBetweenObstacles);
            Instantiate(obstaclePrefab, new Vector3(newXPositon, Random.Range(0, 2), 0f), Quaternion.identity);
            previousObstaclePosition = newXPositon;
        }

        for (int i = 0; i < coinNumber; i++)
        {
            float newXPositon = previousCoinPosition + Random.Range(minDistanceBetweenCoins, maxDistanceBetweenCoins);
            Instantiate(coinPrefab, new Vector3(newXPositon, 2f, 0f), Quaternion.identity);
            previousCoinPosition = newXPositon;
        }
    }

}
