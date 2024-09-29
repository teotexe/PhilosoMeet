using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPageButton : MonoBehaviour
{
    [SerializeField] private PAGE_TYPE _pageToOpen = PAGE_TYPE.HOME;

    private MainViewCTRL _mainViewCTRL;

    private void Awake()
    {
        _mainViewCTRL = FindFirstObjectByType<MainViewCTRL>(); // <- cerca in automatico nella scena
    }

    public void OnClick()
    {
        _mainViewCTRL.OpenPage(_pageToOpen);
    }
    
}
