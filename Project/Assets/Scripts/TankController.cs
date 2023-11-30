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

    private void Awake()
    {
        SetTurret();
    }

    private void Update()
    {
        SetTurret();
    }

    private void SetTurret()
    {
        _head.rotation = Quaternion.Euler(0f, 0f, _angle);
    }
}
