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

    private int _step = 0;

    // Start is called before the first frame update
    void Awake()
    {
        _tank.OnProjectileHit += (projectile, other) =>
        {
            var dist = Vector3.Distance(other.ClosestPoint(projectile.transform.position), _target.position);

            // AddReward(Mathf.Clamp(-dist / 8 + 1, -1, 1) / 100);

            if (_step >= 1000) {
                Debug.Log("End episode " + _step);
                EndEpisode();
            }
            _step++;
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
        // _tank.Angle = actions.ContinuousActions[0] * 180f + 180f;
        // _tank.Power = actions.ContinuousActions[1] * 50f + 50f;

        _tank.Angle = actions.DiscreteActions[0] == 1 ? 0f : 180f;

        _tank.Shoot();

        if (_tank.Angle == 0f) {
            SetReward(1f);
        }
        else {
            SetReward(-1f);
        }
        // EndEpisode();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        var dist = Vector3.Distance(transform.position, _target.position);
        // sensor.AddObservation(dist / (1f + Math.Abs(dist)));
        sensor.AddObservation(true);
    }

    public override void OnEpisodeBegin()
    {
        Debug.Log($"Begin episode {_step}");
        _step = 0;
    }

    public void StartTurn()
    {
        RequestDecision();
    }

    public void EndTurn() { }
}