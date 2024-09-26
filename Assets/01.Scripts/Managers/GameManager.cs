using System;
using System.Collections;
using System.Linq;
using GooglePlayGames;
using ObjectManage;
using ObjectPooling;
using QuestManage;
using SaveSystem;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    const string INGAME_DATA_FILE = "InGameData";
    [field: SerializeField] public Player Player { get; private set; }
    [field: SerializeField] public Transform PlayerTrm { get; private set; }


    protected override void Awake()
    {
        QuestObserver.Instance.ApplyAllQuest();
    }

    private void Start()
    {
        LoadInGameData();
        MapManager.Instance.Generate();
        GameStart();

    }

    public void GameStart()
    {
        ResetPlayer();


    }


    public void ResetPlayer()
    {
        IWindowPanel panel = UIManager.Instance.GetPanel(WindowEnum.Editor);
        if (panel == null)
        {
            Debug.LogWarning("EditorPanel is Null > UIManager");
        }
        else
        {
            EditorPanel editorPanel = panel as EditorPanel;
            editorPanel.ResetEditor();

        }

        CameraManager.Instance.ZoomDefault(15, 0.3f);
        PlayerManager.Instance.player.SetVisualActive(false);
        PlayerManager.Instance.player.MovementCompo.isStun = true;
        Vector2 startPos = MapManager.Instance.GetRoomByCoords(Vector2Int.zero).GetCenterCoords();
        PlayerManager.Instance.player.transform.position = startPos;
        EffectObject effect = PoolingManager.Instance.Pop(PoolingType.PlayerAppearVFX) as EffectObject;
        effect.Initialize(startPos);
        StartCoroutine(GameStartCoroutine());
    }

    private IEnumerator GameStartCoroutine()
    {
        yield return new WaitForSeconds(1f);
        CameraManager.Instance.Shake(10f, 0.2f);
        PlayerManager.Instance.player.MovementCompo.isStun = false;
        PlayerManager.Instance.player.SetVisualActive(true);
        PlayerPartManager.Instance.ChangeAllPart();
    }

    public void SaveInGameData()
    {

        PowerUpManager powerUpManager = PowerUpManager.Instance;

        PowerUpData[] powerUpDatas;
        powerUpDatas = powerUpManager.powerUpDictionary
            .Select(kvp => new PowerUpData(kvp.Key, kvp.Value))
            .ToArray();


        InGameData data = new InGameData()
        {
            ResourceAmount = ResourceManager.Instance.ResourceAmount,

            level = LevelManager.Instance.CurrentLevel,
            exp = LevelManager.Instance.CurrentExp,
            powerUpDatas = powerUpDatas

        };
        SaveManager.Instance.Save<InGameData>(data, INGAME_DATA_FILE);

        PlayerPartManager.Instance.SavePartData();

        // 랭킹 등록 만약 있다면
        if (GoogleLoginSystem.isLogined)
        { // 로그인중
            print($"Google Rank Save Request {LevelManager.Instance.CurrentExp}");
            PlayGamesPlatform.Instance.ReportScore(LevelManager.Instance.CurrentExp, RankUI.RANK_ID, success =>
            {
                print($"Google Rank Save Result {LevelManager.Instance.CurrentExp} ({success})");
            });
        }
    }

    // 초기화
    public void ClearInGameData()
    {
        SaveManager.Instance.Save<InGameData>(default, INGAME_DATA_FILE);
    }

    public void LoadInGameData()
    {
        InGameData data = SaveManager.Instance.Load<InGameData>(INGAME_DATA_FILE);

        // 리소스
        ResourceManager.Instance.UseResource(ResourceManager.Instance.ResourceAmount);
        ResourceManager.Instance.AddResource(data.ResourceAmount);

        // 레벨
        LevelManager.Instance.SetLevelExp(data.level, data.exp);

        PlayerPartManager.Instance.LoadPartData();

        // 파워업
        var powerUpDatas = PowerUpManager.Instance.powerUpDictionary = new(); // 다시 만들어
        if (data.powerUpDatas != null)
            foreach (var item in data.powerUpDatas)
                powerUpDatas.Add(item.id, item.amount);
    }

    public bool IsSavedInGameData() => SaveManager.Instance.Load<InGameData>(INGAME_DATA_FILE).saved;

    public void ExitGame()
    {
        // SaveInGameData(); // 저장해
        QuestObserver.Instance.ResetQuest();
        LoadManager.Instance.StartLoad("MainLobbyScene");
        Destroy(QuestManager.Instance.gameObject);
    }

}