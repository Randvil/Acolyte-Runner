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

    [SerializeField]
    private float jumpSpeed;

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

    private bool isGrounded;
    private Coroutine shrinkingCoroutine;

    private new Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        moveSpeed += acceleration * Time.deltaTime;

        rigidbody.velocity = new Vector3(moveSpeed, rigidbody.velocity.y, 0f);

        isGrounded = Physics.CheckSphere(bottomEdge.position, checkGroundRadius, groundLayerMask);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) rigidbody.velocity = new Vector3(rigidbody.velocity.x, jumpSpeed, 0f);

        if (Input.GetKeyDown(KeyCode.LeftControl) && shrinkingCoroutine == null) shrinkingCoroutine = StartCoroutine(ShrinkingCoroutine());
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

    public void CollectCoin()
    {
        GameManager.instance.OnCoinCollected();
    }
}
