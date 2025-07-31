using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {

        // Cached components

        PlayableDirector playableDir;

        // private variables

        bool hasCinematicPlayed = false;

        private void Awake()
        {
            playableDir = GetComponent<PlayableDirector>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!hasCinematicPlayed && other.CompareTag("Player"))
            {
                playableDir.Play();
                hasCinematicPlayed = true;
            }
            
        }
    }
}
