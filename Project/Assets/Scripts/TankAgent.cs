using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TankAgent : Agent, ITankController
{
    [SerializeField] private TankBehaviour _tank;
    [SerializeField] private Transform _target;
    [SerializeField] private Tags _selfTag;
    [SerializeField] private Terrain2D _terrain;

    public TankBehaviour TankBehaviour { get => _tank; }

    void Awake()
    {
        _tank.OnOutOfBounds += _ =>
        {
            //SetReward(-1);
            EndEpisode();
        };

        _tank.OnProjectileEffectEnd += EndEpisode;

        _tank.OnExplosionEffect += (Explosion explosion, Collider2D other) =>
        {

            //if (other.CompareTag(_outOfBoundsTag.ToString()) || other.CompareTag(_selfTag.ToString()))
            //{
            //    AddReward(-1);
            //}
            //else
            //{
            //    var dist = Vector3.Distance(other.ClosestPoint(explosion.transform.position), _target.position);
            //    AddReward(Mathf.Exp((-dist + 0.5f)*0.2f));
            //}

            if (other.CompareTag(_selfTag.ToString()))
            {
                AddReward(-1);
            }
            else
            {
                AddReward(1);
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
        _tank.Angle = actions.DiscreteActions[0];
        _tank.Power = actions.DiscreteActions[1];

        _tank.Shoot();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        var relativePos = _target.transform.position - transform.position;
        sensor.AddObservation(relativePos);

        var highestPoint = _terrain.HighestPointBetween(transform.position, _target.position);
        sensor.AddObservation(highestPoint - transform.position);
    }

    public override void OnEpisodeBegin()
    {
    }

    public void StartTurn()
    {
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