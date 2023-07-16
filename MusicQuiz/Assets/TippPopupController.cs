//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using TMPro;
//using Assets.Scripts.QuizManagement;

//public class TippPopupController : MonoBehaviour
//{
//    [SerializeField] private QuizManager quizManager = default;

//    [SerializeField] private TMP_Text tipp1 = default;
//    [SerializeField] private TMP_Text tipp2 = default;
//    [SerializeField] private TMP_Text tipp3 = default;

//    // Start is called before the first frame update
//    void Start()
//    {
//        tipp1.text = string.Empty;
//        tipp2.text = string.Empty;
//        tipp3.text = string.Empty;
//    }

//    public void GetTipp()
//    {
//        Tipp[] tipps = quizManager.GetHelp();

//        if (tipps == null || tipps.Length <= 2)
//        {
//            return;
//        }

//        tipp1.text = $"Tipp 1: {tipps[0].tippText}";
//        tipp2.text = $"Tipp 2: {tipps[1].tippText}";
//        tipp3.text = $"Tipp 3: {tipps[2].tippText}";
//    }

//    public void CloseTippPopup()
//    {
//        this.gameObject.SetActive(false);
//    }
//}
