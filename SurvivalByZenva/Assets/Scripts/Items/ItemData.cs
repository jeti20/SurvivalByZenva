using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    Resource, //kamien, drzewo
    Equipable, //bronie 
    Consumable //jedzenie, picie
}

public enum consumableType
{
    Hunger,
    Thirst,
    Health,
    Sleep
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;
    public ItemType type;
    public Sprite icon;
    public GameObject dropPrefab;

    [Header("Stacking")]
    public bool canStack; //czy rzecz moze byc stackowana
    public int maxStackAmount;

    [Header("consumable")]
    public ItemDataConsumable[] consumables;

}

[System.Serializable]
public class ItemDataConsumable
{
    public consumableType type; //hunger, thirst...
    public float value;
}
