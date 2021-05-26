using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Assets/Databases/ItemDatabase")]
public class ItemDatabase : ScriptableObject
{
    public List<Item> allItems;
}
