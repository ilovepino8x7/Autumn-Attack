using UnityEngine;
using TMPro;

public class logic : MonoBehaviour
{
    public int clicks = 0;
    private int leaves = 0;
    public int cps = 0;
    public GameObject spawnPoint;
    public GameObject cursor;
    public GameObject tree;
    public TMP_Text clicktext;
    public TMP_Text cpstext;
    public TMP_Text leaftext;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnPoint = GameObject.Find("Circle");
        tree = GameObject.Find("Tree");
        clicktext = GameObject.FindWithTag("clicks").GetComponent<TMP_Text>();
        cpstext = GameObject.FindWithTag("cps").GetComponent<TMP_Text>();
        leaftext = GameObject.FindWithTag("leaves").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        clicktext.text = clicks.ToString() + " Clicks";
        cpstext.text = cps.ToString() + " Clicks Per Second";
        leaftext.text = leaves.ToString() + " Leaves";
    }
    public void addClicks(int value)
    {
        clicks += value;
        leaves += value;
    }
    public void spawnCursor()
    {
        Vector2 direction = (tree.transform.position - spawnPoint.transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle -= 90;
        Quaternion target = Quaternion.Euler(0, 0, angle);
        Instantiate(cursor, spawnPoint.transform.position, target);
    }
}
