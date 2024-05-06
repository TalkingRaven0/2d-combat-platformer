using System;
using System.Numerics;
using UnityEngine;

public interface IDamageable
{
    public Rigidbody2D rb2D { get; set; }
    public void TakeDamage(float damage);
}
