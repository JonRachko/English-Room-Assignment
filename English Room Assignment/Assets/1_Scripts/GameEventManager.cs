using System;
using UnityEngine;

public static class GameEventManager
{
    public static Action<Equipment_Def> onEquipmentBought;
    public static void TriggerItemBought(Equipment_Def item) => onEquipmentBought?.Invoke(item);
    
    public static Action<IEquipment> OnEquipped;
    public static void TriggerItemEquipped(IEquipment item) => OnEquipped?.Invoke(item);
    
    public static Action<IEquipment> OnUnequipped;
    public static void TriggerItemUnequipped(IEquipment item) => OnUnequipped?.Invoke(item);
    
    public static Action<PlayerStats> OnStatsChanged;
    public static void TriggerStatsChanged(PlayerStats newStats) => OnStatsChanged?.Invoke(newStats); 
}
