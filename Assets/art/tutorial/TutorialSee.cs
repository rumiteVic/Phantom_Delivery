using UnityEngine;
using UnityEngine.SceneManagement;
public class TutorialSee : MonoBehaviour
{
    public bool visible = false;
    private float alphaActual = 0f;
    public float speedFade = 5f;

    public SpriteRenderer[] sprites;

    public bool disableThem;

    void Start()
    {
        int escenaActual = SceneManager.GetActiveScene().buildIndex;
        string key = "Tutorial_" + escenaActual;

        if (PlayerPrefs.GetInt(key, 0) == 1)
        {
            gameObject.SetActive(false);
        }
        SetAlpha(0f);
    }

    public void Show()
    {
        visible = true;
    }

    public void Hide()
    {
        visible = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !disableThem)
        {
            Show();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Hide();
            disableThem = true;
        }
    }

    void Update()
    {
        float objetivo = visible ? 1f : 0f;
        alphaActual = Mathf.Lerp(alphaActual, objetivo, Time.deltaTime * speedFade);

        SetAlpha(alphaActual);
    }

    void SetAlpha(float a)
    {
        foreach (SpriteRenderer sr in sprites)
        {
            Color c = sr.color;
            c.a = a;
            sr.color = c;
        }
    }
}