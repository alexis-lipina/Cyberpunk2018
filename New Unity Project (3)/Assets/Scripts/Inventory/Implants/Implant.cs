using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Implant : MonoBehaviour
{
    protected List<Type> incompatibleTypes;

    public List<Type> IncompatibleTypes { get { return incompatibleTypes; } set { incompatibleTypes = value; } }
}
