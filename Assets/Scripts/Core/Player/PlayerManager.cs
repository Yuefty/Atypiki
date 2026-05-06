using Atypiki.Core.Player;
using UnityEngine;

namespace Atypiki.Core 
{
    public class PlayerManager : MonoBehaviour
    {
        [field: SerializeField]
        public PlayerMovement Movement { get; private set; }
        [field: SerializeField]
        public PlayerBody Body { get; private set; }
        [field: SerializeField]
        public PlayerAnimator PlayerAnimator { get; private set; }

        private void Start()
        {
            Body.SetCanMove(true);
        }
    }
}