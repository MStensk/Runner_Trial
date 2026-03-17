using UnityEngine;

public class TrackTurnTrigger : MonoBehaviour
{
    [SerializeField] private bool allowLeftTurn;
    [SerializeField] private bool allowRightTurn;

    private void OnTriggerEnter(Collider other)
    {
        EndlessRunController player = other.GetComponent<EndlessRunController>();
        if (player != null)
        {
            player.SetTurnAvailability(allowLeftTurn, allowRightTurn);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        EndlessRunController player = other.GetComponent<EndlessRunController>();
        if (player != null)
        {
            player.ClearTurnAvailability();
        }
    }
}
