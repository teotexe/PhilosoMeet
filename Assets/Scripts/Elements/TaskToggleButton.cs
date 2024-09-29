using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskToggleButton : MonoBehaviour
{
    [SerializeField] private Toggle _toggle;
    [SerializeField] private Text _text;

    private TasksPage _tasksPage;
    private Task _taskData;

    private bool _shouldSaveOnToggleStateChange = false;

    public void Setup(TasksPage page, Task task, bool value)
    {
        _tasksPage = page;
        _taskData = task;

        _text.text = _taskData.Text;

        _shouldSaveOnToggleStateChange = false;

        _toggle.isOn = value;

        _shouldSaveOnToggleStateChange = true;
    }

    public void OnStateChanged(bool isOn)
    {
        if (!_shouldSaveOnToggleStateChange) { return; }

        //PersistentDataManager.Current. ... devo trovare il task giusto e settarlo al valore isOn

        // 1) devo sapere quale filosofo sto visualizzando (questo devo riceverlo da TasksPage)
        // 2) devo sapere quale giorno sto visualizzando (questo devo riceverlo da TasksPage)
        // 3) devo sapere l'id del task (questo lo prendo da _taskData)

        PersistentDataManager.Current.SetTaskCompleted(_tasksPage.SelectedPhilosopher.Id, _tasksPage.DayIndex, _taskData.Id, isOn);
        PersistentDataManager.Save();
    }
}
