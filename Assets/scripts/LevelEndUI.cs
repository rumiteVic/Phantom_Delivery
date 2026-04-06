using UnityEngine;
using TMPro;

public class LevelEndUI : MonoBehaviour
{
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

    public TextMeshProUGUI messageText;

    public void ShowResult(int stars)
    {
        star1.SetActive(stars >= 1);
        star2.SetActive(stars >= 2);
        star3.SetActive(stars >= 3);

        switch (stars)
        {
            case 1:
                messageText.text = "Entrega completada... pero tardaste demasiado.";
                break;

            case 2:
                messageText.text = "Buen trabajo. La empresa está satisfecha.";
                break;

            case 3:
                messageText.text = "Entrega perfecta. Nadie lo hizo mejor.";
                break;
        }
    }
}