using Atypiki.Core.Core.BusSystem;
using Atypiki.Core.Core.BusSystem.Event;
using Atypiki.Core.Core.Interaction;
using Atypiki.Core.Core.Items.Interface;
using UnityEngine;

namespace Atypiki.Core.Core.Items
{
    public class Collectable : MonoBehaviour, IInteractable, ICollectable
    {
        [field : SerializeField] public ItemData ItemData { get; private set; }
        
        public void Interact()
        {
            Collect();
            Debug.Log("Collect");
            enabled = false;
        }

        public void Collect()
        {
            EventBus.Publish(new ItemEvent.CollectItemEvent(ItemData.ItemID));
        }
    }
}