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

        _state = GameState.Player1Turn;

        _player1Controller.TankBehaviour.OnProjectileFired += ChangeState;
        _player2Controller.TankBehaviour.OnProjectileFired += ChangeState;

        _player1Controller.TankBehaviour.OnProjectileHit += (_, _) => ChangeState();
        _player2Controller.TankBehaviour.OnProjectileHit += (_, _) => ChangeState();

        _player1Controller.StartTurn();
        _player2Controller.EndTurn();
    }

    public void ChangeState()
    {
        switch (_state) {
            case GameState.Player1Turn:
                _nextTurn = GameState.Player2Turn;
                _state = GameState.Shot;
                _player1Controller.EndTurn();
                break;

            case GameState.Player2Turn:
                _nextTurn = GameState.Player1Turn;
                _state = GameState.Shot;
                _player2Controller.EndTurn();
                break;

            case GameState.Shot:
                ++_currentTurn;
                if (_currentTurn == 2 * _rounds) {
                    _state = GameState.Over;
                    GameEnd();
                    break;
                }

                _state = _nextTurn;
                if (_state == GameState.Player1Turn) {
                    _player1Controller.StartTurn();
                }
                else {
                    _player2Controller.StartTurn();
                }

                break;
        }
    }

    public void GameEnd()
    {
        //We will determine exactly what to do here once we integrate with UnityML
        Debug.Log("It's Joever");
        Debug.Log(_player1Score.Score);
        Debug.Log(_player2Score.Score);
    }
}