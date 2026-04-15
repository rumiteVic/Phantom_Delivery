using UnityEngine;
using System.Collections;
public class FinalNivel : MonoBehaviour
{
    public Desaparecer desaparecer;
    public ParticleSystem particulas;
    public Fade fade;
    public GameObject pantallaNegra;
    public void EjecutarFinal(int stars)
    {
        StartCoroutine(SecuenciaFinal(stars));
    }

    IEnumerator SecuenciaFinal(int stars)
    {
        // partículas
        particulas.Play();
        pantallaNegra.SetActive(true);
        // desaparecer personaje
        yield return StartCoroutine(desaparecer.FadeOut());

        // pequeña pausa
        yield return new WaitForSeconds(0.5f);

        // fade a negro
        fade.FadeOut(() =>
        {
            Debug.Log("FADE TERMINADO");
            GameManager.Instance.MostrarResultadosFinal(stars);
        });
    }
}
