using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum ExperienceType
{
    LOW = 1,
    MEDIUM = 3,
    HIGH = 5,
    BOSS = 10
}

[Serializable]
public struct StatData
{
    [SerializeField] int value;

    public int Value => value;

    public StatData(int _value) => value = _value;

    public void AddValue(int _amount) => value += _amount;
    public void RemoveValue(int _amount) => value -= _amount;
    public void SetValue(int _newValue) => value = _newValue;
}

public class StatsComponent : MonoBehaviour
{
    public event Action OnDeath = null;
    public event Action<float, float> OnLifeChange = null;
    public event Action<float, float> OnManaChange = null;
    public event Action<float, float> OnGainExperience = null;
    public event Action<StatData> OnLevelUp = null;

    public StatData level = new StatData(1);
    public StatData experience = new StatData(0);
    public StatData experienceCap = new StatData(1);
    public StatData statPoints = new StatData(0);
    public ExperienceType type = ExperienceType.LOW;

    [Header("Ressources")]
    public StatData maxHealth = new StatData(5);
    public StatData currentHealth = new StatData(5);
    public StatData maxMana = new StatData(5);
    public StatData currentMana = new StatData(5);

    [Header("Defensive Stats")]
    public StatData armor = new StatData(0);
    public StatData resistance = new StatData(0);

    [Header("Offensive Stats")]
    public StatData damage = new StatData(1);
    public StatData strength = new StatData(0);
    public StatData intelligence = new StatData(0);
    public StatData agility = new StatData(0);


    public void RemoveHealth(int _value)
    {
        currentHealth.RemoveValue(_value);
        OnLifeChange?.Invoke(currentHealth.Value, maxHealth.Value);

        if (currentHealth.Value <= 0)
            OnDeath?.Invoke();
    }

    public void RestaureHealth(int _value)
    {
        currentHealth.AddValue(_value);

        currentHealth.SetValue(currentHealth.Value > maxHealth.Value ? maxHealth.Value : currentHealth.Value);
        OnLifeChange?.Invoke(currentHealth.Value, maxHealth.Value);
    }

    public void RemoveMana(int _value)
    {
        currentMana.RemoveValue(_value);
        OnManaChange?.Invoke(currentMana.Value, maxMana.Value);
    }

    public void RestaureMana(int _value)
    {
        currentMana.AddValue(_value);
        currentMana.SetValue(currentMana.Value > maxMana.Value ? maxMana.Value : currentMana.Value);
        OnManaChange?.Invoke(currentMana.Value, maxMana.Value);
    }

    public int RetreiveDamageAmount()
    {
        float _strengthBonus = strength.Value / 2.0f;
        int _damageAmount = damage.Value + (int)_strengthBonus;

        return _damageAmount;
    }

    public int RetreiveArmorReduction()
    {
        float _damageReduction = MathF.Floor(armor.Value / 10.0f);

        return (int)_damageReduction;
    }

    public int RetreiveResistanceReduction(int _damage)
    {
        int _newDamage = _damage - resistance.Value;
        if (_newDamage <= 2)
            _newDamage = 2;

        return _newDamage;
    }

    public int RetreiveExperience() => (level.Value * 2) * (int)type;

    public void ComputeNextExperienceCap()
    {
        experienceCap.SetValue((level.Value * 2) + ((level.Value + 1) * 2));
    }

    public void GainExperience(int _experienceAmount)
    {
        while (_experienceAmount > 0)
        {
            experience.AddValue(1);
            _experienceAmount--;

            if (experience.Value == experienceCap.Value)
            {
                level.AddValue(1);
                experience.SetValue(0);
                ComputeNextExperienceCap();
                OnLevelUp?.Invoke(level);
                statPoints.AddValue(4);
                Debug.Log("LEVEL UP => " + level);
            }
        }
        OnGainExperience?.Invoke(experience.Value,experienceCap.Value);
    }

    public void InitFromData(CharacterSaveData _data)
    {
        level.SetValue(_data.level);
        experience.SetValue(_data.experience);
        statPoints.SetValue(_data.statPoints);
        maxHealth.SetValue(_data.health);
        currentHealth.SetValue(_data.health);
        maxMana.SetValue(_data.mana);
        currentMana.SetValue(_data.mana);
        armor.SetValue(_data.armor);
        resistance.SetValue(_data.resistance);
        damage.SetValue(_data.damage);
        strength.SetValue(_data.strength);
        intelligence.SetValue(_data.intelligence);
        agility.SetValue(_data.agility);

    }
}
