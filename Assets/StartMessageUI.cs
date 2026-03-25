using UnityEngine;

public class StartMessageUI : MonoBehaviour
{
    [SerializeField] private GameObject messageObject;
    [SerializeField] private float showTime = 3f;

    void Start()
    {
        Debug.Log("START MESSAGE RUNNING");

        messageObject.SetActive(true);

        Invoke(nameof(HideMessage), showTime);
    }

    void HideMessage()
    {
        Debug.Log("HIDING MESSAGE");
        messageObject.SetActive(false);
    }
}