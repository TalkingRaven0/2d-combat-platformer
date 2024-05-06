using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerMovement))]

public class PlayerMelee : AplayerControlled
{
    enum InputTypes
    {
        none, AttackA, AttackB
    }

    [SerializeField] private Transform HitBoxCenter;
    [SerializeField] private LayerMask HittableMask;

    #region Input Buffering Variables
    [SerializeField] private float attackInputBufferTimeSeconds = 0.2f;
    public bool isAttacking { get; set; }
    private InputTypes bufferedInput = InputTypes.none;
    private Coroutine BufferTimer;
    #endregion

    #region Attack Components
        #nullable enable

        private SwordSwipe? SwordSwipeAttack;

        #nullable disable
    #endregion

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        try { 
            SwordSwipeAttack = GetComponent<SwordSwipe>();
            if (!SwordSwipeAttack.enabled) SwordSwipeAttack = null;
        }
        catch { SwordSwipeAttack = null; }
    }
    
    // Coroutine for Clearing the Buffer
    private IEnumerator ClearBufferTimer()
    {
        yield return new WaitForSeconds(attackInputBufferTimeSeconds);
        bufferedInput = InputTypes.none;
    }

    #region Input Handlers
    public override void OnAttackA(InputValue value)
    {
        if(isAttacking)
        {
            bufferedInput = InputTypes.AttackA;
            // Stops the Current Ongoing Buffer Timer
            if (BufferTimer != null) StopCoroutine(BufferTimer);
            BufferTimer = StartCoroutine("ClearBufferTimer");
        } else
        {
            DoAttackA();
        }
    }
    #endregion

    #region Attack Functions
    private void DoAttackA()
    {
        if (SwordSwipeAttack == null) return;
        animator.SetTrigger(SwordSwipeAttack.triggerName);
    }
    private void DoAttackB()
    {
        return;
    }
    #endregion

    #region Interface Attack Manager

    public void startAttackCallback()
    {
        isAttacking = true;
    }

    public void endAttackCallback()
    {
        isAttacking = false;
        if (bufferedInput == InputTypes.none) return;

        StopCoroutine(BufferTimer);
        switch (bufferedInput)
        {
            case InputTypes.AttackA:
                DoAttackA();
                break;
            case InputTypes.AttackB:
                DoAttackB();
                break;
        }

        bufferedInput = InputTypes.none;
    }


    #endregion
}
