using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.Events;
using UnityEngine.EventSystems;
public class TextPressed : MonoBehaviour//, ISelectHandler
{
    // Start is called before the first frame update

    public Text m_MyText;
    public Button button;
 

    public void OnSelect(BaseEventData eventData)
    {
        // Do something.
        Debug.Log("<color=red>Event:</color> Completed selection.");
        m_MyText.color = Color.yellow;

    }
}

