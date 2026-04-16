using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
   [SerializeField] CanvasGroup inventoryCanvas;
   [SerializeField] InventoryItem inventoryItemPrefab;
   [SerializeField] Transform inventoryContainer;
   [SerializeField] PlayerStatsView playerStatsView;

   private List<InventoryItem> items = new();
   bool inventoryOpen = false;
   
   private void Start()
   {
      inventoryCanvas.alpha = 0;
      inventoryCanvas.blocksRaycasts = false;
   }

   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.I))
      {
         inventoryOpen = !inventoryOpen;
         if (inventoryOpen)
         {
            ShowInventory();
         }
         else
         {
            HideInventory();
         }
      }
      
      if(inventoryOpen && Input.GetKeyDown(KeyCode.Escape)){HideInventory();}
   }

   void InitInventory()
   {
      var inventory = PlayerEquipmentManager.Instance.inventory;
      foreach (var item in inventory)
      {
         InitItem(item);
      }
   }

   void InitItem(Equipment_Def item)
   {
      if (items.Find(x => x.itemDef == item) == null)
      {
         var inventoryItem = Instantiate(inventoryItemPrefab, inventoryContainer);
         inventoryItem.Init(item);
         items.Add(inventoryItem);
      }
   }

   void ShowInventory()
   {
      InitInventory();
      inventoryCanvas.DOFade(1, 0.25f).OnComplete(() =>
      {
         inventoryCanvas.blocksRaycasts = true;
         inventoryOpen = true;
      });
      playerStatsView?.ShowStats();
   }

   void HideInventory()
   {
      inventoryCanvas.DOFade(0, 0.25f).OnComplete(() =>
      {
         inventoryCanvas.blocksRaycasts = false;
         inventoryOpen = false;
      });
      playerStatsView?.HideStats();
   }
}
