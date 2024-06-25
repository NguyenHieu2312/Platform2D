using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPlayer : MonoBehaviour
{
    [SerializeField] private MonterData _monsterData;
    [SerializeField] private float attackRange;
    [SerializeField] private float lineOfSight;
    [SerializeField] private Transform _spawnOBJ;

    private Transform player;
    private Animator _anim;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        MoveToFindPlayer();
    }

    private void MoveToFindPlayer()
    {
        float distanceToPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceToPlayer < lineOfSight && distanceToPlayer > attackRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, _monsterData._monsterSpeed * Time.deltaTime);
        }
        else if (distanceToPlayer > lineOfSight)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, _spawnOBJ.position, _monsterData._monsterSpeed * Time.deltaTime);
        }
        _anim.SetBool("isWalking", true);

        if (distanceToPlayer <= attackRange)
        {
            AttackPlayer();
            _anim.SetBool("isWalking", false);
        }
        else
            _anim.SetBool("isAttack", false);

        if (this.transform.position == _spawnOBJ.position)
            _anim.SetBool("isWalking", false);

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private void AttackPlayer()
    {
        //
        _anim.SetBool("isAttack", true);
    }
}
