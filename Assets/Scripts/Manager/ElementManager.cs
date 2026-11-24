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
        {Elements.Maudit, 0},
        { Elements.None, 0}
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

        JaugeElement.instance.RefreshJauge(keys);
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
            elementLevels[element] = 4;
        }
        else if (elementNumberRooms[element] >= numberRoomsToLevel3)
        {
            elementLevels[element] = 3;
        }
        else if (elementNumberRooms[element] >= numberRoomsToLevel2)
        {
            elementLevels[element] = 2;
        }
        else if (elementNumberRooms[element] >= numberRoomsToLevel1)
        {
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
        bool levelOnePlus = false;
        foreach (Elements element in keys)
        {
            if (elementLevels[element] > 0)
            {
                levelOnePlus = true;
            }
        }
        if (levelOnePlus)
        {
            Elements highestLevelElement = keys[0];

            for (int i = 1; i < keys.Length; i++)
            {
                if (elementLevels[keys[i]] > elementLevels[highestLevelElement])
                {
                    highestLevelElement = keys[i];
                }
            }
            
            sameLevelElements.Add(highestLevelElement);

            foreach (Elements element in keys)
            {
                if (element != highestLevelElement && elementLevels[element] == elementLevels[highestLevelElement])
                {
                    sameLevelElements.Add(element);
                }
            }

            if (sameLevelElements.Count > 1)
            {
                List<Elements> sameRatio =  new List<Elements>();
                Elements highestRatioElement = sameLevelElements[0];

                for (int i = 1; i < sameLevelElements.Count; i++)
                {
                    if (sameLevelElements[i] > highestRatioElement)
                    {
                        highestRatioElement = sameLevelElements[i];
                    }
                }
                
                sameRatio.Add(highestRatioElement);

                foreach (Elements element in sameLevelElements)
                {
                    if (element != highestRatioElement && elementRatios[element] == elementRatios[highestRatioElement])
                    {
                        sameRatio.Add(element);
                    }
                }

                if (sameRatio.Count > 1)
                {
                    elementDominant = Elements.None;
                }
                else
                {
                    elementDominant = highestRatioElement;
                }
            }
            else
            {
                elementDominant = highestLevelElement;
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
