using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TasksPage : MonoBehaviour
{
    [SerializeField] private Transform _daysButtonParent;
    [SerializeField] private DayButton _dayButtonPrefab;
    [SerializeField] private Transform _tasksParent;
    [SerializeField] private TaskToggleButton _taskButtonPrefab;
    [SerializeField] private Transform _QuestionAndInputFieldParent;
    [SerializeField] private QuestionAndInputField _QuestionAndInputFieldPrefab;

    public int DayIndex { get; private set; }
    public SO_Philosopher SelectedPhilosopher { get; private set; }

    [SerializeField] private List<DayButton> _dayButtons = new List<DayButton>();
    [SerializeField] private List<TaskToggleButton> _tasksButtons = new List<TaskToggleButton>();
    [SerializeField] private List<QuestionAndInputField> _QuestionAndInputFields = new List<QuestionAndInputField>();

    private void OnEnable()
    {
        Setup(MainViewCTRL.LastSelectedPhilosopher);
    }

    public void Setup(SO_Philosopher philosopher)
    {
        SelectedPhilosopher = philosopher;

        // Setup Day Buttons
        int i = 0;
        for (; i < SO_Philosopher.DAYS_PER_PHILOSOPHER && i < _dayButtons.Count; i++)
        {
            _dayButtons[i].Setup(this, i);
            _dayButtons[i].gameObject.SetActive(true);
        }
        for (; i < SO_Philosopher.DAYS_PER_PHILOSOPHER; i++)
        {
            DayButton dayButton = Instantiate(_dayButtonPrefab, _daysButtonParent);
            dayButton.Setup(this, i);
            _dayButtons.Add(dayButton);
        }
        for (; i < _dayButtons.Count; i++)
        {
            _dayButtons[i].gameObject.SetActive(false);
        }

        int dayToOpen = PersistentDataManager.Current.GetFirstUncompletedDay(SelectedPhilosopher.Id);
        SelectDay(dayToOpen);

        // Populate QuestionAndInputField objects with weekly questions
        List<Question> weeklyQuestions = SelectedPhilosopher.WeeklyQuestions; // Get questions for the entire week

        i = 0;
        for (; i < weeklyQuestions.Count && i < _QuestionAndInputFields.Count; i++)
        {
            string answer = PersistentDataManager.Current.GetQuestionAnswer(SelectedPhilosopher.Id, weeklyQuestions[i].Id);

            _QuestionAndInputFields[i].Setup(this, weeklyQuestions[i], answer);
            _QuestionAndInputFields[i].gameObject.SetActive(true);
        }
        for (; i < weeklyQuestions.Count; i++)
        {
            string answer = PersistentDataManager.Current.GetQuestionAnswer(SelectedPhilosopher.Id, weeklyQuestions[i].Id);

            QuestionAndInputField questionAndInputField = Instantiate(_QuestionAndInputFieldPrefab, _QuestionAndInputFieldParent);
            questionAndInputField.Setup(this, weeklyQuestions[i], answer);
            _QuestionAndInputFields.Add(questionAndInputField);
        }
        for (; i < _QuestionAndInputFields.Count; i++)
        {
            _QuestionAndInputFields[i].gameObject.SetActive(false);
        }
    }

    public void SelectDay(int dayIndex)
    {
        if (DayIndex >= 0 && DayIndex < _dayButtons.Count)
        {
            _dayButtons[DayIndex].SetInteractable(true);
        }

        DayIndex = dayIndex;

        _dayButtons[DayIndex].SetInteractable(false);

        List<Task> dailyTasks = SelectedPhilosopher.DailyTask(DayIndex);

        int i = 0;
        for (; i < dailyTasks.Count && i < _tasksButtons.Count; i++)
        {
            bool isCompleted = PersistentDataManager.Current.IsTaskCompleted(SelectedPhilosopher.Id, DayIndex, dailyTasks[i].Id);

            _tasksButtons[i].Setup(this, dailyTasks[i], isCompleted);
            _tasksButtons[i].gameObject.SetActive(true);
        }
        for (; i < dailyTasks.Count; i++)
        {
            bool isCompleted = PersistentDataManager.Current.IsTaskCompleted(SelectedPhilosopher.Id, DayIndex, dailyTasks[i].Id);

            TaskToggleButton taskButton = Instantiate(_taskButtonPrefab, _tasksParent);
            taskButton.Setup(this, dailyTasks[i], isCompleted);
            _tasksButtons.Add(taskButton);
        }
        for (; i < _tasksButtons.Count; i++)
        {
            _tasksButtons[i].gameObject.SetActive(false);
        }
    }
}
