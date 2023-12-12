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
    private PlayerTags _selfTag;

    [SerializeField]
    private PlayerTags _opponentTag;

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
