using System.Collections.Generic;
using System.Linq;
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
    
    [SerializeField] public Dictionary<Elements, float> elementNumberRooms = new Dictionary<Elements, float>
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
        
        ChangeLevel(room.element);
        BuildRatio();
    }

    private void ChangeLevel(Elements element)
    {
        if (elementNumberRooms[element] >= numberRoomsToLevel4)
        {
            print("level up element");
            elementLevels[element] = 4;
        }
        else if (elementNumberRooms[element] >= numberRoomsToLevel3)
        {
            print("level up element");
            elementLevels[element] = 3;
        }
        else if (elementNumberRooms[element] >= numberRoomsToLevel2)
        {
            print("level up element");
            elementLevels[element] = 2;
        }
        else if (elementNumberRooms[element] >= numberRoomsToLevel1)
        {
            print("level up element");
            elementLevels[element] = 1;
        }
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
        ChangeLevel(room.element);
        BuildRatio();
    }

    public void DominatingElement()
    {
        sameLevelElements.Clear();
        Elements[] elements = elementLevels.Keys.ToArray() ;
        Elements previousElement = Elements.None;
        Elements highestLevelElement = Elements.None;
        for (int i = 0; i < elementLevels.Count; i++)
        {
            if (previousElement != Elements.None && elementLevels[elements[i]] > elementLevels[previousElement])
            {
                highestLevelElement = elements[i];
                print(elementLevels[elements[i]]);
            }
            else if (previousElement != Elements.None && elementLevels[elements[i]] == elementLevels[previousElement])
            {
                if (highestLevelElement != Elements.None && elementLevels[elements[i]] == elementLevels[highestLevelElement])
                {
                    sameLevelElements.Add(elements[i]);
                }
            }
            else if (previousElement == Elements.None)
            {
                highestLevelElement = elements[i];
            }
            previousElement = elements[i];
        }

        if (highestLevelElement != Elements.None && sameLevelElements.Count < 1)
        {
            elementDominant = highestLevelElement;
        }
        else if (sameLevelElements.Count > 1)
        {
            previousElement = Elements.None;
            foreach (Elements element in sameLevelElements)
            {
                if (previousElement != Elements.None && elementRatios[element] > elementRatios[previousElement])
                {
                    elementDominant = element;
                }
                previousElement = element;
            }
        }
        
        /*Elements previousElement = Elements.None;
        
        foreach (KeyValuePair<Elements,int> key in elementLevels)
        {
            print("check level");
            if (previousElement != Elements.None)
            {
                if (elementLevels[key.Key] > elementLevels[previousElement])
                {
                    previousElement = key.Key;
                    elementDominant = key.Key;
                }
                else if (elementLevels[key.Key] == elementLevels[previousElement])
                {
                    print("same level");
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
        sameLevelElements.Clear();*/
    }

}
