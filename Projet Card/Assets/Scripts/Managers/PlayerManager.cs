using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField] Player template = null;

    [SerializeField] Player player = null;
    [SerializeField] Player opponent = null;

    public Player Player => player;
    public Player Opponent => opponent;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Init()
    {
        if (!template) return;

        player = Instantiate(template);
        opponent = Instantiate(template);
    }

    public Player RetreivePlayer(GameSide _side)
    {
        if (_side)
    }
}
