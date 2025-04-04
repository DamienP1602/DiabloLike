using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class StatsPanel : MonoBehaviour
{
    [SerializeField] TMP_Text levelText;
    [SerializeField] TMP_Text pointsText;

    [SerializeField] StatInfo strengthData;
    [SerializeField] StatInfo agilityData;
    [SerializeField] StatInfo intelligenceData;

    [SerializeField] StatInfo damageData;
    [SerializeField] StatInfo healthData;
    [SerializeField] StatInfo manaData;
    [SerializeField] StatInfo armorData;
    [SerializeField] StatInfo resistanceData;

    StatsComponent playerStats;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetStats(StatsComponent _stats) => playerStats = _stats;

    public void Init()
    {
        strengthData.InitButton(() =>
        {
            playerStats.strength.AddValue(1);
            playerStats.statPoints.RemoveValue(1);
            UpdateData();
        });

        agilityData.InitButton(() =>
        {
            playerStats.agility.AddValue(1);
            playerStats.statPoints.RemoveValue(1);
            UpdateData();
        });

        intelligenceData.InitButton(() =>
        {
            playerStats.intelligence.AddValue(1);
            playerStats.statPoints.RemoveValue(1);
            UpdateData();
        });
    }

    public void Open()
    {
        gameObject.SetActive(true);
        SetLevel(playerStats.level.Value);
        UpdateData();
    }

    public void UpdateData()
    {
        SetPoints(playerStats.statPoints.Value);

        strengthData.Init(playerStats.strength.Value, playerStats.statPoints.Value);
        agilityData.Init(playerStats.agility.Value, playerStats.statPoints.Value);
        intelligenceData.Init(playerStats.intelligence.Value, playerStats.statPoints.Value);

        damageData.Init(playerStats.damage.Value, 0);
        healthData.Init(playerStats.maxHealth.Value, 0);
        manaData.Init(playerStats.maxMana.Value, 0);
        armorData.Init(playerStats.armor.Value, 0);
        resistanceData.Init(playerStats.resistance.Value,0);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    void SetLevel(int _level)
    {
        levelText.text = "Level : " + _level.ToString();
    }

    void SetPoints(int _points)
    {
        pointsText.text = "Stats Points : " + _points.ToString();
    }

}
