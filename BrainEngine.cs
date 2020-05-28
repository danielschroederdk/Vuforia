
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using Vuforia;


public class BrainEngine : MonoBehaviour
{
    private int a, b, c;
    public TextMeshProUGUI answerCondition;
    public TextMeshProUGUI equation;
    public TextMeshProUGUI score;
    private string answerInput;
    public GameObject inputField;
    public int numberOfGames = 5;
    private int _countOfGames = 0;
    private int _answeredCorrect = 0;
    private bool answered = false;
    private bool answeredCorrect = false;

    private CardProperty _cardProperty;


    // Start is called before the first frame update
    void Start()
    {
        numberOfGames = PlayerPrefs.GetInt("numberOfGames") > 0 ? PlayerPrefs.GetInt("numberOfGames") : 5;

        //Initial equation
        newEquation();
        StartCoroutine(incrementCountOfGames());
        StartCoroutine(goToStartScreen());
        StartCoroutine(correctResult());

    }

    IEnumerator correctResult()
    {
        answerInput = inputField.GetComponent<InputField>().text;
        while (true)
        {
            yield return new WaitUntil(() => answerInput.Length != 0);
            if (int.Parse(answerInput) == b)
            {
                TrackerManager.Instance.GetTracker<ObjectTracker>().Stop();
                answered = true;
                answeredCorrect = true;
            }
            else
            {
                TrackerManager.Instance.GetTracker<ObjectTracker>().Stop();
                answered = true;
            }
            setConditionText();
            yield return StartCoroutine(Wait());
        }
    }
    
    
    // Update is called once per frame
        void Update()
        {
            answerInput = inputField.GetComponent<InputField>().text;
        }

        void setConditionText()
        {
            if (answeredCorrect)
            {
                answerCondition.text = "Correct! The answer is: " + b;
                answerCondition.color = Color.green;
            }
            else
            {
                answerCondition.text = "Wrong.. On to the next one!";
                answerCondition.color = Color.red;

            }
        }
        IEnumerator incrementCountOfGames()
        {
            while (true)
            {
                if (answered)
                {
                    if (answeredCorrect)
                    {
                        _answeredCorrect++;
                    }
                    _countOfGames++;
                    answered = false;
                    answeredCorrect = false;
                }
                score.text = "Score: (" + _answeredCorrect + "/" + numberOfGames + ")";
                yield return new WaitForSecondsRealtime(0.2f);
            }
        }
        
        void newEquation()
        {
            StartCoroutine(Scramble());
        }


        IEnumerator Scramble()
        {
            int i = 0;
            while(i < 50)
            {
                yield return new WaitForSeconds(0.005f);
                RandomOperator();
                i++;
            }
            StopCoroutine(Scramble());
        }

        public void RandomOperator()
        {
            var randomOperator = Random.Range(1, 5);
            a = Random.Range(1, 11);
            b = Random.Range(1, 11);

            switch (randomOperator)
            {
                case 1:
                    c = a + b;
                    equation.SetText("{0} + ? = {1}",a,c);
                    break;
                case 2:
                    c = a - b;
                    equation.SetText("{0} - ? = {1}",a,c);
                    break;
                //case 3:
                    c = a / b;
                    equation.SetText("{0} / ? = {1}", a, c);
                    break;
                case 4:
                    c = a * b;
                    equation.SetText("{0} * ? = {1}",a,c);
                    break;
            }
        }
        
        IEnumerator Wait()
        {
            answerInput = "";
            inputField.GetComponent<InputField>().text = "";
            yield return new WaitForSeconds(2);
            newEquation();
            TrackerManager.Instance.GetTracker<ObjectTracker>().Start();
        }

        IEnumerator goToStartScreen()
        { 
            yield return new WaitUntil(() => _countOfGames >= numberOfGames);
            answerCondition.SetText("Game done! Score: " + _answeredCorrect + "/" + numberOfGames);
            answerCondition.color = Color.green;
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene(0);
        }
    }
