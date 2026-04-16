using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] PlayerStatsHandler statsHandler;
    public void MoveHorizontal(float value)
    {
        transform.Translate(Vector3.right * (3 * statsHandler.stats.moveSpeed * value * Time.deltaTime));
    }
    
    public void MoveVertical(float value)
    {
        transform.Translate(Vector3.forward * (3 * statsHandler.stats.moveSpeed * value * Time.deltaTime));
    }
}
