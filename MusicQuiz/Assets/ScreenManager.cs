#nullable enable

using MusicQuiz.Exceptions;
using MusicQuiz.Disk;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    [SerializeField]
    private CategoryScreen? categoryScreen;

    [SerializeField]
    private QuizScreen? questionScreen;

    private CategoryScreen CategoryScreen => categoryScreen != null ? categoryScreen : throw new SerializeFieldNotAssignedException();
    private QuizScreen QuestionScreen => questionScreen != null ? questionScreen : throw new SerializeFieldNotAssignedException();

    // Start is called before the first frame update
    void Start()
    {
        CategoryScreen.CategorySelectedEvent += OnCategorySelected;

        CategoryScreen.Show();
        QuestionScreen.Hide();
    }

    private void OnCategorySelected(object _, CategoryDiskData category)
    {
        CategoryScreen.Hide();
        QuestionScreen.Show(category);
    }
}
