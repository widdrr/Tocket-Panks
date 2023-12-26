using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassTurn : MonoBehaviour, ITankController
{
    [SerializeField] private TankBehaviour _tankBehaviour;
    public TankBehaviour TankBehaviour => _tankBehaviour;

    public void EndTurn()
    {
        return;
    }

    public void GameEnd(int selfScore, int opponentScore)
    {
        return;
    }

    public void GameStart()
    {
        return;
    }

    public void StartTurn()
    {
        _tankBehaviour.Power = 0;
        _tankBehaviour.Angle = 270;
        _tankBehaviour.Shoot();
    }
}
