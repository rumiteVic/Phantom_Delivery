using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance;

    public int packagesDelivered;
    public int totalPackages;

    public int bodiesBuried;
    public int totalBodies;

    float startTime;
    float endTime;

    public LevelEndUI end;

    void Awake(){
        Instance = this;
        startTime = Time.time;
    }
    
    // Update is called once per frame
    void Update()
    {
        CheckWinCondition();
    }

    void CheckWinCondition()
    {
        if (packagesDelivered >= totalPackages && bodiesBuried >= totalBodies)
        {
            endTime = Time.time;
            float totalTime = endTime - startTime;

            CalculateStars(totalTime);
        }
    }
    void CalculateStars(float time)
    {
        if (time < 60f)
            Debug.Log("3 estrellas");
        else if (time < 120f)
            Debug.Log("2 estrellas");
        else
            Debug.Log("1 estrella");
    }
}
