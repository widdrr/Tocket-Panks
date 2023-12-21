using UnityEngine;

public class TankBehaviour : MonoBehaviour
{
    [SerializeField] [Range(0f, 360f)] private float _angle;

    public float Angle
    {
        get { return _angle; }
        set { _angle = Mathf.Repeat(value, 360f); }
    }

    [SerializeField] [Range(0f, 100f)] private float _power;

    public float Power
    {
        get { return _power; }
        set { _power = Mathf.Clamp(value, 0, 100); }
    }

    public delegate void OnShotFired();
    public delegate void OnShotLanded(Collider2D other);

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
        OnProjectileFired();

        var projectile = Instantiate(_weapon, _firingPoint.position, Quaternion.identity);

        projectile.owner = this;
        projectile.transform.up = _firingPoint.right;
        projectile.onHit += other => OnProjectileHit(other);
        projectile.GetComponent<Rigidbody2D>().velocity = projectile.transform.up * _power / 5;
    }
}