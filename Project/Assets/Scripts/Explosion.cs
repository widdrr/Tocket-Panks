using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public TankBehaviour owner;

    [SerializeField] private int _baseDamage;
    [SerializeField] private ContactFilter2D _targets;  
    [SerializeField] private float _duration;
    [SerializeField] private float _knockbackStrength;
    [SerializeField] private Vector2 _knockbackBias;
    [SerializeField] private float _maxKnockback;

    private float _intervalRadius;
    private float _radius;

    void Start()
    {
        _radius = GetComponent<CircleCollider2D>().radius * transform.localScale.x;
        _intervalRadius = _radius / _baseDamage;
        
        List<Collider2D> hits = new();
        Physics2D.OverlapCircle(transform.position, _radius, _targets, hits);
        foreach (var hit in hits)
        {
            owner.OnExplosionEffect(this, hit);
        }

        StartCoroutine(Explode());
    }

    private IEnumerator Explode()
    {
        yield return new WaitForSeconds(_duration);
        Destroy(gameObject);
        owner.OnProjectileEffectEnd();
    }

    public int ComputeDamage(float distance)
    {
        return _baseDamage - Mathf.CeilToInt(distance / _intervalRadius);
    }

    public Vector2 ComputeKnockback(Vector2 point)
    {
        var knockbackDirection = new Vector3(point.x, point.y) - transform.position;
        var bias = new Vector3(_knockbackBias.x, _knockbackBias.y);

        return (knockbackDirection + bias).normalized
                * Mathf.Clamp(1 / knockbackDirection.magnitude * _knockbackStrength, 0, _maxKnockback);
    }
}
