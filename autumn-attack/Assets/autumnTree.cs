using UnityEngine;

public class autumnTree : MonoBehaviour
{
    public logic ls;
    public int value = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ls = GameObject.FindWithTag("logos").GetComponent<logic>();  
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnMouseDown()
    {
        ls.addClicks(value);
    }
}
