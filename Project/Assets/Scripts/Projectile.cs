using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    public TankBehaviour owner;

    public delegate void OnHit(Projectile self, Collider2D other);

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
        if (gameObject.activeInHierarchy)
        {
            Debug.Log("Hit");
            onHit(this, other);
            Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }
}