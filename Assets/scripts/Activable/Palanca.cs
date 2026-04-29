using UnityEngine;

public class Palanca : MonoBehaviour
{
    public bool on = true;
    public Activable[] act;

    public GameObject pal_on;
    public GameObject pal_off;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.CompareTag("Pakete"))
        {
            on = !on;
            HazCosa();
        }
            
    }

    public void HazCosa()
    {
        if(on){
            foreach (Activable item in act)
            {
                item.Activate();
            }
            pal_on.SetActive(true);
            pal_off.SetActive(false);
        }
        else
        {
            foreach (Activable item in act)
            {
                item.Activate();
            }
            pal_on.SetActive(false);
            pal_off.SetActive(true);
        }
    }
    
}
