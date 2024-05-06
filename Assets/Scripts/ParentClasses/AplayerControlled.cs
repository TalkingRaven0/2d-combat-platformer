using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]

public class AplayerControlled : MonoBehaviour
{
    [SerializeField] public bool BlockInput = false;
    public virtual void OnMove(InputValue value)
    {
        return;
    }

    public virtual void OnAttackA(InputValue value)
    {
        return;
    }

    public virtual void OnAttackB(InputValue value)
    {
        return;
    }

    public virtual void OnModifierA(InputValue value)
    {
        return;
    }

    public virtual void OnModifierB(InputValue value)
    {
        return;
    }

    public virtual void OnAtttack1(InputValue value)
    {
        return;
    }
    public virtual void OnAtttack2(InputValue value)
    {
        return;
    }
    public virtual void OnAtttack3(InputValue value)
    {
        return;
    }
    public virtual void OnJump(InputValue value)
    {
        return;
    }
}
