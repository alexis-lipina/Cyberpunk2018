using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Implant : MonoBehaviour
{
    //[SerializeField] protected List<Type> incompatibleTypes = new List<Type>();

    public abstract List<Type> IncompatibleTypes { get; }
}
