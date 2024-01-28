using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoSingleton<StageManager>
{
    [SerializeField] private StageButton[] _curMap; // 스테이지 버튼
    [SerializeField] private StageEnemyData[] _stageEnemies; // 각 스테이지별 몬스터정보


    /// <summary> 스테이지 버튼을 눌렀을 때 호출, 현재 스테이지 정보 전달 </summary>
    public void SetStage(int value)
    {
        GameManager.Instance.CurStage = value;
        GameManager.Instance.teamManager.GetStageManager(this);
    }
    /// <summary> 스테이지 별 몬스터 정보 전달 </summary>
    public StageEnemyData[] GetStageEnemyData()
    {
        return _stageEnemies;
    }
    public string GetStarKey(int stage)
    {
        return PlayerPrefs.GetString($"stage{stage}:starscore", $"stage{stage}:3");
    }
    public bool SetStartKey(int stage)
    {
        PlayerPrefs.SetString($"stage{stage}:starscore", $"stage{stage}:3");
        return false;
    }
    private void OnEnable()
    {
        for (int i = 0; i < _curMap.Length; i++)
        {
            _curMap[i].Id = i+1;
            _curMap[i].gameObject.SetActive(true);
        }
        int curstage = PlayerPrefs.GetInt("curstage", 1);
        for (int i = 0; i < curstage-1; i++)
        {
            _curMap[i].ActiveButton();
        }
        _curMap[curstage-1].NowStage();
    }
}
