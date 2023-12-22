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

    // Start is called before the first frame update
    void Awake()
    {
        _tank.OnProjectileHit += (projectile, other) =>
        {
            var dist = Vector3.Distance(other.ClosestPoint(projectile.transform.position), _target.position);

            AddReward(Mathf.Clamp(-dist / 8 + 1, -1, 1));
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
        base.OnActionReceived(actions);
        _tank.Angle = actions.ContinuousActions[0] * 180f + 180f;
        _tank.Power = actions.ContinuousActions[1] * 50f + 50f;

        _tank.Shoot();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        base.CollectObservations(sensor);
        sensor.AddObservation(transform.position);
        sensor.AddObservation(_target.position);
    }

    public override void OnEpisodeBegin()
    {
        base.OnEpisodeBegin();
    }

    public void StartTurn()
    {
        RequestDecision();
    }

    public void EndTurn() { }
}