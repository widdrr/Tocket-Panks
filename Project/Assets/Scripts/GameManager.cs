using UnityEngine;

public enum GameState
{
    Player1Turn,
    Player2Turn,
    Shot
}
public class GameManager : MonoBehaviour
{

    [SerializeField]
    private TankController _player1;

    [SerializeField]
    private TankController _player2;

    private GameState _state;
    private GameState _nextTurn;

    private void Awake()
    {
        _state = GameState.Player1Turn;
        _player2.enabled = false;
    }

    public void ChangeTurn()
    {
        switch ( _state)
        {
            case GameState.Player1Turn: 
                _nextTurn = GameState.Player2Turn;
                _state = GameState.Shot;
                _player1.enabled = false;
                break;

            case GameState.Player2Turn:
                _nextTurn = GameState.Player1Turn;
                _state = GameState.Shot;
                _player2.enabled = false;
                break;

            case GameState.Shot:
                _state = _nextTurn;
                if(_state == GameState.Player1Turn) _player1.enabled = true;
                else _player2.enabled = true;
                break;
        }
    }


}
