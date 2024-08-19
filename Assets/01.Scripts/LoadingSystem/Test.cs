﻿using System;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LoadManager.Instance.StartLoad("MainLobbyScene", () =>
            {
                Debug.Log("MainLobby Load Complete");
            });
        }
    }
}