using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Projectile : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;

    public TankBehaviour owner;

    protected void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    protected void Update()
    {
        transform.up = _rigidbody.velocity.normalized;
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.activeInHierarchy)
        {
            OnHit(other);
            Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }
    protected abstract void OnHit(Collider2D other);
}