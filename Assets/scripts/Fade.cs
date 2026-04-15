using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Fade : MonoBehaviour
{
    public Image imagenNegra;
    public float duracion = 1f;

    public void FadeOut(System.Action onComplete = null)
    {
        StartCoroutine(FadeCoroutine(1f, onComplete));
    }

    public void FadeIn()
    {
        StartCoroutine(FadeCoroutine(0f, null));
    }

    IEnumerator FadeCoroutine(float targetAlpha, System.Action onComplete)
    {
        float startAlpha = imagenNegra.color.a;
        float tiempo = 0f;

        Color c = imagenNegra.color;

        while (tiempo < duracion)
        {
            tiempo += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, tiempo / duracion);

            imagenNegra.color = new Color(c.r, c.g, c.b, alpha);

            yield return null;
        }

        imagenNegra.color = new Color(c.r, c.g, c.b, targetAlpha);

        onComplete?.Invoke();
    }
}