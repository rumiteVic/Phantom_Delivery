using UnityEngine;

public class LevelsManager : MonoBehaviour
{
    public BotonSelector[] allButtons;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int nivelLlegado = PlayerPrefs.GetInt("LevelReached", 1);
        for (int i = 0; i < allButtons.Length; i++)
        {
            int levelNum = i + 1;
            int stars = PlayerPrefs.GetInt("Stars_Level_" + levelNum, 0);
            bool unlocked = levelNum <= nivelLlegado;

            allButtons[i].BotonSeleccionador(levelNum, stars, unlocked);
        }
    }
}
