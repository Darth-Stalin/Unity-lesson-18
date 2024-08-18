using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement vars")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float speed;
    [SerializeField] private bool isGrounded = false;

    [Header("Settings")]
    [SerializeField] private Transform groundColliderTransform;
    [SerializeField] private AnimationCurve curve;
    [SerializeField]private float jumpOffset;
    [SerializeField] private LayerMask groundMask;
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector3 overlapCirclePosition = groundColliderTransform.position;
        isGrounded = Physics2D.OverlapCircle(overlapCirclePosition, jumpOffset, groundMask);
    }

    public void Move (float direction, bool isJumpButtonPressed)
    {
        if(isJumpButtonPressed)
            Jump();

        if (MathF.Abs(direction) > 0.01f)
        HorizontalMovement(direction);
    }

    private void Jump()
    {
        if(isGrounded)
                rb.velocity = new Vector2(rb.velocity.x, jumpForce) ;
    }

    private void HorizontalMovement(float direction)
    {
        rb.velocity= new Vector2(curve.Evaluate(direction),rb.velocity.y);
    }
}
