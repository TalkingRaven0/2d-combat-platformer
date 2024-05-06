using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
[RequireComponent (typeof(Rigidbody2D))]

public class PlayerVisuals : AplayerControlled
{

    #region Animator Parameter Names
    private const String animatorMoving = "IsMoving";
    private const String animatorRising = "IsRising";
    private const String animatorFalling = "IsFalling";
    #endregion

    #region Component References
    private Animator animator;
    private Rigidbody2D rigidBody2D;
    #endregion

    private float currentFacing = 1;
    [SerializeField] private bool DisableTurning = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        #region Animation States
        // Manage Animation States Rise and Fall
        if (rigidBody2D.velocity.y > 0.15f)
        {
            animator.SetBool(animatorRising, true);
            animator.SetBool(animatorFalling, false);
        }
        else if (rigidBody2D.velocity.y < -0.15f)
        {
            animator.SetBool(animatorRising, false);
            animator.SetBool(animatorFalling, true);
        }
        else
        {
            animator.SetBool(animatorRising, false);
            animator.SetBool(animatorFalling, false);
        }

        // Manage Animation States Running
        if (Mathf.Abs(rigidBody2D.velocity.x) > 0.1f) animator.SetBool(animatorMoving, true);
        else animator.SetBool(animatorMoving, false);
        #endregion

        if(currentFacing != transform.localScale.x && !DisableTurning) transform.localScale = new Vector2(currentFacing,transform.localScale.y);
    }

    #region Input Handlers

    public override void OnMove(InputValue value)
    {
        // For Selectively Disabling player Input per component
        if (BlockInput) return;

        // Checking if value is Null
        Vector2 assigner;
        assigner = (value == null) ? Vector2.zero : value.Get<Vector2>();
        if (assigner.x == 0) return;

        currentFacing = assigner.x*-1;
    }

    #endregion
}
