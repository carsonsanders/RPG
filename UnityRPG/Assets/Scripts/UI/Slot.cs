using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [FormerlySerializedAs("_icon")] [SerializeField] private Image _iconImage;
    [SerializeField] private TMP_Text _text;
    public Item Item { get; private set; }
    public bool isEmpty => Item == null;
    public Image IconImage => _iconImage;
    
    private void OnValidate()
    {
        _text = GetComponentInChildren<TMP_Text>();

        int hotkeyNumber = transform.GetSiblingIndex() + 1;
        _text.SetText(hotkeyNumber.ToString());
        gameObject.name = "Slot" + hotkeyNumber;
    }
}