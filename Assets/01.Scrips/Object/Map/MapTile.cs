using Obstacles;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class MapTile : MonoBehaviour
{
    [SerializeField] private float offset = 1.0f;
    public CinemachineCamera followCam;

    [SerializeField] Obstacle[] obstacles;


    public void SetActiveTile(bool value)
    {
        if(value)
        {
            foreach (var obstacle in obstacles)
            {
                obstacle.ResetObstacle();
            }
        }

        gameObject.SetActive(value);

    }

    void Update()
    {
        // transform.position = new Vector2(transform.position.x, transform.position.y + MapManager.Instance.fallingSpeed * Time.deltaTime);

        if (followCam != null && transform.position.y >followCam.transform.position.y + followCam.Lens.OrthographicSize + offset)
        {
            SetActiveTile(false);
        }
    }
}
