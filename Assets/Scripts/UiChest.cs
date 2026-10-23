using UnityEngine;

public class UiChest : MonoBehaviour
{
    public LevelUPPanel levelupPanel;
    public static UiChest instance;

    void Awake()
    {
        instance = this;
    }

    public void EnableChest()
    {
        levelupPanel.gameObject.SetActive(true);
        Time.timeScale = 0;
        
    }
}
