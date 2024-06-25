using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private LayerMask checkPoint;
    public Vector2 safegroundlocation { get; private set; } = Vector2.zero;
    private Collider2D coll;
    private float safeOffSetY;

    private void Start()
    {
        safegroundlocation = transform.position;

        coll = GetComponent<Collider2D>();

        safeOffSetY = (coll.bounds.size.y) / 2;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((checkPoint.value & (1 << collision.gameObject.layer)) > 0)
        {
            safegroundlocation = new Vector2(collision.bounds.center.x, collision.bounds.min.y + safeOffSetY);
        }
    }

    public void WarpPlayertoSafeGround()
    {
        transform.position = safegroundlocation;
    }
}
