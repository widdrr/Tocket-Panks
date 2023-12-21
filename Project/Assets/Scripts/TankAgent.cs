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
        _tank.OnProjectileHit += other =>
        {
            var dist = Vector3.Distance(other.transform.position, _target.position);
            Debug.Log(dist + " " + _tank.gameObject.name);
            SetReward(-dist);
            EndTurn();
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
        _tank.Angle = actions.ContinuousActions[0] * 360f;
        _tank.Power = actions.ContinuousActions[1] * 100f;

        // System.Threading.Thread.Sleep(500);
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

    public void EndTurn()
    {
        EndEpisode();
    }
}