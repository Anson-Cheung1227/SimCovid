using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Core Game", menuName = "Scriptable Objects/CoreGameSO")]
public class CoreGameSO : ScriptableObject 
{
    [SerializeField] public List<SceneEnum> SceneList;
}