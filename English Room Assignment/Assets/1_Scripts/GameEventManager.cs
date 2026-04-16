using System;
using UnityEngine;

public static class GameEventManager
{
    public static Action<Equipment_Def> onEquipmentBought;
    public static void TriggerItemBought(Equipment_Def item) => onEquipmentBought?.Invoke(item);
    
    public static Action<Equipment_Def, EquipmentSlot> OnEquipped;
    public static void TriggerItemEquipped(Equipment_Def item, EquipmentSlot slot) => OnEquipped?.Invoke(item, slot);
    
    public static Action<Equipment_Def, EquipmentSlot> OnUnequipped;
    public static void TriggerItemUnequipped(Equipment_Def item, EquipmentSlot slot) => OnUnequipped?.Invoke(item, slot);
    
    public static Action<PlayerStats> OnStatsChanged;
    public static void TriggerStatsChanged(PlayerStats newStats) => OnStatsChanged?.Invoke(newStats); 
    
    public static Action<EquipmentSlot, EquipmentSlot> OnHandsSwitched;
    public static void TriggerHandsSwitched(EquipmentSlot oldSlot, EquipmentSlot newSlot) => OnHandsSwitched?.Invoke(oldSlot, newSlot);
}
