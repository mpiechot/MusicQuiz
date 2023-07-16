using System;
using UnityEngine.UIElements;

public class UIQuestion : VisualElement
{
    public QuestionComponent questionComp { get; private set; }
    private Button questionButton;

    public EventHandler playQuestionEvent;

    public UIQuestion(QuestionComponent questionComp)
    {
        
        this.questionComp = questionComp;
    }

    public void Initialize()
    {
        questionButton = this.Q<Button>("question");
        questionButton.clicked += OnQuestionClicked;
        //questionButton.text = "" + (questionComp.question.id + 1);
    }
    
    
    private void OnQuestionClicked()
    {
        playQuestionEvent.Invoke(this, EventArgs.Empty);
    }

    
}