using RPG.Saving;
using System;
using System.Collections;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        [SerializeField] float fadeInTime = 0.3f;

        const string DEFAULT_SAVE_FILE = "save";

        IEnumerator Start()
        {
           Fader fader = FindObjectOfType<Fader>();
           fader.FadeOutImmediate();
           yield return GetComponent<SavingSystem>().LoadLastScene(DEFAULT_SAVE_FILE);
           yield return fader.FadeIn(fadeInTime);
        }


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

        public void Save()
        {
            GetComponent<SavingSystem>().Save(DEFAULT_SAVE_FILE);
        }

        public void Load()
        {
            GetComponent<SavingSystem>().Load(DEFAULT_SAVE_FILE);
        }
    }
}
