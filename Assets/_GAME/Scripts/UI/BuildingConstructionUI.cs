using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingConstructionUI : MonoBehaviour
{
    Building building;
    public Button btnClose;
    public UIFollowObject frame;
    public Button btnBuild;
    public TMP_Text tmpName;
    public TMP_Text tmpPrice;
    public Image icon;

    private void Start()
    {
        btnClose.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });

        btnBuild.onClick.AddListener(() =>
        {
            //TODO: check and removeCoin
            building.SwitchState(BuildingState.Constructing);
            gameObject.SetActive(false);
        });
    }
    public void Init(Building building)
    {
        gameObject.SetActive(true);
        this.building = building;
        frame.Init(building.buildFocus);
        tmpName.text = building.currentInfo.id;
        tmpPrice.text = Utils.FormatNumber(building.currentInfo.buildPrice);
        icon.sprite = GameManager.Instance.gameData.ProductAtlas.GetSprite(building.currentInfo.productId);
    }
}
