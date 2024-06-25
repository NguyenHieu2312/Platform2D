using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageAble
{
    [SerializeField] private float _maxHealth = 100f;
    private float _currentHealth;
    public PLayerHealthBar _healthBar;
    private Animator _anim;
    public bool HasTakenDamage { get; set; }

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _currentHealth = _maxHealth;
        _healthBar.UpdateHealthBar(_currentHealth, _maxHealth);
    }

    public void Damaged(float damageAmount)
    {
        HasTakenDamage = true;
        _currentHealth -= damageAmount;
        Debug.Log("Player has taken:" + damageAmount);

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            _anim.Play("Die");
        }
        _healthBar.UpdateHealthBar(_currentHealth, _maxHealth);
    }

    // Call in Animation Event
    public void DestroyGOB()
    {
        Destroy(gameObject);
    }
}
