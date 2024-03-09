using System.Collections;
using TMPro;
using UnityEngine;

namespace Excursion.Clipboard
{
    public class ObjectDescriptionController : MonoBehaviour
    {
        [SerializeField] private ElementObservation.ElementObservation elementObservation;
        [SerializeField] private TextMeshProUGUI headerTextMeshPro;
        [SerializeField] private TextMeshProUGUI bodyTextMeshPro;
        [SerializeField] private string headerPlaceholder = "Название: ";
        [SerializeField] private string bodyPlaceholder = "";
        [SerializeField] private float timeBetweenUpdate = 1f;
        private int _currentPoint = -1;

        private void Start()
        {
            StartCoroutine(UpdateObjectDescription());
        }

        private void SetHeader(string text)
        {
            headerTextMeshPro.text = headerPlaceholder + text;
        }

        private void SetDescription(string text)
        {
            bodyTextMeshPro.text = bodyPlaceholder + text;
        }

        private IEnumerator UpdateObjectDescription()
        {
            while (true)
            {
                int elementObservationCurrentPoint = elementObservation.currentPoint;
                if (_currentPoint != elementObservationCurrentPoint &&
                    elementObservationCurrentPoint < elementObservation.kElements.Count)
                {
                    _currentPoint = elementObservationCurrentPoint;
                    SetHeader(elementObservation.kElements[_currentPoint].elementHeaderText);
                    SetDescription(elementObservation.kElements[_currentPoint].elementBodyText);
                }

                yield return new WaitForSeconds(timeBetweenUpdate);
            }
        }
    }
}