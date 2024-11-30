using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : Singleton<BoardManager>
{
    Dictionary<string,GameSide> allSides = new Dictionary<string,GameSide>();

    public GameSide PlayerSide => allSides["player"];

    public void Add(GameSide _side,string _id)
    {
        allSides.Add(_id, _side);
    }

}
