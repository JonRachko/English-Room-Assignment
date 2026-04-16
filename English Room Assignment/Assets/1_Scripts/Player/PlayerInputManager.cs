using System;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    public static bool lockMovement;

    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerAnimationManager playerAnimationManager;
    [SerializeField] Transform playerCamera;


    private void Update()
    {
        HandleMovement();
        HandleCamera();
    }

    void HandleMovement()
    {
        var moveInput = lockMovement
            ? new Vector2(0, 0)
            : new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (moveInput.x != 0)
        {
            playerMovement.MoveHorizontal(moveInput.x);
        }

        if (moveInput.y != 0)
        {
            playerMovement.MoveVertical(moveInput.y);
        }


        playerAnimationManager.UpdateMovement(moveInput);
    }

    float pitch = 0f;

    void HandleCamera()
    {
        if (lockMovement) return;
        var mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        //Accumulate pitch
        pitch -= mouseInput.y * 5f;
        pitch = Mathf.Clamp(pitch, -90f, 90f);

        //Apply
        playerCamera.localRotation = Quaternion.Euler(pitch, 0f, 0f);

        //Yaw
        transform.Rotate(Vector3.up, mouseInput.x * 10f);
    }
}