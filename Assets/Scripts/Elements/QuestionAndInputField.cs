using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionAndInputField : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private InputField _inputField;

    private TasksPage _tasksPage;
    private Question _questionData;

    public void Setup(TasksPage page, Question question, string answer)
    {
        _tasksPage = page;
        _questionData = question;

        _text.text = _questionData.Text;
        _inputField.text = answer;

        // Add listener to the input field's onValueChanged event
        _inputField.onValueChanged.AddListener(OnInputFieldValueChanged);
    }

    public void OnInputFieldValueChanged(string content)
    {
        Debug.Log("E' stato modificato il contenuto dell'inputfield con:");
        Debug.Log(content);
        Debug.Log("Dovreste salvarlo sul file!");

        PersistentDataManager.Current.SetQuestionAnswer(_tasksPage.SelectedPhilosopher.Id, _questionData.Id, content);
        PersistentDataManager.Save();
    }
}
