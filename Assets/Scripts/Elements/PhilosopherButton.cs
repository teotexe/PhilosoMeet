using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhilosopherButton : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private SO_Philosopher _data;

    private GetPhiloInfo _philoInfoPage;

    public void Setup(SO_Philosopher data, GetPhiloInfo philoInfoPage)
    {
        _data = data;
        _philoInfoPage = philoInfoPage;

        _image.sprite = data.CarouselImage;
    }

    public void OnButtonClicked()
    {
        _philoInfoPage.Setup(_data);
    }
}
