using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Charater", menuName = "Charater", order = 1)]
public class CharaterData : ScriptableObject
{
    public int id;
    public string Name;
    public string SkillName;
    public string SkillText;
    public int Level;
    public float Range;
    public float MoveSpeed;
    public float AttackSpeed;
    public float AttaackDealy;
    public float MaxHelath;
    public float Health;
    public float Damage;
    public float Armor;
    public float SkillCollDown;
    public float DefaultSkillCollDown;
    public float SkillCollDownSpeed;
    public int SkillDamage;
    public Sprite Icon;
    public Sprite FirstSynergyIcon;
    public Sprite SecondSynergyIcon;
    public ParticleSystem SkillEffect;
    public List<Synergy> characteristic = new List<Synergy>();
    public HeroType heroType;
    private float _damage;
    private float _defalutSkillCollDown;

    public bool SaveDefaultState()
    {
        _damage = Damage;
        _defalutSkillCollDown = DefaultSkillCollDown;
        return false;
    }
    public bool ReturnState()
    {
        Damage = _damage;
        DefaultSkillCollDown = _defalutSkillCollDown;
        return false;
    }
}
public enum HeroType
{
    Healer, Dealer
}
public enum Synergy
{
    Warrior, Wizard, Holy, Fire, Earth
}
