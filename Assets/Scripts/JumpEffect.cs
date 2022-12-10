using UnityEngine;

public class JumpEffect : BaseEffect
{
    [SerializeField]
    private float gravityModifier;

    protected override void Effect() => Player.gravity *= gravityModifier;

    public override void Ending()
    {
        Player.gravity /= gravityModifier;
        
        base.Ending();
    }
}
