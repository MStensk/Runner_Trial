using UnityEngine;
using System.Collections;

public class StartMessageUI : MonoBehaviour
{
    [SerializeField] private GameObject messageObject;

    [Header("Timing")]
    [SerializeField] private float delayBeforeShow = 2f;
    [SerializeField] private float fadeDuration = 1.2f;
    [SerializeField] private float visibleTime = 2f;

    private CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup = messageObject.GetComponent<CanvasGroup>();

        // Start hidden
        canvasGroup.alpha = 0f;
        messageObject.SetActive(true);

        StartCoroutine(ShowMessageSequence());
    }

    IEnumerator ShowMessageSequence()
    {
        // 1️⃣ Wait before showing
        yield return new WaitForSeconds(delayBeforeShow);

        // 2️⃣ Fade IN
        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = t / fadeDuration;
            yield return null;
        }

        canvasGroup.alpha = 1f;

        // 3️⃣ Stay visible
        yield return new WaitForSeconds(visibleTime);

        // 4️⃣ Fade OUT
        t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = 1f - (t / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 0f;
        messageObject.SetActive(false);
    }
}