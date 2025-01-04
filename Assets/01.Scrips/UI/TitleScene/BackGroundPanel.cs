using UnityEngine;
using UnityEngine.UI;

namespace UIManage.TitleScene
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