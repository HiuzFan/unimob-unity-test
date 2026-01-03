using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class IngameUI : MonoBehaviour
{
    public BuildingConstructionUI buildingView;
    public BuildingUpgradeUI upgradeView;
    public ConstructionProgressUI constructionProgressPrefab;
    List<ConstructionProgressUI> listProgressUI = new List<ConstructionProgressUI>();
    public TMP_Text tmpGold;


    public ConstructionProgressUI GetProgressUI(Building building)
    {
        var progress = constructionProgressPrefab.Use(transform, listProgressUI);
        progress.Init(building);
        return progress;
    }

    public void UpdateGold(long amount)
    {
        tmpGold.text = Utils.FormatNumber(amount);
    }
}
