using System;
using UnityEngine;

namespace Atypiki.Core.Core.Items
{
    [CreateAssetMenu(fileName = "Item", menuName = "Atypiki/Items/ItemData", order = 0)]
    public class ItemData : ScriptableObject
    {
        [field : SerializeField] public ItemType ItemType { get; private set; }
        [field : SerializeField] public String ItemID { get; private set; }
    }

    public enum ItemType
    {
        quest,
        furniture,
        tool
    }
}