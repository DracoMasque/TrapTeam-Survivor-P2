using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIArmurieEquipement : MonoBehaviour
{
    public static UIArmurieEquipement instance;
    
    public GameObject[] buttonList;
    
    public GameObject UIChoice;
    public GameObject UIChoiceEquipement;
    public LevelUPPanel UINewGear;
    
    public GameObject activateButton;
    public GameObject cancelButton;
    private Armurie armurie;
    
    private int chosenType;

    public RandomDrop commonUncommon;
    public RandomDrop uncommonRare;
    public RandomDrop rare;
    public RandomDrop arme;

    private GameObject getArme;
    private GameObject getEquipement;
    
    void Awake()
    {
        instance = this;
    }

    public void ActivateTrade()
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            if (i <= PlayerEquiment.instance.equipements.Count-1)
            {
                buttonList[i].SetActive(true);
            }
        }
    }

    public void DeactivateTrade()
    {
        foreach (GameObject button in buttonList)
        {
            button.SetActive(false);
        }
    }

    public void StartArmurie(Armurie actual)
    {
        armurie = actual;
        UIChoice.SetActive(true);
    }
    
    public void ChoseType(int type)
    {
        chosenType = type;
        UIChoice.SetActive(false);
        UIChoiceEquipement.SetActive(true);
        ActivateTrade();
    }

    public void ChoseEquipement(int equipement)
    {
        cancelButton.SetActive(false);
        if(chosenType == 1)
        {
            Equipement equipementGet;
            switch (PlayerEquiment.instance.equipements[equipement].rarity)
            {
                case Rarity.Common:
                    getEquipement = commonUncommon.GetRandomDrop();
                    equipementGet = getEquipement.GetComponent<Equipement>();
                    UINewGear.gameObject.SetActive(true);
                    UINewGear.descriptionText.text = equipementGet.description;
                    UINewGear.nameText.text = equipementGet.name;
                    UINewGear.imagePoint.GetComponent<Image>().sprite = equipementGet.imageLevelUp;
                    break;
                case Rarity.Uncommon:
                    getEquipement = uncommonRare.GetRandomDrop();
                    equipementGet = getEquipement.GetComponent<Equipement>();
                    UINewGear.gameObject.SetActive(true);
                    UINewGear.descriptionText.text = equipementGet.description;
                    UINewGear.nameText.text = equipementGet.name;
                    UINewGear.imagePoint.GetComponent<Image>().sprite = equipementGet.imageLevelUp;
                    break;
                case Rarity.Rare:
                    getEquipement = rare.GetRandomDrop();
                    equipementGet = getEquipement.GetComponent<Equipement>();
                    UINewGear.gameObject.SetActive(true);
                    UINewGear.descriptionText.text = equipementGet.description;
                    UINewGear.nameText.text = equipementGet.name;
                    UINewGear.imagePoint.GetComponent<Image>().sprite = equipementGet.imageLevelUp;
                    break;
            }
        }
        else
        {
            getArme = arme.GetRandomDrop();
            Arme armeGet = getArme.GetComponent<Arme>();
            UINewGear.gameObject.SetActive(true);
            UINewGear.descriptionText.text = armeGet.description;
            UINewGear.nameText.text = armeGet.name;
            UINewGear.imagePoint.GetComponent<Image>().sprite = armeGet.image;
        }
        PlayerEquiment.instance.equipements[equipement].LetGoEquipement(Controlleur.instance);
        UIChoiceEquipement.SetActive(false);
        DeactivateTrade();
    }

    public void Choice()
    {
        if (chosenType == 1)
        {
            GameObject equipementInstance = Instantiate(getEquipement);
            Equipement equipement = equipementInstance.GetComponent<Equipement>();
            PlayerEquiment.instance.equipements.Add(equipement);
            equipement.OnPickUp(Controlleur.instance);
        }
        else
        {
            GameObject armeInstance = Instantiate(getArme, Controlleur.instance.gameObject.transform);
            switch (armeInstance.GetComponent<Arme>().type)
            {
                case TypeArme.Aura : armeInstance.gameObject.transform.SetParent(Controlleur.instance.transform.GetChild(1).transform);
                    break;
                case TypeArme.Range : armeInstance.gameObject.transform.SetParent(Controlleur.instance.transform.GetChild(2).transform);
                    break;
            }
        }
        CloseTrade();
    }

    public void CloseTrade()
    {
        UINewGear.gameObject.SetActive(false);
        armurie.UsedArmurie();
        armurie.cooldown.Play();
        GameManager.instance.paused = false;
    }

    public void Cancel()
    {
        UIChoice.gameObject.SetActive(false);
        UIChoiceEquipement.SetActive(false);
        cancelButton.SetActive(false);
        GameManager.instance.paused = false;
    }
}
