using Map.MapManager;
using UnityEngine;

public class ScrollStructure : MonoBehaviour
{
    [SerializeField] private float offset = 1.0f;

    void Update()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y + MapManager.Instance.fallingSpeed * Time.deltaTime);

        if (transform.position.y > Camera.main.transform.position.y + Camera.main.orthographicSize + offset)
        {
            DeleteObject();
        }
    }

    void DeleteObject()
    {
        gameObject.SetActive(false);
    }
}
