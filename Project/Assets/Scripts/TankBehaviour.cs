using System;
using UnityEngine;

public class TankBehaviour : MonoBehaviour
{
    [SerializeField][Range(0, 359)] private int _angle;

    public int Angle
    {
        get { return _angle; }
        set { _angle = value % 360; }
    }

    [SerializeField][Range(0, 100)] private int _power;

    public int Power
    {
        get { return _power; }
        set { _power = Math.Clamp(value, 0, 100); }
    }

    public delegate void OnShotFired();
    public delegate void OnShotLanded(Projectile projectile, Collider2D other);

    public OnShotFired OnProjectileFired;
    public OnShotLanded OnProjectileHit;

    [SerializeField] private Transform _head;

    [SerializeField] private Transform _firingPoint;

    [SerializeField] private Projectile _weapon;

    private void Update()
    {
        SetTurret();
    }

    private void SetTurret()
    {
        _head.rotation = Quaternion.Euler(0f, 0f, _angle);
    }

    public void Shoot()
    {
        SetTurret();
        OnProjectileFired();

        var projectile = Instantiate(_weapon, _firingPoint.position, Quaternion.identity, transform);

        projectile.owner = this;
        projectile.transform.up = _firingPoint.right;
        projectile.onHit += (projectile, other) => OnProjectileHit(projectile, other);
        projectile.GetComponent<Rigidbody2D>().velocity = projectile.transform.up * _power / 5;
    }
}