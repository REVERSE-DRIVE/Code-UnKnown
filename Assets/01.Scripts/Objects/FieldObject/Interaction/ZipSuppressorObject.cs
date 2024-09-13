using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using ItemManage;
using ObjectManage;
using TMPro;
using UnityEngine;

public class ZipSuppressorObject : InteractObject
{
    static readonly int openHash = Animator.StringToHash("Open");
    static readonly int DissolveLevel = Shader.PropertyToID("_DissolveLevel");

    [Header("Visual Setting")]
    [SerializeField] private ParticleSystem _openParticle;
    [SerializeField] private Material _dissolveMaterial;
    [SerializeField] Transform _progressBar;

    [Header("Reward Setting")]
    [SerializeField] Vector2Int rewardAmount = new(1, 3);
    [SerializeField] RandomPercentUtil<ItemSO>.Value[] rewardList;

    [SerializeField] TextMeshPro timer;
    bool isActive = false;
    [SerializeField] Animator anim;

    int originTime;
    int delayTime;
    RoomSuppressor room;

    public void Init(RoomSuppressor _room, int time) {
        room = _room;
        originTime = delayTime = time;
    }

    public override void Interact(InteractData data)
    {
        if (isActive) return;
        base.Interact(data);
        
        isActive = true;

        // 시작
        // timer.gameObject.SetActive(true);
        StartCoroutine(TimeHandler());
    }

    IEnumerator TimeHandler() {
        do {
            // timer.text = $"{delayTime / 60}:{delayTime % 60:D2}";
            _progressBar.localScale = new Vector3((float)delayTime / originTime, 1, 1);
            yield return new WaitForSeconds(1);
        } while (--delayTime > 0);
    }

    public void Open() {
        delayTime = -1;
        timer.gameObject.SetActive(false);

        StartCoroutine(OpenEffect());
    }

    IEnumerator OpenEffect() {
        _progressBar.localScale = Vector3.zero;
        anim.SetTrigger(openHash);
        yield return new WaitForSeconds(0.7f);
        
        Dissolve();
        Instantiate(_openParticle, transform.position, Quaternion.identity);

        RandomPercentUtil<ItemSO> randUtil = new RandomPercentUtil<ItemSO>(rewardList);
        for (int i = 0; i < Random.Range(rewardAmount.x, rewardAmount.y + 1); i++)
        {
            ItemSO item = randUtil.GetValue();
            Vector3 endPos = transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);

            ItemDropManager.Instance.DropItem(item.itemType, item.id, transform.position, endPos);
            yield return new WaitForSeconds(0.5f);
        }
        
        Destroy(gameObject, 0.5f);
    }

    private void Dissolve()
    {
        _visualRenderer.material = _dissolveMaterial;
        _visualRenderer.material.DOFloat(0f, DissolveLevel, 0.5f);
    }
}
