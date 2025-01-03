using UnityEngine;
using UnityEngine.UI;

namespace UIManage.LobbyScene
{

    public class BackGroundPanel : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Color[] _colorSets;


        private void Awake()
        {
            _image.color = _colorSets[Random.Range(0, _colorSets.Length)];
        }
    }

}