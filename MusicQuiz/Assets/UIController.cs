using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private Sprite questionSprite;

    private Button checkButton;
    private Button backButton;
    private Button tippButton;
    private TextField inputField;
    private Label tipp1;
    private Label tipp2;
    private Label tipp3;
    private Label categoryLbl;
    private Label instructionLabel;
    private Label logo;
    private VisualElement questionContainer;
    private VisualElement mainContainer;
    private List<Button> questionButtons = new List<Button>();
    private ScrollView questionScroller;

    public event EventHandler<string> CheckAnswerEvent;
    public event EventHandler TippEvent;



    // Start is called before the first frame update
    void Awake()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        checkButton = root.Q<Button>("checkButton");
        checkButton.clicked += OnCheckButtonPressed;
        backButton = root.Q<Button>("backButton");
        backButton.clicked += OnBackButtonPressed;
        tippButton = root.Q<Button>("tippButton");
        tippButton.clicked += OnTippButtonPressed;

        inputField = root.Q<TextField>("answer");

        logo = root.Q<Label>("logo");
        tipp1 = root.Q<Label>("tipp1");
        tipp2 = root.Q<Label>("tipp2");
        tipp3 = root.Q<Label>("tipp3");
        instructionLabel = root.Q<Label>("instruction");

        mainContainer = root.Q<VisualElement>("main");
        questionContainer = root.Q<VisualElement>("questions");
        categoryLbl = root.Q<Label>("categoryLbl");

        questionScroller = root.Q<ScrollView>("questionScroller");
        questionScroller.horizontalScroller.style.height = 0;
        questionScroller.horizontalScroller.visible = false;
    }

    void OnCheckButtonPressed()
    {
        var givenAnswer = inputField.text;
        CheckAnswerEvent?.Invoke(this, givenAnswer);

    }

    void OnBackButtonPressed()
    {
        SceneManager.LoadScene(0);
    }

    void OnTippButtonPressed()
    {
        Debug.Log("OnTippButtonPressed");
        TippEvent?.Invoke(this, EventArgs.Empty);
    }

    public Button CreateQuestion()
    {
        Button questionButton = new Button();
        questionButton.style.backgroundImage = new StyleBackground(questionSprite);
        questionButton.style.width = 60;

        questionButton.style.backgroundColor = new StyleColor(new Color(0, 0, 0, 0));
        questionButton.style.unityBackgroundImageTintColor = new Color(.2f, .2f, .2f);
        questionButton.style.color = Color.black;
        questionButton.style.fontSize = 22;
        questionButton.style.unityFontStyleAndWeight = new StyleEnum<FontStyle>(FontStyle.Bold);

        questionButton.style.borderBottomWidth = 0;
        questionButton.style.borderTopWidth = 0;
        questionButton.style.borderLeftWidth = 0;
        questionButton.style.borderRightWidth = 0;
        questionButton.style.marginBottom = 0;
        questionButton.style.marginTop = 0;
        questionButton.style.marginLeft = 0;
        questionButton.style.marginRight = 0;
        questionButton.style.paddingBottom = 0;
        questionButton.style.paddingTop = 0;
        questionButton.style.paddingLeft = 0;
        questionButton.style.paddingRight = 0;

        //questionButton.text = "" + (questionComp.question.id + 1);
        questionContainer.Add(questionButton);
        return questionButton;
    }

    public void ChangeColorScheme(Color color)
    {
        categoryLbl.style.color = color;
        mainContainer.style.unityBackgroundImageTintColor = color;
        logo.style.color = color;
        backButton.style.color = color;
        checkButton.style.color = color;
    }

    public void ChangeCategoryText(string category)
    {
        if (categoryLbl == null)
        {
            Debug.LogError("CategoryLabel is null?!");
            return;
        }
        categoryLbl.text = category;
    }

    public void ChangeSelectedAnswer(string answer)
    {
        inputField.SetValueWithoutNotify(answer);
    }
    public void ChangeInstruction(string instruction)
    {
        instructionLabel.text = instruction;
    }

    public void ChangeTipps(int tippLevel, string tipp)
    {
        Debug.Log("Show Tipp: " + tippLevel);
        switch (tippLevel)
        {
            case 0:
                tipp1.text = "\nTipp " + (tippLevel + 1) + ": " + tipp;
                break;
            case 1:
                tipp2.text = "\nTipp " + (tippLevel + 1) + ": " + tipp;
                break;
            case 2:
                tipp3.text = "\nTipp " + (tippLevel + 1) + ": " + tipp;
                break;
            default:
                Debug.LogError("Asked for unexpected Tipp!");
                break;
        }
    }

    public void ClearTipps()
    {
        tipp1.text = string.Empty;
        tipp2.text = string.Empty;
        tipp3.text = string.Empty;
    }
    public void ChangeQuestionBackground(int id, int distance)
    {
        var questionButton = questionContainer[id];
        if (distance == 0)
        {
            //Debug.Log("That's right!");
            questionButton.style.unityBackgroundImageTintColor = Color.green;
        }
        else if (distance <= 3)
        {
            //Debug.Log("Close...");
            questionButton.style.unityBackgroundImageTintColor = Color.yellow;
        }
        else
        {
            //Debug.Log("Nope!");
            questionButton.style.unityBackgroundImageTintColor = Color.red;
        }
    }
}
