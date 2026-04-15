using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

public class PlayerEquipmentManager : MonoBehaviour
{
    public Dictionary<EquipmentSlot, IEquipment> activeEquipment = new();
    public List<IEquipment> inventory = new();
    [SerializedDictionary] public SerializedDictionary<EquipmentSlot, Transform> visualEquipment = new();
    
    public void AddEquipment(IEquipment equipment)
    {
        inventory.Add(equipment);
    }

    public void RemoveEquipment(IEquipment equipment)
    {
        if(inventory.Contains(equipment))
        {
            inventory.Remove(equipment);
        }
    }
    
    public void Equip(IEquipment equipment)
    {
        
    }
}
