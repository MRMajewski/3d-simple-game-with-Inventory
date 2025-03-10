using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float wanderRadius = 5f;
    [SerializeField] private float chaseRange = 7f;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private float normalSpeed = 3.5f;
    [SerializeField] private float chaseSpeed = 5.5f;
    [SerializeField] private int damage = 10;

    [SerializeField]
    private Transform target;

    public Transform Target
    {
        get => target;
        set => target = value;
    }
    [SerializeField]
    private NavMeshAgent agent;

    private void Start()
    {
        Patrol();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, target.position);

        if (distanceToPlayer <= attackRange)
        {
            AttackPlayer();
        }
        else if (distanceToPlayer <= chaseRange)
        {
            ChasePlayer();
        }
        else if (!agent.hasPath || agent.remainingDistance < 0.5f)
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        Vector3 randomPoint = RandomNavMeshPoint(transform.position, wanderRadius);
        agent.speed = normalSpeed;
        agent.SetDestination(randomPoint);
    }

    private void ChasePlayer()
    {
        agent.speed = chaseSpeed;
        agent.SetDestination(target.position);
    }

    private void AttackPlayer()
    {
        PlayerStatsManager playerStats = target.GetComponent<PlayerStatsManager>();
        //here methods
        Destroy(gameObject); 
    }

    private Vector3 RandomNavMeshPoint(Vector3 origin, float distance)
    {
        Vector3 randomDirection = Random.insideUnitSphere * distance;
        randomDirection += origin;
        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, distance, NavMesh.AllAreas))
        {
            return hit.position;
        }
        return origin;
    }
}
