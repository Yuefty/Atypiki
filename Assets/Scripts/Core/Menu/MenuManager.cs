using PaulosMenuController;
using UnityEngine;

namespace Atypiki.Core.Core.Menu
{
    public class MenuManager : PlayerComponent
    {
        [field: SerializeField] public GameMenuController Menu { get; set; }
        [field: SerializeField] public GameObject Diary { get; set; }
        
        public void ShowMenu()
        {
            Cursor.visible = true;
            Manager.LockPlayerMovement();
        }

        public void HideMenu()
        {
            Cursor.visible = false;
            Manager.UnLockPlayerMovement();
        }
        
        public void ToggleMenu()
        {
            if (Menu.IsMenuOpen())
            {
                Menu.ButtonCloseMenu();
                HideMenu();
            }
            else
            {
                Menu.ButtonOpenMenu();
                ShowMenu();
            }
        }
        
        public void ToggleDiary()
        {
            if (Diary.activeSelf)
            {
                Diary.SetActive(false);
                HideMenu();
            }
            else
            {
                Diary.SetActive(true);
                ShowMenu();
            }
        }
    }
}