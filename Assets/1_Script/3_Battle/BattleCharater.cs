using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BattleCharater : MonoBehaviour
{
    [HideInInspector]
    public float _health;

    private CharaterData charaterData;
    [SerializeField] private Image IMG_HpBar;
    [SerializeField] private Image IMG_Charater;

    [Header("=====CharaterData=====")]
    private HeroType _heroType;
    private ParticleSystem _skillEffect;
    private float _moveSpeed;
    private float _attackDelay;
    private float _attackSpeed;
    private float _maxhelath;
    private float _damage;
    private float _armor;
    private float _skillCollDown;
    private float _defaultSkillCollDown;
    private float _skillCollDownSpeed;
    private float _range;
    private float _attackTime;
    private int _skillDamage;

    [Header("=====CharaterState=====")]
    private Transform _target;
    private BattleCharater _targetCharater;
    private Animator animator;
    private bool _isLeft; // ��ǥ���� X ��ǥ������ �¿� ���
    private bool _isDead;
    public enum Type { Freindly, Enemy}
    public Type thistype;

    void Start()
    {
        _attackTime = 0f;
        animator = GetComponent<Animator>();
        InvokeRepeating("UpdateTartget", 0f, 0.5f);
    }
    /// <summary> 0.5�ʸ��� ���� Ž�� </summary>
    void UpdateTartget()
    {
        GameObject[] enemies = null;
        if (thistype == Type.Freindly)
        {
            enemies = GameObject.FindGameObjectsWithTag(GameManager.Instance.EnemyTag);
        }
        if(thistype == Type.Enemy)
        {
            enemies = GameObject.FindGameObjectsWithTag(GameManager.Instance.TeamTag);
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
    /// <summary> ĳ���� Ÿ��(�Ʊ�, ����) �ɷ�ġ �Ҵ� </summary>
    public void SetStatus(int type, CharaterData charater)
    {
        if(type == 0)
        {
            thistype = Type.Freindly;
            gameObject.tag = GameManager.Instance.TeamTag;
        }
        if (type == 1)
        {
            thistype = Type.Enemy;
            gameObject.tag = GameManager.Instance.EnemyTag;
        }
        charaterData = charater;
        _heroType = charaterData.heroType;
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
        _skillEffect = charaterData.SkillEffect;
    }
    void Update()
    {
        UpdateHpbar();

        if (_target == null)
            return;

        if (_isDead)
            return;

        Move();
    }
    private void Move()
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
            _attackTime = _attackDelay;
        }
    }
    IEnumerator CharaterSkillAction()
    {
        // ���� ĳ���Ͷ�� ��ų ��������ŭ ����
        if (_heroType == HeroType.Dealer)
        {
            ParticleSystem Effect = Instantiate(_skillEffect, transform.transform.position, transform.rotation);
            Destroy(Effect, 2f);

            BattleManager.Instance.SkillAction(charaterData, gameObject.tag);
            yield return new WaitForSeconds(0.5f);
            _targetCharater.Damage(_skillDamage);
        }

        // ���� ĳ���Ͷ�� ��ų ������ ��ŭ ���� ��ü ����, ���ݼӵ� ���
        if (_heroType == HeroType.Healer)
        {
            BattleManager.Instance.SkillAction(charaterData, gameObject.tag);
            yield return new WaitForSeconds(0.5f);

            GameObject[] frendly = GameObject.FindGameObjectsWithTag(GameManager.Instance.TeamTag);
            foreach(GameObject freind in frendly)
            {
                BattleCharater battleCharater = freind.GetComponent<BattleCharater>();
                battleCharater._health += _skillDamage;
                battleCharater._attackSpeed *= 1.1f;

                ParticleSystem Effect = Instantiate(_skillEffect, battleCharater.transform.position, transform.rotation, battleCharater.transform);
                Destroy(Effect, 2f);
            }
        }
    }

    /// <summary> �ǰݽ� </summary>
    public void Damage(float value)
    {
        StartCoroutine(DamageCorutine(value));
    }
    IEnumerator DamageCorutine(float value)
    {
        // ������Ʈ Ǯ������ ������ �ؽ�Ʈ ����
        GameObject DamageText = BattleManager.Instance.objectPoolingManager.Get(0);
        DamageText.transform.SetParent(BattleManager.Instance.TextTransform(), false);
        DamageText.transform.position = transform.position + Vector3.up * 8f;

        TextMeshProUGUI damageText = DamageText.GetComponent<TextMeshProUGUI>();


        // ���¸�ŭ ������ �� ����
        float guardedDamage = Mathf.Max(0, value - _armor);

        damageText.text = Mathf.RoundToInt(guardedDamage).ToString();
        _health -= guardedDamage;
        BattleManager.Instance.DamageUpdate(guardedDamage, gameObject.tag);

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
            DamageText.SetActive(false);
            yield return new WaitForSeconds(1f);
            Destroy(gameObject);
            yield break;
        }

        IMG_Charater.color = new Color(0, 0, 0, 0);
        yield return new WaitForSeconds(0.4f);

        DamageText.SetActive(false);
        IMG_Charater.color = new Color(1, 1, 1, 1);
    }
}
