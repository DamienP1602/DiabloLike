using UnityEngine;

[CreateAssetMenu(fileName = "Healing Potion", menuName = "Item/Create Healing Potion")]
public class SO_HealingPotion : BaseItem
{
    public int lifeRestaured = 0;

    public override void Execute()
    {
        if (!owner) return;

        StatsComponent _ownerStats = owner.GetComponent<StatsComponent>();
        if (!_ownerStats)
        {
            Debug.Log("No Stats Found in => " + owner.CharacterName + " From => " + itemName);
            return;
        }

        _ownerStats.RestaureHealth(lifeRestaured);       
    }
}
