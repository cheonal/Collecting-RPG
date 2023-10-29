using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleManager : MonoSingleton<BattleManager>
{
    [SerializeField] private CameraShaker cameraShaker;
    [SerializeField] private TextMeshProUGUI TXT_Timer;
    [SerializeField] private Slider SLID_FrednlyBar;
    [SerializeField] private Slider SLID_EnemyBar;

    [SerializeField] private GameObject IMG_Black;
    [SerializeField] private GameObject IMG_FrendlySkill;
    [SerializeField] private GameObject IMG_EnemySkill;

    [SerializeField] private Image IMG_FrednlyCharaterTexutre;
    [SerializeField] private Image IMG_EnemyCharaterTexutre;
    [SerializeField] private TextMeshProUGUI TXT_FrendlyText;
    [SerializeField] private TextMeshProUGUI TXT_EnemyText;

    [SerializeField] private Transform[] TRAN_FrednlyArea;
    [SerializeField] private Transform[] TRAN_EnemyArea;
    [SerializeField] private GameObject GO_BattleCharater;

    [SerializeField] private GameObject IMG_ResultWin;
    [SerializeField] private GameObject IMG_ResultLose;

    private float time;
    private List<CharaterData> _curTeam = new List<CharaterData>();
    private List<CharaterData> _enemyTeam = new List<CharaterData>();

    public float _frendlyTeamHealth;
    public float _enemyTeamHealth;

    public float _curfrendlyTeamHealth;
    public float _curenemyTeamHealth;
    private bool _pauseGame;
    private void Start()
    {
        Setting();
        StartBattle();
    }
    public void Setting()
    {
        _curTeam = TeamManager.Instance.CurTeam;
        _enemyTeam = TeamManager.Instance.EnemyTeam;
        for (int i = 0; i < _curTeam.Count; i++)
        {
            GameObject charater = Instantiate(GO_BattleCharater, TRAN_FrednlyArea[i]);
            BattleCharater battleCharater = charater.GetComponent<BattleCharater>();
            battleCharater.SetStatus(0, _curTeam[i]);

            _frendlyTeamHealth += battleCharater._health;
            _curfrendlyTeamHealth = _frendlyTeamHealth;
        }
        for (int i = 0; i < _enemyTeam.Count; i++)
        {
            GameObject charater = Instantiate(GO_BattleCharater, TRAN_EnemyArea[i]);
            BattleCharater battleCharater = charater.GetComponent<BattleCharater>();
            battleCharater.SetStatus(1, _enemyTeam[i]);

            _enemyTeamHealth += battleCharater._health;
            _curenemyTeamHealth = _enemyTeamHealth;
        }

    }
    private void StartBattle()
    {
        _pauseGame = false;
        time = 60f;
    }
    private void Update()
    {
        if (_pauseGame)
            return;

        time -= Time.deltaTime;

        int secound = Mathf.FloorToInt(time % 60);
        int minute = Mathf.FloorToInt(time / 60);
        TXT_Timer.text = $"{minute:00}:{secound:00}";

        SLID_FrednlyBar.value = Mathf.Max(0, _curfrendlyTeamHealth / _frendlyTeamHealth);
        SLID_EnemyBar.value = Mathf.Max(0,_curenemyTeamHealth / _enemyTeamHealth);

        if(SLID_FrednlyBar.value<= 0)
        {
            StartCoroutine(GameOverCorutine("Lose"));
        }
        if (SLID_EnemyBar.value <= 0)
        {
            StartCoroutine(GameOverCorutine("Win"));
        }
    }
    public void BattleOut()
    {
        SceneManager.LoadScene("StageScene");
    }
    private IEnumerator GameOverCorutine(string result)
    {
        _pauseGame = true;
        Time.timeScale = 1f;
        cameraShaker.ResetShake();

        yield return new WaitForSeconds(0.5f);

        if (result == "Lose")
        {
            IMG_ResultLose.SetActive(true);
        }
        if(result == "Win")
        {
            IMG_ResultWin.SetActive(true);
            if (TeamManager.Instance.CurStage <= PlayerPrefs.GetInt("curstage"))
                yield break;

            PlayerPrefs.SetInt("curstage", TeamManager.Instance.CurStage+1);
            PlayerPrefs.SetString($"stage{TeamManager.Instance.CurStage}:starscore", $"stage{TeamManager.Instance.CurStage}:3");
        }
    }
    public void DamageUpdate(float Damage, string tag)
    {
        if(tag == "Freindly")
        {
            _curfrendlyTeamHealth -= Damage;
        }
        if(tag == "Enemy")
        {
            _curenemyTeamHealth -= Damage;
        }
    }
    public void SkillAction(CharaterData charaterData, string tag)
    {
        StartCoroutine(SkillActionCoruntine(charaterData, tag));
    }
    public IEnumerator SkillActionCoruntine(CharaterData charaterData, string tag)
    {
        if(tag == "Freindly")
        {
            IMG_FrendlySkill.SetActive(true);
            IMG_FrednlyCharaterTexutre.sprite = charaterData.Icon;
            TXT_FrendlyText.text = charaterData.SkillName;
        }
        if(tag == "Enemy")
        {
            IMG_EnemySkill.SetActive(true);
            IMG_EnemyCharaterTexutre.sprite = charaterData.Icon;
            TXT_EnemyText.text = charaterData.SkillName;
        }

        IMG_Black.SetActive(true);
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(0.5f);

        if (tag == "Freindly")
        {
            IMG_FrendlySkill.SetActive(false);
        }
        if (tag == "Enemy")
        {
            IMG_EnemySkill.SetActive(false);
        }

        IMG_Black.SetActive(false);
        Time.timeScale = 1f;
        cameraShaker.StartShake();
        yield return new WaitForSeconds(0.3f);

        cameraShaker.ResetShake();
    }
}
