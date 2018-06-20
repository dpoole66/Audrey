using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]


public class BoolReference {

    public BoolVariable Variable;

    public bool Value{

    get{ return Variable.Value; }
        set{

            if (Variable != null)
                Variable.Value = true;

            else Variable.Value = false;
        }
    }


}
