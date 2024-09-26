using UnityEngine;

[CreateAssetMenu(menuName = "SO/Map/MapListSO")]
public class MapListSO : ScriptableObject {
    [SerializeField] MapGenerateSO[] stages;

    public MapGenerateSO GetStage(int idx) {
        return stages[idx]; // 여기에서 터졌다면 리스트를 확인해봐.
    }
}