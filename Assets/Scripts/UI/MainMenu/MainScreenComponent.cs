using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScreenComponent : MonoBehaviour
{
    [SerializeField] CustomButton backgroundButton;

    public CustomButton Button => backgroundButton;

    private void Awake()
    {
        backgroundButton = GetComponent<CustomButton>();
    }
}
