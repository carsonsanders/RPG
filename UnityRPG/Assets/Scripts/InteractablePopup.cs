using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractablePopup : MonoBehaviour
{
    [SerializeField] public GameObject MessagePanel;

    [SerializeField] private Text message;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        var player = FindObjectOfType<Player>();
        while (player == null)
        {
            yield return null;
            player = FindObjectOfType<Player>();
        }
        player.popupBind(this);
    }

    public void OpenMessagePanel(string text)
    {
        message.text = "Press F to " + text;
        MessagePanel.SetActive(true);
    }

    public void CloseMessagePanel()
    {
        MessagePanel.SetActive(false);
    }

    
}
