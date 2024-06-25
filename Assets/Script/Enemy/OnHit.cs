using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHit : MonoBehaviour
{
    private PlayerHealth playerHealth;
    [SerializeField] private MonterData _mDataHit;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            InvokeRepeating("SendDamage", 0, .2f);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerHealth = null;
            CancelInvoke("SendDamage");
        }
    }

    private void SendDamage()
    {
        playerHealth.Damaged(_mDataHit._monsterDamage);
        Debug.Log("take damage" + _mDataHit._monsterDamage);
    }
}
