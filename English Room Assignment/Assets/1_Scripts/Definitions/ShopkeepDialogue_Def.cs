using UnityEngine;

[CreateAssetMenu(fileName = "New Shopkeep Dialogue", menuName = "Shopkeep Dialogue")]
public class ShopkeepDialogue_Def : ScriptableObject
{
    [TextArea(3, 10)]
    public string shopkeepText;
    public PlayerLine[] playerLines;
    public int payout = 0;
}

[System.Serializable]
public class PlayerLine
{
    public string text;
    public ShopkeepDialogue_Def dialogueDef;
    public bool isGoodbye;
}
