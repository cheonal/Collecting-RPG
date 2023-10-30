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

    /// <summary> 현재 스테이지에 따라서 ScriptObject에 저장해둔 보상 팝업에 표시 </summary>
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
    /// <summary> 현재 스테이지에서 획득한 별의 개수에 따라 팝업창에도 별 개수 표시 </summary>
    public bool SettingPopupStar(int starvalue)
    {
        for (int i = 0; i < starvalue; i++)
        {
            GO_Star[i].SetActive(true);
        }
        return false;
    }
}
