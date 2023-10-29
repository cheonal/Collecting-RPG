using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageButton : MonoBehaviour
{
    [SerializeField] private GameObject[] GO_Star;
    [SerializeField] private PopupManager popupManager;
    public int Id;
    private Button _startBtn;
    private void OnEnable()
    {
        _startBtn = GetComponent<Button>();
    }
    public void PopupOn()
    {
        popupManager.gameObject.SetActive(true);
        popupManager.Setting(int.Parse(StarKey()));
    }
    public void CheckClear()
    {
        _startBtn.interactable = true;
        int score = int.Parse(StarKey());
        for (int i = 0; i < score; i++)
        {
            GO_Star[i].SetActive(true);
        }
    }
    public string StarKey()
    {
        string getValue = PlayerPrefs.GetString($"stage{Id}:starscore", $"stage{Id}:3");

        if (string.IsNullOrEmpty(getValue))
            return null;
        string[] values = getValue.Split(':');

        string key = values[1];
        return key;
    }
    public bool NowStage()
    {
        _startBtn.interactable = true;
        return false;
    }
}
