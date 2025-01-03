using Unity.Cinemachine;
using UnityEngine;

public class MapTileNonactivation : MonoBehaviour
{
    [SerializeField] private float offset = 1.0f;
    public CinemachineCamera followCam;

    void Update()
    {
        // transform.position = new Vector2(transform.position.x, transform.position.y + MapManager.Instance.fallingSpeed * Time.deltaTime);

        if (followCam != null && transform.position.y >followCam.transform.position.y + followCam.Lens.OrthographicSize + offset)
        {
            gameObject.SetActive(false);
        }
    }
}
