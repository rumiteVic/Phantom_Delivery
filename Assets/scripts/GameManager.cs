using UnityEngine;
using TMPro;
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

    bool win = false;

    public TMP_Text packagesText;
    public TMP_Text bodiesText;
    void Awake(){
        Instance = this;
        startTime = Time.time;
    }

    void Start(){
        UpdatePackagesText();
        UpdateBodiesText();
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
            ShowEndScreen(stars);
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

    void ShowEndScreen(int stars){
        endPanel.SetActive(true);
        levelEndUI.ShowResult(stars);
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
}
