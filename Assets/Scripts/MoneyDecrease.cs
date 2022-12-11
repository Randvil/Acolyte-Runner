using UnityEngine;

public class MoneyDecrease : BaseEffect
{
    [SerializeField]
    private int amount;

    protected override void Effect() => Player.LostCoins(amount);
}
