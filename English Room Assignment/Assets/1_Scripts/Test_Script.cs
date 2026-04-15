using System;
using UnityEngine;

public class Test_Script : MonoBehaviour
{
    [SerializeField] TargetFollower targetFollower;
    [SerializeField] Transform[] targets;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            targetFollower.Init(targets[0]);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            targetFollower.Init(targets[1]);
        }
    }
}