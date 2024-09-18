using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillPieceVFX : AgentVFX
{
    [SerializeField] ParticleSystem greenFlame;

    public void SetGreenFlame(bool active) {
        if (active)
            greenFlame.Play();
        else
            greenFlame.Stop();
    }
}
