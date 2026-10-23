using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class LevelUPManager : MonoBehaviour
{
    public float currentXp = 0;
    public int level = 0;
    public float maxXp = 10;
    public GameObject levelUpMenu;
    public TextMeshProUGUI levelTextTemp;
    public Slider xpSlider;
    [SerializeField] private Equipement[] equipementsPossible;
    [SerializeField] private Arme[] armesPossible;
    [SerializeField] private LevelUPPanel[] levelUpPanels;
    
    [SerializeField] private List<GameObject> possiblilities;
    [SerializeField] private PlayerEquiment playerEquiment;

    public static LevelUPManager instance;
    
    void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
        xpSlider = GameObject.Find("XpBar").GetComponent<Slider>();
        xpSlider.value = currentXp;
        equipementsPossible = Resources.LoadAll<Equipement>("Prefabs/Equipement");
        armesPossible = Resources.LoadAll<Arme>("Prefabs/Competences");
        playerEquiment = GameObject.FindWithTag("Player").GetComponent<PlayerEquiment>();
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
        BuildLevelUpChoice();
    }

    public void UiLevelUpClose()
    {
        levelUpMenu.SetActive(false);
        possiblilities.Clear();
        Time.timeScale = 1;
    }

    void BuildLevelUpChoice()
    {
        for (int i = 0; i < levelUpPanels.Length; i++)
        {
            int type = Random.Range(1, 3);
            print(type);
            switch (type)
            {
                case 1: possiblilities.Add(equipementsPossible[Random.Range(0, equipementsPossible.Length)].gameObject);
                    Equipement equipement = possiblilities[i].GetComponent<Equipement>();
                    levelUpPanels[i].descriptionText.text = equipement.description;
                    levelUpPanels[i].nameText.text = equipement.name;
                    levelUpPanels[i].imagePoint.GetComponent<Image>().sprite = equipement.image;
                    break;
                case 2: possiblilities.Add(armesPossible[Random.Range(0, armesPossible.Length)].gameObject);
                    Arme arme = possiblilities[i].GetComponent<Arme>();
                    levelUpPanels[i].nameText.text = arme.name;
                    levelUpPanels[i].descriptionText.text = arme.description;
                    levelUpPanels[i].imagePoint.GetComponent<Image>().sprite = arme.image;
                    break;
            }
        }
    }

    public void Choice(int number)
    {
        if (equipementsPossible.Contains(possiblilities[number].GetComponent<Equipement>()))
        {
            Equipement equipement = possiblilities[number].GetComponent<Equipement>();
            if (playerEquiment.equipements.Count < playerEquiment.maxEquipements)
            {
                playerEquiment.equipements.Add(equipement);
                equipement.OnPickUp(playerEquiment.gameObject.GetComponent<Controlleur>());
            }
        }
        else if (armesPossible.Contains(possiblilities[number].GetComponent<Arme>()))
        {
            GameObject armeInstant = Instantiate(possiblilities[number], playerEquiment.transform.position, playerEquiment.transform.rotation);
            switch (possiblilities[number].GetComponent<Arme>().type)
            {
                case TypeArme.Aura : armeInstant.gameObject.transform.SetParent(playerEquiment.transform.GetChild(1).transform);
                    break;
                case TypeArme.Range : armeInstant.gameObject.transform.SetParent(playerEquiment.transform.GetChild(2).transform);
                    break;
            }
        }
        UiLevelUpClose();
    }
}
