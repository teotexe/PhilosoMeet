using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetPhiloInfo : MonoBehaviour
{
    [SerializeField] private GameObject alertPanel; // Array of alert panels corresponding to each image

    // Abbiamo bisogno dei riferimenti alle varie componenti grafiche
    [SerializeField] private Text _philoName;
    [SerializeField] private Text _philoDescription;
    [SerializeField] private Image _philoDescriptionImage;

    public void Setup(SO_Philosopher philosopherData)
    {
        _philoName.text = philosopherData.Name;
        _philoDescription.text = philosopherData.Description;
        _philoDescriptionImage.sprite = philosopherData.DescriptionImage;

        alertPanel.SetActive(true);
    }


    public void CloseAlert()
    {
        // Hide the corresponding alert panel
        alertPanel.SetActive(false);
    }
}
