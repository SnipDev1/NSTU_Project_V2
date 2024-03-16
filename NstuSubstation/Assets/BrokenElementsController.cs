using System;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class BrokenElementsController : MonoBehaviour
{
    [Serializable]
    public class BrokenElement
    {
        public GameObject brokenElementObject;

        public string brokenElementName;
        public int brokenElementID;
        
        public bool isBroken;
    }
    
    [SerializeField] private List<BrokenElement> brokenElements = new List<BrokenElement>();
    
    [SerializeField] private int brokenElementsNum;
    
    public void Start()
    {
        SetRandomBoolForSomeInstances(brokenElementsNum);
    }

    #region Broken Element Generation & Control
    private void SetRandomBoolForSomeInstances(int count)
    {
        List<int> indexes = new List<int>();
        for (int i = 0; i < brokenElements.Count; i++)
        {
            indexes.Add(i);
        }

        Shuffle(indexes);

        for (int i = 0; i < count; i++)
        {
            brokenElements[indexes[i]].isBroken = true;
        }
    }

    private static void Shuffle(IList<int> list)
    {
        int n = list.Count;
        
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            
            (list[k], list[n]) = (list[n], list[k]);
        }
    }
    #endregion
}