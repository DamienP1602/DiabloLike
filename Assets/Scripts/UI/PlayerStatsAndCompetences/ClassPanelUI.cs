using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClassPanelUI : MonoBehaviour
{
    [SerializeField] CustomButton showStatsButton;
    [SerializeField] CustomButton showCompetencesButton;

    [SerializeField] StatsPanel statsPanel;
    [SerializeField] CompetencesPanel competencesPanel;

    [SerializeField] CompetenceIcon prefab;

    public StatsPanel StatsPanel => statsPanel;
    public CompetencesPanel CompetencesPanel => competencesPanel;

    void Awake()
    {
        showStatsButton.AddLeftClickAction(() => SetStatsPanelStatus(true));
        showStatsButton.AddLeftClickAction(() => SetCompetencesPanelStatus(false));

        showCompetencesButton.AddLeftClickAction (() => SetStatsPanelStatus(false));
        showCompetencesButton.AddLeftClickAction(() => SetCompetencesPanelStatus(true));
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
