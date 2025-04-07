using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScreenComponent : MonoBehaviour
{
    [SerializeField] Button backgroundButton;

    public Button Button => backgroundButton;

    private void Awake()
    {
        backgroundButton = GetComponent<Button>();
    }
}
