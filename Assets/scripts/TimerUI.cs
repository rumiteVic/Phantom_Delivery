using UnityEngine;
using TMPro;
public class TimerUI : MonoBehaviour
{
    public float tiempo = 0f; // empieza en 0
    public TMP_Text textoTimer;

    public Color colorNormal = Color.white;
    public Color colorWarning = Color.yellow;
    public Color colorDanger = Color.red;

    public GameManager man;
    void Start(){
        man = GameManager.Instance;
    }

    void Update()
    {
        if(!man.win){
            tiempo += Time.deltaTime;

            // Mostrar tiempo (sin decimales)
            textoTimer.text = Mathf.Floor(tiempo).ToString();

            // Cambiar color según marcas de tiempo
            if (tiempo >= man.timeForThree){
                textoTimer.color = colorWarning;
                
            }
            else if (tiempo >= man.timeForTwo){
                textoTimer.color = colorDanger;
            }
            else{
                textoTimer.color = colorNormal;
            }
        }
    }
}
