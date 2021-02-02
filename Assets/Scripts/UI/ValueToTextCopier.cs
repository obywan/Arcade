using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueToTextCopier : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI text;
    [SerializeField] private ScriptableObject objectToCopyFrom;

    public void Copy()
    {
        if(objectToCopyFrom is IHaveStringValue)
            text.text = (objectToCopyFrom as IHaveStringValue).GetStringValue;
    }
}
