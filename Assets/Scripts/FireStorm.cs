using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireStorm : BaseEffect
{
    [SerializeField]
    private float moveXatFrame;

    private bool activated = false;

    [SerializeField]
    private Sprite stormSprite;

    protected override void Effect()
    {
        GetComponent<SpriteRenderer>().sprite = stormSprite;
        gameObject.transform.localScale = new(1.7f, 1.7f, 1);

        activated = true;
    }

    void Update()
    {
        if (activated)
            GetComponent<Rigidbody>().velocity = (new(moveXatFrame, 0f, 0f));
    }

    protected override void OnTriggerEnter(Collider other)
    {
        Player = other.GetComponent<PlayerController>();

        if (Player != null)
        {
            Player.TakeEffect(this);
            StartTime = Time.timeSinceLevelLoad;

            Effect();
        }

        if (Player == null)
        { // —юда можно добавить уничтожение чего угодно, не только ќбстаклов
            Obstacle obst = other.GetComponent<Obstacle>();
            if (obst != null)
            {
                Destroy(obst.gameObject);
            }
        }
    }

    public override void Ending()
    {
        base.Ending();
    }
}
