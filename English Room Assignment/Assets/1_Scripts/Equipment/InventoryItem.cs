using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text statText;
    [SerializeField] private Button equipButton;
    [SerializeField] private TMP_Text equipButtonText;
    [SerializeField] private Button switchHandButton;
    
    [SerializeField] Color equipColor, unequipColor;

    public Equipment_Def itemDef { get; private set; }
    EquipmentSlot slot;

    private void Start()
    {
        equipButton.onClick.AddListener(EquipButton);
        switchHandButton.onClick.AddListener(SwitchHand);
        
        SubscribeToEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeToEvents();
    }

    public void Init(Equipment_Def definition)
    {
        itemDef = definition;
        itemName.text = definition.itemName;
        icon.sprite = definition.itemIcon;
        SetSwitchButton();
        SetStatTexts(definition.statModifiers);
        equipButton.targetGraphic.color = equipColor;
    }

    void SetSwitchButton()
    {
        switchHandButton.gameObject.SetActive(slot == EquipmentSlot.Mainhand || slot == EquipmentSlot.Offhand
                                                                             || slot == EquipmentSlot.RingLeft ||
                                                                             slot == EquipmentSlot.RingRight);
    }

    void SetStatTexts(PlayerStats stats)
    {
        List<string> statTexts = new();
        statTexts.Add(GetStatText(stats.health, "Health"));
        statTexts.Add(GetStatText(stats.mana, "Mana"));
        statTexts.Add(GetStatText(stats.moveSpeed, "Move Speed"));
        statTexts.Add(GetStatText(stats.strength, "Strength"));
        statTexts.Add(GetStatText(stats.height, "Height"));
        statTexts.Add(GetStatText(stats.magicPower, "Magic Power"));
        statTexts.RemoveAll(x => x == null);

        string newText = "";
        for (int i = 0; i < statTexts.Count; i++)
        {
            var separator = i % 2 == 1 ? "\t" : "\n"; 
            newText += statTexts[i] + separator;
        }
        
        statText.text = newText;
    }

    string GetStatText(int stat, string statName)
    {
        if(stat == 0) return null;
        if(stat > 0) return $"<Color=\"Green\">+{stat} {statName}</color>";
        else return $"<Color=\"Red\">{stat} {statName}</color>";
    }
    
    string GetStatText(float stat, string statName)
    {
        if(stat == 0) return null;
        if(stat > 0) return $"<Color=\"Green\">+{stat:F2} {statName}</color>";
        else return $"<Color=\"Red\">{stat:F2} {statName}</color>";
    }

    public void EquipButton()
    {
        if (slot == EquipmentSlot.None)
        {
            slot = PlayerEquipmentManager.Instance.Equip(itemDef);
            return;
        }
        
        PlayerEquipmentManager.Instance.Unequip(itemDef);
        slot = EquipmentSlot.None;
    }

    void SetEquipped(EquipmentSlot slot)
    {
        //Unequip
        if (slot == EquipmentSlot.None)
        {
            equipButtonText.text = "Equip";
            equipButton.targetGraphic.color = equipColor;
        }
        
        //Equip
        else
        {
            equipButtonText.text = "Unequip";
            equipButton.targetGraphic.color = unequipColor;
        }
        
        this.slot = slot;
        SetSwitchButton();
    }
    
    public void SwitchHand()
    {
        PlayerEquipmentManager.Instance.SwitchHands(slot);
    }

    void ItemEquipped(Equipment_Def item, EquipmentSlot slot)
    {
        if(item != itemDef) return;
        
        SetEquipped(slot);
    }

    void ItemUnequipped(Equipment_Def item, EquipmentSlot slot)
    {
        if(item != itemDef) return;
        
        SetEquipped(EquipmentSlot.None);
    }

    void HandsSwitched(EquipmentSlot slot1, EquipmentSlot slot2)
    {
        if(slot != slot1 && slot != slot2) return;
        var newSlot = slot == slot1 ? slot2 : slot1;
        slot = newSlot;
    }
    
    void SubscribeToEvents()
    {
        GameEventManager.OnEquipped += ItemEquipped;
        GameEventManager.OnUnequipped += ItemUnequipped;
        GameEventManager.OnHandsSwitched += HandsSwitched;
    }
    
    void UnsubscribeToEvents()
    {
        GameEventManager.OnEquipped -= ItemEquipped;
        GameEventManager.OnUnequipped -= ItemUnequipped;
        GameEventManager.OnHandsSwitched -= HandsSwitched;
    }
}
