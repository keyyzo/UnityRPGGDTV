using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] int sceneToLoad;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Portal has been triggered!");
                SceneManager.LoadScene(sceneToLoad);
            }

            
        }
    }
}
