using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class prestigeManager : MonoBehaviour
{
    public static prestigeManager Instance;
    public int multiplier;
    private int cost = 100000;
    private int[] prices = { 100000, 250000, 500000, 1000000, 2500000, 5000000, 7500000, 10000000 };
    public TMP_Text price;
    public Button pst;
    public logic ls;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        cost = prices[multiplier];
        if (SceneManager.GetActiveScene().name == "Game")
        {
            if (ls == null)
            {
                ls = GameObject.FindWithTag("logos").GetComponent<logic>();
            }
            if (pst == null)
            {
                pst = GameObject.FindWithTag("pst").GetComponent<Button>();
            }
            if (price == null)
            {
                price = GameObject.FindWithTag("monero").GetComponent<TMP_Text>();
            }
            if (ls.GetLeaves() <= cost)
            {
                pst.interactable = false;
            }
            else
            {
                pst.interactable = true;
            }
            price.text = ((int)(cost / 1000)).ToString() + "K";
        }
    }
    public void Prestige()
    {
        multiplier++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
