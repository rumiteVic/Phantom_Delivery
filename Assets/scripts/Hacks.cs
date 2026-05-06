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
            ActivateHacks();
        }
    }
    void ActivateHacks()
    {
        int totalLevels = 8; 

        // Desbloquea el acceso a los niveles
        PlayerPrefs.SetInt("LevelReached", totalLevels);

        for (int i = 0; i <= totalLevels; i++)
        {
            //Desbloquea tutoriales
            PlayerPrefs.SetInt("Tutorial_" + i, 1);
        }

        PlayerPrefs.Save();
    }
}
