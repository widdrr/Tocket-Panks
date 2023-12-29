using System;
using UnityEngine;

public class TankScore : MonoBehaviour
{
    public int Score { get; private set; }

    [SerializeField]
    private TankBehaviour _tank;

    [SerializeField]
    private Tags _selfTag;

    [SerializeField]
    private Tags _opponentTag;

    private void Awake()
    {
        _tank.OnExplosionEffect += (Explosion explosion, Collider2D other) =>
        {
            var contact = other.ClosestPoint(explosion.transform.position);
            float distance = Vector3.Distance(explosion.transform.position, new Vector3(contact.x, contact.y));

            AddScore(explosion.ComputeDamage(distance), other);
        };
    }

    public void AddScore(int score, Collider2D other)
    {
        if(other.gameObject.CompareTag(_opponentTag.ToString()))
        {
            Score += score;
        }
        else if (other.gameObject.CompareTag(_selfTag.ToString()))
        {
            Score -= score;
        }
    }

    public void ResetScore()
    {
        Score = 0;
    }
}
