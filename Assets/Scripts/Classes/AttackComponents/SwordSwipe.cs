using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class SwordSwipe : AAttackProperty
{

    [SerializeField] public float feedbackStrength = 10;

    [SerializeField] public float hitBoxRadius = 1.8f;

    #region Animation Events
    public void DetectHits()
    {

        RaycastHit2D[] hitArray;

        // Do a Physics Cast depending in attack type
        hitArray = Physics2D.CircleCastAll(hitboxCenter.localToWorldMatrix.GetPosition(), hitBoxRadius, Vector2.zero, 0, hitMask);


        // No Objects Hit
        if (hitArray == null || hitArray.Length == 0) return;

        // Call Damage function on all hit objects
        foreach (RaycastHit2D hit in hitArray)
        {
            hit.collider.gameObject.GetComponent<IDamageable>()?.TakeDamage(damage);
        }

        FeedBackForce();
    }
    #endregion


    private void FeedBackForce()
    {
        Vector2 calculatedDirection = Vector2.right;
        calculatedDirection.x *= transform.localScale.x;/*
        rb2D.velocity = calculatedDirection.normalized * currentAttack.FeedBackStrength;
        */
        rb2D.AddForce(calculatedDirection.normalized * feedbackStrength, ForceMode2D.Impulse);
    }
}
