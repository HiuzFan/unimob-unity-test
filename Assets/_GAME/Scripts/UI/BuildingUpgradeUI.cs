using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingUpgradeUI : MonoBehaviour
{
    Building building;
    public Button btnClose;
    public UIFollowObject frame;
    public Button btnUpgrade;
    public TMP_Text tmpLevel;
    public TMP_Text tmpProduct;
    public TMP_Text tmpProductRate;
    public Slider levelProgress;

    public TMP_Text tmpPrice;
    public GameObject MaxPrice;
    public GameObject UpgradePrice;

    private void Start()
    {
        btnClose.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });

        btnUpgrade.onClick.AddListener(() =>
        {
            //TODO: upgrade
            if (building.currentLevel <= GameConstant.MaxLevelIndex)
            {
                ++building.currentLevel;
                UpdateStatus();
            }
        });
    }

    public void Init(Building building)
    {
        gameObject.SetActive(true);
        this.building = building;
        frame.Init(building.buildFocus);
        tmpProduct.text = building.currentInfo.productId;

        UpdateStatus();
    }

    void UpdateStatus()
    {
        bool upgradable = building.currentLevel < GameConstant.MaxLevelIndex;

        MaxPrice.SetActive(!upgradable);
        UpgradePrice.SetActive(upgradable);
        levelProgress.normalizedValue = (float)building.currentLevel / GameConstant.MaxLevelIndex;
        var product = GameManager.Instance.gameData.productData.First(x => x.id == building.currentInfo.productId);
        tmpProductRate.text = Utils.FormatNumber((int)((product.productSellPrice * 60f / building.currentInfo.productionTime) * (1 + GameConstant.productionRatePerLevel * building.currentLevel)));
        tmpLevel.text = $"Level {(building.currentLevel + 1)}";
        if (upgradable)
        {
            tmpPrice.text = Utils.FormatNumber((int)(building.currentInfo.baseUpgradePrice * Mathf.Pow(GameConstant.priceMultiplierPerLevel, building.currentLevel)));
        }
        btnUpgrade.interactable = upgradable;
    }
}
