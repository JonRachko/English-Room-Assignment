using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueOption : MonoBehaviour
{
    Button button;
    TMP_Text text;
    RectTransform rectTransform;

    private void Awake()
    {
        button = GetComponent<Button>();
        text = GetComponentInChildren<TMP_Text>();
        rectTransform = GetComponent<RectTransform>();
        
        button.onClick.AddListener(UpdateText);
    }

    public void Init(PlayerLine line, ShopkeepDialogueManager dialogueManager)
    {
        this.text.text = line.text;
        
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
        {
            if (line.isGoodbye)
                dialogueManager.Goodbye();
            else dialogueManager.ApplyDialogue(line.dialogueDef);
        });
    }

    public void UpdateText()
    {
        text.rectTransform.sizeDelta = new Vector2(text.rectTransform.rect.width, text.preferredHeight);
        rectTransform.sizeDelta = new Vector2(rectTransform.rect.width, text.preferredHeight+5);
    }
}
