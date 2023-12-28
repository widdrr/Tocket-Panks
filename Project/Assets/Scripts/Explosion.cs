using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public TankBehaviour owner;

    [SerializeField] private int _baseDamage;
    [SerializeField] private ContactFilter2D _targets;  
    [SerializeField] private float _duraton;

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
        yield return new WaitForSeconds(_duraton);
        Destroy(gameObject);
        owner.OnProjectileEffectEnd();
    }

    public int ComputeDamage(float distance)
    {
        return _baseDamage - Mathf.CeilToInt(distance / _intervalRadius) + 1;
    }
}
