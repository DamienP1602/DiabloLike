using System;
using Unity.Netcode;
using UnityEngine;

[Serializable]
class AttackData
{
    public float attackCooldown = 1.0f;
    public float attackDelay = 1.0f;

    public float currentCooldown = 0.0f;
    public bool canAttack = true;

    public void Recover()
    {
        currentCooldown += Time.deltaTime;
        if (currentCooldown >= attackCooldown)
        {
            currentCooldown = 0.0f;
            canAttack = true;
        }
    }
}

public class AttackComponent : NetworkBehaviour
{
    public event Action<float> onLaunchAttack = null;
    public event Action OnKillTarget = null;

    AnimationComponent animRef = null;

    [SerializeField, Range(1.0f, 10.0f)] float range = 1.0f;
    [SerializeField] bool drawRange = false;
    [SerializeField] GameObject target = null;

    [SerializeField] AttackData data;

    public float Range => range;

    public void SetTarget(GameObject _target) => target = _target;

    // Start is called before the first frame update    
    void Start()
    {
        animRef = GetComponent<AnimationComponent>();
    }


    public bool IsCloseEnough() => Vector3.Distance(transform.position, target.transform.position) <= range;

    // Update is called once per frame
    void Update()
    {
        if (!data.canAttack)
        {
            data.Recover();
        }
        else if (target)
        {
            if (IsCloseEnough())
                LaunchAttack();
        }
    }

    private void OnDrawGizmos()
    {
        if (drawRange)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position,range);
        }
    }

    public void LaunchAttack()
    {
        animRef.StartAttackAnimation();

        data.canAttack = false;
        onLaunchAttack?.Invoke(data.attackDelay - 0.1f);

        Invoke(nameof(DealDamage),data.attackDelay);
        Invoke(nameof(StopAnimation),data.attackCooldown);
    }

    public void DealDamage()
    {
        if (!target || !IsCloseEnough()) return;

        StatsComponent _targetStats = target.GetComponent<StatsComponent>();
        if (!_targetStats) return;

        StatsComponent _stats = GetComponent<StatsComponent>();

        int _damageDeal = GetDamageDeal(_stats,_targetStats);

        _targetStats.RemoveHealth(_damageDeal);
        target = null;

        if (_targetStats.currentHealth.Value <= 0)
        {

            OnKillTarget?.Invoke();

            int _experienceGain = _targetStats.RetreiveExperience();

            _stats.GainExperience(_experienceGain);
        }
    }

    public int GetDamageDeal(StatsComponent _damageDealer, StatsComponent _target)
    {
        int _damageDeal = _damageDealer.RetreiveDamageAmount();
        int _damageReduction = _target.RetreiveArmorReduction();

        int _result = _damageDeal - _damageReduction;
        _result = _result <= 0 ? 1 : _result;

        return _result;
    }

    void StopAnimation()
    {
        animRef.StopAttackAnimation();
    }

}
