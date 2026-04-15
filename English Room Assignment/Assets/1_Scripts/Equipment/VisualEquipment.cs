using UnityEngine;

public interface IEquipment
{
    void Equip();
    void Unequip();   
    Equipment_Def Definition { get; }
}

public class VisualEquipment : MonoBehaviour, IEquipment
{
    public ItemType type;
    public Transform target;
    private Equipment_Def definition;
    public Equipment_Def Definition => definition;
    
    public void Equip()
    {
        
    }

    public void Unequip()
    {
        
    }
}
