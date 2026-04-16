using System;
using UnityEngine;

public class PlayerStatsHandler : MonoBehaviour
{
    public PlayerStats stats;

    private void Start()
    {
        GameEventManager.TriggerStatsChanged(stats);
        
        SubscribeToEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }

    void OnItemEquipped(Equipment_Def item, EquipmentSlot slot)
    {
        Debug.Log("Item equipped - stats before:");
        stats.LogStats();
        
        stats.health += item.statModifiers.health;
        stats.mana += item.statModifiers.mana;
        stats.moveSpeed += item.statModifiers.moveSpeed;
        stats.strength += item.statModifiers.strength;
        stats.height += item.statModifiers.height;
        stats.magicPower += item.statModifiers.magicPower;
        
        Debug.Log("Item equipped - stats after:");
        stats.LogStats();
        
        GameEventManager.TriggerStatsChanged(stats);
    }
    
    void OnItemUnequipped(Equipment_Def item, EquipmentSlot slot)
    {
        stats.health -= item.statModifiers.health;
        stats.mana -= item.statModifiers.mana;
        stats.moveSpeed -= item.statModifiers.moveSpeed;
        stats.strength -= item.statModifiers.strength;
        stats.height -= item.statModifiers.height;
        stats.magicPower -= item.statModifiers.magicPower;
        
        GameEventManager.TriggerStatsChanged(stats);
    }

    void SubscribeToEvents()
    {
        GameEventManager.OnEquipped += OnItemEquipped;
        GameEventManager.OnUnequipped += OnItemUnequipped;
    }
    
    void UnsubscribeFromEvents()
    {
        GameEventManager.OnEquipped -= OnItemEquipped;
        GameEventManager.OnUnequipped -= OnItemUnequipped;
    }
}
