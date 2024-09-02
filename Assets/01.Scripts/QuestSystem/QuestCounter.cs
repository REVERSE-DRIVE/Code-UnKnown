using UnityEngine;

public class QuestCounter : MonoBehaviour
{
    private int _enemyKillCount;
    private int _skillCount;
    private int _detectiveCount;
    private int _focusCount;
    private int _theFileCount;
    private int _trackerCount;
    
    public int AddEnemyKillCount()
    {
        return _enemyKillCount++;
    }
    
    public int AddSkillCount()
    {
        return _skillCount++;
    }
    
    public int AddFocusCount()
    {
        return _focusCount++;
    }
    
    public int AddTheFileCount()
    {
        return _theFileCount++;
    }
    
    public int AddTrackerCount()
    {
        return _trackerCount++;
    }
    
    public int AddDetectiveCount()
    {
        return _detectiveCount++;
    }
}