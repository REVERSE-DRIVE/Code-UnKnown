using PlayerPartsManage;

[System.Serializable]
public struct PartData
{
    public int partID;
    public PartType partType;
    public int partLevel;
    public int partRemainingPeriod;
    
    public PartData(int partID, PartType partType, int partLevel = 1, int partRemainingPeriod = 0)
    {
        this.partID = partID;
        this.partType = partType;
        this.partLevel = partLevel;
        this.partRemainingPeriod = partRemainingPeriod;
    }
}