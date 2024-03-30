using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Excursion.Clipboard
{
    public class ObjectsList : MonoBehaviour
    {
        [SerializeField] private List<ElementController> elementControllerList;
        [SerializeField] private ElementObservation.ElementObservation elementObservation;
        [SerializeField] private GameObject elementPrefab;
        [SerializeField] private GameObject alignmentGameObject;
        [SerializeField] private Color defaultColor = Color.gray;
        [SerializeField] private Color chosenColor = Color.cyan;
        [SerializeField] private float timeBetweenUpdate = 1f;
        private int currentPoint = -1;


        private void Start()
        {
            SetListValues();
            StartCoroutine(UpdateCurrentElement());
        }

        private void SetListValues()
        {
            for (int i = 0; i < elementObservation.kElements.Count; i++)
            {
                var instantiatedGameObject = InstantiateGameObject();
                var element = instantiatedGameObject.GetComponent<ElementController>();
                var elementName = elementObservation.kElements[i].elementName;

                elementControllerList.Add(element);
                element.SetColor(defaultColor);
                element.SetText(elementName);
            }
        }

        private void ChangeAllColorsOnDefault()
        {
            for (int i = 0; i < elementControllerList.Count; i++)
            {
                ChangeColorOnDefaultByIndex(i);
            }
        }

        private void ChangeColorOnChooseByIndex(int index)
        {
            elementControllerList[index].SetColor(chosenColor);
        }

        private void ChangeColorOnDefaultByIndex(int index)
        {
            elementControllerList[index].SetColor(defaultColor);
        }

        private GameObject InstantiateGameObject()
        {
            return Instantiate(elementPrefab, alignmentGameObject.transform);
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private IEnumerator UpdateCurrentElement()
        {
            while (true)
            {
                int elementObservationCurrentPoint = elementObservation.currentPoint;
                if (currentPoint != elementObservationCurrentPoint && elementObservationCurrentPoint < elementControllerList.Count)
                {
                    currentPoint = elementObservationCurrentPoint;
                    ChangeAllColorsOnDefault();
                    ChangeColorOnChooseByIndex(currentPoint);
                }

                yield return new WaitForSeconds(timeBetweenUpdate);
            }
        }
    }
}