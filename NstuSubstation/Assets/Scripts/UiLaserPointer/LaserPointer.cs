using System;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.Extras;

namespace UiLaserPointer
{
    public class LaserPointer : SteamVR_LaserPointer
    {
        [SerializeField] private Color onPointerInColor;
        [SerializeField] private Color onPointerClickColor;
        [SerializeField] private Color onPointerOutColor;
        private Color _previousColor;
        public override void OnPointerIn(PointerEventArgs e)
        {
            base.OnPointerIn(e);
            if (!e.target.CompareTag("ButtonUI")) return;
            
            _previousColor = e.target.GetComponent<Image>().color;
            e.target.GetComponent<Image>().color = onPointerInColor;
        }

        public override void OnPointerClick(PointerEventArgs e)
        {
            base.OnPointerIn(e);
            e.target.GetComponent<Button>().onClick.Invoke();
        }

        public override void OnPointerOut(PointerEventArgs e)
        {
            if (e.target.CompareTag("ButtonUI"))
            {
                e.target.GetComponent<Image>().color = _previousColor;
            }
        }
    }
}
