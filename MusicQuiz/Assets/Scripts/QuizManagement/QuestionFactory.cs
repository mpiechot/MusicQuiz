using Assets.Scripts;
//using DanielLochner.Assets.SimpleScrollSnap;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionFactory : MonoBehaviour
{
    [SerializeField] private GameObject questionPrefab = default;
    [SerializeField] private RectTransform questionParent = default;

    public QuestionComponent CreateQuestionGameObject(Question question, Action<QuestionComponent> onPlayQuestion)
    {
        // Panel
        var panel = Instantiate(questionPrefab, questionParent);
        if (panel.TryGetComponent<QuestionComponent>(out var questionComponent))
        {
            questionComponent.Initialize(question, onPlayQuestion);
            return questionComponent;
        }

        return null;
    }
}
