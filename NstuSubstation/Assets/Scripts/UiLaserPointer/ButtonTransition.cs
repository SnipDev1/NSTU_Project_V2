using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UiLaserPointer
{
    public class ButtonTransition : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        public Color32 normalColor = Color.white;
        public Color32 hoverColor = Color.grey;
        public Color32 downColor = Color.white;

        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            print("Enter");

            _image.color = hoverColor;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            print("Exit");

            _image.color = normalColor;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            print("Down");

            _image.color = downColor;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            print("Up");
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            print("Click");

            _image.color = hoverColor;
        }
    }
}
