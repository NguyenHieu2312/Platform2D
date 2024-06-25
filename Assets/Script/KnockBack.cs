using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class KnockBack : MonoBehaviour
{
    public float _knockBackTime = .2f;
    public float _hitDirectionForce = 10f;
    public float _consForce = 5f;
    public float inputForce = 7f;

    private Rigidbody2D _rb;
    private Coroutine _knockBackCoroutine;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public bool IsKnockBack { get; private set; }


    public IEnumerator KnockBackAction(Vector2 hitDirection, Vector2 constantDirection, float inputDirection)
    {
        IsKnockBack = true;

        Vector2 _hitForce;
        Vector2 _constantForce;
        Vector2 _knockBackForce;
        Vector2 _combieForce;

        _hitForce = hitDirection * _hitDirectionForce;

        _constantForce = constantDirection * _consForce;



        float _elapsedTime = 0f;
        while (_elapsedTime < _knockBackTime)
        {
            _elapsedTime += Time.fixedDeltaTime;

            _knockBackForce = _hitForce + _constantForce;

            if (inputDirection != 0)
            {
                _combieForce = _knockBackForce + new Vector2(inputDirection, 0f);
            }
            else
            {
                _combieForce = _knockBackForce;
            }

            _rb.velocity = _combieForce;
            yield return new WaitForFixedUpdate();
        }

        IsKnockBack = false;
    }
}
