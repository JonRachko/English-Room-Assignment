using System;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    public int funds;
    public Color buyColor, cantBuyColor, boughtColor;
    public List<string> boughtItems;
    
    [SerializeField] Equipment_Def[] equipmentDefinitions;
    [SerializeField] EquipmentStoreItem itemPrefab;
    [SerializeField] Transform itemContainer;
    List<EquipmentStoreItem> items = new();

    private void Start()
    {
        EquipmentStoreItem.manager = this;
        
        SubscribeToEvents();
        InitializeItems(); 
    }

    void InitializeItems()
    {
        foreach (var def in equipmentDefinitions)
        {
            var item = Instantiate(itemPrefab, itemContainer);
            item.Init(def);
            items.Add(item);
        }
    }

    void OnItemBought(Equipment_Def item)
    {
        funds -= item.itemPrice;
        boughtItems.Add(item.itemName);
        foreach (var storeItem in items)
        {
            storeItem.UpdateButton();
        }
    }
    
    void SubscribeToEvents()
    {
        GameEventManager.onEquipmentBought += OnItemBought;
    }
    
    void UnsubscribeFromEvents()
    {
        GameEventManager.onEquipmentBought -= OnItemBought;
    }
}
