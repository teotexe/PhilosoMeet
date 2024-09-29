using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainViewCTRL : MonoBehaviour
{
    public static SO_Philosopher LastSelectedPhilosopher;

    [SerializeField] private GameObject _backgroundImage;
    [SerializeField] private AspectRatioFitter _aspectRatioFitter;

    [SerializeField] private BasePage[] _pages;

    private BasePage _currentPage;
    

    void Start()
    {
        float _screenAspectRatio = (float)Screen.width / Screen.height; // in 16:9 sarebbe 1.7777 in 9:16 sarebbe 0.5625
        _backgroundImage.SetActive(_screenAspectRatio >= _aspectRatioFitter.aspectRatio );
        OpenPage(PAGE_TYPE.HOME);
    }

    public void OpenPage(PAGE_TYPE pageType)
    {
        if (_currentPage != null)
        {
            if (_currentPage.GetPageType() == pageType)
            {
                return;
            }
            else
            {
                _currentPage.gameObject.SetActive(false);
            }
        }

        for(int i = 0; i < _pages.Length; i++)
        {
            if ( _pages[i].GetPageType() == pageType )
            {
                _pages[i].gameObject.SetActive(true);
                _currentPage = _pages[i];
                return;
            }
        }
    }

}
