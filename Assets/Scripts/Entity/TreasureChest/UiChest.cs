using UnityEngine;
using UnityEngine.UI;

public class UiChest : MonoBehaviour
{
    public LevelUPPanel levelupPanel;
    private Equipement equipement;
    private PlayerEquiment playerEquiment;
    
    public static UiChest instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        playerEquiment = GameObject.FindWithTag("Player").GetComponent<PlayerEquiment>();
    }

    public void EnableChest()
    {
        levelupPanel.gameObject.SetActive(true);
        equipement = GetComponent<RandomDrop>().GetRandomDrop().GetComponent<Equipement>();
        levelupPanel.nameText.text = equipement.name;
        levelupPanel.descriptionText.text = equipement.description;
        levelupPanel.imagePoint.GetComponent<Image>().sprite = equipement.image;
        Time.timeScale = 0;
    }

    public void Choice()
    {
        playerEquiment.equipements.Add(equipement);
        Close();
    }

    public void Close()
    {
        levelupPanel.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
