using UnityEngine;
using System.Collections;
public class FinalNivel : MonoBehaviour
{
    public Desaparecer desaparecer;
    public ParticleSystem particulas;
    public Fade fade;
    public GameObject pantallaNegra;
    public static FinalNivel Instance;

    void Awake(){
        Instance = this;
    }

    IEnumerator Start()
    {
        yield return null; // esperar 1 frame (evita bugs raros de UI)
        EjecutarInicio();
    }
    public void EjecutarFinal(int stars)
{
    StartCoroutine(SecuenciaFinal(() =>
    {
        GameManager.Instance.MostrarResultadosFinal(stars);
        }));
    }

    IEnumerator SecuenciaFinal(System.Action onComplete = null)
    {
        // partículas
        if (particulas != null)
            particulas.Play();

        pantallaNegra.SetActive(true);

        // desaparecer personaje
        if (desaparecer != null)
            yield return StartCoroutine(desaparecer.FadeOut());

        // pausa
        yield return new WaitForSeconds(0.5f);

        // fade a negro
        fade.FadeOut(() =>
        {
            Debug.Log("FADE TERMINADO");
            onComplete?.Invoke();
        });
    }
    public void EjecutarMuerte()
    {
        StartCoroutine(SecuenciaFinal(() =>
        {
            GameManager.Instance.ReiniciarNivel();
        }));
    }
        public void EjecutarInicio()
    {
        pantallaNegra.SetActive(true);

        fade.FadeIn(() =>
        {
            pantallaNegra.SetActive(false);
        });
    }
}
