using System;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class TankAgent : Agent, ITankController
{
    [SerializeField] private TankBehaviour _tank;
    [SerializeField] private Transform _target;

    public TankBehaviour TankBehaviour { get => _tank; }

    void Awake()
    {
        _tank.OnProjectileHit += (projectile, other) =>
        {
            if (other.CompareTag("OutOfBounds") || other.CompareTag("Player1"))
            {
                AddReward(-1);
            }
            else
            {
                var dist = Vector3.Distance(other.ClosestPoint(projectile.transform.position), _target.position);
                AddReward(Mathf.Exp((-dist + 0.5f)*0.2f));
            }
            EndEpisode();
        };
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     var discreteActions = actionsOut.DiscreteActions;
        //     discreteActions[0] = 1;
        // }
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        _tank.Angle = actions.DiscreteActions[0];
        _tank.Power = actions.DiscreteActions[1];

        _tank.Shoot();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        var relativePos = _target.transform.position - transform.position;
        sensor.AddObservation(relativePos);
    }

    public override void OnEpisodeBegin()
    {
    }

    public void StartTurn()
    {
        Debug.Log("Action");
        RequestDecision();
    }

    public void EndTurn() { }

    public void GameEnd(int selfScore, int opponentScore)
    {
        //if(selfScore > opponentScore)
        //{
        //    SetReward(1);
        //}
        //else if(selfScore == opponentScore)
        //{
        //    SetReward(0);
        //}
        //else if(selfScore < opponentScore)
        //{
        //    SetReward(-1);
        //}
        //EndEpisode();
    }

    public void GameStart()
    {
        return;
    }
}