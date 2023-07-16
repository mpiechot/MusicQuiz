using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CategoryComponent : MonoBehaviour
{
    [SerializeField] private Image categoryImage;
    [SerializeField] private TextMeshProUGUI categoryName;

    /// <summary>
    /// Selects this category and triggers the transition to the selected quiz screen.
    /// </summary>
    public void SelectCategory()
    {
        PlayerPrefs.SetString("category", categoryName.text);
        SceneManager.LoadScene(1);
        Debug.Log("Load Category " + categoryName.text + "...");
    }

    /// <summary>
    /// Sets the category name.
    /// </summary>
    /// <param name="name">The name for this category.</param>
    public void SetCategoryName(string name)
    {
        categoryName.text = name;
    }

    /// <summary>
    /// Sets the sprite for this category. This sprite is displayed in the selection screen for choosing a category.
    /// </summary>
    /// <param name="sprite">The sprite to display for this category.</param>
    public void SetCategorySprite(Sprite sprite)
    {
        categoryImage.sprite = sprite;
    }
}
