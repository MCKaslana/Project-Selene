using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ShapeKeyObject", menuName = "Scriptable Objects/ShapeKeyObject")]
public class ShapeKeyObject : ScriptableObject
{
    public ShapeKey keyValue;
    public GameObject keyObject;

    [NonSerialized] public GameObject runTimeObject;
}
