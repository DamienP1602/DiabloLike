using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClassPanelUI : MonoBehaviour
{
    [SerializeField] Button showStatsButton;
    [SerializeField] Button showCompetencesButton;

    [SerializeField] StatsPanel statsPanel;
    [SerializeField] CompetencesPanel competencesPanel;

    public StatsPanel StatsPanel => statsPanel;
    public CompetencesPanel CompetencesPanel => competencesPanel;

    void Awake()
    {
        showStatsButton.onClick.AddListener(() => SetStatsPanelStatus(true));
        showStatsButton.onClick.AddListener(() => SetCompetencesPanelStatus(false));

        showCompetencesButton.onClick.AddListener(() => SetStatsPanelStatus(false));
        showCompetencesButton.onClick.AddListener(() => SetCompetencesPanelStatus(true));
    }

    private void SetStatsPanelStatus(bool _value)
    {
        statsPanel.gameObject.SetActive(_value);
    }

    private void SetCompetencesPanelStatus(bool _value)
    {
        competencesPanel.gameObject.SetActive(_value);
        competencesPanel.Open();
    }

    public void Open()
    {
        gameObject.SetActive(true);
        statsPanel.Open();
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
