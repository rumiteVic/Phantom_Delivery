using UnityEngine;

public class Activable : MonoBehaviour
{
    public bool activa = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform puntoA;
    public Transform puntoB;
    public bool isPlat = true;
    public float velocidad = 2f;

    void Update()
    {
        if(isPlat){
            if (activa){
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
        
    }

    public void Activate(){
        activa = !activa;
    }
}
