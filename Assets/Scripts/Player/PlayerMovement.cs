using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private List<Animator> allPlayerAnimators;

    private void Update()
    {
        if (!GameController.Instance.IsGameActive())
            return;
        HandleMovement();
        UpdateAnimationSpeed();
    }

    private void HandleMovement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                agent.SetDestination(hit.point);
            }
        }
    }

    private void UpdateAnimationSpeed()
    {
        float speed = agent.velocity.magnitude;

        foreach (Animator animator in allPlayerAnimators)
        {
            if (animator.gameObject.activeSelf)
                animator.SetFloat("Speed", speed);
        }
    }
    public void StopPlayerMovement()
    {
        agent.isStopped = true;  
        agent.ResetPath();
        agent.velocity = Vector3.zero;

        foreach (Animator animator in allPlayerAnimators)
        {
            if (animator.gameObject.activeSelf)
            {
                animator.SetFloat("Speed", 0);
                animator.Play("Idle", 0, 0f);
            }       
        }
    }
    public void UpdateMovementSpeed(float newSpeed)
    {
        if (agent != null)
        {
            newSpeed = Mathf.Max(newSpeed, 3);

            agent.speed = newSpeed;
            agent.acceleration = agent.speed * 2f;

            float walkingSpeed = Mathf.Max(newSpeed / 3.85f, 1f);

            foreach (Animator animator in allPlayerAnimators)
            {
                if (animator.gameObject.activeSelf)
                {
                    animator.SetFloat("WalkingSpeed", walkingSpeed);
                }
            }
        }
    }
}
