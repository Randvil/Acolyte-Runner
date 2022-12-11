using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerDestroyer : MonoBehaviour
{
    [SerializeField]
    float interval;

    void Start()
    {
        Destroy(gameObject, interval);
    }

}
