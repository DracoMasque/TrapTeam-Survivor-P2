using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private int height;
    [SerializeField] private int width;

    void Start()
    {
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if (BuildingSystem.cellSize <= 0 || width <= 0 || height <= 0) return;
        Vector2 origin = transform.position;
        for (int y = 0; y < height; y++)
        {
            Vector2 start = origin + new Vector2(0, y * BuildingSystem.cellSize);
            Vector2 end = origin + new Vector2(width * BuildingSystem.cellSize, y * BuildingSystem.cellSize);
            Gizmos.DrawLine(start, end);
        }

        for (int x = 0; x < width; x++)
        {
            Vector2 start = origin + new Vector2(x * BuildingSystem.cellSize, 0);
            Vector2 end = origin + new Vector2(x * BuildingSystem.cellSize, height * BuildingSystem.cellSize);
            Gizmos.DrawLine(start, end);
        }
    }
}
