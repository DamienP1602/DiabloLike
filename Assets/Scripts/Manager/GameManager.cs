using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public struct PlayerGameInfo
{
    public int playerID;
    public Player objectReference;

    public PlayerGameInfo(int _id, Player _player)
    {
        playerID = _id;
        objectReference = _player;
    }
}

public class GameManager : Singleton<GameManager>
{
    [SerializeField] bool isMultiplayer;
    [SerializeField] List<PlayerGameInfo> allPlayers;
    [SerializeField] Player playerPrefab;

    public List<PlayerGameInfo> AllPlayers => allPlayers;

    public Player Player => allPlayers[0].objectReference;

    protected override void Awake()
    {
        base.Awake();
        allPlayers = new List<PlayerGameInfo>();
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMultiplayer(bool _value) => isMultiplayer = _value;

    public void AddPlayer(Player _player)
    {
        allPlayers.Add(new PlayerGameInfo(allPlayers.Count,_player));
    }
    
    public void CreatePlayer(CharacterSaveData _data, SO_CharacterClass _classData)
    {
        Player _player = Instantiate(playerPrefab);
        _player.InitPlayer(isMultiplayer,_data,_classData);
        AddPlayer(_player);
    }
}
