using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CategoryComponent : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI CategoryName;

    public void SelectCategory()
    {
        PlayerPrefs.SetString("category", CategoryName.text);
        SceneManager.LoadScene(1);
        Debug.Log("Load Category " + CategoryName.text + "...");
    }
}
