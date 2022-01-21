using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
A example scriptableObject for the Resource Index.
*/

[System.Serializable]
[CreateAssetMenu(fileName = "TestScriptable", menuName = "ScriptableObjects/Example Scriptable", order = 1)]
public class ExampleResourceSO : ScriptableObject
{
    [SerializeField] 
    public string data;
}
