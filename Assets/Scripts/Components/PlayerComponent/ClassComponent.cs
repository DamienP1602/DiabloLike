using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassComponent : MonoBehaviour
{
    [SerializeField] SO_CharacterClass classData;

    public SO_CharacterClass ClassData => classData;

    public void SetCharacterClass(SO_CharacterClass _class) => classData = _class;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
