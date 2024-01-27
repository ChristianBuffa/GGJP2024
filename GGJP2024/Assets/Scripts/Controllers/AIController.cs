using UnityEngine;

[CreateAssetMenu(fileName = "AiController", menuName = "InputController/AiController")] 
public class AiController : InputController
{
    public override bool RetrieveCrouchInput()
    {
        return false;
    }

    public override bool RetrieveJumpInput()
    {
        return true;
    }

    public override float RetrieveMoveInput()
    {
        return 1f;
    }
}