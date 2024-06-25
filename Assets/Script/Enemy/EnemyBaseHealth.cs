using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseHealth : MonoBehaviour, IDamageAble
{
    
    [SerializeField] protected MonterData _mData;
    private float currentHealth;
    public EHealthBar healthBar;
    private Animator anim;

    public bool HasTakenDamage { get; set; }


    private void Start()
    {
        currentHealth = _mData._monsterHealth;
        anim = GetComponent<Animator>();
    }

    public void Damaged(float damageAmount)
    {
        HasTakenDamage = true;
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        healthBar.UpdateHealthBar(currentHealth, _mData._monsterHealth);
    }
    public void Die()
    {
        Destroy(gameObject);
    }
}
