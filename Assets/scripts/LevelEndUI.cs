using UnityEngine;
using TMPro;
using System.Collections;

public class LevelEndUI : MonoBehaviour
{
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;
    public TextMeshProUGUI messageText;
    public AudioSource aud;

    public float delayBetweenStars = 0.5f;
    public float typingSpeed = 0.02f;

    public void ShowResult(int stars)
    {
        StartCoroutine(AnimateStarsSequence(stars));
    }

    private IEnumerator AnimateStarsSequence(int stars)
    {
        // Inicio
        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);
        messageText.text = "";

        // Se ponen estrellas
        if (stars >= 1) {
            yield return new WaitForSeconds(delayBetweenStars);
            star1.SetActive(true);
            aud.Play();
        }
        if (stars >= 2) {
            yield return new WaitForSeconds(delayBetweenStars);
            star2.SetActive(true);
            aud.Play();
        }
        if (stars >= 3) {
            yield return new WaitForSeconds(delayBetweenStars);
            star3.SetActive(true);
            aud.Play();
        }

        yield return new WaitForSeconds(0.3f);

        // 3. Lanzamos el efecto de escritura
        string fullMessage = GetMessageString(stars);
        yield return StartCoroutine(TypeText(fullMessage));
    }

    private IEnumerator TypeText(string textToType)
    {
        messageText.text = "";
        foreach (char letter in textToType.ToCharArray())
        {
            messageText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    private string GetMessageString(int stars)
    {
        switch (stars)
        {
            case 1: return "Delivery done... improve your performance.";
            case 2: return "Nice job. The company it's satisfied";
            case 3: return "Perfect delivery. Nobody did it better.";
            default: return "";
        }
    }
}