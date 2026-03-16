using UnityEngine;

public class EndlessRunController : MonoBehaviour
{

    private CharacterController playerController;

//[SerializeField] private GameObject player;

private void LeftMovement(){
/*
Vector3 playerCurrentPosition = transform.position;

playerCurrentPosition.x -= 0.5f;

transform.position = playerCurrentPosition;  */

    Vector3 move = new Vector3(-1.2f, 0f, 0f);
    playerController.Move(move);

}
private void RightMovement()
{
    /*
Vector3 playerCurrentPosition = transform.position;

playerCurrentPosition.x += 1.2f;

transform.position = playerCurrentPosition;  */

    Vector3 move = new Vector3(1.2f, 0f, 0f);
    playerController.Move(move);
}
 
private void RunForward()
    {
      //  Vector3 playerCurrentPosition = transform.position;
    //    playerCurrentPosition.z += 5.0f;

/*
// gamle Move, inden der kom flere retninger
Vector3 move = new Vector3(0f, 0f, 5.0f);
    playerController.Move(move);

    */

  Vector3 move = transform.forward * 1.5f;
    playerController.Move(move);

    //    transform.position = playerCurrentPosition;

    }

private void HandleLeftMovement(){
//Metode som undersøger om character spech indikerer at det er et turn felt, så character
//skal dreje og følge ruten, frem for at skifte spor
     //Finde character nuværende path, og antal paths i området, og If player.specs.path
     //er en højere værdi end laveste path værdi opsamlet af området gennem colide
     //så kaldes Leftmovement, ellers ignoreres.

if(Input.GetKeyDown(KeyCode.A)) 
{
    LeftMovement();
}

}

private void HandleRightMovement()
{
    //Metode som undersøger om character spech indikerer at det er et turn felt, så character
//skal dreje og følge ruten, frem for at skifte spor
     //Finde character nuværende path, og antal paths i området, og If player.specs.path
     //er en højere værdi end laveste path værdi opsamlet af området gennem colide
     //så kaldes Leftmovement, ellers ignoreres.

     if(Input.GetKeyDown(KeyCode.D))
     {
        RightMovement();
     }
}

private void HandleRun()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            RunForward();
        }
    }

/*
private void Jump()
{
    Vector3 playerCurrentPosition = transform.position;

   // playerCurrentPosition.y

}*/

    // Start is called once before the first execution of Update after the MonoBehaviour is created
private void TurnLeft()
{
    transform.Rotate(0f, -90f, 0f);
}

private void TurnRight()
{
    transform.Rotate(0f, 90f, 0f);
}

private void HandleTurnLeft()
{
    if(Input.GetKeyDown(KeyCode.Z))
    {
        TurnLeft();
    }
}

private void HandleTurnRight()
{
    if(Input.GetKeyDown(KeyCode.X))
    {
        TurnRight();
    }
}

void Start()
    {
      playerController = GetComponent<CharacterController>();  
    }

    // Update is called once per frame
    void Update()
    {
        HandleLeftMovement();
        HandleRightMovement();
        HandleRun();

        HandleTurnLeft();
    HandleTurnRight();

    }
}
