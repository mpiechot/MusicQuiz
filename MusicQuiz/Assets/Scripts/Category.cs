using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Category
{
    public string categoryName { get; set; }
    public string categoryPrefix { get; set; }
    public Color categoryColor { get; set; }
    public Time categoryTime { get; set; }
    //public List<QuestionComponent> questions { get; set; }
}
