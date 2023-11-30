using UnityEngine;

public class TankController : MonoBehaviour
{
    [SerializeField]
    [Range(-180f, 180f)]
    private float _angle;
    public float Angle
    {
        get { return _angle; }
        set { _angle = Mathf.Clamp(value, -180f, 180f); }
    }

    [SerializeField]
    [Range(0f,100f)]
    private float _power;
    public float Power
    {
        get { return _power; }
        set { _power = Mathf.Clamp(value, 0, 100); }
    }

    [SerializeField]
    private Transform _head;

    [SerializeField]
    private Transform _firingPoint;

    [SerializeField]
    private Projectile _weapon;

    private void Update()
    {
        SetTurret();
        if (Input.GetKeyDown(KeyCode.F))
        {
            Shoot();
        }
    }

    private void SetTurret()
    {
        _head.rotation = Quaternion.Euler(0f, 0f, _angle);
    }

    public void Shoot()
    {
        var projectile = Instantiate(_weapon, _firingPoint.position, Quaternion.identity);
        projectile.transform.up = _firingPoint.right;
        projectile.GetComponent<Rigidbody2D>().velocity = projectile.transform.up * _power / 5;
    }
}
