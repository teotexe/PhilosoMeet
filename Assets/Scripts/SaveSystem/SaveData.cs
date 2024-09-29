using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

[System.Serializable]
public class TaskSaveData
{
    public string id;
    public bool completed;
}


[System.Serializable]
public class QuestionSaveData
{
    public string id;
    public string answer;
}

[System.Serializable]
public class DailyTasksSaveData
{
    public List<TaskSaveData> tasks = new List<TaskSaveData>();

    public TaskSaveData FindTask(string taskId)
    {
        foreach (TaskSaveData t in tasks)
        {
            if (t.id.Equals(taskId))
            {
                return t;
            }
        }

        Debug.LogError("Task id " + taskId + " not found in tasks.");
        return null;
    }


    public bool IsTaskCompleted(string taskId)
    {
        TaskSaveData foundTask = FindTask(taskId);
        if (foundTask != null)
        {
            return foundTask.completed;
        }
        return false;
    }

    public void SetTaskCompleted(string taskId, bool completed)
    {
        TaskSaveData foundTask = FindTask(taskId);
        if (foundTask != null)
        {
            foundTask.completed = completed;
        }
    }
}

[System.Serializable]
public class PhilosopherSaveData
{
    public string id;
    public string name;
    public List<DailyTasksSaveData> dailyTasks = new List<DailyTasksSaveData>();
    public List<QuestionSaveData> questions = new List<QuestionSaveData>();


    public DailyTasksSaveData FindDailyTasksSaveData(int dayIndex)
    {
        if (dayIndex >= dailyTasks.Count)
        {
            Debug.LogError("Tried to read dayIndex = " + dayIndex + " while dailyTasks count was " + dailyTasks.Count);
            return null;
        }

        return dailyTasks[dayIndex];
    }

    public QuestionSaveData FindQuestion(string questionId)
    {
        foreach (QuestionSaveData q in questions)
        {
            if (q.id.Equals(questionId))
            {
                return q;
            }
        }

        Debug.LogError("Question id " + questionId + " not found in questions.");
        return null;
    }

    public bool IsTaskCompleted(int dayIndex, string taskId)
    {
        DailyTasksSaveData dailyTasksSaveData = FindDailyTasksSaveData(dayIndex);
        if (dailyTasksSaveData != null)
        {
            return dailyTasks[dayIndex].IsTaskCompleted(taskId);
        }
        return false;
    }

    public void SetTaskCompleted(int dayIndex, string taskId, bool completed)
    {
        DailyTasksSaveData dailyTasksSaveData = FindDailyTasksSaveData(dayIndex);
        if (dailyTasksSaveData != null)
        {
            dailyTasks[dayIndex].SetTaskCompleted(taskId, completed);
        }
    }

    public string GetQuestionAnswer(string questionId)
    {
        QuestionSaveData question = FindQuestion(questionId);
        if (question != null)
        {
            return question.answer;
        }
        return null;
    }

    public void SetQuestionAnswer(string questionId, string answer)
    {
        QuestionSaveData question = FindQuestion(questionId);
        if (question != null)
        {
            question.answer = answer;
        }
    }
}

[System.Serializable]
public class SaveData
{
    public List<PhilosopherSaveData> philosophers = new List<PhilosopherSaveData>();

    public PhilosopherSaveData FindPhilosopherSaveData(string philosopherId)
    {
        foreach (PhilosopherSaveData p in philosophers)
        {
            if (p.id.Equals(philosopherId))
            {
                return p;
            }
        }

        Debug.Log("No philosopher found for id " + philosopherId);
        return null;
    }

    public int GetFirstUncompletedDay(string philosopherId)
    {
        PhilosopherSaveData p = FindPhilosopherSaveData(philosopherId);
        if (p != null)
        {
            for(int day = 0; day < p.dailyTasks.Count; day++)
            {
                if (!AreAllTasksOfDayCompleted(philosopherId, day))
                {
                    return day; // <- giorno non completo
                }
            }
            return p.dailyTasks.Count - 1; // <- tutti completi, restituiamo l'ultimo indice
        }
        return 0; // <- filosofo non trovato, restituiamo il primo
    }

    public bool AreAllTasksOfDayCompleted(string philosopherId, int dayIndex)
    {
        PhilosopherSaveData p = FindPhilosopherSaveData(philosopherId);
        if (p != null)
        {
            DailyTasksSaveData d = p.FindDailyTasksSaveData(dayIndex);
            foreach(TaskSaveData t in d.tasks)
            {
                if (!t.completed)
                {
                    return false; // <- almeno uno non e' completo
                }
            }
            return true; // <- sono tutti completi
        }
        return false; // <- manca il salvataggio
    }


    public bool IsTaskCompleted(string philosopherId, int dayIndex, string taskId)
    {
        PhilosopherSaveData p = FindPhilosopherSaveData(philosopherId);
        if (p != null)
        {
            return p.IsTaskCompleted(dayIndex, taskId);
        }
        return false;
    }

    public void SetTaskCompleted(string philosopherId, int dayIndex, string taskId, bool completed)
    {
        PhilosopherSaveData p = FindPhilosopherSaveData(philosopherId);
        if (p != null)
        {
            p.SetTaskCompleted(dayIndex, taskId, completed);
        }
    }

    public string GetQuestionAnswer(string philosopherId, string questionId)
    {
        PhilosopherSaveData p = FindPhilosopherSaveData(philosopherId);
        if (p != null)
        {
            return p.GetQuestionAnswer(questionId);
        }
        return null;
    }

    public void SetQuestionAnswer(string philosopherId, string questionId, string answer)
    {
        PhilosopherSaveData p = FindPhilosopherSaveData(philosopherId);
        if (p != null)
        {
            p.SetQuestionAnswer(questionId, answer);
        }
    }


    public static SaveData GenerateBlankSaveData(SO_Philosopher[] philosophersData)
    {
        SaveData data = new SaveData();

        foreach (SO_Philosopher p in philosophersData)
        {
            PhilosopherSaveData pSave = new PhilosopherSaveData
            {
                id = p.Id,
                name = p.Name
            };

            // Create the list of incomplete tasks
            for (int d = 0; d < SO_Philosopher.DAYS_PER_PHILOSOPHER; d++)
            {
                DailyTasksSaveData dailyTasks = new DailyTasksSaveData();
                List<Task> tasksData = p.DailyTask(d);

                for (int t = 0; t < tasksData.Count; t++)
                {
                    TaskSaveData tSave = new TaskSaveData
                    {
                        id = tasksData[t].Id,
                        completed = false
                    };
                    dailyTasks.tasks.Add(tSave);
                }

                pSave.dailyTasks.Add(dailyTasks);
            }

            // Create the list of unanswered questions
            foreach (Question q in p.WeeklyQuestions)
            {
                QuestionSaveData qSave = new QuestionSaveData
                {
                    id = q.Id,
                    answer = ""
                };
                pSave.questions.Add(qSave);
            }

            data.philosophers.Add(pSave);
        }

        return data;
    }
}
