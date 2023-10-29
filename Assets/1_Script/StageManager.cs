using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private StageButton[] _curMap;
    [SerializeField] private StageEnemyData[] _stageEnemies;
    public void SetStage(int value)
    {
        TeamManager.Instance.CurStage = value;
        TeamManager.Instance.GetStageManager(this);
    }
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
