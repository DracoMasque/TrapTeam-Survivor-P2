using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuildingChoise : MonoBehaviour
{
    [SerializeField] private ChoicePanel[] choicePanel;
    [SerializeField] private Room[] sallePossible;
    [SerializeField] private Groupement[] formePossible;
    
    [SerializeField] private BuildingSystem building;

    public List<List<Room>> sallePossibleChoice =  new List<List<Room>>();
    public List<Groupement> formePossibleChoice;

    private void Start()
    {
        sallePossible = Resources.LoadAll<Room>("BuildingPrefabs\\Salles");
        formePossible = Resources.LoadAll<Groupement>("BuildingPrefabs\\Groupements");
    }

    public void BuildingChoice()
    {
        foreach (ChoicePanel c in choicePanel)
        {
            c.ClearRoomNames();
        }
        BuildGroupPossible();
        for (int i = 0; i < choicePanel.Length; i++)
        {
            Groupement g = Instantiate(formePossibleChoice[i], choicePanel[i].imagePoint.transform);
            //g.BuildThumbnail(sallePossibleChoice[i]);
            g.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
            for (int k = 0; k < sallePossibleChoice[i].Count; k++)
            {
                choicePanel[i].roomNames[k].text = "- " + sallePossibleChoice[i][k].name;
                choicePanel[i].elementNames[k].text = "- " + sallePossibleChoice[i][k].elements.elements;
            }
        }
    }

    private void BuildGroupPossible()
    {
        for (int i = 0; i < choicePanel.Length; i++)
        {
            formePossibleChoice.Add(formePossible[Random.Range(0, formePossible.Length)]);
        }

        foreach (Groupement forme in formePossibleChoice)
        {
            List<Room> formeRooms = new List<Room>();
            for (int i = 0; i < forme.groupedRooms.Count; i++)
            {
                formeRooms.Add(GetComponent<RandomDrop>().GetRandomDrop().GetComponent<Room>());
            }
            sallePossibleChoice.Add(formeRooms);
        }
    }

    public void Choice(int number)
    {
        GameObject groupement = Instantiate(formePossibleChoice[number].gameObject, transform.position, new Quaternion(0,0,0,0));
        groupement.GetComponent<Groupement>().BuildGroup(sallePossibleChoice[number]);
        building.groupement = groupement.GetComponent<Groupement>();
        groupement.transform.SetParent(building.cellIndicator.transform);
        groupement.transform.SetLocalPositionAndRotation(new Vector3(0,0,0), building.cellIndicator.transform.localRotation);
        sallePossibleChoice.Clear();
        formePossibleChoice.Clear();
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }
}
