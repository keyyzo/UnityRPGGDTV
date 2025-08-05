using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        enum DestinationIdentifier
        { 
            A, B, C, D, E
        }

        [SerializeField] int sceneToLoad;
        [SerializeField] Transform spawnPoint;
        [SerializeField] DestinationIdentifier destination;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Portal has been triggered!");
                StartCoroutine(TransitionRoutine());
                
            }

            
            
        }

        IEnumerator TransitionRoutine()
        {
            if (sceneToLoad < 0)
            {
                Debug.LogError("Scene to load is not set!");
                yield break;
            }

            DontDestroyOnLoad(gameObject);
            yield return SceneManager.LoadSceneAsync(sceneToLoad);

            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);

            Destroy(gameObject);
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            playerObj.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
            //playerObj.GetComponent<NavMeshAgent>().enabled = false;
           // playerObj.transform.position = otherPortal.spawnPoint.position;
            playerObj.transform.rotation = otherPortal.spawnPoint.rotation;
            //playerObj.GetComponent<NavMeshAgent>().enabled = true;

        }

        private Portal GetOtherPortal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this)
                    continue;
                if (portal.destination != destination)
                    continue;

                return portal;
            }

            return null;
        }
    }
}
