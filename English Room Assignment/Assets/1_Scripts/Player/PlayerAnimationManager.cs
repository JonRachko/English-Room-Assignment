using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    [SerializeField] Animator animator;
    bool moving = false;

    public void UpdateMovement(Vector2 movement)
    {
        if(movement == Vector2.zero && moving)
        {
            animator.SetBool("Moving", false);
            moving = false;
        }
        else if(movement != Vector2.zero && !moving)
        {
            animator.SetBool("Moving", true);
            moving = true;
        }
    }
}
