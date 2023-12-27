using UnityEngine;

public enum GameState
{
    Player1Turn,
    Player2Turn,
    Shot,
    Over,
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _player1;
    private ITankController _player1Controller;
    private TankScore _player1Score;

    [SerializeField] private GameObject _player2;
    private ITankController _player2Controller;
    private TankScore _player2Score;

    [SerializeField] private int _rounds;
    public int _currentTurn = 0;

    public GameState _state;

    private GameState _nextTurn;

    private void Awake()
    {
        _player1Controller = _player1.GetComponent<ITankController>();
        _player2Controller = _player2.GetComponent<ITankController>();

        _player1Score = _player1.GetComponent<TankScore>();
        _player2Score = _player2.GetComponent<TankScore>();

        _player1Controller.TankBehaviour.OnProjectileFired += ChangeState;
        _player2Controller.TankBehaviour.OnProjectileFired += ChangeState;

        _player1Controller.TankBehaviour.OnProjectileHit += (_, _) => ChangeState();
        _player2Controller.TankBehaviour.OnProjectileHit += (_, _) => ChangeState();

        GameStart();
    }

    public void ChangeState()
    {
        switch (_state)
        {
            case GameState.Player1Turn:
                _nextTurn = GameState.Player2Turn; //Remember to change this back to 2
                _player1Controller.EndTurn();
                _state = GameState.Shot;
                break;

            case GameState.Player2Turn:
                _nextTurn = GameState.Player1Turn;
                _player2Controller.EndTurn();
                _state = GameState.Shot;
                break;

            case GameState.Shot:
                ++_currentTurn;
                if (_currentTurn == 2 * _rounds)
                {
                    _state = GameState.Over;
                    GameEnd();
                    break;
                }

                _state = _nextTurn;
                if (_state == GameState.Player1Turn)
                {
                    _player1Controller.StartTurn();
                }
                else
                {
                    _player2Controller.StartTurn();
                }

                break;
        }
    }

    private void GameStart()
    {
        _state = GameState.Player1Turn;
        _currentTurn = 0;
        _player1Controller.StartTurn();
        _player2Controller.EndTurn();
    }

    private void GameEnd()
    {
        _player1Controller.GameEnd(_player1Score.Score, _player2Score.Score);
        _player2Controller.GameEnd(_player2Score.Score, _player1Score.Score);

        _player1Score.ResetScore();
        _player2Score.ResetScore();

        GameStart();
    }
}