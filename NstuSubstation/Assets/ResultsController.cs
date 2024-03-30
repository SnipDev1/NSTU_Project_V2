using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class ResultsController : MonoBehaviour
{
    private List<BrokenElementsController.BrokenElement> BrokenElement;

    private void Start() => BrokenElement = BrokenElementsController.Instance.brokenElements;

    public void CheckResults()
    {
        CreateFile();
    }

    private void CreateFile()
    {
        if(File.Exists("Labwork #1.json"))
        {
            File.Delete("Labwork #1.json");
            
            for (int i = 0; i < BrokenElement.Count(); i++)
            {
                File.AppendAllText("Labwork #1.json", JsonUtility.ToJson(BrokenElement[i], true));
            }
        }
        else
        {
            for (int i = 0; i < BrokenElement.Count(); i++)
            {
                File.AppendAllText("Labwork #1.json", JsonUtility.ToJson(BrokenElement[i], true));
            }
        }
    }
}
