using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConstructionProgressUI : MonoBehaviour
{
    Building building;
    public Image fill;
    public TMP_Text tmpContent;
    public UIFollowObject uiFollow;

    public void Init(Building building)
    {
        this.building = building;
        uiFollow.Init(building.buildFocus);
    }

    public void SetProgress(float value, string content)
    {
        fill.fillAmount = value;
        tmpContent.text = content;
    }
}
