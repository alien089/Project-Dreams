using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToolTipManager : MonoBehaviour
{
    // internal variable to assure only single instance of manager
    public static ToolTipManager _xInstance;

    // public variable related to text
    public TextMeshProUGUI xTextComponent;
    private void Awake()
    {
        //Check only one istance is active
        if (_xInstance != null && _xInstance != this )
        {
            Destroy(this.gameObject);
        }
        else
        {
            _xInstance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Input.mousePosition;
    }

    //function to show tooltip
    public void SetAndShowToolTip(string strMessage)
    {
        gameObject.SetActive(true);
        xTextComponent.text = strMessage;
    }

    //function to hide tooltip
    public void HideToolTip()
    {
        gameObject.SetActive(false);
        xTextComponent.text = string.Empty;
    }
}
