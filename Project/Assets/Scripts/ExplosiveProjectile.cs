using UnityEngine;

public class ExplosiveProjectile : Projectile
{
    [SerializeField]
    private Explosion _explosionPrefab;

    protected override void OnHit(Collider2D other)
    {
        var explosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        explosion.owner = owner;
    }
}
