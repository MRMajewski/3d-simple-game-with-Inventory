using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    private PlayerStatsManager statsManager;
    [SerializeField]
    private PlayerMovement playerMovement;

    public void UpdatePlayerStats()
    {
        statsManager.UpdatePlayerStats();
        UpdatePlayerSkills();
    }

    public void UpdatePlayerSkills()
    {
        playerMovement.UpdateMovementSpeed(statsManager.TotalMovementSpeed / 3f);
    }

    public void StopPlayerMovementAnim()
    {
        playerMovement.StopPlayerMovement();
    }
}
