using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Item")]
public class Item : ScriptableObject
{
    [Header("Only Gameplay")]
    public string id;
    public ItemType type;
    public ActionType actionType;

    [Header("Only Ui")]
    public bool stackable = true;
    public int amount;

    [Header("Both")]
    public Sprite image;

    public enum ItemType
    {
        Card,
    }

    public enum ActionType
    {
        OpenDoor
    }
}
