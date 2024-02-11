using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Level", menuName = "Scriptable Objects / Levels", order = 0)] 
public class LevelSO : ScriptableObject
{
    public Road[] roads;

}
