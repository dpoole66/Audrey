using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]

public class TransformRef {

    public TransformVariable Target;

    public Transform T_Target {

        get { return Target.T_Target; }

        set {

            Target.T_Target = T_Target;

        }
    }
}
