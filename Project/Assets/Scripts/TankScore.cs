using System;
using UnityEngine;

public enum PlayerTags
{
    Player1,
    Player2
}

public class TankScore : MonoBehaviour
{
    public int Score { get; private set; }

    [SerializeField]
    private TankBehaviour _tank;

    [SerializeField]
    private PlayerTags _selfTag;

    [SerializeField]
    private PlayerTags _opponentTag;

    private void Awake()
    {
        _tank.OnProjectileHit += AddScore;
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
}
