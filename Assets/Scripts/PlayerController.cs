using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float acceleration;

    [SerializeField]
    private float minMoveSpeed;

    //[SerializeField]
    //private float jumpSpeed;

    [SerializeField]
    private Transform bottomEdge;

    [SerializeField]
    private float checkGroundRadius;

    [SerializeField]
    private LayerMask groundLayerMask;

    [SerializeField]
    private float shrinkingTime;

    [SerializeField]
    private float shrinkingCooldown;

    private Coroutine shrinkingCoroutine;

    private new Rigidbody rigidbody;

    //gravity variables
    public float gravity = -6f;//-9.8f;
    private float groundedGravity = -0.5f;

    //jumping variables
    private float initialJumpVelocity;
    [SerializeField]
    private float maxJumpHeight = 25.0f;
    [SerializeField]
    private float maxJumpTime = 1.5f;
    private bool isGrounded;

    public List<BaseEffect> effects;

    private void Awake()
    {
        SetupJumpVariables();
    }

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        moveSpeed += acceleration * Time.deltaTime;

        HandleGravity(moveSpeed);
        PlayerJump(moveSpeed);

        rigidbody.velocity = new Vector3(moveSpeed, rigidbody.velocity.y, 0f);

        if (Input.GetKeyDown(KeyCode.LeftControl) && shrinkingCoroutine == null) shrinkingCoroutine = StartCoroutine(ShrinkingCoroutine());

        CheckEffects();
    }

    private IEnumerator ShrinkingCoroutine()
    {
        Vector3 initScale = transform.localScale;
        transform.localScale = initScale / 2f;

        yield return new WaitForSeconds(shrinkingTime);

        transform.localScale = initScale;

        yield return new WaitForSeconds(shrinkingCooldown);

        shrinkingCoroutine = null;
    }

    public void DecreaseSpeed(float value)
    {
        moveSpeed = Mathf.Clamp(moveSpeed - value, minMoveSpeed, float.MaxValue);
    }

    public void IncreaseSpeed(float value)
    {
        moveSpeed = Mathf.Clamp(moveSpeed + value, minMoveSpeed, float.MaxValue);
    }

    public void CollectCoin()
    {
        GameManager.instance.OnCoinCollected();
    }

    public void TakeEffect(BaseEffect effect)
    {
        // TODO: Add effect to UI (maybe bar of effects)

        effects.Add(effect);
    }

    private void CheckEffects()
    {
        for (int i = 0; i < effects.Count; i++)
        {
            var effect = effects[i];
            if (Time.timeSinceLevelLoad - effect.StartTime >= effect.TimeOfAction)
            {
                // TODO: Add removing from UI

                effects.Remove(effect);
                effect.Ending();

                i = 0;
            }
        }
    }

    private void HandleGravity(float x)
    {
        isGrounded = Physics.CheckSphere(bottomEdge.position, checkGroundRadius, groundLayerMask);

        bool isFalling = rigidbody.velocity.y <= 0.0f || !Input.GetKeyDown(KeyCode.Space);
        float fallMultiplier = 2.0f;

        /*if (isGrounded)
        {
            rigidbody.velocity = new Vector3(x, groundedGravity, 0);
        }
        else */if (isFalling)
        {
            float previousYVelocity = rigidbody.velocity.y;
            float newYVelocity = rigidbody.velocity.y + (fallMultiplier * gravity * Time.deltaTime);
            float nextYVelocity = (previousYVelocity + newYVelocity) * .5f;
            rigidbody.velocity = new Vector3(x, nextYVelocity, 0);
        }
        else
        {
            float previousYVelocity = rigidbody.velocity.y;
            float newYVelocity = rigidbody.velocity.y + (gravity * Time.deltaTime);
            float nextYVelocity = (previousYVelocity + newYVelocity) * .5f;
            rigidbody.velocity = new Vector3(x, nextYVelocity, 0);
        }
    }

    private void SetupJumpVariables()
    {
        float timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
    }

    private void PlayerJump(float x)
    {
        // jumping
        isGrounded = Physics.CheckSphere(bottomEdge.position, checkGroundRadius, groundLayerMask);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rigidbody.velocity = new Vector3(x, initialJumpVelocity * .5f, 0);
        }
    }
}
