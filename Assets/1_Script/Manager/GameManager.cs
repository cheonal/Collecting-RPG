using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public TeamManager teamManager;
    public PlayerData playerData;
    public int CurStage;
    public int MaxTeamCount;
    public string TeamTag = "Freindly";
    public string EnemyTag = "Enemy";


    void Start()
    {
        teamManager = GetComponent<TeamManager>();
        playerData = GetComponent<PlayerData>();
    }
}
