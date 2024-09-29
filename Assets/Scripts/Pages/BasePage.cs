using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PAGE_TYPE
{
    HOME,
    TASKS,
    CHAT
}


public class BasePage : MonoBehaviour
{
    [SerializeField] private PAGE_TYPE _type = PAGE_TYPE.HOME;

    public PAGE_TYPE GetPageType() => _type; // sintesi di { return _type; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

}
