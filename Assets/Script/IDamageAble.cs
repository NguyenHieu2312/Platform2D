using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageAble
{
    public void Damaged(float damageAmount);
    public bool HasTakenDamage { get; set; }
}
