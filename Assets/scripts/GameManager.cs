using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance;

    public int packagesDelivered;
    public int totalPackages;

    public int bodiesBuried;
    public int totalBodies;

    float startTime;
    float endTime;

    public GameObject endPanel;
    public LevelEndUI levelEndUI;

    public float timeForThree = 60f;
    public float timeForTwo = 120f;

    public bool win = false;

    public TMP_Text packagesText;
    public TMP_Text bodiesText;

    public FinalNivel finalNivel;
    public UIFade fadeUI;

    public AudioSource aud;
    public AudioClip winning;

    void Awake(){
        Instance = this;
        startTime = Time.time;
    }

    void Start(){
        UpdatePackagesText();
        UpdateBodiesText();
        GestionarTutorial();
    }
    
    // Update is called once per frame
    void Update()
    {
        CheckWinCondition();
    }

    void CheckWinCondition()
    {
        if (packagesDelivered >= totalPackages && bodiesBuried >= totalBodies && !win)
        {
            endTime = Time.time;
            float totalTime = endTime - startTime;

            int stars = CalculateStars(totalTime);
            finalNivel.EjecutarFinal(stars);
            SaveProgress(stars);
            win = true;
        }
    }
    int CalculateStars(float time)
    {
        if (time < timeForThree)
            return 3;
        else if (time < timeForTwo)
            return 2;
        else
            return 1;
    }

    public void MostrarResultadosFinal(int stars){
        endPanel.SetActive(true);
        UIFade fadeUI = endPanel.GetComponent<UIFade>();
        fadeUI.canvasGroup.alpha = 0f;

        levelEndUI.ShowResult(stars);
        fadeUI.FadeIn();
        aud.clip = winning;
        aud.Play();
        Time.timeScale = 0f;
    }

    public void UpdatePakage(){
        packagesDelivered++;
        UpdatePackagesText();
    }
    public void UpdateBody(){
        bodiesBuried++;
        UpdateBodiesText();
    }

    void UpdatePackagesText(){
        packagesText.text = $"{packagesDelivered}/{totalPackages}";
    }

    void UpdateBodiesText(){
        bodiesText.text = $"{bodiesBuried}/{totalBodies}";
    }

    public void ReiniciarNivel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SaveProgress(int stars)
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        string starLevel = "Stars_Level_" + currentLevelIndex;
        int previousStars = PlayerPrefs.GetInt(starLevel, 0);
        if (currentLevelIndex == 2) 
        {
            MarcarTutorialComoHecho();
        }
        if(stars > previousStars)
        {
            PlayerPrefs.SetInt(starLevel, stars);
        }

        int levelReached = PlayerPrefs.GetInt("LevelReached", 1);
        if(levelReached <= currentLevelIndex)
        {
            PlayerPrefs.SetInt("LevelReached", currentLevelIndex + 1);
        }

        PlayerPrefs.Save();
    }
    void GestionarTutorial()
    {
        int escenaActual = SceneManager.GetActiveScene().buildIndex;

        // Solo si es la escena 2 (tu Nivel 1) Y no se ha completado antes
        if (escenaActual == 2 && PlayerPrefs.GetInt("TutorialCompletado", 0) == 0)
        {
            OneButton.Instance.Tutorial();
        }
    }

    public void MarcarTutorialComoHecho()
    {
        PlayerPrefs.SetInt("TutorialCompletado", 1);
        PlayerPrefs.Save();
    }
}
