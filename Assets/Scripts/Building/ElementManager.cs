using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ElementManager : MonoBehaviour
{
    public static ElementManager instance;
    
    public List<Room> rooms;
    [SerializeField] private List<Elements> sameLevelElements;
    
    public int numberRoomsToLevel1 = 3;
    public int numberRoomsToLevel2 = 4;
    public int numberRoomsToLevel3 = 5;
    public int numberRoomsToLevel4 = 6;
    
    [SerializeField] public Dictionary<Elements, int> elementLevels = new Dictionary<Elements, int>
    {
        {Elements.Feu, 0},
        {Elements.Glace, 0},
        {Elements.Nature, 0},
        {Elements.Maudit, 0}
    };
    
    [SerializeField] public Dictionary<Elements, float> elementRatios = new Dictionary<Elements, float>
    {
        {Elements.Feu, 0f},
        {Elements.Glace, 0f},
        {Elements.Nature, 0f},
        {Elements.Maudit, 0f}
    };
    
    [SerializeField] public Dictionary<Elements, int> elementNumberRooms = new Dictionary<Elements, int>
    {
        {Elements.Feu, 0},
        {Elements.Glace, 0},
        {Elements.Nature, 0},
        {Elements.Maudit, 0}
    };
    
    public Elements elementDominant;

    void Awake()
    {
        instance = this;
    }

    private void BuildRatio()
    {
        elementRatios[Elements.Feu] = elementNumberRooms[Elements.Feu]/rooms.Count;
        elementRatios[Elements.Glace] = elementNumberRooms[Elements.Glace] / rooms.Count;
        elementRatios[Elements.Nature] = elementNumberRooms[Elements.Nature]/rooms.Count;
        elementRatios[Elements.Maudit] = elementNumberRooms[Elements.Maudit] / rooms.Count;

        foreach (KeyValuePair<Elements, int> key in elementLevels)
        {
            if (elementNumberRooms[key.Key] == numberRoomsToLevel1)
            {
                elementLevels[key.Key] = 1;
            }
            else if (elementNumberRooms[key.Key] == numberRoomsToLevel2)
            {
                elementLevels[key.Key] = 2;
            }
            else if (elementNumberRooms[key.Key] == numberRoomsToLevel3)
            {
                elementLevels[key.Key] = 3;
            }
            else if (elementNumberRooms[key.Key] == numberRoomsToLevel4)
            {
                elementLevels[key.Key] = 4;
            }
        }

        DominatingElement();
    }
    
    public void AjouterRoom(Room room)
    {
        rooms.Add(room);
        switch (room.element)
        {
            case Elements.Feu : elementNumberRooms[Elements.Feu]++;
                break;
            case Elements.Glace : elementNumberRooms[Elements.Glace]++;
                break;
            case Elements.Nature : elementNumberRooms[Elements.Nature]++;
                break;
            case Elements.Maudit : elementNumberRooms[Elements.Maudit]++;
                break;
        }
        
        BuildRatio();
    }
    
    public void RemoveRoom(Room room)
    {
        rooms.Remove(room);
        switch (room.element)
        {
            case Elements.Feu : elementNumberRooms[Elements.Feu]--;
                break;
            case Elements.Glace : elementNumberRooms[Elements.Glace]--;
                break;
            case Elements.Nature : elementNumberRooms[Elements.Nature]--;
                break;
            case Elements.Maudit : elementNumberRooms[Elements.Maudit]--;
                break;
        }
        BuildRatio();
    }

    public void DominatingElement()
    {
        Elements previousElement = Elements.None;
        
        foreach (KeyValuePair<Elements,int> key in elementLevels)
        {
            if (previousElement != Elements.None)
            {
                if (elementLevels[key.Key] > elementLevels[previousElement])
                {
                    previousElement = key.Key;
                    elementDominant = key.Key;
                }
                else if (elementLevels[key.Key] == elementLevels[previousElement])
                {
                    sameLevelElements.Add(key.Key);
                    previousElement = key.Key;
                }
            }
            previousElement = key.Key;
        }

        previousElement = Elements.None;

        int hightestLevel = 0;
        foreach (Elements elements in sameLevelElements)
        {
            if (elementLevels[elements] > hightestLevel)
            {
                hightestLevel = elementLevels[elements];
            }
        }
        
        List<Elements> sameLevelElementsTemp = sameLevelElements;
        foreach (Elements elements in sameLevelElementsTemp)
        {
            if (elementLevels[elements] > hightestLevel)
            {
                sameLevelElements.Remove(elements);
            }
        }
        
        if (sameLevelElements.Count < 1)
        {
            foreach (Elements elements in sameLevelElements)
            {
                if (previousElement != Elements.None)
                {
                    if (elementRatios[elements] > elementRatios[previousElement])
                    {
                        previousElement = elements;
                        elementDominant = elements;
                    }
                }
                previousElement = elements;
            }
        }
    }

}
