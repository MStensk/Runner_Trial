using UnityEngine;

public class TrackTurnTrigger : MonoBehaviour
{
    [SerializeField] private bool allowLeftTurn;
    [SerializeField] private bool allowRightTurn;

    [Header("Lane Targets After Turn")]
    [SerializeField] private Transform laneLeft;
    [SerializeField] private Transform laneCenter;
    [SerializeField] private Transform laneRight;

    private void OnTriggerEnter(Collider other)
    {
        EndlessRunController player = other.GetComponent<EndlessRunController>();
        if (player == null) return;

        player.SetTurnAvailability(allowLeftTurn, allowRightTurn);
        player.SetTurnLaneTargets(laneLeft, laneCenter, laneRight);
    }

    private void OnTriggerExit(Collider other)
    {
        EndlessRunController player = other.GetComponent<EndlessRunController>();
        if (player == null) return;

        player.ClearTurnAvailability();
    }
}