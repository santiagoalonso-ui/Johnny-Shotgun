using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    PlayerCtrl playerControls;

    public Vector2 movementInput;

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerCtrl();

            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
        }

        playerControls.Enable();

    }

    private void OnDisable()
    {
        playerControls.Disable();
    }
}
