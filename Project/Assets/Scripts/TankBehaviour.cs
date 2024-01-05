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

    public Delegates.VoidDelegate OnProjectileFired;
    public Delegates.HitDelegate  OnOutOfBounds;
    public Delegates.VoidDelegate OnProjectileEffectEnd;
    public Delegates.ExplosionDelegate OnExplosionEffect;

    [SerializeField] private Transform _head;

    [SerializeField] private Transform _firingPoint;

    [SerializeField] private Projectile _weapon;


    private void Awake()
    {
        OnExplosionEffect += ApplyKnockback;
    }

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
        projectile.GetComponent<Rigidbody2D>().velocity = projectile.transform.up * _power / 5;
    }

    private static void ApplyKnockback(Explosion explosion, Collider2D other)
    {
        var force = explosion.ComputeKnockback(other.transform.position);

        other.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
    }
}