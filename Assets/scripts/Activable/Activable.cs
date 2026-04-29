using UnityEngine;

public class Activable : MonoBehaviour
{
    public bool on = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform puntoA;
    public Transform puntoB;

    public float velocidad = 2f;

    void Update()
    {

        if (on){
            transform.position = Vector3.MoveTowards(
                transform.position,
                puntoB.position,
                velocidad * Time.deltaTime
            );
        }
        
        else{
            transform.position = Vector3.MoveTowards(
                transform.position,
                puntoA.position,
                velocidad * Time.deltaTime
            );
        }
    }
    
    public void Activate()
    {
        on = !on;
    }
}

