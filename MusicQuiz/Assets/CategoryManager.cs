using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class CategoryManager : MonoBehaviour
{
    [SerializeField]
    private GameObject categoryPrefab;
    [SerializeField]
    private Transform parentTransform;
    [SerializeField]
    private TextMeshProUGUI categoryLbl;

    void Start()
    {
        DirectoryInfo directory = new DirectoryInfo(Application.streamingAssetsPath);

        categoryLbl.text = new DirectoryInfo(Application.streamingAssetsPath).GetFiles().Length + "";
        TextAsset categorysJson = Resources.Load<TextAsset>(Application.dataPath + "/Resources/categorys.json");
        Debug.Log(categorysJson.text);
        string[] categorys = JsonUtility.FromJson<string[]>(categorysJson.text);
        Debug.Log(categorys.Length + " -> " + categorys[0]);

        foreach (DirectoryInfo subFolder in directory.GetDirectories())
        {
            GameObject category = Instantiate(categoryPrefab);
            category.transform.SetParent(parentTransform);
            category.GetComponentInChildren<TextMeshProUGUI>().text = char.ToUpper(subFolder.Name[0]) + subFolder.Name.Substring(1);

            CategoryComponent catcomp = category.GetComponent<CategoryComponent>();
            //catcomp.CategoryIconSprite = Resources.Load<Sprite>(subFolder.Name + "_icon");
            //catcomp.CategoryBackgroundSprite = Resources.Load<Sprite>(subFolder.Name + "_img");

            Debug.Log("Folder: " + subFolder.Name);
        }
    }
}
