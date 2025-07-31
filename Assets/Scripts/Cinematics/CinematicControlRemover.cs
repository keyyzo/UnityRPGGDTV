using UnityEngine;
using UnityEngine.Playables;
using RPG.Core;
using RPG.Control;

namespace RPG.Cinematics
{
    public class CinematicControlRemover : MonoBehaviour
    {


        // Cached components

        PlayableDirector playableDir;

        GameObject playerObj;

        private void Awake()
        {
            playableDir = GetComponent<PlayableDirector>();
        }

        private void Start()
        {
            playerObj = GameObject.FindWithTag("Player");

            playableDir.played += DisableControl;
            playableDir.stopped += EnableControl;
        }

        void DisableControl(PlayableDirector pd)
        {
            
            playerObj.GetComponent<ActionScheduler>().CancelCurrentAction();
            playerObj.GetComponent<PlayerController>().enabled = false;
        }

        void EnableControl(PlayableDirector pd)
        {
            playerObj.GetComponent<PlayerController>().enabled = true;
        }
    }
}
