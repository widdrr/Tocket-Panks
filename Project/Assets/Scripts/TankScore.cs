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
        _tank.OnProjectileHit += (_, other) => AddScore(other);
    }

    public void AddScore(Collider2D other)
    {
        if(other.gameObject.CompareTag(_opponentTag.ToString()))
        {
            Score += 1;
        }
        else if (other.gameObject.CompareTag(_selfTag.ToString()))
        {
            Score -= 2;
        }
    }

    public void ResetScore()
    {
        Score = 0;
    }
}
