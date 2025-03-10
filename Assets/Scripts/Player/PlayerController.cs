using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    private PlayerStatsManager statsManager;

    public void UpdateMovementSpeed()
    {
        if (agent != null)
        {     
            if (statsManager.TotalMovementSpeed / 3f < 2 )
            {
                agent.speed = 2f;
            }
            else
            {
                agent.speed = statsManager.TotalMovementSpeed / 3f;
            }
             agent.acceleration = agent.speed * 2f;
        }
    }

    public void UpdatePlayerStats()
    {
        statsManager.UpdatePlayerStats();
        UpdatePlayerSkills();
    }

    public void UpdatePlayerSkills()
    {
        UpdateMovementSpeed();
    }
}
