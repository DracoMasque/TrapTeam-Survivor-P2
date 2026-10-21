using UnityEngine;

public class LevelUp : MonoBehaviour
{
    public bool isLevelUp = false;
    public GameObject levelUpMenu;
    
    void Start()
    {
        isLevelUp = !isLevelUp;
    }

    // Update is called once per frame
    void Update()
    {
        if (isLevelUp == true)
            {
                Time.timeScale = 0;
                levelUpMenu.SetActive(isLevelUp);
            }
            else
            {
                Time.timeScale = 1;
            }
    }
    
}
