using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BlackScreenFade : MonoBehaviour {

    [SerializeField] UnityEngine.UI.Image fadeToBlackPanel;
    [SerializeField] float totalTime = 3f;
    [SerializeField] float blackTime = 1f;

    [Tooltip("a:1->0")]
    [SerializeField] bool isOnStart;

    private void Awake()
    {
        if (isOnStart)
        {
            LerpFTBPanelAlpha(1);
            StartCoroutine(StartSequence(null, true));
        }
    }
    // 0->1
    public IEnumerator StartSequence(UnityAction action = null, bool isInverse = false, float? totalTime = null)
    {
        if (totalTime == null) totalTime = this.totalTime;

        float lerpTime = (float)totalTime - blackTime;
        float startTime = Time.time;
        float timeRatio = 0;

        while (timeRatio < 1)
        {
            timeRatio = (Time.time - startTime) / lerpTime;

            if (timeRatio >= 1)
            {
                timeRatio = 1;
                LerpFTBPanelAlpha(isInverse ? 0 : 1); // Fail-safe...
            }
            LerpFTBPanelAlpha(isInverse ? 1-timeRatio : timeRatio);

            yield return null;
        }
        yield return new WaitForSeconds(blackTime);
        if (action != null) action.Invoke();
    }

    void LerpFTBPanelAlpha(float t)
    {
        float a = Mathf.Sin(0.5f * Mathf.PI * t);
        //Debug.Log(a);
        fadeToBlackPanel.color = new Color(0, 0, 0, a);
    }
}
