using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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

    [SerializeField] TextMeshPro timer;
    bool isActive = false;
    [SerializeField] Animator anim;

    int delayTime;
    RoomSuppressor room;

    public void Init(RoomSuppressor _room, int time) {
        room = _room;
        delayTime = time;
    }

    public override void Interact(InteractData data)
    {
        if (isActive) return;
        base.Interact(data);
        
        isActive = true;

        // 시작
        timer.gameObject.SetActive(true);
        StartCoroutine(TimeHandler());
    }

    IEnumerator TimeHandler() {
        while (--delayTime > 0) {
            yield return new WaitForSeconds(1);
            timer.text = $"{delayTime / 60}:{delayTime % 60:D2}";
        }
    }

    public void Open() {
        delayTime = -1;
        timer.gameObject.SetActive(false);

        StartCoroutine(OpenEffect());
    }

    IEnumerator OpenEffect() {
        anim.SetTrigger(openHash);
        yield return new WaitForSeconds(0.7f);
        
        Dissolve();
        Instantiate(_openParticle, transform.position, Quaternion.identity);    
        
        Destroy(gameObject, 0.5f);
    }

    private void Dissolve()
    {
        _visualRenderer.material = _dissolveMaterial;
        _visualRenderer.material.DOFloat(0f, DissolveLevel, 0.5f);
    }
}
