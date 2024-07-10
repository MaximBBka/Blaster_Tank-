using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class PetDelivery : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    private LayerMask ground;
    private Vector2 direction;
    [SerializeField] private float speed;
    private void Start()
    {
        ground = 1 << LayerMask.NameToLayer("Ground");
        direction = Vector2.right;
    }
    public void Move(Vector2 direction)
    {
        rb.velocity = direction * speed;       
    }
    public void Find()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 0.4f, ground);
        if (hit.collider == null)
        {
            return;
        }
        if (hit.collider != null)
        {
            direction.x *= -1;
        }
    }
    private void Update()
    {
        Move(direction);
        Find();
    }
}
