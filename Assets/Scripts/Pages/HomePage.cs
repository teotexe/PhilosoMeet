using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomePage : MonoBehaviour, IAssociatedTextHandler
{
    [SerializeField] private SO_Philosopher[] _philosophers;

    [SerializeField] private PhilosopherButton _philosopherButtonPrefab;

    [SerializeField] private RectTransform _philosopherButtonsParent;

    [SerializeField] private Text _philosopherName;
    [SerializeField] private UIContentCarousel _contentCarousel;

    private GetPhiloInfo _philoInfoPage; // <- essendo esterno a HomePage NON voglio collegarlo tramite Inspector

    public void UpdateAssociatedText(int index)
    {
        // dobbiamo aggiornare la scritta in cima
        _philosopherName.text = _philosophers[index].Name;
    }

    void Awake() // tutte le Awake() vengono richiamate PRIMA di tutti gli Start()
    {
        LoadSaveData();

        _contentCarousel.SetAssociatedTextHandler(this); // <- QUESTA componente e' l'handler che deve mostrare il nome del filosofo

        _philoInfoPage = FindFirstObjectByType<GetPhiloInfo>( FindObjectsInactive.Include );

        //for(int i = 0; i < _philosophers.Length; i++)
        foreach( SO_Philosopher data in _philosophers )
        {
            PhilosopherButton clone = Instantiate<PhilosopherButton>(_philosopherButtonPrefab, _philosopherButtonsParent);
            clone.Setup(data, _philoInfoPage);
        }
    }

    void OnEnable()
    {
        MainViewCTRL.LastSelectedPhilosopher = _philosophers[_contentCarousel.currentIndex];
    }

    void OnDisable()
    {
        MainViewCTRL.LastSelectedPhilosopher = _philosophers[_contentCarousel.currentIndex];
    }

    void LoadSaveData()
    {
        if (PersistentDataManager.Current != null)
        {
            return; // abbiamo gia' un SaveData attivo
        }

        if (PersistentDataManager.DoesSaveFileExist())
        {
            PersistentDataManager.LoadSaveData();
        }
        else
        {
            SaveData saveData = SaveData.GenerateBlankSaveData(_philosophers);
            PersistentDataManager.Save(saveData);
        }
    }

}
