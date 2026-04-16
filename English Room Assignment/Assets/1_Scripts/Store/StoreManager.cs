using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
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
    
    [SerializeField] ShopkeepDialogueManager dialogueManager;
    [SerializeField] ShopkeepDialogue_Def welcomeDialogue;
    [SerializeField] PlayerStatsView playerStatsView;
    
    [SerializeField] TMP_Text fundsText;
    
    CanvasGroup canvasGroup;

    private void Start()
    {
        EquipmentStoreItem.manager = this;
        canvasGroup = GetComponent<CanvasGroup>();
        
        SubscribeToEvents();
        InitializeItems(); 
        
        OpenShop();
    }

    public void OpenShop()
    {
        UpdateFundsText();
        dialogueManager.ApplyDialogue(welcomeDialogue);
        canvasGroup.DOFade(1, 0.25f).OnComplete(() => canvasGroup.blocksRaycasts = true);
        playerStatsView?.ShowStats();
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
        
        UpdateItems();
        UpdateFundsText();
        
        PlayerEquipmentManager.Instance.AddEquipment(item);
    }


    public void GainFunds(int amount)
    {
        funds += amount;
        UpdateItems();
        UpdateFundsText();
    }

    void UpdateItems()
    {
        foreach (var storeItem in items)
        {
            storeItem.UpdateButton();
        }
    }
    void UpdateFundsText()
    {
        fundsText.text = $"{funds}$";
    }
    public void CloseShop()
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.DOFade(0, 0.25f);
        playerStatsView?.HideStats();
    }
    
    void SubscribeToEvents()
    {
        GameEventManager.onEquipmentBought += OnItemBought;
    }
    
    void UnsubscribeFromEvents()
    {
        GameEventManager.onEquipmentBought -= OnItemBought;
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }
}
