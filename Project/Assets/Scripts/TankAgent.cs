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
    private int steps = 0;

    // Start is called before the first frame update
    void Awake()
    {
        _tank.OnProjectileHit += (projectile, other) =>
        {
            var dist = Vector3.Distance(other.ClosestPoint(projectile.transform.position), _target.position);
            SetReward(Mathf.Clamp(-dist / 8 + 1, -1, 1));
            ++steps;
            if (steps >= 100)
            {
                EndEpisode();
            }
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
        _tank.Angle = actions.ContinuousActions[0] * 180f + 180f;
        _tank.Power = actions.ContinuousActions[1] * 50f + 50f;

        _tank.Shoot();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        var dist = Vector3.Distance(transform.position, _target.position) * _target.position.x >= transform.position.x ? 1 : -1;
        sensor.AddObservation(dist / (1f + Math.Abs(dist)));
    }

    public override void OnEpisodeBegin()
    {
        steps = 0;
    }

    public void StartTurn()
    {
        RequestDecision();
    }

    public void EndTurn() { }
}