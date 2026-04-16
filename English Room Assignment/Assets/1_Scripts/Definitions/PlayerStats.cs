using UnityEngine;

[System.Serializable]
public class PlayerStats
{
    public int health = 0;
    public int mana = 0;
    public float moveSpeed = 0f;
    public float strength = 0f;
    public float height = 0f;
    public float magicPower = 0f;

    public PlayerStats Clone()
    {
        PlayerStats clone = new PlayerStats();
        clone.health = health;
        clone.mana = mana;
        clone.moveSpeed = moveSpeed;
        clone.strength = strength;
        clone.height = height;
        clone.magicPower = magicPower;
        return clone;
    }

    public void LogStats()
    {
        string stats = $"Health: {health}\n";
        stats += $"Mana: {mana}\n";
        stats += $"Move Speed: {moveSpeed:F2}\n";
        stats += $"Strength: {strength:F2}\n";
        stats += $"Height: {height:F2}\n";
        stats += $"Magic Power: {magicPower:F2}\n";
        
        Debug.Log(stats); 
    }
}
