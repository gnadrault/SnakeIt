using System.Collections;
using UnityEngine;

namespace Effect
{
    public class FadeMaterial : MonoBehaviour
    {
        public static FadeMaterial Instance;
        
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }
        
        public void CallFadeMaterial(Material material, float duration)
        {
            StartCoroutine(Fade(material, duration));
        }

        /// <summary>
        /// Coroutine reduce the alpha of the material during duration time
        /// </summary>
        IEnumerator Fade(Material material, float duration)
        {
            float time = 0f;
            float targetAlpha = 0.001f;
            Color startColor = material.color;

            while (time < duration)
            {
                float alpha = Mathf.Lerp(startColor.a, targetAlpha, time / duration);

                Color c = material.color;
                c.a = alpha;
                material.color = c;

                time += Time.unscaledDeltaTime;
                yield return null;
            }

            Color final = material.color;
            final.a = targetAlpha;
            material.color = final;
        }
    }
}
