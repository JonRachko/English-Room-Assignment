using System;
using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using UnityEngine;

public class PlayerEquipmentManager : MonoBehaviour
{
    #region Singleton

    public static PlayerEquipmentManager Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogError($"Multiple PlayerEquipmentManagers! {Instance.gameObject.name} and {gameObject.name}");
            Destroy(this);
            return;
        }

        Instance = this;
    }

    #endregion

    public Dictionary<EquipmentSlot, Equipment_Def> activeEquipment = new();
    public List<Equipment_Def> inventory = new();
    [SerializedDictionary] public SerializedDictionary<EquipmentSlot, Transform> visualEquipment = new();

    private void Start()
    {
        var allSlots = Enum.GetValues(typeof(EquipmentSlot));
        for (int i = 1; i < allSlots.Length; i++)
        {
            activeEquipment.Add((EquipmentSlot) i, null);
        }
    }

    public void AddEquipment(Equipment_Def equipment)
    {
        inventory.Add(equipment);
    }

    public void RemoveEquipment(Equipment_Def equipment)
    {
        if(inventory.Contains(equipment))
        {
            inventory.Remove(equipment);
        }
    }
    
    public EquipmentSlot Equip(Equipment_Def equipment)
    {
        var slot = FindSlot(equipment);
        activeEquipment[slot] = equipment;
        
        GameEventManager.TriggerItemEquipped(equipment, slot);
        return slot;
    }

    EquipmentSlot FindSlot(Equipment_Def equipment)
    {
        switch (equipment.itemType)
        {
            case ItemType.None:
                Debug.LogError("Equipment has no type");
                return EquipmentSlot.None;
            case ItemType.HeldItem:
                //No item in main hand
                if (activeEquipment[EquipmentSlot.Mainhand] == null)
                {
                    return EquipmentSlot.Mainhand;
                }
                
                //There is an item in main hand, but no offhand
                else if (activeEquipment[EquipmentSlot.Offhand] == null)
                {
                    return EquipmentSlot.Offhand;
                }
                
                //There is an item in main hand and offhand, unequip main hand
                else
                {
                    Unequip(activeEquipment[EquipmentSlot.Mainhand]);
                    return EquipmentSlot.Mainhand;
                }
            
            case ItemType.Ring:
                //No ring in right hand
                if (activeEquipment[EquipmentSlot.RingRight] == null)
                {
                    return EquipmentSlot.RingRight;
                }
                
                //There is a ring on right hand, but no left
                else if (activeEquipment[EquipmentSlot.RingLeft] == null)
                {
                    return EquipmentSlot.RingLeft;
                }
                
                //There is a ring on right hand and left, unequip right hand
                else
                {
                    Unequip(activeEquipment[EquipmentSlot.RingRight]);
                    return EquipmentSlot.RingRight;
                }
            
            case ItemType.Boots:
                return EquipmentSlot.Boots;
            case ItemType.Pants:
                return EquipmentSlot.Pants;
            case ItemType.Torso:
                return EquipmentSlot.Torso;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    public void Unequip(Equipment_Def equipment)
    {
        var pair = activeEquipment.FirstOrDefault(x => x.Value == equipment);
        var slot = pair.Key;
        activeEquipment[slot] = null;
        GameEventManager.TriggerItemUnequipped(equipment, slot);
    }

    public void SwitchHands(EquipmentSlot slot)
    {
        
        switch (slot)
        {
            case EquipmentSlot.Mainhand or EquipmentSlot.Offhand:
                SwitchSlots(EquipmentSlot.Mainhand, EquipmentSlot.Offhand);
                break;
            case EquipmentSlot.RingLeft or EquipmentSlot.RingRight:
                SwitchSlots(EquipmentSlot.RingLeft, EquipmentSlot.RingRight);
                break;
            
            default:
                Debug.LogError($"Invalid slot {slot}");
                break;
        }
        
        
        void SwitchSlots(EquipmentSlot slot1, EquipmentSlot slot2)
        {
            var temp = activeEquipment[slot1];
            activeEquipment[slot1] = activeEquipment[slot2];
            activeEquipment[slot2] = temp;
            
            GameEventManager.TriggerHandsSwitched(slot1, slot2);
        }
    }
}
