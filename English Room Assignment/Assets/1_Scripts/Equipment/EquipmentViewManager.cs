using System;
using AYellowpaper.SerializedCollections;
using UnityEngine;
using UnityEngine.UI;
public class EquipmentViewManager : MonoBehaviour
{
    [SerializedDictionary] public SerializedDictionary<EquipmentSlot, Image> inventoryIcons = new();

    private void Start()
    {
        SubscribeToEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }

    private void ItemEquipped(Equipment_Def item, EquipmentSlot slot)
    {
        inventoryIcons[slot].sprite = item.itemIcon;
    }
    
    private void ItemUnequipped(Equipment_Def item, EquipmentSlot slot)
    {
        inventoryIcons[slot].sprite = null;
    }
    
    private void HandsSwitched(EquipmentSlot slot1, EquipmentSlot slot2)
    {
        var temp = inventoryIcons[slot1].sprite;
        inventoryIcons[slot1].sprite = inventoryIcons[slot2].sprite;
        inventoryIcons[slot2].sprite = temp;
    }
    
    void SubscribeToEvents()
    {
        GameEventManager.OnEquipped += ItemEquipped;
        GameEventManager.OnUnequipped += ItemUnequipped;
        GameEventManager.OnHandsSwitched += HandsSwitched;
    }
    
    void UnsubscribeFromEvents()
    {
        GameEventManager.OnEquipped -= ItemEquipped;
        GameEventManager.OnUnequipped -= ItemUnequipped;
        GameEventManager.OnHandsSwitched -= HandsSwitched;
    }
}
