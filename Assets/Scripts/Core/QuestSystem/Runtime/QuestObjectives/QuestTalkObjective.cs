using Atypiki.Core.Core.BusSystem;
using Atypiki.Core.Core.BusSystem.Event;
using Atypiki.Core.Core.NPC;
using Atypiki.Core.Core.QuestSystem.Data;
using UnityEngine;

namespace Atypiki.Core.Core.QuestSystem.Runtime.QuestObjectives
{
    public class QuestTalkObjective : QuestObjective
    {
        private CharacterData NpcId;
        
        public QuestTalkObjective(QuestTalkObjectiveData data) :  base(data)
        {
            NpcId = data.NpcId;
        }

        public override void Initialize()
        {
            EventBus.Subscribe<EndDialogEvent>(OnNpcTalk);
        }

        /*
         * Function that is called when a dialog is ended
         * If the npc, the player talked to, is the targeted npc, then the quest objective is complete
         */
        public void OnNpcTalk(EndDialogEvent e)
        {
            if(NpcId != e.characterData)
                return;
            
            IsObjectiveCompleted();
            EventBus.Unsubscribe<EndDialogEvent>(OnNpcTalk);
        }
    }
}