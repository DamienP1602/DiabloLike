using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CardHandler : MonoBehaviour
{
    [SerializeField] GameObject card = null;
    [SerializeField] Vector3 startingPosition = Vector3.zero;
    [SerializeField] bool isSelected = false;
    [SerializeField] Button tempButton = null;

    public Transform Transform => card.transform;

    // Start is called before the first frame update
    void Start()
    {
        tempButton = card.AddComponent<Button>();
        tempButton.onClick.AddListener(ClickCard);
    }

    // Update is called once per frame
    void Update()
    {
        if (isSelected)
            Move();
    }

    public void ClickCard()
    {
        if (!isSelected)
            startingPosition = card.transform.position;
        else if (isSelected)
        {

            card.transform.position = startingPosition;
        }

        isSelected = !isSelected;

    }
    void Move()
    {
        card.transform.position = Input.mousePosition;
    }
}
