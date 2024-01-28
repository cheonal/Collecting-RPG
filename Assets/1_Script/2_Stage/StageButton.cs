using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageButton : MonoBehaviour
{
    [HideInInspector]
    public int Id;

    [SerializeField] private GameObject[] GO_Star; // ȹ���� ������ ���� �� ǥ��
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
    /// <summary> Ŭ������ ������������ ȹ���� ���� ���� �� ������Ʈ Ȱ��ȭ, Ŭ�� Ȱ��ȭ  </summary>
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
    /// <summary> ������������ ȹ���� �� ��ȯ, �⺻�� 3��</summary>
    public string StarKey()
    {
        string getValue = StageManager.Instance.GetStarKey(Id);

        if (string.IsNullOrEmpty(getValue))
            return null;
        string[] values = getValue.Split(':');

        string key = values[1];
        return key;
    }
    /// <summary> ���� ���� ������ �������� Ŭ�� Ȱ��ȭ </summary>
    public bool NowStage()
    {
        _startBtn.interactable = true;
        return false;
    }
}
