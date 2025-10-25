using UnityEngine;
using TMPro;
using UnityEditor.Rendering;
using Unity.Mathematics;
using System;
using System.Collections;
using UnityEngine.UI;

public class logic : MonoBehaviour
{
    public int clicks = 0;
    private int leaves = 0;
    public float cps = 0;
    public GameObject spawnPoint;
    public GameObject cursor;
    public GameObject tree;
    public autumnTree at;
    public GameObject tut;
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
    private int ShearUpPrice = 150;
    public TMP_Text SUP;
    public Button shearbut;
    private int DadUpPrice = 650;
    public Button DadBut;
    public TMP_Text DUP;
    private int BlowerUpPrice = 10000;
    public TMP_Text BUP;
    public Button BloBut;
    public TMP_Text blowerCost;
    private bool upgraded = false;
    private bool upgradedach = false;
    public GameObject presto;
    public int shearCount;
    public int shearUpgrade;
    public int dadUpgrade;
    public int blowerUpgrade;
    public int dadCount;
    public int blowerCount;
    private bool LoadingFromCode;
    private bool protect = true;
    public GameObject Freec;
    public GameObject Freed;
    public GameObject Freeb;
    public bool doneTut = false;
    public saveLoad sl;
    public AudioSource self;
    public AudioClip crunch;
    public AudioClip achDing;
    public AudioClip click;
    public AudioClip levelUp;
    public AudioClip warning;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        presto.SetActive(false);
        spawnPoint = GameObject.Find("Circle");
        tree = GameObject.Find("Tree");
        clicktext = GameObject.FindWithTag("clicks").GetComponent<TMP_Text>();
        cpstext = GameObject.FindWithTag("cps").GetComponent<TMP_Text>();
        leaftext = GameObject.FindWithTag("leaves").GetComponent<TMP_Text>();
        mouseCost.text = 25.ToString();
        anim = GameObject.FindWithTag("ack").GetComponent<Animator>();
        anim.enabled = false;
        at = tree.GetComponent<autumnTree>();
        checkPrestige();
        tut.SetActive(false);
        sl = GetComponent<saveLoad>();
        self = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sl.toggle == false && !doneTut)
        {
            doneTut = true;
            tut.SetActive(true);
        }
        if (LoadingFromCode == true && protect)
        {
            StartCoroutine(loadIdles(FreeCursor, shearCount));
            StartCoroutine(loadIdles(FreeDad, dadCount));
            StartCoroutine(loadIdles(FreeBlower, blowerCount));
            protect = false;
            LoadingFromCode = false;
        }
        blowerCost.text = lBNum.ToString();
        BUP.text = ((int)(BlowerUpPrice / 1000)).ToString() + "K";
        SUP.text = ShearUpPrice.ToString();
        DUP.text = DadUpPrice.ToString();
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
        if (leaves < ShearUpPrice)
        {
            shearbut.interactable = false;
        }
        else if (GameObject.FindGameObjectWithTag("cursor") != null && leaves >= ShearUpPrice)
        {
            shearbut.interactable = true;
        }
        if (leaves < DadUpPrice)
        {
            DadBut.interactable = false;
        }
        else if (GameObject.FindGameObjectWithTag("dad") != null && leaves >= DadUpPrice)
        {
            DadBut.interactable = true;
        }
        if (leaves < BlowerUpPrice)
        {
            BloBut.interactable = false;
        }
        else if (GameObject.FindGameObjectWithTag("wind") != null && leaves >= BlowerUpPrice)
        {
            BloBut.interactable = true;
        }
    }
    public void addClicks(int value)
    {
        clicks += value;
        leaves += value;
        at.spawnLeaf();
        self.PlayOneShot(crunch);

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
            self.PlayOneShot(click);
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
            self.PlayOneShot(click);
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
            self.PlayOneShot(click);
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
        else
        {
            Vector2 direction = (tree.transform.position - spawnPoint.transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle -= 90;
            Quaternion target = Quaternion.Euler(0, 0, angle);
            Instantiate(blower, spawnPoint.transform.position, target);
        }
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
                tut.SetActive(false);
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
            AchDesc.text = "You're either hacking or have no life \U0001F940";
            if (!Open)
            {
                anim.enabled = true;
                StartCoroutine(OpenAchievement());
                doneLife = true;
            }
        }
        else if (upgraded && !upgradedach)
        {
            AchTitle.text = "UPGRADE. UPGRADE";
            AchDesc.text = "You made the cybermen happy by UPGRADING";
            if (!Open)
            {
                anim.enabled = true;
                StartCoroutine(OpenAchievement());
                upgradedach = true;
            }
        }
    }
    public IEnumerator OpenAchievement()
    {
        Ack = true;
        Open = true;
        self.PlayOneShot(achDing);
        yield return new WaitForSeconds(2);
        Ack = false;
        Open = false;
        anim.SetBool("Open", false);
    }
    public void UpgradeShears()
    {
        if (GameObject.FindGameObjectWithTag("cursor") != null)
        {
            if (leaves >= ShearUpPrice)
            {
                GameObject.FindGameObjectWithTag("cursor").GetComponent<CursorMove>().Upgrade();
                leaves -= ShearUpPrice;
                self.PlayOneShot(levelUp);
                shearUpgrade++;
                ShearUpPrice = (int)Math.Round(ShearUpPrice * 1.2);
                upgraded = true;
            }
        }
    }
    public void UpgradePapa()
    {
        if (GameObject.FindWithTag("dad") != null)
        {
            if (leaves >= DadUpPrice)
            {
                GameObject.FindWithTag("dad").GetComponent<DadMove>().Upgrade();
                leaves -= DadUpPrice;
                self.PlayOneShot(levelUp);
                dadUpgrade++;
                DadUpPrice = (int)Math.Round(DadUpPrice * 1.2);
                upgraded = true;
            }
        }
    }
    public void UpgradeBlower()
    {
        if (GameObject.FindWithTag("wind") != null)
        {
            if (leaves >= BlowerUpPrice)
            {
                GameObject.FindWithTag("wind").GetComponent<LeafBlowerMove>().Upgrade();
                leaves -= BlowerUpPrice;
                self.PlayOneShot(levelUp);
                blowerUpgrade++;
                BlowerUpPrice = (int)Math.Round(BlowerUpPrice * 1.2);
                upgraded = true;
            }
        }
    }
    private void checkPrestige()
    {
        if (prestigeManager.Instance.multiplier == 0)
        {
            return;
        }
        else if (prestigeManager.Instance.multiplier == 1)
        {
            doneBW = true;
            doneLazy = true;
            doneLB = true;
            doneLife = false;
            doneSG = true;
            dadGot = true;
            shearsGot = true;
            upgraded = true;
            upgradedach = true;
            mCNum = 15;
            dCNum = 100;
            lBNum = 2500;
            ShearUpPrice = 100;
            DadUpPrice = 500;
            BlowerUpPrice = 5000;
        }
        else if (prestigeManager.Instance.multiplier == 2)
        {
            doneBW = true;
            doneLazy = true;
            doneLB = true;
            doneLife = false;
            doneSG = true;
            dadGot = true;
            shearsGot = true;
            upgraded = true;
            upgradedach = true;
            mCNum = 10;
            dCNum = 60;
            lBNum = 1500;
            ShearUpPrice = 75;
            DadUpPrice = 300;
            BlowerUpPrice = 2500;
        }
        else if (prestigeManager.Instance.multiplier == 3)
        {
            doneBW = true;
            doneLazy = true;
            doneLB = true;
            doneLife = false;
            doneSG = true;
            dadGot = true;
            shearsGot = true;
            upgraded = true;
            upgradedach = true;
            mCNum = 5;
            dCNum = 40;
            lBNum = 750;
            ShearUpPrice = 50;
            DadUpPrice = 150;
            BlowerUpPrice = 1000;
        }
        else if (prestigeManager.Instance.multiplier == 4)
        {
            doneBW = true;
            doneLazy = true;
            doneLB = true;
            doneLife = false;
            doneSG = true;
            dadGot = true;
            shearsGot = true;
            upgraded = true;
            upgradedach = true;
            mCNum = 1;
            dCNum = 10;
            lBNum = 200;
            ShearUpPrice = 10;
            DadUpPrice = 200;
            BlowerUpPrice = 500;
        }
        else
        {
            doneBW = true;
            doneLazy = true;
            doneLB = true;
            doneLife = false;
            doneSG = true;
            dadGot = true;
            shearsGot = true;
            upgraded = true;
            upgradedach = true;
            mCNum = 1;
            dCNum = 1;
            lBNum = 1;
            ShearUpPrice = 1;
            DadUpPrice = 1;
            BlowerUpPrice = 1;
        }
    }

    public void Prestige()
    {
        self.PlayOneShot(warning);
        prestigeManager.Instance.Prestige();
    }
    public void confirmPrestige()
    {
        self.PlayOneShot(warning);
        presto.SetActive(true);
    }

    public int GetLeaves()
    {
        return leaves;
    }
    public int GetMCNUM()
    {
        return mCNum;
    }
    public int GetDCNUM()
    {
        return dCNum;
    }
    public int GetlBNUM()
    {
        return lBNum;
    }
    public int GetShearUpPrice()
    {
        return ShearUpPrice;
    }
    public int GetDadUpPrice()
    {
        return DadUpPrice;
    }
    public int GetBlowerUpPrice()
    {
        return BlowerUpPrice;
    }
    public void setLeaves(int input)
    {
        leaves = input;
    }
    public void setMCNum(int input)
    {
        mCNum = input;
    }
    public void setDCNum(int input)
    {
        dCNum = input;
    }
    public void setLBNum(int input)
    {
        lBNum = input;
    }
    public void setShearUpPrice(int input)
    {
        ShearUpPrice = input;
    }
    public void setDadUpPrice(int input)
    {
        DadUpPrice = input;
    }
    public void setBlowerUpPrice(int input)
    {
        BlowerUpPrice = input;
    }
    public IEnumerator spawnOnDelay(Action spawn)
    {
        yield return new WaitForSeconds(0.5f);
        spawn.Invoke();
    }
    private IEnumerator loadIdles(Action spawn, int typeNum)
    {
        for (int i = 0; i < typeNum; i++)
        {
            yield return StartCoroutine(spawnOnDelay(spawn));
        }
        if (spawn == FreeCursor)
        {
            loadUpgrades(FreeUpgradeShears, shearUpgrade);
        }
        else if (spawn == FreeDad)
        {
            loadUpgrades(FreeUpgradePapa, dadUpgrade);
        }
        else if (spawn == FreeBlower)
        {
            loadUpgrades(FreeUpgradeBlower, blowerUpgrade);
        }
    }
    private void loadUpgrades(Action upgrade, int times)
    {
        for (int i = 0; i < times; i++)
        {
            upgrade.Invoke();
        }
    }
    public void SetLC()
    {
        LoadingFromCode = true;
    }
    public void FreeCursor()
    {
        Vector2 direction = (tree.transform.position - spawnPoint.transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle -= 90;
        Quaternion target = Quaternion.Euler(0, 0, angle);
        Instantiate(Freec, spawnPoint.transform.position, target);
    }
    public void FreeDad()
    {
        Instantiate(Freed, spawnPoint.transform.position, Quaternion.identity);
    }
    public void FreeBlower()
    {
        Vector2 direction = (tree.transform.position - spawnPoint.transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle -= 90;
        Quaternion target = Quaternion.Euler(0, 0, angle);
        Instantiate(Freeb, spawnPoint.transform.position, target);
    }
    public void FreeUpgradeShears()
    {
        if (GameObject.FindGameObjectWithTag("freec") != null)
        {
            GameObject.FindGameObjectWithTag("freec").GetComponent<CursorMove>().Upgrade();
        }
    }
    public void FreeUpgradePapa()
    {
        if (GameObject.FindWithTag("freed") != null)
        {
            GameObject.FindWithTag("freed").GetComponent<DadMove>().Upgrade();
        }
    }
    public void FreeUpgradeBlower()
    {
        if (GameObject.FindWithTag("freeb") != null)
        {
            GameObject.FindWithTag("freeb").GetComponent<LeafBlowerMove>().Upgrade();
        }
    }
}

