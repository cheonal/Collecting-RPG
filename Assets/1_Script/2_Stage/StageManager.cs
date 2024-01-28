using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoSingleton<StageManager>
{
    [SerializeField] private StageButton[] _curMap; // �������� ��ư
    [SerializeField] private StageEnemyData[] _stageEnemies; // �� ���������� ��������


    /// <summary> �������� ��ư�� ������ �� ȣ��, ���� �������� ���� ���� </summary>
    public void SetStage(int value)
    {
        GameManager.Instance.CurStage = value;
        GameManager.Instance.teamManager.GetStageManager(this);
    }
    /// <summary> �������� �� ���� ���� ���� </summary>
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
