using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBooster : BaseEffect
{
    [SerializeField]
    private float speedUp;

    protected override void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        player.TakeEffect(this);
        player?.IncreaseSpeed(speedUp);

        StartTime = Time.timeSinceLevelLoad;
    }

    public override void Ending(PlayerController player)
    {
        player?.DecreaseSpeed(speedUp);
        Debug.Log("Deleted");

        base.Ending(player); //Destroy GameObject
    }
}
