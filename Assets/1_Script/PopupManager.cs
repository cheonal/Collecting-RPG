using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour
{
    [SerializeField] private SettingTeamManager settingTeamManager;
    [SerializeField] private GameObject[] GO_Star;
    [SerializeField] private StageClearResult[] stageClearResult;
    [SerializeField] private Transform TRAN_SpawnArea;
    [SerializeField] private Button BTN_Start;
    private List<GameObject> _curSpawnResult = new List<GameObject>();
    private void OnEnable()
    {
        int curstage = TeamManager.Instance.CurStage;

        for (int i = 0; i < stageClearResult[curstage-1].Result.Length; i++)
        {
            _curSpawnResult.Add(Instantiate(stageClearResult[curstage-1].Result[i], TRAN_SpawnArea));
        }

        BTN_Start.onClick.AddListener(() => settingTeamManager.GetCharater());
    }
    private void OnDisable()
    {
        foreach(GameObject result in _curSpawnResult)
        {
            Destroy(result);
        }
        _curSpawnResult.Clear();
    }
    public bool Setting(int starvalue)
    {
        for (int i = 0; i < starvalue; i++)
        {
            GO_Star[i].SetActive(true);
        }
        return false;
    }
    public void SettingBattle()
    {

    }

}
