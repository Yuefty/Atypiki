using System.Collections.Generic;
using Atypiki.Core.Core.DialogSystem.Helpers;
using UnityEngine;

namespace Atypiki.Core.Core.DialogSystem.Data.Nodes
{
    [CreateAssetMenu(fileName = "Choice", menuName = "Atypiki/Dialog/Node/ChoiceData", order = 0)]
    public class ChoiceNodeData : NodeData
    {
        
        private int amountOfAnswers = 1;

        public List<ChoiceStruct> Answers = new();
        public int choiceIndex;
        
        public override NodeData GetNextNode()
        {
            return Answers[choiceIndex].ChildNode;
        }

        public override void ProcessNode(DialogBehaviour dialogBehaviour)
        {
            dialogBehaviour.InitChoice(Answers.Count);
            for (int i = 0; i < Answers.Count; i++)
            {
                int index = i;
                dialogBehaviour.AddChoice(index, GetAnswerText(index),()=> ChoiceSelection(index));
            }
        }

        [SerializeField, HideInInspector]
        private bool hasInitialized = false;
        

        public string GetAnswerText(int index)
        {
            if (index < 0 || index >= Answers.Count)
                return string.Empty;
            
            return Answers[index].choiceText;
        }
        
        public void ChoiceSelection(int index)
        {
            choiceIndex =  index;
        }
    }
}