using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    public delegate void OnHit();

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
        onHit();
        Destroy(gameObject);
    }
}
