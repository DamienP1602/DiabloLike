using UnityEngine;

[CreateAssetMenu(fileName = "Mana Potion", menuName = "Item/Create Mana Potion")]
public class SO_ManaPotion : BaseItem
{
    public int manaRestaured;

    public override void Execute()
    {
        if (!owner) return;

        StatsComponent _ownerStats = owner.GetComponent<StatsComponent>();
        if (!_ownerStats)
        {
            Debug.Log("No Stats Found in => " + owner.CharacterName + " From => " + itemName);
            return;
        }

        _ownerStats.RestaureMana(manaRestaured);
    }

}
