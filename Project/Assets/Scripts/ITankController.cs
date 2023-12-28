
public interface ITankController
{
    public TankBehaviour TankBehaviour { get; }

    public void StartTurn();

    public void EndTurn();

    public void GameEnd(int selfScore, int opponentScore);
    
    public void GameStart();
}