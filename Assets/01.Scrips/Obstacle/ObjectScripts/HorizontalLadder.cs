using UnityEngine;

public class HorizontalLadder : MonoBehaviour, IDestroyable
{
    public void Destroy()
    {
        gameObject.SetActive(false);
    }
}
