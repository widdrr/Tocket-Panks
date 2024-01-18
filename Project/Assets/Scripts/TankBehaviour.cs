using System;
using UnityEngine;

public class TankBehaviour : MonoBehaviour
{
    [SerializeField]
    [Range(0f, 360f)]
    private float _angle;
    public float Angle
    {
        get { return _angle; }
        set { _angle = Mathf.Repeat(value, 360f); }
    }

    [SerializeField]
    [Range(0f, 100f)]
    private float _power;
    public float Power
    {
        get { return _power; }
        set { _power = Mathf.Clamp(value, 0, 100); }
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
        other.GetComponent<Rigidbody2D>().totalTorque = 0;
    }
}