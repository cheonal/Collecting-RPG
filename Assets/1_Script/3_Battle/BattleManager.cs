using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleManager : MonoSingleton<BattleManager>
{
    [Header("=====Manager=====")]
    public ObjectPoolingManager objectPoolingManager;
    [SerializeField] private CameraShaker cameraShaker;

    [Header("=====BattleState=====")]
    [SerializeField] private TextMeshProUGUI TXT_Timer;
    [SerializeField] private Slider SLID_FrednlyBar;
    [SerializeField] private Slider SLID_EnemyBar;

    private float _frendlyTeamHealth;
    private float _enemyTeamHealth;
    private float _curfrendlyTeamHealth;
    private float _curenemyTeamHealth;

    [Header("=====BattleSkill=====")]
    [SerializeField] private GameObject IMG_Black;
    [SerializeField] private GameObject IMG_FrendlySkill;
    [SerializeField] private GameObject IMG_EnemySkill;
    [SerializeField] private Image IMG_FrednlyCharaterTexutre;
    [SerializeField] private Image IMG_EnemyCharaterTexutre;
    [SerializeField] private TextMeshProUGUI TXT_FrendlyText;
    [SerializeField] private TextMeshProUGUI TXT_EnemyText;

    [Header("=====InGame=====")]
    [SerializeField] private Transform[] TRAN_FrednlyArea;
    [SerializeField] private Transform[] TRAN_EnemyArea;
    [SerializeField] private Transform TRAN_TextSpawn;
    [SerializeField] private GameObject GO_BattleCharater;
    [SerializeField] private GameObject IMG_DamageText;
    [SerializeField] private GameObject IMG_ResultWin;
    [SerializeField] private GameObject IMG_ResultLose;

    private List<CharaterData> _curTeam = new List<CharaterData>();
    private List<CharaterData> _enemyTeam = new List<CharaterData>();

    private float _time;
    private bool _pauseGame;

    private void Start()
    {
        Setting();
    }
    /// <summary> TeamManager의 아군, 적군 정보로 BattleCharater 생성, 정보 할당 </summary>
    public void Setting()
    {
        _curTeam = GameManager.Instance.teamManager.CurTeam;
        _enemyTeam = GameManager.Instance.teamManager.EnemyTeam;
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

        // 전투 시작
        _pauseGame = false;
        _time = 60f;
    }
    private void Update()
    {
        if (_pauseGame)
            return;
        TimerControll();
        HelathBarControll();
    }
    private void TimerControll()
    {
        _time -= Time.deltaTime;
        int secound = Mathf.FloorToInt(_time % 60);
        int minute = Mathf.FloorToInt(_time / 60);
        TXT_Timer.text = $"{minute:00}:{secound:00}";
    }
    private void HelathBarControll()
    {
        SLID_FrednlyBar.value = Mathf.Max(0, _curfrendlyTeamHealth / _frendlyTeamHealth);
        SLID_EnemyBar.value = Mathf.Max(0, _curenemyTeamHealth / _enemyTeamHealth);

        if (SLID_FrednlyBar.value <= 0)
        {
            StartCoroutine(GameOverCorutine("Lose"));
        }
        if (SLID_EnemyBar.value <= 0)
        {
            StartCoroutine(GameOverCorutine("Win"));
        }
    }
    /// <summary> 승,패 결과 표시 </summary>
    private IEnumerator GameOverCorutine(string result)
    {
        _pauseGame = true;
        Time.timeScale = 1f;
        cameraShaker.ResetShake();

        yield return new WaitForSeconds(1.5f);

        if (result == "Lose")
        {
            IMG_ResultLose.SetActive(true);
        }
        if (result == "Win")
        {
            IMG_ResultWin.SetActive(true);
            if (GameManager.Instance.CurStage < PlayerPrefs.GetInt("curstage"))
                yield break;

            PlayerPrefs.SetInt("curstage", GameManager.Instance.CurStage + 1);
            PlayerPrefs.SetString($"stage{GameManager.Instance.CurStage}:starscore", $"stage{GameManager.Instance.CurStage}:3");
        }
    }

    /// <summary> 아군, 적군 데미지 값에 따라 팀 체력바 업데이트 </summary>
    public void DamageUpdate(float Damage, string tag)
    {
        if (tag == "Freindly")
        {
            _curfrendlyTeamHealth -= Damage;
        }
        if (tag == "Enemy")
        {
            _curenemyTeamHealth -= Damage;
        }
    }
    /// <summary> 스킬 사용시 아군, 적군에 따라 다른 연출, 카메라 흔들림 효과 </summary>
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
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(1f);

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

    /// <summary> 전투 완료 후 돌아가기 버튼 클릭시 </summary>
    public void BattleOut()
    {
        LoadingManager.LoadScene("MainScene");
        //  PlayerData.Instance.ReturnCharaterState();
   //     SceneManager.LoadScene("StageScene");
    }

    /// <summary> 데미지 텍스트 Transform 반환 </summary>
    public Transform TextTransform()
    {
        return TRAN_TextSpawn;
    }
}
