using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingTeamManager : MonoBehaviour
{
    [SerializeField] private Button BTN_StartBattle;
    [SerializeField] private Button BTN_Charater;
    [SerializeField] private Transform TRAN_CharterIcon;
    [SerializeField] private Transform TRAN_SelectedCharaterIcon;
    public List<CharaterData> _frendlyTeamList;
    private List<GameObject> _icon;


    private void OnEnable()
    {
        BTN_StartBattle.onClick.AddListener(() => GameManager.Instance.teamManager.GameStart());
        _icon = new List<GameObject>();
        _frendlyTeamList = new List<CharaterData>();
        GameManager.Instance.teamManager.GetSettingTeamManager(this);
    }
    public List<CharaterData> GetTeamList()
    {
        return _frendlyTeamList;
    }

    /// <summary> 현재 보유중인 캐릭터 선택할 수 있는 아이콘으로 생성 </summary>
    public void GetCharater()
    {
        gameObject.SetActive(true);

        CharaterData[] playerData = GameManager.Instance.playerData.PlayerCharater;

        for (int i = 0; i < playerData.Length; i++)
        {
            Button charatericon = Instantiate(BTN_Charater, TRAN_CharterIcon);
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
        }
    }
    /// <summary> 캐릭터 아이콘 클릭시 호출, 편성창에 표시 </summary>
    public void SetTeam(CharaterData charaterData)
    {
        if (_frendlyTeamList.Count > 2)
            return;

        BTN_StartBattle.gameObject.SetActive(true);
        Instantiate(_icon[charaterData.id], TRAN_SelectedCharaterIcon);
        _frendlyTeamList.Add(charaterData);
    }
}
