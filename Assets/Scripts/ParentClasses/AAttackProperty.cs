using System;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerMelee))]
public abstract class AAttackProperty : MonoBehaviour
{
    [SerializeField] public float damage;
    [SerializeField] public string triggerName;

    [NonSerialized] public Rigidbody2D rb2D;
    [NonSerialized] public PlayerMelee attackManager;
    [SerializeField] public Transform hitboxCenter;
    [SerializeField] public LayerMask hitMask;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        attackManager = GetComponent<PlayerMelee>();
    }

    public virtual void AttackStart() 
    {
        attackManager.startAttackCallback();
    }
    public virtual void AttackEnd()
    {
        attackManager.endAttackCallback();
    }
}
