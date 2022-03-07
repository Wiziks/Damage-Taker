using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Right,
    Left
}

public class Aircraft : MonoBehaviour
{
    [Header("Aircraft")]
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private Transform target;
    [SerializeField] private float _speed;
    [SerializeField] private float _timeToMad;

    [Header("Bullet")]
    [SerializeField] private AircraftBullet _bullet;
    [SerializeField] private float _startSpeed;
    private Direction current;
    private float timer;
    public static Aircraft Instance;

    void Start()
    {
        Instance = this;
        this.enabled = false;
    }

    void Update()
    {
        if (transform.position.x > target.position.x)
        {
            _sprite.flipX = true;
            current = Direction.Left;
        }
        else if (transform.position.x < target.position.x)
        {
            _sprite.flipX = false;
            current = Direction.Right;
        }

        if (current == Direction.Right)
        {
            RaycastHit2D hitRight1 = Physics2D.Raycast(transform.position + Vector3.up * 0.45f, Vector2.right, 1f);
            RaycastHit2D hitRight2 = Physics2D.Raycast(transform.position - Vector3.up * 0.45f, Vector2.right, 1f);
            Debug.DrawRay(transform.position + Vector3.up * 0.45f, Vector2.right, Color.green);
            Debug.DrawRay(transform.position - Vector3.up * 0.45f, Vector2.right, Color.green);
            Move(hitRight1, hitRight2);
        }
        else if (current == Direction.Left)
        {
            RaycastHit2D hitLeft1 = Physics2D.Raycast(transform.position + Vector3.up * 0.45f, Vector2.left, 1f);
            RaycastHit2D hitLeft2 = Physics2D.Raycast(transform.position - Vector3.up * 0.45f, Vector2.left, 1f);
            Debug.DrawRay(transform.position + Vector3.up * 0.45f, Vector2.left, Color.green);
            Debug.DrawRay(transform.position - Vector3.up * 0.45f, Vector2.left, Color.green);
            Move(hitLeft1, hitLeft2);
        }
    }

    void Move(RaycastHit2D hit1, RaycastHit2D hit2)
    {
        if (_bullet.gameObject.activeSelf) return;

        if (hit1 || hit2)
        {
            if (hit1)
            {
                if (hit1.collider.GetComponent<CircleSelector>()) return;
            }
            else if (hit2)
            {
                if (hit2.collider.GetComponent<CircleSelector>()) return;
            }
            AvoidObstacle();

            timer += Time.deltaTime;
            if (timer > _timeToMad)
                Shot();
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);
            timer = 0;
        }
    }

    void AvoidObstacle()
    {
        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, Vector2.up, 1f);
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down, 1f);
        Debug.DrawRay(transform.position, Vector2.up, Color.red);
        Debug.DrawRay(transform.position, Vector2.down, Color.red);
        if (!hitUp)
        {
            transform.Translate(Vector2.up * _speed * Time.deltaTime);
        }
        else if (!hitDown)
        {
            transform.Translate(Vector2.down * _speed * Time.deltaTime);
        }
        else
        {
            Shot();
        }
    }

    void Shot()
    {
        Vector2 direction = Vector2.right;
        if (current == Direction.Left)
            direction = Vector2.left;
        _bullet.Setup(direction, _startSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

    }
}
