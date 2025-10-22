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
    public autumnTree at;
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
    public TMP_Text dadCost;
    public int dCNum = 150;
    public int lBNum = 5000;
    public GameObject Dad;
    private bool dadGot = false;
    private bool shearsGot = false;
    private bool doneSG = false;
    private bool doneBW = false;
    private bool doneLB = false;
    public GameObject blower;
    private bool doneLazy = false;
    private bool doneLife = false;
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
        at = tree.GetComponent<autumnTree>();
    }

    // Update is called once per frame
    void Update()
    {
        clicktext.text = clicks.ToString() + " Clicks";
        cpstext.text = Math.Round(cps,2).ToString() + " Clicks Per Second";
        leaftext.text = leaves.ToString() + " Leaves";
        mouseCost.text = mCNum.ToString();
        dadCost.text = dCNum.ToString();
        if (!Open)
        {
            CheckAchievements();
        }
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
        at.spawnLeaf();
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
            if (achNum == 2)
            {
                achNum++;
                shearsGot = true;
            }
        }
        else return;
    }
    public void spawnDad()
    {
        if (leaves >= dCNum)
        {
            leaves -= dCNum;
            dCNum = (int)Math.Round(dCNum * 1.5);
            Instantiate(Dad, spawnPoint.transform.position, Quaternion.identity);
            if (!dadGot)
            {
                AchTitle.text = "Papa's Here";
                AchDesc.text = "Summon The Great LawnMower: Dad";
                if (!Open)
                {
                    anim.enabled = true;
                    StartCoroutine(OpenAchievement());
                    dadGot = true;
                    if (dadGot)
                    {
                        if (shearsGot)
                        {
                            achNum = 4;
                        }
                        else
                        {
                            achNum = 2;
                        }
                    }
                }
            }
        }
    }
    public void spawnBlower()
    {
        if (leaves >= lBNum)
        {
            Vector2 direction = (tree.transform.position - spawnPoint.transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle -= 90;
            Quaternion target = Quaternion.Euler(0, 0, angle);
            leaves -= lBNum;
            lBNum = (int)Math.Round(lBNum * 1.2);
            Instantiate(blower, spawnPoint.transform.position, target);
            if (!doneLB)
            {
                doneLB = true;
                AchTitle.text = "Pass Wind";
                AchDesc.text = "Buy a leafblower";
                if (!Open)
                {
                    anim.enabled = true;
                    StartCoroutine(OpenAchievement());
                    if (doneLB)
                    {
                        if (shearsGot)
                        {
                            achNum = 4;
                        }
                        else
                        {
                            achNum = 2;
                        }
                    }
                }
            }
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
                achNum++;
            }
        }
        else if (clicks == 10 && achNum == 1)
        {
            AchTitle.text = "Leaf Plucker";
            AchDesc.text = "Harvest 10 Leaves";
            if (!Open)
            {
                anim.enabled = true;
                StartCoroutine(OpenAchievement());
                achNum++;

            }
        }
        else if (achNum == 3)
        {
            AchTitle.text = "Rich Kid";
            AchDesc.text = "Buy Shears to Pick Leaves for you";
            if (!Open)
            {
                anim.enabled = true;
                StartCoroutine(OpenAchievement());
                achNum++;
            }
        }
        else if (cps >= 1 && !doneSG)
        {
            AchTitle.text = "Speedy Gonzales";
            AchDesc.text = "Zooming along at a whole click per second! How cute.";
            if (!Open)
            {
                anim.enabled = true;
                StartCoroutine(OpenAchievement());
                doneSG = true;
            }
        }
        else if (Math.Round(cps, 2) >= 10 && !doneBW)
        {
            AchTitle.text = "Billy Whizz";
            AchDesc.text = "Hit 10 CPS!";
            if (!Open)
            {
                anim.enabled = true;
                StartCoroutine(OpenAchievement());
                doneBW = true;
            }
        }
        else if (Math.Round(cps, 2) >= 100 && !doneLazy)
        {
            AchTitle.text = "Lazy Bones";
            AchDesc.text = "Don't even have to work anymore";
            if (!Open)
            {
                anim.enabled = true;
                StartCoroutine(OpenAchievement());
                doneLazy = true;
            }
        }
        else if (Math.Round(cps, 2) >= 1000 && !doneLife)
        {
            AchTitle.text = "Get a Life";
            AchDesc.text = "You're either hacking or have no life  🥀";
            if (!Open)
            {
                anim.enabled = true;
                StartCoroutine(OpenAchievement());
                doneLazy = true;
            }
        }
    }
    public IEnumerator OpenAchievement()
    {
        Ack = true;
        Open = true;
        yield return new WaitForSeconds(2);
        Ack = false;
        Open = false;
        anim.SetBool("Open", false);
    }
}
