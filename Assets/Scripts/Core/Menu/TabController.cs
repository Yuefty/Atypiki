using System;
using System.Collections.Generic;
using UnityEngine;

namespace Atypiki.Core.Core.Menu
{
    public class TabController : MonoBehaviour
    {
        [field : SerializeField] public List<GameObject> Pages { get; private set; }
        private GameObject currentOpenTab;

        private void Start()
        {
            foreach (var page in Pages)
            {
                if (page.activeSelf)
                    currentOpenTab = page;
            }
        }

        /*
         * Function that open the selected tab
         */
        public void OpenTab(int tabIndex)
        {
            if(tabIndex >= Pages.Count)
                return;
            
            GameObject tabToOpen = Pages[tabIndex];
            if(tabToOpen == currentOpenTab)
                return;
            
            currentOpenTab.SetActive(false); //close last opened tab
            tabToOpen.SetActive(true);
            currentOpenTab = tabToOpen;
        }
    }
}