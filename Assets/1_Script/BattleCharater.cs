using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleCharater : MonoBehaviour
{
    public CharaterData charaterData;
    [SerializeField] private Image IMG_HpBar;
    [SerializeField] private Image IMG_Charater;
    private float _moveSpeed;
    private float _attackDelay;
    private float _attackSpeed;
    public float _health;
    private float _maxhelath;
    private float _damage;
    private float _armor;
    private float _skillCollDown;
    private float _defaultSkillCollDown;
    private float _skillCollDownSpeed;
    private int _skillDamage;
    private float _range;
    private float _attackTime;
    private Transform _target;
    private BattleCharater _targetCharater;
    private Animator animator;
    private bool _isLeft;
    private bool _isDead;
    public enum Type { Freindly, Enemy}
    public Type thistype;
    void Start()
    {
        _attackTime = 0f;
        animator = GetComponent<Animator>();
        InvokeRepeating("UpdateTartget", 0f, 0.5f);
    }
    void UpdateTartget()
    {
        GameObject[] enemies = null;
        if (thistype == Type.Freindly)
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
        }
        if(thistype == Type.Enemy)
        {
            enemies = GameObject.FindGameObjectsWithTag("Freindly");
        }

        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach(GameObject enemy in enemies)
        {
            float distancToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distancToEnemy < shortestDistance)
            {
                shortestDistance = distancToEnemy;
                nearestEnemy = enemy;
            }
        }
        if(nearestEnemy != null)
        {
            _target = nearestEnemy.transform;
            _targetCharater = _target.GetComponent<BattleCharater>();
            
        }
        else
        {
            _target = null;
        }
    }

    public void SetStatus(int type, CharaterData charater)
    {
        if(type == 0)
        {
            thistype = Type.Freindly;
            gameObject.tag = "Freindly";
        }
        if (type == 1)
        {
            thistype = Type.Enemy;
            gameObject.tag = "Enemy";
        }
        charaterData = charater;
        IMG_Charater.sprite = charaterData.Icon;

        _moveSpeed = charaterData.MoveSpeed;

        _attackSpeed = charaterData.AttackSpeed;
        _attackDelay = charaterData.AttaackDealy;
        _range = charaterData.Range;
        _damage = charaterData.Damage;

        _maxhelath = charaterData.Health;
        _health = charaterData.Health;
        _armor = charaterData.Armor;

        _skillCollDown = charaterData.SkillCollDown;
        _defaultSkillCollDown = charaterData.SkillCollDown;
        _skillCollDownSpeed = charaterData.SkillCollDownSpeed;
        _skillDamage = charaterData.SkillDamage;
    }
    void Update()
    {
        UpdateHpbar();

        if (_target == null)
            return;

        if (_isDead)
            return;

        CheckMove();
    }
    private void CheckMove()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        Vector3 newEulerAngles = rectTransform.eulerAngles;

        Vector3 dir = _target.position - transform.position;
        if (dir.x < 0)
        {
            _isLeft = true;
        }
        else
        {
            _isLeft = false;
        }
        rectTransform.eulerAngles = newEulerAngles;
        if (dir.magnitude > _range)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.position, _moveSpeed * Time.deltaTime);
            if (_isLeft)
            {
                animator.SetBool("IsLeft", true);
                animator.SetBool("IsRight", false);
            }
            else
            {
                animator.SetBool("IsRight", true);
                animator.SetBool("IsLeft", false);
            }
        }
        else
        {
            Attack();
        }
    }
    private void UpdateHpbar()
    {
        float amout = _health / _maxhelath;
        if (amout <= 0)
        {
            amout = 0;
        }
        IMG_HpBar.fillAmount = amout;
    }
    void Attack()
    {
        _skillCollDown -= Time.deltaTime * _skillCollDownSpeed;
        
        _attackTime -= Time.deltaTime * _attackSpeed;
        if(_attackTime <= 0)
        {
            if (_isLeft)
            {
                animator.SetTrigger("AttackLeft");
            }
            else
            {
                animator.SetTrigger("AttackRight");
            }

            _targetCharater.Damage(_damage);
            _attackTime = _attackDelay;
        }
        if(_skillCollDown <= 0)
        {
            StartCoroutine(CharaterSkillAction());
            _skillCollDown = _defaultSkillCollDown;

        }
    }
    IEnumerator CharaterSkillAction()
    {
        BattleManager.Instance.SkillAction(charaterData, gameObject.tag);
        yield return new WaitForSeconds(0.5f);
        _targetCharater.Damage(_skillDamage);
    }
    public void Damage(float value)
    {
        BattleManager.Instance.DamageUpdate(value, gameObject.tag);
        StartCoroutine(DamageCorutine(value));

    }
    IEnumerator DamageCorutine(float value)
    {
        _health -= value;
        if (_health <= 0)
        {
            if (_isLeft)
            {
                animator.SetTrigger("DeadLeft");
            }
            else
            {
                animator.SetTrigger("DeadRight");
            }
            _isDead = true;
            gameObject.tag = "Dead";
            yield return new WaitForSeconds(1f);
            Destroy(gameObject);
        }
        IMG_Charater.color = new Color(0, 0, 0, 0);
        yield return new WaitForSeconds(0.4f);
        IMG_Charater.color = new Color(1, 1, 1, 1);
    }
}
