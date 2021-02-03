using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoleSelection : MonoBehaviour
{
    [SerializeField] private Texture soloImage;
    [SerializeField] private Texture rockerBoyImage;
    [SerializeField] private Texture netrunnerImage;

    [SerializeField] private Button nextButton;
    [SerializeField] private Button previousButton;// Start is called before the first frame update

    private RawImage _image;
    private Texture _currentImage;
    private TMPro.TextMeshProUGUI _roleText;

    void Start()
    {
        _image =  GetComponentInChildren<RawImage>();


        _roleText = GetComponentInChildren<TMPro.TextMeshProUGUI>();

        _currentImage = soloImage;

        nextButton.onClick.AddListener(()=> nextRole());
        previousButton.onClick.AddListener(() => previousRole());
        
    }

    private void previousRole()
    {
        if (_currentImage == soloImage)
        {
            _currentImage = netrunnerImage;
            _image.texture = netrunnerImage;
            _roleText.text = "NETRUNNER";
        }

        else if (_currentImage == rockerBoyImage)
        {
            _currentImage = soloImage;
            _image.texture = soloImage;
            _roleText.text = "SOLO";
        }

        else if (_currentImage == netrunnerImage)
        {
            _currentImage = rockerBoyImage;
            _image.texture = rockerBoyImage;
            _roleText.text = "ROCKERBOY";
        }
        Debug.Log($"CurrentRole: {_currentImage.name}");
    }
    
    private void nextRole()
    {
        if (_currentImage == soloImage)
        {
            _currentImage = rockerBoyImage;
            _image.texture = rockerBoyImage;
            _roleText.text = "ROCKERBOY";
        }

        else if (_currentImage == rockerBoyImage)
        {
            _currentImage = netrunnerImage;
            _image.texture = netrunnerImage;
            _roleText.text = "NETRUNNER";
        }

        else if (_currentImage == netrunnerImage)
        {
            _currentImage = soloImage;
            _image.texture = soloImage;
            _roleText.text = "SOLO";
        }
        Debug.Log($"CurrentRole: {_currentImage.name}");
    }
}