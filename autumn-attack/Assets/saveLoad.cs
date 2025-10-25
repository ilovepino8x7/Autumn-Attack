using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class saveLoad : MonoBehaviour
{
    public logic ls;
    private string code;
    private string toDecode;
    public TMP_Text codetext;
    public GameObject menu;
    public bool toggle = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ls = GetComponent<logic>();
        menu.SetActive(toggle);
    }

    // Update is called once per frame
    void Update()
    {
        generateCode();
    }
    public void generateCode()
    {
        code =
        "7" +
        "~" +
        ls.clicks +
        "~" +
        ls.GetLeaves().ToString() +
        "~" +
        ls.GetMCNUM().ToString() +
        "~" +
        ls.GetDCNUM().ToString() +
        "~" +
        ls.GetlBNUM().ToString() +
        "~" +
        ls.GetShearUpPrice().ToString() +
        "~" +
        ls.GetDadUpPrice().ToString() +
        "~" +
        ls.GetBlowerUpPrice().ToString() +
        "~" +
        ls.shearCount.ToString() +
        "~" +
        ls.shearUpgrade.ToString() +
        "~" +
        ls.dadCount.ToString() +
        "~" +
        ls.dadUpgrade.ToString() +
        "~" +
        ls.blowerCount.ToString() +
        "~" +
        ls.blowerUpgrade.ToString() +
        "~" +
        prestigeManager.Instance.multiplier.ToString() +
        "~" +
        "7";
        if (codetext.text != code)
        {
            codetext.text = code;
        }
    }
    public void toggleMenu()
    {
        toggle = !toggle;
        menu.SetActive(toggle);
    }
    public void Copy()
    {
        GUIUtility.systemCopyBuffer = code;
    }
    public void GetCode()
    {
        toDecode = GUIUtility.systemCopyBuffer;
    }
    public void DeCode()
    {
        if (string.IsNullOrEmpty(toDecode))
        {
            //Bad
            return;
        }
        string[] sections = toDecode.Split('~');
        if (sections[0] != "7" || sections.Length < 3 || sections[sections.Length - 1] != "7")
        {
            //Bad
            return;
        }
        try
        {
            ls.clicks = int.Parse(sections[1]);
            ls.setLeaves(int.Parse(sections[2]));
            ls.setMCNum(int.Parse(sections[3]));
            ls.setDCNum(int.Parse(sections[4]));
            ls.setLBNum(int.Parse(sections[5]));
            ls.setShearUpPrice(int.Parse(sections[6]));
            ls.setDadUpPrice(int.Parse(sections[7]));
            ls.setBlowerUpPrice(int.Parse(sections[8]));
            ls.shearCount = int.Parse(sections[9]);
            ls.shearUpgrade = int.Parse(sections[10]);
            ls.dadCount = int.Parse(sections[11]);
            ls.dadUpgrade = int.Parse(sections[12]);
            ls.blowerCount = int.Parse(sections[13]);
            ls.blowerUpgrade = int.Parse(sections[14]);
            prestigeManager.Instance.multiplier = int.Parse(sections[15]);
            ls.SetLC();
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error decoding save code: " + e.Message);
        }
    }
    public void GetAndLoad()
    {
        GetCode();
        DeCode();
        toggleMenu();
    }
    
}
