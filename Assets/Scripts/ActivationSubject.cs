using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActivationItems
{
    RedKey, GreenKey
}

////////////////////////////////////////////////////////
// select for your item (key or other object in enum) in order to get its type
////////////////////////////////////////////////////////
public class ActivationSubject : MonoBehaviour
{
    [SerializeField] ActivationItems activationItemsType;
    public ActivationItems ItemType => activationItemsType;
}
