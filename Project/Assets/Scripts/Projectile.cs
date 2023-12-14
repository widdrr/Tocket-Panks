using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    public TankBehaviour owner;

    public delegate void OnHit(Collider2D other);

    public OnHit onHit;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        transform.up = _rigidbody.velocity.normalized;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        onHit(other);
        Destroy(gameObject);
    }
}
