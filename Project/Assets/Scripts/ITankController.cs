using UnityEngine;

public interface ITankController
{
    public TankBehaviour TankBehaviour { get; }

    public void StartTurn();

    public void EndTurn();
}