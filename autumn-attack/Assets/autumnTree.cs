using UnityEngine;

public class autumnTree : MonoBehaviour
{
    public logic ls;
    public int value = 1;
    public GameObject[] leafSpawns;
    public GameObject leaf;
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
    public void spawnLeaf()
    {
        Instantiate(leaf, leafSpawns[Random.Range(0, 7)].transform.position, Quaternion.Euler(0, 0, Random.Range(-90, 90)));
    }
}
