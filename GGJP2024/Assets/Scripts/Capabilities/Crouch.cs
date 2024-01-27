using UnityEngine;

public class Crouch : MonoBehaviour
{
    [SerializeField] private InputController input = null;
    private bool isCrouching = false;
    private BoxCollider2D boxCollider;
    private Vector2 standingSize;
    private Vector2 crouchingSize;

    private bool desiredCrouch;

    // Start is called before the first frame update
    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();

        standingSize = boxCollider.size;

        crouchingSize = new Vector2(standingSize.x, standingSize.y * 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        desiredCrouch |= input.RetrieveCrouchInput();
    }

    private void FixedUpdate()
    {
        if (desiredCrouch)
        {
            desiredCrouch = false;
            CrouchAction();
        }
    }

    private void CrouchAction()
    {
        isCrouching = !isCrouching;

        if(isCrouching)
        {
            boxCollider.size = crouchingSize;
        }
        else
        {
            boxCollider.size = standingSize;
        }
    }
}