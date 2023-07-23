using UnityEngine;

namespace Assets.Scripts.QuizManagement
{
    public class TippManager : MonoBehaviour
    {
        //[SerializeField] private int helpCharacterNumber = 3;

        //public Tipp[] UpdateTipps(Question question)
        //{
        //    return GiveTippsUpTo(question, question.actualTipLevel);
        //}

        //public Tipp[] GiveTippsUpTo(Question question, int tippLevel)
        //{
        //    ClearTipps();
        //    Tipp[] tipps = new Tipp[3];



        //    if (tippLevel > 0)
        //    {
        //        if (question.tipps[0].Equals("?"))
        //        {
        //            CreateTippOne(question);
        //        }
        //        for (int i = 0; i < tippLevel; i++)
        //        {
        //            tipps[i] = new Tipp();
        //            tipps[i].tippText = question.tipps[i];
        //        }
        //    }

        //    return tipps;
        //}

        //public void ClearTipps()
        //{
        //    //foreach (var tippField in tippFields)
        //    //{
        //    //    tippField.text = string.Empty;
        //    //}
        //}

        //public void SetTipp(int tippIndex, string tipp)
        //{
        //    //tippFields[tippIndex].text = tipp;
        //}

        //private void CreateTippOne(Question question)
        //{
        //    string answer = question.sourcePath;
        //    string tipp = "";
        //    int[] answerCharacters = new int[helpCharacterNumber];
        //    for (int i = 0; i < helpCharacterNumber; i++)
        //    {
        //        int randomCharacter = Random.Range(0, answer.Length);
        //        while (answer.ElementAt(randomCharacter) == ' ' || answerCharacters.Contains(randomCharacter))
        //        {
        //            randomCharacter = Random.Range(0, answer.Length);
        //        }
        //        answerCharacters[i] = randomCharacter;
        //    }
        //    for (int i = 0; i < answer.Length; i++)
        //    {
        //        if (answer.ElementAt(i) == ' ')
        //        {
        //            tipp += "  ";
        //        }
        //        else
        //        {
        //            if (answerCharacters.Contains(i))
        //            {
        //                tipp += answer.ElementAt(i) + " ";
        //            }
        //            else
        //            {
        //                tipp += "_ ";
        //            }
        //        }
        //    }
        //    question.tipps[0] = tipp;
        //    SetTipp(0, tipp);
        //}
    }
}
