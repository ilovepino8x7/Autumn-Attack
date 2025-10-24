using System.Collections;
using UnityEngine;

public class LeafBlowerMove : MonoBehaviour
{
    public GameObject[] path;
    private int pathnum = 0;
    public GameObject tree;
    public logic ls;
    public float timer = 1;
    public float spawnTime = 1;
    private int value = 0;
    private int[] values = { 25, 50, 100, 250, 1000, 5000, 10000, 25000, 50000, 100000, 1000000, 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tree = GameObject.Find("Tree");
        GameObject[] unordered = GameObject.FindGameObjectsWithTag("PathPoint");
        System.Array.Sort(unordered, (GameObject a, GameObject b) => GrabNum(a.name).CompareTo(GrabNum(b.name)));
        path = unordered;
        ls = GameObject.FindWithTag("logos").GetComponent<logic>();
        ls.cps += (float)values[value] / (float)spawnTime;
        if (transform.gameObject.tag != "freeb")
        {
            ls.blowerCount++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(path[pathnum].transform.position, (Vector2)transform.position) < 0.001)
        {
            pathnum++;
        }
        if (pathnum > path.Length - 1)
        {
            pathnum = 0;
        }
        moveToNext(pathnum);
        Vector2 direction = (tree.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle -= 90;
        Quaternion target = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, target, 200 * Time.deltaTime);

        if (timer >= spawnTime)
        {
            timer = 0;
            // add upgrades adn a function to increase the value
            ls.addClicks(values[value]);
        }
        else
        {
            timer += Time.deltaTime;
        }
    }
    public void moveToNext(int num)
    {
        transform.position = Vector3.MoveTowards(transform.position, path[num].transform.position, 2 * Time.deltaTime);

    }
    private int GrabNum(string nom)
    {
        if (nom == "Circle") return 0;
        int start = nom.IndexOf('(');
        int end = nom.IndexOf(')');
        if (start >= 0 && end >= 0)
        {
            string numberStr = nom.Substring(start + 1, end - start - 1);
            if (int.TryParse(numberStr, out int result))
            {
                return result;
            }
        }
        return int.MaxValue;
    }
    public void Upgrade()
    {
        value += 1;
        if (ls == null)
        {
            ls = GameObject.FindWithTag("logos").GetComponent<logic>();
        }
        if (ls != null)
        {
            ls.cps -= (float)values[value - 1] / (float)spawnTime;
            ls.cps += (float)values[value] / (float)spawnTime;
        }
    }


}
