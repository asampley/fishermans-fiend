using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ProjectileManager : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rigidBody;
    [SerializeField]
    private float _speed = 10f;

    private int _attackDamage;
    public int AttackDamage => _attackDamage;
    private Vector2 _travelDirection;


    private void Update()
    {
        transform.position += _speed * Time.deltaTime * transform.right;
    }


    public void Initialize(Vector2 target, int attackDamage, float speed)
    {
        _attackDamage = attackDamage;
        _speed = speed;

        float initAngle = Mathf.Atan2(target.y - transform.position.y, target.x - transform.position.x) * Mathf.Rad2Deg;
        float distance = Vector2.Distance(transform.position, target);
        float newAngleRad = -(initAngle - 90) * Mathf.Deg2Rad;
        float newdirX = distance * Mathf.Sin(newAngleRad);
        float newdirY = distance * Mathf.Cos(newAngleRad);
        Vector2 newTravelDest = new(newdirX + transform.position.x, newdirY + transform.position.y);

        _travelDirection = (newTravelDest - (Vector2)transform.position).normalized;
        transform.eulerAngles = new(0, 0, _GetAngleFromVectorFloat(_travelDirection));
    }

    private float _GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<ICollidable>().Collide(this);
    }
}
