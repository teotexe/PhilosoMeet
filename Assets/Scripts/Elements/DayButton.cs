using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DayButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Text _text;

    private int _dayIndex;
    private TasksPage _tasksPage;

    public void Setup(TasksPage tasksPage, int dayIndex)
    {
        _tasksPage = tasksPage;
        _dayIndex = dayIndex;

        _text.text = "Day\n" + (_dayIndex+1);
    }

    public void OnClick()
    {
        _tasksPage.SelectDay(_dayIndex);
    }

    public void SetInteractable(bool interactable)
    {
        _button.interactable = interactable;
    }
}
