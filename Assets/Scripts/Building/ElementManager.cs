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
    
    public Dictionary<Elements, float> elementBonusTotal = new Dictionary<Elements, float>
    {
        {Elements.Feu, 0},
        {Elements.Glace, 0},
        {Elements.Nature, 0},
        {Elements.Maudit, 0}
    };

    private Elements[] keys;
    
    public Elements elementDominant;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        keys = elementNumberRooms.Keys.ToArray();
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
        switch (room.elements.elements)
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
        
        ChangeLevel(room.elements.elements);
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
        switch (room.elements.elements)
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
        ChangeLevel(room.elements.elements);
        BuildRatio();
    }

    public void DominatingElement()
    {
        sameLevelElements.Clear();
        Elements previousElement = Elements.None;
        Elements highestLevelElement = Elements.None;
        for (int i = 0; i < elementLevels.Count; i++)
        {
            if (previousElement != Elements.None && elementLevels[keys[i]] > elementLevels[previousElement])
            {
                highestLevelElement = keys[i];
                print(elementLevels[keys[i]]);
            }
            else if (previousElement != Elements.None && elementLevels[keys[i]] == elementLevels[previousElement])
            {
                if (highestLevelElement != Elements.None && elementLevels[keys[i]] == elementLevels[highestLevelElement])
                {
                    sameLevelElements.Add(keys[i]);
                }
            }
            else if (previousElement == Elements.None)
            {
                highestLevelElement = keys[i];
            }
            previousElement = keys[i];
        }

        if (highestLevelElement == Elements.None && elementLevels[highestLevelElement] > 0)
        {
            elementDominant = Elements.None;
        }
        else if (highestLevelElement != Elements.None && sameLevelElements.Count < 1)
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
    }

    public void ElementBonusTotalCalcul()
    {
        foreach (Elements element in keys)
        {
            elementBonusTotal[element] = 0;
        }
        foreach (Elements element in keys)
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                if (rooms[i].elements.elements == element)
                {
                    elementBonusTotal[element] += rooms[i].bonusElement;
                }
            }
        }
    }
}
