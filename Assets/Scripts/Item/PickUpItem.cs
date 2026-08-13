using UnityEngine;


[System.Serializable]
public class PickUpItem
{
    public GameObject itemPrefab;
    [Range(0, 100)] public float dropChance;
}
