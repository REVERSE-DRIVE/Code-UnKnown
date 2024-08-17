using System.Collections.Generic;

public class PowerUpManager : MonoSingleton<PowerUpManager>
{
    public Dictionary<int, int> powerUpDictionary = new Dictionary<int, int>();

    private void LoadPowerData()
    {
        // 저장된 세이브 데이터에서 파워업 정보를 불러온다
    }

    /**
     * <summary>
     * 획득한 파워업 카드중에서 id에 해당하는 파워업을 몇번 획득했는지를 반환한다
     * </summary>
     */
    public int Find(int id)
    {
        if (powerUpDictionary.TryGetValue(id, out int amount))
        {
            return amount;
        }
        return 0;
    }
    
    public void ApplyPowerUp(int id)
    {
        if (powerUpDictionary.ContainsKey(id))
        {
            powerUpDictionary[id]++;
            return;
        }
        powerUpDictionary.Add(id, 1);
    }
}