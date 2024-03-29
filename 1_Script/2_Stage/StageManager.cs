using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private StageButton[] _curMap; // 스테이지 버튼
    [SerializeField] private StageEnemyData[] _stageEnemies; // 각 스테이지별 몬스터정보


    /// <summary> 스테이지 버튼을 눌렀을 때 호출, 현재 스테이지 정보 전달 </summary>
    public void SetStage(int value)
    {
        TeamManager.Instance.CurStage = value;
        TeamManager.Instance.GetStageManager(this);
    }
    /// <summary> 스테이지 별 몬스터 정보 전달 </summary>
    public StageEnemyData[] GetStageEnemyData()
    {
        return _stageEnemies;
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
            _curMap[i].CheckClear();
        }
        _curMap[curstage-1].NowStage();
    }
}
