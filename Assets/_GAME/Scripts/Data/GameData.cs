using System;
using System.Numerics;
using UnityEngine;
using UnityEngine.U2D;

public enum ModifierType
{
    Flat,
    Percentage,
    Override,
}

public struct Modifier
{
    public ModifierType type;
    public float value;
}

[Serializable]
public struct BuildingInfo
{
    public string id;
    public float buildTime;
    public int buildPrice;
    public int baseUpgradePrice;
    public float productionTime;
    public string productId;
    public int productCount;
}

[Serializable]
public struct ProductInfo
{
    public string id;
    public GameObject productGFX;
    public float productSellPrice;
}

[CreateAssetMenu(fileName = "GameData", menuName = "GameData", order = 0)]
public class GameData : ScriptableObject
{
    public BuildingInfo[] buildingData;
    public ProductInfo[] productData;

    public SpriteAtlas ProductAtlas;
    public SpriteAtlas CurrencyAtlas;
}
