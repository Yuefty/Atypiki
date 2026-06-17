using UnityEngine;

namespace Atypiki.Core.Core.Player.Component
{
    /*
     * This class allow access to PlayerManager
     */
    public class PlayerComponent : MonoBehaviour
    {
        
        [field: SerializeField]
        public PlayerManager Manager { get; private set; }

        private void OnValidate()
        {
            if (Manager == null)
                Manager = FindObjectOfType<PlayerManager>();
        }
    }
}
