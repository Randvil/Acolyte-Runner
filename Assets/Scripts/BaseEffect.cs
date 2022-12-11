using UnityEngine;

public abstract class BaseEffect : MonoBehaviour
{
    [SerializeField]
    private float time;
    public float TimeOfAction => time;
    public float StartTime { get; set; }

    public PlayerController Player { get; protected set; }

    protected virtual void OnTriggerEnter(Collider other)
    {
        Player = other.GetComponent<PlayerController>();

        if (Player != null)
        { 
            GetComponent<SpriteRenderer>().enabled = false;
            Player.TakeEffect(this);
            StartTime = Time.timeSinceLevelLoad;

            Effect();
        }
    }

    protected abstract void Effect();

    public virtual void Ending()
    {
        //When override add logic to remove effect from player
        Destroy(gameObject);
    }
}
