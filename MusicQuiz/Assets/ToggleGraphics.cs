using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleGraphics : MonoBehaviour
{
    [SerializeField] private Image backgroundImage = default;
    [SerializeField] private Image selectedImage = default;

    public Image BackgroundImage => backgroundImage; 
    public Image SelectedImage => selectedImage; 
}
