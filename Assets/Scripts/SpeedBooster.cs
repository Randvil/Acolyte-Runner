using UnityEngine;

public class SpeedBooster : BaseEffect
{
    [SerializeField]
    private float speedUp;

    protected override void Effect() => Player.IncreaseSpeed(speedUp);

    public override void Ending()
    {
        Player.DecreaseSpeed(speedUp);

        base.Ending(); //Destroy GameObject
    }   
}
