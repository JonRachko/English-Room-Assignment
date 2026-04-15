using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Singleton
    public static PlayerController instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.LogError($"Multiple instances of PlayerController: {instance.gameObject.name} and {gameObject.name}");
            Destroy(gameObject);
            return;
        }

        instance = this;
    }
    #endregion

    public PlayerStatsHandler playerStatsHandler;
    public PlayerVisuals playerVisuals;

    void OnPlayerStatsChanged(PlayerStats newStats)
    {
        
    }
}
