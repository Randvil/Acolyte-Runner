using UnityEngine;

public abstract class BaseEffect : MonoBehaviour
{
    [SerializeField]
    private float time;
    public float TimeOfAction => time;
    public float StartTime { get; set; }

    protected abstract void OnTriggerEnter(Collider other);

    public virtual void Ending(PlayerController player)
    {
        //When override add logic to remove effect from player
        Destroy(gameObject);
    }
}
