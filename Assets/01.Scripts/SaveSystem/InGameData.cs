using System.Collections.Generic;

[System.Serializable]
public struct InGameData
{
    public List<PartData> PartDatas;
    public int ResourceAmount;
    public float Infection;
    public float Adaptability;
    public float Exp;
}