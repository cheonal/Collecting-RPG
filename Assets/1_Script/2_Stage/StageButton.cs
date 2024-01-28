using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageButton : MonoBehaviour
{
    [HideInInspector]
    public int Id;

    [SerializeField] private GameObject[] GO_Star; // 획득한 점수에 따라 별 표시
    [SerializeField] private StagePopupManager popupManager;
    private Button _startBtn;

    private void OnEnable()
    {
        _startBtn = GetComponent<Button>();
    }
    public void StagePopupOn()
    {
        popupManager.gameObject.SetActive(true);
        popupManager.SettingPopupStar(int.Parse(StarKey()));
    }
    /// <summary> 클리어한 스테이지에서 획득한 별에 따라 별 오브젝트 활성화, 클릭 활성화  </summary>
    public void ActiveButton()
    {
        _startBtn.interactable = true;
        ActiveStar();
    }
    private bool ActiveStar()
    {
        int score = int.Parse(StarKey());
        for (int i = 0; i < score; i++)
        {
            GO_Star[i].SetActive(true);
        }
        return false;
    }
    /// <summary> 스테이지에서 획득한 별 반환, 기본값 3개</summary>
    public string StarKey()
    {
        string getValue = StageManager.Instance.GetStarKey(Id);

        if (string.IsNullOrEmpty(getValue))
            return null;
        string[] values = getValue.Split(':');

        string key = values[1];
        return key;
    }
    /// <summary> 현재 진행 가능한 스테이지 클릭 활성화 </summary>
    public bool NowStage()
    {
        _startBtn.interactable = true;
        return false;
    }
}
