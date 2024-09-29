using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Task
{
    [SerializeField] private string _id;
    [SerializeField] private string _text;
    public string Id => _id;

    public string Text => _text;
}

[System.Serializable]
public class DailyTasks
{
    [SerializeField] private List<Task> _tasks;

    public List<Task> Tasks => _tasks;
}


[System.Serializable]
public class Question
{
    [SerializeField] private string _id;
    [SerializeField] private string _text;

    public string Id => _id;
    public string Text => _text;
}


[CreateAssetMenu(fileName ="New Philosopher", menuName ="Data/Philosopher")]
public class SO_Philosopher : ScriptableObject
{
    public const int DAYS_PER_PHILOSOPHER = 7;

    [SerializeField] private string _id;
    [SerializeField] private string _name;
    [TextArea(10,20)]
    [SerializeField] private string _description;
    [SerializeField] private Sprite _carouselImage;
    [SerializeField] private Sprite _descriptionImage;

    [SerializeField] private DailyTasks[] _dailyTasks= new DailyTasks[DAYS_PER_PHILOSOPHER];
    [SerializeField] private List<Question> _weeklyQuestions = new List<Question>();

    // Funzione get
    public string GetName() => _name; // <- return _name
                                      //{
                                      //    return _name;
                                      //}

    // Property con set privato
    // la uso dall'esterno come se fosse una variabile, tipo   Debug.Log(  myObject.Cognome  );
    // oppure    myObject.Cognome = "Brazoff";    
    // sembra una variabile ma in realta' sta richiamando un get o un set
    // (siccome in questo ho scritto "private set;" allora myObject.Cognome = "Brazoff"; mi dara' errore, perche' la property Cognome
    // la posso modificare SOLO dentro la classe, come qualsiasi variabile private)
    //public string Cognome { get; private set; } // <- qui pero' non ho modo di vedere il valore tramite inspector
    // internamente questa dicitura fa una cosa di questo tipo
    //private string _Cognome; // <- creata automaticamente
    //public string Cognome
    //{
    // e specifico per esteso il get e il set
    //get
    //{
    //    return _Cognome;
    //}
    //private set
    //{
    //    _Cognome = value; // <- dove value e' cio' che riceve a destra dell'uguale
    //}
    //}

    // Property get
    public string Id => _id;
    public string Name => _name; // sintesi di una property con solo il get
    // dall'esterno potro' scrivere  myScriptableObject.Name per accedere al valore di _name

    public string Description => _description;
    public Sprite CarouselImage => _carouselImage;
    public Sprite DescriptionImage => _descriptionImage;

    public List<Task> DailyTask(int day) => _dailyTasks[day].Tasks;

    public List<Question> WeeklyQuestions => _weeklyQuestions;
}
