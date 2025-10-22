using UnityEngine;
using TMPro;
using UnityEditor.Rendering;
using Unity.Mathematics;
using System;
using System.Collections;

public class logic : MonoBehaviour
{
    public int clicks = 0;
    private int leaves = 0;
    public float cps = 0;
    public GameObject spawnPoint;
    public GameObject cursor;
    public GameObject tree;
    public TMP_Text clicktext;
    public TMP_Text cpstext;
    public TMP_Text leaftext;
    public TMP_Text mouseCost;
    private int mCNum = 25;
    private int achNum = 0;
    public TMP_Text AchTitle;
    public TMP_Text AchDesc;
    public Animator anim;
    private bool Ack;
    private bool Open = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnPoint = GameObject.Find("Circle");
        tree = GameObject.Find("Tree");
        clicktext = GameObject.FindWithTag("clicks").GetComponent<TMP_Text>();
        cpstext = GameObject.FindWithTag("cps").GetComponent<TMP_Text>();
        leaftext = GameObject.FindWithTag("leaves").GetComponent<TMP_Text>();
        mouseCost.text = 25.ToString();
        anim = GameObject.FindWithTag("ack").GetComponent<Animator>();
        anim.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        clicktext.text = clicks.ToString() + " Clicks";
        cpstext.text = cps.ToString() + " Clicks Per Second";
        leaftext.text = leaves.ToString() + " Leaves";
        mouseCost.text = mCNum.ToString();
        CheckAchievements();
        if (Ack)
        {
            anim.SetBool("Open", true);
        }
        else
        {
            anim.SetBool("Open", false);
        }
    }
    public void addClicks(int value)
    {
        clicks += value;
        leaves += value;
    }
    public void spawnCursor()
    {
        if (leaves >= mCNum)
        {
            Vector2 direction = (tree.transform.position - spawnPoint.transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle -= 90;
            Quaternion target = Quaternion.Euler(0, 0, angle);
            leaves -= mCNum;
            mCNum = (int)Math.Round(mCNum * 1.2);
            Instantiate(cursor, spawnPoint.transform.position, target);
        }
        else return;
    }
    public void CheckAchievements()
    {
        if (clicks == 1 && achNum == 0)
        {
            AchTitle.text = "Slow & Steady";
            AchDesc.text = "Harvest 1 Leaf";
            if (!Open)
            {
                anim.enabled = true;
                StartCoroutine(OpenAchievement());
            }
        }
    }
    public IEnumerator OpenAchievement()
    {
        Ack = true;
        Open = true;
        yield return new WaitForSeconds(2);
        Ack = false;
        achNum = 1;
        anim.SetBool("Open", false);
    }
}
