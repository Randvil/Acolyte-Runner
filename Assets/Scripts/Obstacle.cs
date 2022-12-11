using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField]
    private float deceleration;

    [SerializeField]
    private Sprite[] sprites;

    private void Start()
    {
        SpriteRenderer spriteRendere = GetComponent<SpriteRenderer>();
        spriteRendere.sprite = sprites[Random.Range(0, sprites.Length)];
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            player.GetHit();
            GameManager.instance.LoseGame();
        }
    }
}
