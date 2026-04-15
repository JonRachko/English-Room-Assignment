using UnityEngine;

public class TargetFollower : MonoBehaviour
{
    [SerializeField] Transform target;
    private Quaternion baseRotation;

    public void Init(Transform target)
    {
        this.target = target;
        baseRotation = Quaternion.Inverse(target.rotation) * transform.rotation;
    }

    private void FixedUpdate()
    {
        if (!target) return;

        transform.position = target.position;
        transform.rotation = target.rotation * baseRotation;
    }
}
