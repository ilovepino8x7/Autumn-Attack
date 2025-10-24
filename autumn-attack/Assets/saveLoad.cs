using TMPro;
using UnityEngine;

public class saveLoad : MonoBehaviour
{
    public logic ls;
    private string code;
    public TMP_Text codetext;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ls = GetComponent<logic>();
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
    /// Rules to make a code
    // Start and End digit must be 7
    // Then a ~ tilda (separate each item with a tilda)
    // Then the number of clicks
    // Leaves
    // mCNum
    // dCNum
    // lBNum
    // ShearUpPrice
    // DadUpPrice
    // BlowerUpPrice
    // Number of shears
    // Which upgrade they are on

    // Number of Dads
    // Which upgrade they are on
    // Number of Blowers
    // Which upgrade they are on

    // Prestige multiplier
    // ~ Tilda
    /// End with 7
    
}
