using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Excursion.Clipboard
{
    public class ElementController : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI textMeshPro;
        public void SetColor(Color color)
        {
            Debug.Log("Set Color");
            image.color = color;
        }

        public void SetText(string text)
        {
            Debug.Log("Set Text");
            textMeshPro.text = text;
        }
    }
}
