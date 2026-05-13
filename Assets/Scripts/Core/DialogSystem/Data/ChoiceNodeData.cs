using System.Collections.Generic;
using UnityEngine;

namespace Atypiki.Core.Core.DialogSystem
{
    public class AnswerNodeData : NodeData
    {
        
        private int _amountOfAnswers = 1;

        public List<string> Answers = new();
        public List<string> AnswerKeys = new();
        
        public List<NodeData> ChildNodes = new();

        private const float LabelFieldSpace = 18f;
        private const float TextFieldWidth = 120f;

        private const float AnswerNodeWidth = 190f;
        private const float AnswerNodeHeight = 115f;

        private float _currentAnswerNodeHeight = 115f;
        private const float AdditionalAnswerNodeHeight = 20f;

        public int nextNodeIndex;
        
        public override NodeData GetNextNode()
        {
            return ChildNodes[nextNodeIndex];
        }

        public override Awaitable ProcessNode(DialogBehaviour dialogBehaviour)
        {
            throw new System.NotImplementedException();
        }
        

        [SerializeField, HideInInspector]
        private bool hasInitialized = false;
        

        public string GetAnswerText(int index)
        {
            if (index < 0 || index >= Answers.Count)
                return string.Empty;
            
            return Answers[index];
        }
    }
}