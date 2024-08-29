using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTip : MonoBehaviour
{
    //message variable, can twick via inspector
    public string strMessage;

    // these are default unitiy functions
    private void OnMouseEnter()
    {
        ToolTipManager._xInstance.SetAndShowToolTip(strMessage);
    }

    private void OnMouseExit()
    {
        ToolTipManager._xInstance.HideToolTip();
    }

}
