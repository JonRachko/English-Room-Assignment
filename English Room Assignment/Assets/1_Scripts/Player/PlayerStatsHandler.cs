using System;
using UnityEngine;

public class PlayerStatsHandler : MonoBehaviour
{
    public PlayerStats stats;

    private void Start()
    {
        GameEventManager.TriggerStatsChanged(stats);
    }

    void OnItemEquipped(IEquipment item)
    {
        stats.health += item.Definition.statModifiers.health;
        stats.mana += item.Definition.statModifiers.mana;
        stats.moveSpeed += item.Definition.statModifiers.moveSpeed;
        stats.strength += item.Definition.statModifiers.strength;
        stats.height += item.Definition.statModifiers.height;
        stats.magicPower += item.Definition.statModifiers.magicPower;
        
        GameEventManager.TriggerStatsChanged(stats);
    }
    
    void OnItemUnequipped(IEquipment item)
    {
        stats.health -= item.Definition.statModifiers.health;
        stats.mana -= item.Definition.statModifiers.mana;
        stats.moveSpeed -= item.Definition.statModifiers.moveSpeed;
        stats.strength -= item.Definition.statModifiers.strength;
        stats.height -= item.Definition.statModifiers.height;
        stats.magicPower -= item.Definition.statModifiers.magicPower;
        
        GameEventManager.TriggerStatsChanged(stats);
    }
}
