using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TeamManager : MonoBehaviour
{
    [HideInInspector]
    public List<CharaterData> CurTeam;
    [HideInInspector]
    public List<CharaterData> EnemyTeam;

    [SerializeField] private Button BTN_Charater;

    private StageManager _stageManager;
    private SettingTeamManager _settingTeamManager;
    private List<int> curSynergyValue;
    public bool GetStageManager(StageManager stageManager)
    {
        _stageManager = stageManager;
        return false;
    }
    public bool GetSettingTeamManager(SettingTeamManager settingTeamManager)
    {
        _settingTeamManager = settingTeamManager;
        return false;
    }

    public void GameStart()
    {
        SetCharater();
        CheckSynergy();
        LoadingManager.LoadScene("BattleScene");
      //  SceneManager.LoadScene("BattleScene");
    }
    /// <summary> 캐릭터 편성창에서 선택한 캐릭터들, 현재 스테이지 적군 몬스터 정보 저장 </summary>
    public void SetCharater()
    {
        CurTeam = new List<CharaterData>();
        EnemyTeam = new List<CharaterData>();

        for (int i = 0; i < _settingTeamManager.GetTeamList().Count; i++)
        {
            CurTeam = _settingTeamManager.GetTeamList();
        }
        int selectStage = GameManager.Instance.CurStage;

        for (int i = 0; i < _stageManager.GetStageEnemyData()[selectStage - 1].Enemy.Length; i++)
        {
            EnemyTeam.Add(_stageManager.GetStageEnemyData()[selectStage - 1].Enemy[i]);
        }
    }

    /// <summary> 게임 시작전 시너지 확인 </summary>
    public void CheckSynergy()
    {
        curSynergyValue = new List<int>();
        for (int i = 0; i < Enum.GetNames(typeof(Synergy)).Length; i++)
        {
            curSynergyValue.Add(0);
        }
        foreach (CharaterData charater in CurTeam)
        {
            for (int i = 0; i < charater.characteristic.Count; i++)
            {
                curSynergyValue[(int)charater.characteristic[i]]++;
            }
        }
        ActiveSynergy();
    }

    /// <summary> 배치한 캐릭터 시너지에 따라 능력치 상승 </summary>
    private bool ActiveSynergy()
    {
        #region  Warrior
        if (curSynergyValue[(int)Synergy.Warrior] >= 2 && curSynergyValue[(int)Synergy.Warrior] < 3)
        {
            WarriorSynergy(0);
        }
        else if (curSynergyValue[(int)Synergy.Warrior] >= 3 && curSynergyValue[(int)Synergy.Warrior] < 5)
        {
            WarriorSynergy(1);
        }
        else if (curSynergyValue[(int)Synergy.Warrior] >= 5 && curSynergyValue[(int)Synergy.Warrior] < 6)
        {
            WarriorSynergy(2);
        }
        else if (curSynergyValue[(int)Synergy.Warrior] >= 6 && curSynergyValue[(int)Synergy.Warrior] < 7)
        {
            WarriorSynergy(3);
        }
        else if (curSynergyValue[(int)Synergy.Warrior] >= 7 && curSynergyValue[(int)Synergy.Warrior] < 8)
        {
            WarriorSynergy(4);
        }
        else if (curSynergyValue[(int)Synergy.Warrior] >= 8 && curSynergyValue[(int)Synergy.Warrior] < 9)
        {
            WarriorSynergy(5);
        }
        #endregion
        #region Wizard
        if (curSynergyValue[(int)Synergy.Wizard] >= 2 && curSynergyValue[(int)Synergy.Wizard] < 3)
        {
            WizardSynergy(0);
        }
        else if (curSynergyValue[(int)Synergy.Wizard] >= 3 && curSynergyValue[(int)Synergy.Wizard] < 4)
        {
            WizardSynergy(1);
        }
        else if (curSynergyValue[(int)Synergy.Wizard] >= 4 && curSynergyValue[(int)Synergy.Wizard] < 5)
        {
            WizardSynergy(2);
        }
        else if (curSynergyValue[(int)Synergy.Wizard] >= 5 && curSynergyValue[(int)Synergy.Wizard] < 6)
        {
            WizardSynergy(3);
        }
        else if (curSynergyValue[(int)Synergy.Wizard] >= 6 && curSynergyValue[(int)Synergy.Wizard] < 7)
        {
            WizardSynergy(4);
        }
        else if (curSynergyValue[(int)Synergy.Wizard] >= 7 && curSynergyValue[(int)Synergy.Wizard] < 8)
        {
            WizardSynergy(5);
        }
        #endregion
        #region Holy
        if (curSynergyValue[(int)Synergy.Holy] >= 1 && curSynergyValue[(int)Synergy.Holy] < 2)
        {
            HolySynergy(0);
        }
        else if (curSynergyValue[(int)Synergy.Holy] >= 2 && curSynergyValue[(int)Synergy.Holy] < 3)
        {
            HolySynergy(1);
        }

        else if (curSynergyValue[(int)Synergy.Holy] >= 3 && curSynergyValue[(int)Synergy.Holy] < 5)
        {
            HolySynergy(2);
        }
        else if (curSynergyValue[(int)Synergy.Holy] >= 5 && curSynergyValue[(int)Synergy.Holy] < 7)
        {
            HolySynergy(3);
        }
        else if (curSynergyValue[(int)Synergy.Holy] >= 7 && curSynergyValue[(int)Synergy.Holy] < 9)
        {
            HolySynergy(4);
        }
        else if (curSynergyValue[(int)Synergy.Holy] >= 9 && curSynergyValue[(int)Synergy.Holy] < 10)
        {
            HolySynergy(5);
        }
        #endregion
        #region Fire
        if (curSynergyValue[(int)Synergy.Fire] >= 1 && curSynergyValue[(int)Synergy.Fire] < 2)
        {
            FireSynergy(0);
        }
        else if (curSynergyValue[(int)Synergy.Fire] >= 2 && curSynergyValue[(int)Synergy.Fire] < 3)
        {
            FireSynergy(1);
        }

        else if (curSynergyValue[(int)Synergy.Fire] >= 3 && curSynergyValue[(int)Synergy.Fire] < 5)
        {
            FireSynergy(2);
        }
        else if (curSynergyValue[(int)Synergy.Fire] >= 5 && curSynergyValue[(int)Synergy.Fire] < 7)
        {
            FireSynergy(3);
        }
        else if (curSynergyValue[(int)Synergy.Fire] >= 7 && curSynergyValue[(int)Synergy.Fire] < 9)
        {
            FireSynergy(4);
        }
        else if (curSynergyValue[(int)Synergy.Fire] >= 9 && curSynergyValue[(int)Synergy.Fire] < 10)
        {
            FireSynergy(5);
        }
        #endregion
        #region Earth
        if (curSynergyValue[(int)Synergy.Earth] >= 2 && curSynergyValue[(int)Synergy.Earth] < 3)
        {
            EarthSynergy(0);
        }
        else if (curSynergyValue[(int)Synergy.Earth] >= 3 && curSynergyValue[(int)Synergy.Earth] < 4)
        {
            EarthSynergy(1);
        }

        else if (curSynergyValue[(int)Synergy.Earth] >= 4 && curSynergyValue[(int)Synergy.Earth] < 5)
        {
            EarthSynergy(2);
        }
        else if (curSynergyValue[(int)Synergy.Earth] >= 5 && curSynergyValue[(int)Synergy.Earth] < 7)
        {
            EarthSynergy(3);
        }
        else if (curSynergyValue[(int)Synergy.Earth] >= 7 && curSynergyValue[(int)Synergy.Earth] < 9)
        {
            EarthSynergy(4);
        }
        else if (curSynergyValue[(int)Synergy.Earth] >= 9 && curSynergyValue[(int)Synergy.Earth] < 10)
        {
            EarthSynergy(5);
        }
        #endregion
        return false;
    }
    private bool WarriorSynergy(int value)
    {
        switch (value)
        {
            case 0:
                foreach (CharaterData charater in CurTeam)
                {
                    if (charater.characteristic.Contains(Synergy.Warrior))
                    {
                        charater.SkillCollDown -= 1f;
                    }
                }
                break;
            case 1:
                foreach (CharaterData charater in CurTeam)
                {
                    if (charater.characteristic.Contains(Synergy.Warrior))
                    {
                        charater.SkillCollDown -= 2f;
                    }
                }
                break;
            case 2:
                foreach (CharaterData charater in CurTeam)
                {
                    if (charater.characteristic.Contains(Synergy.Warrior))
                    {
                        charater.SkillCollDown -= 3f;
                    }
                }
                break;
            case 3:
                foreach (CharaterData charater in CurTeam)
                {
                    if (charater.characteristic.Contains(Synergy.Warrior))
                    {
                        charater.SkillCollDown -= 4f;
                    }
                }
                break;
            case 4:
                foreach (CharaterData charater in CurTeam)
                {
                    if (charater.characteristic.Contains(Synergy.Warrior))
                    {
                        charater.SkillCollDown -= 5f;
                    }
                }
                break;
            case 5:
                foreach (CharaterData charater in CurTeam)
                {
                    if (charater.characteristic.Contains(Synergy.Warrior))
                    {
                        charater.SkillCollDown -= 6f;
                    }
                }
                break;
            default:
                return true;
        }
        return false;
    }
    private bool WizardSynergy(int value)
    {
        switch (value)
        {
            case 0:
                foreach (CharaterData charater in CurTeam)
                {
                    if (charater.characteristic.Contains(Synergy.Wizard))
                    {
                        charater.SkillCollDownSpeed *= 1.25f;
                    }
                }
                break;
            case 1:
                foreach (CharaterData charater in CurTeam)
                {
                    if (charater.characteristic.Contains(Synergy.Wizard))
                    {
                        charater.SkillCollDownSpeed *= 1.35f;
                    }
                }
                break;
            case 2:
                foreach (CharaterData charater in CurTeam)
                {
                    if (charater.characteristic.Contains(Synergy.Wizard))
                    {
                        charater.SkillCollDownSpeed *= 1.45f;
                    }
                }
                break;
            case 3:
                foreach (CharaterData charater in CurTeam)
                {
                    if (charater.characteristic.Contains(Synergy.Wizard))
                    {
                        charater.SkillCollDownSpeed *= 1.55f;
                    }
                }
                break;
            case 4:
                foreach (CharaterData charater in CurTeam)
                {
                    if (charater.characteristic.Contains(Synergy.Wizard))
                    {
                        charater.SkillCollDownSpeed *= 1.65f;
                    }
                }
                break;
            case 5:
                foreach (CharaterData charater in CurTeam)
                {
                    if (charater.characteristic.Contains(Synergy.Wizard))
                    {
                        charater.SkillCollDownSpeed *= 1.85f;
                    }
                }
                break;
            default:
                return true;
        }
        return false;
    }
    private bool HolySynergy(int value)
    {
        switch (value)
        {
            case 0:
                foreach (CharaterData charater in CurTeam)
                {
                    charater.Damage *= 1.2f;
                }
                break;
            case 1:
                foreach (CharaterData charater in CurTeam)
                {
                    charater.Damage *= 1.3f;
                }
                break;
            case 2:
                foreach (CharaterData charater in CurTeam)
                {
                    charater.Damage *= 1.4f;
                }
                break;
            case 3:
                foreach (CharaterData charater in CurTeam)
                {
                    charater.Damage *= 1.6f;
                }
                break;
            case 4:
                foreach (CharaterData charater in CurTeam)
                {
                    charater.Damage *= 1.75f;
                }
                break;
            case 5:
                foreach (CharaterData charater in CurTeam)
                {
                    charater.Damage *= 1.85f;
                }
                break;
            default:
                return true;
        }
        return false;
    }
    private bool FireSynergy(int value)
    {
        switch (value)
        {
            case 0:
                foreach (CharaterData charater in CurTeam)
                {
                    if (charater.characteristic.Contains(Synergy.Fire))
                    {
                        charater.Damage *= 1.25f;
                    }
                }
                break;
            case 1:
                foreach (CharaterData charater in CurTeam)
                {
                    if (charater.characteristic.Contains(Synergy.Fire))
                    {
                        charater.Damage *= 1.4f;
                    }
                }
                break;
            case 2:
                foreach (CharaterData charater in CurTeam)
                {
                    if (charater.characteristic.Contains(Synergy.Fire))
                    {
                        charater.Damage *= 1.6f;
                    }
                }
                break;
            case 3:
                foreach (CharaterData charater in CurTeam)
                {
                    if (charater.characteristic.Contains(Synergy.Fire))
                    {
                        charater.Damage *= 2f;
                    }
                }
                break;
            case 4:
                foreach (CharaterData charater in CurTeam)
                {
                    if (charater.characteristic.Contains(Synergy.Fire))
                    {
                        charater.Damage *= 2.5f;
                    }
                }
                break;
            case 5:
                foreach (CharaterData charater in CurTeam)
                {
                    if (charater.characteristic.Contains(Synergy.Fire))
                    {
                        charater.Damage *= 3f;
                    }
                }
                break;
            default:
                return true;
        }
        return false;
    }
    private bool EarthSynergy(int value) 
    {
        switch (value)
        {
            case 0:
                foreach (CharaterData charater in CurTeam)
                {
                    if (charater.characteristic.Contains(Synergy.Earth))
                    {
                        charater.Armor *= 1.7f;
                    }
                }
                break;
            case 1:
                foreach (CharaterData charater in CurTeam)
                {
                    if (charater.characteristic.Contains(Synergy.Earth))
                    {
                        charater.Armor *= 1.8f;
                    }
                }
                break;
            case 2:
                foreach (CharaterData charater in CurTeam)
                {
                    if (charater.characteristic.Contains(Synergy.Earth))
                    {
                        charater.Armor *= 2.1f;
                    }
                }
                break;
            case 3:
                foreach (CharaterData charater in CurTeam)
                {
                    if (charater.characteristic.Contains(Synergy.Earth))
                    {
                        charater.Armor *= 2.5f;
                    }
                }
                break;
            case 4:
                foreach (CharaterData charater in CurTeam)
                {
                    if (charater.characteristic.Contains(Synergy.Earth))
                    {
                        charater.Armor *= 3.0f;
                    }
                }
                break;
            case 5:
                foreach (CharaterData charater in CurTeam)
                {
                    if (charater.characteristic.Contains(Synergy.Earth))
                    {
                        charater.Armor *= 3.5f;
                    }
                }
                break;
            default:
                return true;
        }
        return false;
    }
}
