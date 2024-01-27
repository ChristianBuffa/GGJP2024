using UnityEngine;

[CreateAssetMenu(fileName = "AiController", menuName = "InputController/AiController")] 
public class AiController : InputController
{
    public float moveInput;
    
    public override bool RetrieveJumpInput()
    {
        return true;
    }

    public override float RetrieveMoveInput()
    {
        return moveInput;
    }
}