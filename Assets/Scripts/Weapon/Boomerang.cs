using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : Weapon
{
    [SerializeField] private float _speed = 0.1f;
    Vector2 directionVector = Vector2.right;

    void Update()
    {
        if (!Collider.enabled) return;

        ClearDictionary();
        transform.Translate(directionVector * _speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!Collider.enabled) return;

        if (other.collider.GetComponent<Weapon>())
            ChangeDirection();
        else if (other.collider.GetComponent<Wall>())
            ChangeDirection();
    }

    void ChangeDirection()
    {
        if (directionVector == Vector2.right)
            directionVector = Vector2.left;
        else
            directionVector = Vector2.right;
    }
}
