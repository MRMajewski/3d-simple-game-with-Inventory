using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] private Transform target;
    public Transform Target { get => target; set => target = value; }

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private List<Animator> allEnemyAnimators;
    [SerializeField] private Animator enemyAnimator; 

    private bool isAttacking = false; 

    private void Start()
    {
        Patrol();
    }

    private void Update()
    {
        if (isAttacking) return; 

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
        UpdateAnimation();
    }

    private void Patrol()
    {
        Vector3 randomPoint = RandomNavMeshPoint(transform.position, wanderRadius);
        agent.speed = Random.Range(normalSpeed - 1f, normalSpeed + 1);
        agent.SetDestination(randomPoint);
        UpdateMovementSpeed(agent.speed);
    }

    private void UpdateAnimation()
    {
        float speed = agent.velocity.magnitude;
        foreach (Animator animator in allEnemyAnimators)
        {
            if (animator.gameObject.activeSelf)
                animator.SetFloat("Speed", speed);
        }
    }

    public void UpdateMovementSpeed(float newSpeed)
    {
        if (agent != null)
        {
            newSpeed = Mathf.Max(newSpeed, 2);
            agent.speed = newSpeed;
            agent.acceleration = agent.speed * 2f;

            float walkingSpeed = Mathf.Max(newSpeed / 3f, 1f);

            foreach (Animator animator in allEnemyAnimators)
            {
                if (animator.gameObject.activeSelf)
                {
                    animator.SetFloat("WalkingSpeed", walkingSpeed);
                }
            }
        }
    }

    private void ChasePlayer()
    {
        agent.speed = Random.Range(chaseSpeed - 1f, chaseSpeed + 1);
        agent.SetDestination(target.position);
        UpdateMovementSpeed(agent.speed);
    }

    private void AttackPlayer()
    {
        if (isAttacking) return;

        isAttacking = true;
        agent.isStopped = true; 

        foreach (Animator enemyAnimator in allEnemyAnimators)
        {
            enemyAnimator.SetTrigger("Attack");
        }
        StartCoroutine(AttackAndDestroy());
    }

    private IEnumerator AttackAndDestroy()
    {
        yield return new WaitForSeconds(1.0f); 

        PlayerStatsManager playerStats = target.GetComponent<PlayerStatsManager>();
        if (playerStats != null)
        {
            playerStats.TakeDamage(damage);
        }
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
