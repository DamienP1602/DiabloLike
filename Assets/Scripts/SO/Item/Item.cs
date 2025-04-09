using UnityEngine;

[RequireComponent(typeof(Light), typeof(BoxCollider))]
public class Item : MonoBehaviour
{
    [SerializeField] BaseItem itemData;
    [SerializeField] Light pointLight;

    public BaseItem ItemData => itemData;

    private void Start()
    {
        ItemManager.Instance.AddItem(this);

        pointLight = GetComponent<Light>();
        pointLight.color = itemData.ratity.GetColorFromRarity();
    }

    void Update()
    {
        float _value = (Mathf.Sin(Time.time) / 8.0f) + 0.3f;
        Vector3 _currentPos = transform.position;

        Vector3 _newPos = new Vector3(_currentPos.x, _value, _currentPos.z);
        transform.position = _newPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() is Player _player)
        {
            Inventory _playerInventory = _player.GetComponent<Inventory>();
            if (!_playerInventory)
            {
                Debug.Log("No Inventory Found in => " + itemData.owner.CharacterName + " From => " + itemData.itemName);
                return;
            }
            _playerInventory.AddItem(itemData);
            ItemManager.Instance.RemoveItem(this);
            Destroy(gameObject);
        }
    }

}
