using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentStoreItem : MonoBehaviour
{
    public static StoreManager manager;
    
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text buyText;
    [SerializeField] private Button buyButton;
    Equipment_Def definition;

    public void Init(Equipment_Def definition)
    {
        this.definition = definition;
        icon.sprite = definition.itemIcon;
        itemName.text = definition.itemName;
        UpdateButton();
        
        buyButton.onClick.AddListener(()=>GameEventManager.TriggerItemBought(definition));
    }

    public void UpdateButton()
    {
        buyButton.interactable = false;
        
        //Already bought
        if (manager.boughtItems.Contains(definition.itemName))
        {
            buyText.text = "Bought";
            buyButton.targetGraphic.color = manager.boughtColor;
            return;
        }
        
        buyText.text = $"Buy - ${definition.itemPrice}";
        
        //Not enough funds
        if (manager.funds < definition.itemPrice)
        {
            buyButton.targetGraphic.color = manager.cantBuyColor;
            return;
        }
        
        //Can buy
        buyButton.interactable = true;
        buyButton.targetGraphic.color = manager.buyColor;
    }
}
