using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingTeamManager : MonoBehaviour
{
    [SerializeField] private Button BTN_StartBattle;
    [SerializeField] private Button BTN_Charater;
    [SerializeField] private Transform TRAN_SpawnCharterIcon;
    [SerializeField] private Transform TRAN_CharaterSet;
    public List<CharaterData> _teamList;
    private List<GameObject> _icon;
    private void OnEnable()
    {
        BTN_StartBattle.onClick.AddListener(() => TeamManager.Instance.GameStart());
        _icon = new List<GameObject>();
        _teamList = new List<CharaterData>();
        TeamManager.Instance.GetSettingTeamManager(this);
    }
    public List<CharaterData> GetTeamList()
    {
        return _teamList;
    }
    public void GetCharater()
    {
        CharaterData[] playerData = PlayerData.Instance.PlayerCharater;

        for (int i = 0; i < playerData.Length; i++)
        {
            Button charatericon = Instantiate(BTN_Charater, TRAN_SpawnCharterIcon);
            _icon.Add(charatericon.gameObject);

            Transform charaterTexture = charatericon.transform.GetChild(1);
            Transform charaterFirstSynergy = charatericon.transform.GetChild(2);
            Transform charaterSecondSynergy = charatericon.transform.GetChild(3);
            Transform charaterLevel = charatericon.transform.GetChild(4);

            Image charaterIconTexure = charaterTexture.GetComponent<Image>();
            Image charaterIconFirstSynergy = charaterFirstSynergy.GetComponent<Image>();
            Image charaterIconSecondSynergy = charaterSecondSynergy.GetComponent<Image>();
            TextMeshProUGUI charaterIconLevel = charaterLevel.GetComponent<TextMeshProUGUI>();

            charaterIconTexure.sprite = playerData[i].Icon;
            charaterIconFirstSynergy.sprite = playerData[i].FirstSynergyIcon;
            charaterIconSecondSynergy.sprite = playerData[i].SecondSynergyIcon;
            charaterIconLevel.text = "LV." + playerData[i].Level.ToString();

            int currentIndex = i;
            charatericon.onClick.AddListener(() => SetTeam(playerData[currentIndex]));
           // charatericon.onClick.AddListener(() => TeamManager.Instance.SetCharater(playerData[currentIndex]));
        }
    }
    public void SetTeam(CharaterData charaterData)
    {
        BTN_StartBattle.gameObject.SetActive(true);
        Instantiate(_icon[charaterData.id], TRAN_CharaterSet);
        _teamList.Add(charaterData);
    }
}
