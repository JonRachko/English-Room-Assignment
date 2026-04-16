using System;
using UnityEngine;

public class PlayerVisuals : MonoBehaviour
{
    [SerializeField] private Transform torso;
    [SerializeField] private Transform[] partsToMove;
    private float height;

    private void Start()
    {
        GameEventManager.OnStatsChanged += OnStatsChanged;
    }

    private void OnDestroy()
    {
        GameEventManager.OnStatsChanged -= OnStatsChanged;
    }

    void OnStatsChanged(PlayerStats newStats)
    {
        if (height == 0)
        {
            height = newStats.height;
            return;
        }
        
        ChangeHeight((newStats.height - height)/3f);
        height = newStats.height;
    }

    void ChangeHeight(float heightDelta)
    {
        torso.localScale = new Vector3(torso.localScale.x, torso.localScale.y + heightDelta, torso.localScale.z);
        torso.localPosition = new Vector3(torso.localPosition.x, torso.localPosition.y + heightDelta/2f, torso.localPosition.z);
        foreach (var part in partsToMove)
        {
            part.localPosition = new Vector3(part.localPosition.x, part.localPosition.y + heightDelta/2f, part.localPosition.z);
        }
    }
}
