using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : DontDestroyMonoSingleton<GameManager>
{
    public TeamManager teamManager;
    public PlayerData playerData;
    public int CurStage;

    void Start()
    {
        teamManager = GetComponent<TeamManager>();
        playerData = GetComponent<PlayerData>();
    }
}
