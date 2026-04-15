using UnityEngine;

public class Entierro : MonoBehaviour
{
    private Collider2D collider;

    public GameObject plant1;
    public GameObject plant2;
    public GameObject plant3;

    public GameObject part;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    public void Entierra() {

            plant1.SetActive(true);
            plant2.SetActive(true);
            plant3.SetActive(true);
            collider.enabled = false;
            Instantiate(part, transform.position, transform.rotation);
    }
}
