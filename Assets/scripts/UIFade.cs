using UnityEngine;
using System.Collections;
public class UIFade : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float duracion = 1f;

    public void FadeIn()
    {
        StartCoroutine(FadeCoroutine(1f));
    }

    IEnumerator FadeCoroutine(float target)
    {
        float start = canvasGroup.alpha;
        float tiempo = 0f;

        while (tiempo < duracion)
        {
            tiempo += Time.unscaledDeltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, target, tiempo / duracion);
            yield return null;
        }

        canvasGroup.alpha = target;
    }
}
