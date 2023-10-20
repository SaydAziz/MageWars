using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetController : MonoBehaviour
{
    bool isHolding = false;
    [SerializeField] GameObject rHand;

    public void ToggleHandFlip()
    {
        float rotVal = isHolding ? 0 : 180;
        rHand.transform.Rotate(0, 0, rotVal);
    }
}
