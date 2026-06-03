using UnityEngine;
public class Hacks : MonoBehaviour
{

    private InputSystem_Actions input;
    void Start()
    {
        input = new InputSystem_Actions();
        input.Player.Enable();
    }

    void Update()
    {
        if (input.Player.Hacks.WasPressedThisFrame())
        {
            SetAllStars(3);
        }
        if (input.Player.Hacks2.WasPressedThisFrame())
        {
            SetAllStars(1);
        }
    }
    void SetAllStars(int stars)
    {
        int totalLevels = 9;

        PlayerPrefs.SetInt("LevelReached", totalLevels);

        for (int i = 0; i <= totalLevels; i++)
        {
            PlayerPrefs.SetInt("Tutorial_" + i, 1);
        }

        for (int levelNum = 2; levelNum <= totalLevels; levelNum++)
        {
            PlayerPrefs.SetInt("Stars_Level_" + levelNum, stars);
        }

        PlayerPrefs.Save();

        Debug.Log($"Hacks activados: todos los niveles con {stars} estrellas");
    }
}
