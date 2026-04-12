using System.Collections;
using UI;
using UnityEngine;

namespace Effect
{
    public class SlowTime : MonoBehaviour
    {
        public static SlowTime Instance;
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }

        public void CallSlowTime()
        {
            StartCoroutine(Slow());
        }
    
        /// <summary>
        /// Slowdown the time
        /// </summary>
        /// <returns></returns>
        IEnumerator Slow()
        {
            TimeManager.Instance.SetTimeScale(0.5f);
            yield return new WaitForSecondsRealtime(3);
            TimeManager.Instance.SetTimeScale(1);
        }
    }
}
