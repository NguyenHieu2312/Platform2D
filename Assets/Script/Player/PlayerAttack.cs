using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform attackTranform;
    [SerializeField] private float attackRange = .65f;
    [SerializeField] private LayerMask attackAbleLayer;
    [SerializeField] private float damageAmount = 2f;

    [SerializeField] private float timebtwAttack = 0.2f;

    private List<IDamageAble> iDamageable = new List<IDamageAble>();

    private RaycastHit2D[] hit;
    private Animator anim;
    private float timeAttackCounter;
    public bool canBeDamage { get; private set; } = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (UserInput.instance.controls.Player.Attack.WasPressedThisFrame() && timeAttackCounter >= timebtwAttack)
        {
            timeAttackCounter = 0f;
            anim.SetTrigger("attack");
        }
        timeAttackCounter += Time.deltaTime;
    }


    public IEnumerator DamageWhileSlashActive()
    {
        canBeDamage = true;
        while (canBeDamage)
        {
            hit = Physics2D.CircleCastAll(attackTranform.position, attackRange, transform.right, 0f, attackAbleLayer);

            for (int i = 0; i < hit.Length; i++)
            {
                IDamageAble iDamage = hit[i].collider.gameObject.GetComponent<IDamageAble>();

                if (iDamage != null && !iDamageable.Contains(iDamage))
                {
                    //DMG here...
                    iDamage.Damaged(damageAmount);
                    iDamageable.Add(iDamage);
                }
            }

            yield return null;
        }

        ReturnAttackAbleToDamage();
    }

    void ReturnAttackAbleToDamage()
    {
        foreach (IDamageAble ThatWasDamaged in iDamageable)
        {
            ThatWasDamaged.HasTakenDamage = false;
        }
        iDamageable.Clear();
    }

    void CanBeDamageTrue()
    {
        canBeDamage = true;
    }

    void CanBeDamageFalse()
    {
        canBeDamage = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackTranform.position, attackRange);
    }
}
