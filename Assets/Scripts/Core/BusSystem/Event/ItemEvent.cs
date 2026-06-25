using System;
using Atypiki.Core.Core.QuestSystem.Runtime;

namespace Atypiki.Core.Core.BusSystem.Event
{
    public class ItemEvent
    {
        public struct CollectItemEvent
        {
            public String itemID;
        
            public CollectItemEvent(String id)
            {
                itemID = id;
            }
        }
    }
}