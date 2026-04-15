using UnityEngine;
using System.Collections;
public class Desaparecer : MonoBehaviour
{
    public SpriteRenderer sprite;
    public float duracion = 1f;

    public IEnumerator FadeOut()
    {
        float tiempo = 0f;
        Color c = sprite.color;

        while (tiempo < duracion)
        {
            tiempo += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, tiempo / duracion);

            sprite.color = new Color(c.r, c.g, c.b, alpha);

            yield return null;
        }

        sprite.color = new Color(c.r, c.g, c.b, 0f);
    }
}
