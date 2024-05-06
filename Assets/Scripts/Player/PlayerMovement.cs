using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovement : AplayerControlled
{
    #region Movement Variables
    [Header("Movement Variables")]
    [SerializeField] private float playerSpeed = 9;
    [SerializeField] private float accelerationValue = 9;
    [SerializeField] private float deccelerationValue = 9;
    [SerializeField] private float accelMulti = 1.2f;
    #endregion
    [Space]
    #region Jump Variables
    [Header("Jump Variables")]
    [SerializeField] private float jumpStrength = 10;
    [SerializeField] private float jumpCutMultiplier = 0.4f;
    [SerializeField] private float cayoteTime = 0.2f;
    [SerializeField] private float gravityMultiplier = 2.1f;
    #endregion

    #region Miscellaneous
    [Header("Miscellaneous")]
    [SerializeField] private LayerMask TerrainCheck;
    #endregion

    #region Component References
    private Rigidbody2D rigidBody2D;
    private CapsuleCollider2D capsuleCollider;
    #endregion

    #region State Variables
    private Vector2 inputDir = Vector2.zero;
    private bool isJumping = false;
    private bool isGrounded = false;
    private float lastGroundedTime = 0;
    #endregion

    float originalGravity = 1;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize References
        rigidBody2D = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        originalGravity = rigidBody2D.gravityScale;
    }

    private void Update()
    {
    }

    // Physics Update
    void FixedUpdate()
    {
        SmoothRun();

        GroundCheck();

        JumpCut();

        // Cayote Time Tracker
        lastGroundedTime = isGrounded ? 0 : lastGroundedTime + Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        Gizmos.DrawRay(transform.position, Vector3.down * ((capsuleCollider.size.y / 2) + 0.1f));
    }

    #region Input Handlers

    public override void OnMove(InputValue value)
    {
        // For Selectively Disabling player Input per component
        if (BlockInput) return;

        // Checking if value is Null
        Vector2 assigner;
        assigner = (value == null) ? Vector2.zero : value.Get<Vector2>();

        inputDir = value.Get<Vector2>();
    }

    public override void OnJump(InputValue value)
    {
        // For Selectively Disabling player Input per component
        if (BlockInput) return;

        // Evaluate Input as Boolean
        isJumping = value.Get() != null ? true : false;
        DoJump(isJumping);
    }

    #endregion

    #region Movement Functions

    private void SmoothRun()
    {
        float targetSpeed = inputDir.x * playerSpeed;
        float speedDifference = targetSpeed - rigidBody2D.velocity.x;
        float speedChangeRate = (Mathf.Abs(targetSpeed) > 0.01f ? accelerationValue : deccelerationValue);
        float finalForce = Mathf.Pow(Mathf.Abs(speedDifference) * speedChangeRate, accelMulti) * Mathf.Sign(speedDifference);
        rigidBody2D.AddForce(finalForce * Vector2.right);
    }

    private void DoJump(bool isJumping)
    {
        bool cayoteValidation = lastGroundedTime < cayoteTime;

        if (isJumping){
            rigidBody2D.gravityScale = originalGravity;
        }
        else {
            // Set Original Gravity
            // Capture changes made at runtime
            originalGravity = rigidBody2D.gravityScale;
            rigidBody2D.gravityScale *= gravityMultiplier; 
        }

        if (!cayoteValidation) return;

        if (isJumping) rigidBody2D.AddForce(Vector2.up * jumpStrength, ForceMode2D.Impulse);
    }

    private void JumpCut()
    {
        if (isJumping && rigidBody2D.velocity.y > 0)
        {
            rigidBody2D.AddForce(Vector2.up * rigidBody2D.velocity.y * (1 - jumpCutMultiplier), ForceMode2D.Force);
        }
    }
    #endregion

    private void GroundCheck() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, (capsuleCollider.size.y / 2) + 0.1f, TerrainCheck);
        isGrounded = hit.collider != null;
    }
}
