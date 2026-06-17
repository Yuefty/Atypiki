using Atypiki.Core.Core.BusSystem;
using Atypiki.Core.Core.BusSystem.Event;
using Atypiki.Core.Core.DialogSystem.Data;
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
            Debug.Log("we are initializing the quest talk objective");
            EventBus.Subscribe<EndDialogEvent>(OnNpcTalk);
        }

        /*
         * Function that is called when a dialog is ended
         * If the npc, the player talked to, is the targeted npc, then the quest objective is complete
         */
        public void OnNpcTalk(EndDialogEvent e)
        {
            Debug.Log("we have finish talking");
            
            if(NpcId != e.characterData)
                return;
            
            Debug.Log("we have talk to the right npc");
            
            IsObjectiveCompleted();
            EventBus.Unsubscribe<EndDialogEvent>(OnNpcTalk);
        }
    }
}