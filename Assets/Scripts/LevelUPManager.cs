using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class LevelUPManager : MonoBehaviour
{
    public float currentXp = 0;
    public int level = 0;
    public float maxXp = 10;
    public GameObject levelUpMenu;
    public TextMeshProUGUI levelTextTemp;
    public Slider xpSlider;

    public static LevelUPManager instance;
    
    void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
        xpSlider = GameObject.Find("XpBar").GetComponent<Slider>();
        xpSlider.value = currentXp;
    }
    public void AddXp(float xp)
    {
        currentXp += xp;
        xpSlider.value = currentXp/maxXp;
        CheckLevelUp();
    }

    public void CheckLevelUp()
    {
        if (currentXp >= maxXp)
        {
            currentXp -= maxXp;
            level++;
            UILevelUpOpen();
            levelTextTemp.text = "level :  " + level;
        }
    }

    void UILevelUpOpen()
    {
        Time.timeScale = 0;
        levelUpMenu.SetActive(true);
    }

    public void UiLevelUpClose()
    {
        levelUpMenu.SetActive(false);
        Time.timeScale = 1;
    }
}
