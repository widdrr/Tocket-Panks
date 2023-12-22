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

    public void StartTurn()
    {
        _tankBehaviour.Shoot();
    }
}
