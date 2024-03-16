using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ClipboardController : MonoBehaviour
{
    [SerializeField] private List<Image> elementControllerList;
    [SerializeField] private GameObject elementPrefab;
    [SerializeField] private GameObject parentGameObject;
    
    private void Start()
    {
        SetListValues();
    }
    
    private void Update()
    {
        for (int i = 0; i < elementControllerList.Count; i++)
        {
            elementControllerList[i].GetComponentInChildren<Toggle>().onValueChanged.Invoke(BrokenElementsController.Instance.brokenElements[i].isToggle = elementControllerList[i].GetComponentInChildren<Toggle>().isOn);
        }
    }

    private void SetListValues()
    {
        for (int i = 0; i < BrokenElementsController.Instance.brokenElements.Count(); i++)
        {
            var instantiatedGameObject = InstantiateGameObject();
            var element = instantiatedGameObject.GetComponent<Image>();

            elementControllerList.Add(element);
            element.GetComponentInChildren<Text>().text = BrokenElementsController.Instance.brokenElements[i].brokenElementName;
        }
    }

    private GameObject InstantiateGameObject()
    {
        return Instantiate(elementPrefab, parentGameObject.transform);
    }

    public void OnSendResultsButtonClick()
    {
        ResultsController.Instance.CheckResults();
    }
}
