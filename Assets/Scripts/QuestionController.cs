using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionController : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public Button buttonAnswer1;
    public Button buttonAnswer2;
    public Button buttonAnswer3;
    public Button buttonAnswer4;
    private int randomNumber;

    // вопросы
    private string[] questionsLevel1 = {
        "Как называется наука, изучающая историческое прошлое человечества по вещественным источникам?",
        "Какое занятие первобытных людей привело к возникновению земледелие?",
        "Город Якутск основал?",
        "Ясак – это:" };

    // варианты ответов
    private string[,] answersLevel1 = new string[,] {
        { "вариант1", "вариант2", "вариант3", "вариант4" },
        { "вариант1", "вариант2", "вариант3", "вариант4" },
        { "вариант1", "вариант2", "вариант3", "вариант4" },
        { "вариант1", "вариант2", "вариант3", "вариант4" },
    };

    //номера правильных ответов
    private int[] correct_answer_level1 = { 2, 3, 3, 1 };
    
    void OnEnable()
    {
        randomNumber = Random.Range(0, questionsLevel1.Length);
        // set content to UI
        questionText.text = questionsLevel1[randomNumber];
        buttonAnswer1.GetComponentInChildren<TextMeshProUGUI>().text = randomNumber + 1 + " " + answersLevel1[randomNumber, 0];
        buttonAnswer2.GetComponentInChildren<TextMeshProUGUI>().text = randomNumber + 1 + " " + answersLevel1[randomNumber, 1];
        buttonAnswer3.GetComponentInChildren<TextMeshProUGUI>().text = randomNumber + 1 + " " + answersLevel1[randomNumber, 2];
        buttonAnswer4.GetComponentInChildren<TextMeshProUGUI>().text = randomNumber + 1 + " " + answersLevel1[randomNumber, 3];
    }

    public void CheckAnswer(int numberOfAnswer)
    {
        if (numberOfAnswer == correct_answer_level1[randomNumber])
        {
            GameManager.Instance.diamondsCount += 1;
        }
        else
        {
            Debug.Log("Bad!");
        }
        GameManager.Instance.GameResume();
    }
}
