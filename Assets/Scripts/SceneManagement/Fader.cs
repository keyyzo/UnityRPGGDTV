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

            //StartCoroutine(FadeOutIn());
        }

        IEnumerator FadeOutIn()
        {
            yield return FadeOut(fadeOutTime);
            Debug.Log("Faded out");
            yield return FadeIn(fadeInTime);
            Debug.Log("Faded in");
        }

        public IEnumerator FadeOut(float timeForFade)
        {
            while (canvasGroup.alpha < 1) // alpha is not 1
            { 
                // moving alpha towards 1
                canvasGroup.alpha += Time.deltaTime / timeForFade;

                yield return null;
            }
        }

        public IEnumerator FadeIn(float timeForFade)
        {
            while (canvasGroup.alpha > 0) // alpha is not 0
            {
                // moving alpha towards 0
                canvasGroup.alpha -= Time.deltaTime / timeForFade;

                yield return null;
            }
        }
    }
}
