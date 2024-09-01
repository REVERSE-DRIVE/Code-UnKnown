using System.Collections.Generic;

[System.Serializable]
public struct PowerUpData
{
    public int id;
    public int amount;

    public PowerUpData(int id, int amount)
    {
        this.id = id;
        this.amount = amount;
    }
}

[System.Serializable]
public struct InGameData
{
    public int ResourceAmount;
    public int infectLevel;
    public int level;
    public int exp;
    public PowerUpData[] powerUpDatas;
    
    
}