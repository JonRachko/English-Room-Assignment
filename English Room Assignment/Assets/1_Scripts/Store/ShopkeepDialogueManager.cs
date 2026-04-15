using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopkeepDialogueManager : MonoBehaviour
{
    [SerializeField] private TMP_Text shopkeepText;
    [SerializeField] private DialogueOption dialogueOptionPrefab;
    [SerializeField] private Transform dialogueOptionContainer;
    List<DialogueOption> activeOptions = new();
    
    StoreManager storeManager;

    private void Start()
    {
        storeManager = GetComponent<StoreManager>();
    }

    public void ApplyDialogue(ShopkeepDialogue_Def dialogueDef)
    {
        shopkeepText.text = dialogueDef.shopkeepText;
        SetDialogueOptions(dialogueDef.playerLines);

        if (dialogueDef.payout > 0)
        {
            storeManager.GainFunds(dialogueDef.payout);
        }
    }

    void SetDialogueOptions(PlayerLine[] lines)
    {
        if (activeOptions.Count < lines.Length)
        {
            for (int i = activeOptions.Count-1; i < lines.Length; i++)
            {
                activeOptions.Add(Instantiate(dialogueOptionPrefab, dialogueOptionContainer));
            }
        }
        
        StartCoroutine(SetOptions());
        return;

        IEnumerator SetOptions()
        {
            for (int i = 0; i < activeOptions.Count; i++)
            {
                if (i >= lines.Length)
                {
                    activeOptions[i].gameObject.SetActive(false);
                    continue;
                }
                
                activeOptions[i].gameObject.SetActive(true);
                activeOptions[i].Init(lines[i], this);
                yield return null; //Waiting a frame for the text size to be calculated
                activeOptions[i].UpdateText();
            }
        }
    }

    public void Goodbye()
    {
        storeManager.CloseShop();
    }
}
