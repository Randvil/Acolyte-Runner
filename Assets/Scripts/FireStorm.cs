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
        gameObject.transform.localScale = new(0.3f, 0.3f, 1);
        transform.localPosition = new(transform.localPosition.x, transform.localPosition.y + 2, 0f);
        GetComponent<BoxCollider>().size = new(2, 10);

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
