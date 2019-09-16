using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Question Bank", menuName = "Data/Questions")]
public class QuestionBank : ScriptableObject
{

    private void OnDisable()
    {
        foreach (var quest in questions)
        {
            quest.wasUsed = false;
        }
    }

    [SerializeField] public List<questionBase> questions = new List<questionBase>();

    public questionBase GetNewQuestion()
    {
        var index = Random.Range(0, questions.Count);
        int counter = 0;
        while (questions[index].wasUsed && counter <= questions.Count)
        {
            counter += 1;
            index = Random.Range(0, questions.Count);
        }
        questions[index].wasUsed = true;
        return questions[index];
    }

}

[System.Serializable]
public class questionBase
{
    [TextArea]
    public string text;

    public bool trueOrFalse;

    [Header("dont change this")]
    public bool wasUsed;


}
