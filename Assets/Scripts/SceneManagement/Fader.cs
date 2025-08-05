using System.Collections;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        [SerializeField] float fadeOutTime = 3f;
        [SerializeField] float fadeInTime = 2f;

        CanvasGroup canvasGroup;

        private void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();

            StartCoroutine(FadeOut(fadeOutTime));
        }

        IEnumerator FadeOut(float timeForFade)
        {
            while (canvasGroup.alpha != 1) // alpha is not 1
            { 
                // moving alpha towards 1
                canvasGroup.alpha = Time.deltaTime / timeForFade;

                yield return null;
            }
        }
    }
}
