using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class PlayerStatsView : MonoBehaviour
{
    PlayerStats stats = new();
    
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text manaText;
    [SerializeField] private TMP_Text moveSpeedText;
    [SerializeField] private TMP_Text strengthText;
    [SerializeField] private TMP_Text heightText;
    [SerializeField] private TMP_Text magicPowerText;

    [SerializeField] private TMP_Text healthSliderText;
    [SerializeField] private TMP_Text manaSliderText;

    private void Start()
    {
        SubscribeToEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }

    void SubscribeToEvents()
    {
        GameEventManager.OnStatsChanged += OnPlayerStatsChanged;
    }
    
    void UnsubscribeFromEvents()
    {
        GameEventManager.OnStatsChanged -= OnPlayerStatsChanged;
    }

    void OnPlayerStatsChanged(PlayerStats newStats)
    {
        if(newStats.health != stats.health)
        {
            healthText.text = $"Health: {newStats.health.ToString()}";
            AnimateText(healthText, newStats.health > stats.health);
            healthSliderText.text = $"Health: {newStats.health.ToString()}/{newStats.health.ToString()}";
        }

        if (newStats.mana != stats.mana)
        {
            manaText.text = $"Mana: {newStats.mana.ToString()}";
            AnimateText(manaText, newStats.mana > stats.mana);
            manaSliderText.text = $"Mana: {newStats.mana.ToString()}/{newStats.mana.ToString()}";
        }

        if (!Mathf.Approximately(newStats.moveSpeed, stats.moveSpeed))
        {
            moveSpeedText.text = $"Speed: {newStats.moveSpeed.ToString("F2")}";
            AnimateText(moveSpeedText, newStats.moveSpeed > stats.moveSpeed);
        }
        
        if (!Mathf.Approximately(newStats.strength, stats.strength))
        {
            strengthText.text = $"Speed: {newStats.strength.ToString("F2")}";
            AnimateText(strengthText, newStats.strength > stats.strength);
        }
        
        if (!Mathf.Approximately(newStats.height, stats.height))
        {
            heightText.text = $"Speed: {newStats.height.ToString("F2")}";
            AnimateText(heightText, newStats.height > stats.height);
        }
        
        if (!Mathf.Approximately(newStats.magicPower, stats.magicPower))
        {
            magicPowerText.text = $"Speed: {newStats.magicPower.ToString("F2")}";
            AnimateText(magicPowerText, newStats.magicPower > stats.magicPower);
        }
    }
    
    void AnimateText(TMP_Text text, bool isPositive)
    {
        text.color = isPositive ? Color.green : Color.red;
        text.DOColor(Color.white, 1f).SetEase(Ease.InCubic);
    }
}
