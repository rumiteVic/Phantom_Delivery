using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class BotonSelector : MonoBehaviour
{
    public TextMeshProUGUI levelText;
    public GameObject[] stars;
    public GameObject lockIcon;
    public Button button;
    private int myLevelIndex;
    public void BotonSeleccionador (int levelIndex, int starsCaptured, bool isUnlocked)
    {
        myLevelIndex = levelIndex;
        levelText.text = "Level: "+ levelIndex.ToString();
        button.interactable = isUnlocked;
        lockIcon.SetActive(!isUnlocked);
        for(int i = 0; i < stars.Length; i++)
        {
            stars[i].SetActive(isUnlocked && i < starsCaptured);
        }
    }

    public void OpenLevel()
    {
        SceneManager.LoadScene(myLevelIndex);
    }
}
