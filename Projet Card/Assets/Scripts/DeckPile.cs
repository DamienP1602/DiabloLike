using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckPile : MonoBehaviour
{
    [SerializeField] GameSide side = null;
    [SerializeField] Button button = null;

    public void SetSide(GameSide _side) => side = _side;

    // Start is called before the first frame update
    void Start()
    {
        Init();   
    }

    void Init()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Draw);
    }

    void Draw()
    {
        Player _player = PlayerManager.Instance.RetreivePlayer(side);
    }
}
