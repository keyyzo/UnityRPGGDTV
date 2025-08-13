using RPG.Saving;
using System;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {

        const string DEFAULT_SAVE_FILE = "save";
        
        
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }
        }

        private void Save()
        {
            GetComponent<SavingSystem>().Save(DEFAULT_SAVE_FILE);
        }

        private void Load()
        {
            GetComponent<SavingSystem>().Load(DEFAULT_SAVE_FILE);
        }
    }
}
