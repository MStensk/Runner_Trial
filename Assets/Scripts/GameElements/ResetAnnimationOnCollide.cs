using UnityEngine;

public class ResetAnnimationOnCollide : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider playerNinjaCollider)
    {
        if (!playerNinjaCollider.CompareTag("PlayerNinja")) return;

        Animator animator = playerNinjaCollider.GetComponentInChildren<Animator>();
        if (animator != null)
        {
        animator.Play("Run", 0, 0f);
        animator.Update(0f);
        }

       
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
