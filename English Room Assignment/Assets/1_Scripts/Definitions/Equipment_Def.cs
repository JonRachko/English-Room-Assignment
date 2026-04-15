using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment Definition", menuName = "Equipment/Definition")]
public class Equipment_Def : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public int itemPrice;
    public ItemType itemType;
    public GameObject itemPrefab;
    public PlayerStats statModifiers = new PlayerStats();

}
    