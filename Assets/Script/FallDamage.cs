using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDamage : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private CheckPoint checkPoint;
    [SerializeField] private float damageHit;
    [SerializeField] private float safeTime = 1f;
    private float safeTimeCounter;
    private void Start()
    {
        checkPoint = GameObject.FindGameObjectWithTag("Player").GetComponent<CheckPoint>();
        safeTimeCounter = safeTime;
    }
    private void Update()
    {
        safeTimeCounter -= Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerHealth = collision.GetComponent<PlayerHealth>();
            if (safeTimeCounter <= 0 )
            {
                playerHealth.Damaged(damageHit);
            }
            safeTimeCounter = safeTime;
            checkPoint.WarpPlayertoSafeGround();
        }
    }
}
