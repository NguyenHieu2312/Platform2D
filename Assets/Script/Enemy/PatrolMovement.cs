using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolMovement : MonsterMovement
{
    const string LEFT = "left";
    const string RIGHT = "right";

    string facingDirection;
    private Vector3 baseScale;

    [SerializeField] private MonterData _monterData;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] Transform _rCast;
    [SerializeField] private float _baseCastDistance;

    private Animator _anim;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        baseScale = transform.localScale;
        facingDirection = RIGHT;
    }

    private void FixedUpdate()
    {
        Move();
    }

    public override void Flip()
    {
        if (facingDirection == LEFT)
        {
            ChangeFacingDirection(RIGHT);
        }else if (facingDirection == RIGHT)
        {
            ChangeFacingDirection(LEFT);
        }
    }

    void ChangeFacingDirection(string newDirection)
    {
        Vector3 newScale = baseScale;
        if (newDirection == LEFT)
        {
            newScale.x = -baseScale.x;
        }else if (newDirection == RIGHT)
        {
            newScale.x = baseScale.x;
        }

        transform.localScale = newScale;

        facingDirection = newDirection;
    }

    public override void Move()
    {
        float moveX = _monterData._monsterSpeed;
        if (facingDirection == LEFT)
        {
            moveX = -_monterData._monsterSpeed;
        }
        _rb.velocity = new Vector2(moveX, _rb.velocity.y);
        _anim.SetBool("isMoving", true);

        if (IsHitWall() || IsOutOfMoveRange())
        {
            Flip();
        }
    }

    public virtual bool IsHitWall()
    {
        bool val = false;
        float castDistance = _baseCastDistance;

        if (facingDirection == LEFT)
        {
            castDistance = -_baseCastDistance;
        }
        else
        {
            castDistance = _baseCastDistance;
        }

        Vector3 targetPosition = _rCast.position;
        targetPosition.x += castDistance;

        if (Physics2D.Linecast(_rCast.position, targetPosition, 1 << LayerMask.NameToLayer("Ground")))
        {
            val = true;
        }
        else
        {
            val = false;
        }

        return val;
    }

    public virtual bool IsOutOfMoveRange()
    {
        bool val = true;
        float castDistance = _baseCastDistance;

        Vector3 targetPosition = _rCast.position;
        targetPosition.y -= castDistance;

        Debug.DrawLine(_rCast.position, targetPosition, Color.green);

        if (Physics2D.Linecast(_rCast.position, targetPosition , 1 << LayerMask.NameToLayer("Ground")))
        {
            val = false;
        }
        else
        {
            val = true;
        }

        return val;
    }
}
