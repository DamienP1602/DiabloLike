using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Item/Create Weapon")]
public class SO_Weapon : BaseItem
{
    public int damage;
    public int strength;
    public int intelligence;
    public int agility;

    public override void Execute()
    {
        // Play Effect if there's one
        // TODO add effect to weapons
    }

    public void Unequip(StatsComponent _stats)
    {
        _stats.damage.RemoveValue(damage);
        _stats.strength.RemoveValue(strength);
        _stats.intelligence.RemoveValue(intelligence);
        _stats.agility.RemoveValue(agility);
    }

    public void Equip(StatsComponent _stats)
    {
        _stats.damage.AddValue(damage);
        _stats.strength.AddValue(strength);
        _stats.intelligence.AddValue(intelligence);
        _stats.agility.AddValue(agility); ;
    }
}
