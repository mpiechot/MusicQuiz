using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using Assets.Scripts;

/* Question:
 * =========
 * - AudioTrackPfad
 * - Korrekte Antwort
 * - Letzte Eingabe
 * - Genauen Titel oder Serie erraten
 * - Tipps
 * - Aktueller Tipplevel
 * - Freigeschaltet
 */

/* Tipps:
 * =====
 * - besteht aus String Array:
 *     - Index 0: 3 Random Buchstaben
 *     - Index 1: 3 Hinweise
 *     - Index 2: tba
 */

public class QuestionComponent : MonoBehaviour
{
    public AudioSource source;
    public Question question;
    public QuizManager qm;
    public float startSoftStopAfter = 28f;

    private float startTime;
    private bool softStarted = false;


    //public Question(AudioClip clip, string answer)
    //{
    //    this.answer = answer;
    //    sound = new Sound(clip, answer);
    //}
    void Awake()
    {
        if (qm == null)
        {
            qm = QuizManager.GetQuizManager();
        }
    }
    void Update()
    {
        if (source.isPlaying)
        {
            if ((Time.fixedUnscaledTime - startTime) >= startSoftStopAfter && !softStarted)
            {
                softStarted = true;
                StartCoroutine(AudioFades.FadeOut(source, 3f));
            }
        }
    }
    public void Play()
    {
        Debug.Log("Play! " + question.sourcePath);
        startTime = Time.fixedUnscaledTime;
        qm.StopAllSounds();
        if (qm.activeQuestion != this)
        {
            if (qm.activeQuestion != null)
            {
                qm.activeQuestion.source.Stop();
            }
            qm.SetActiveQuestion(this);
            if (source == null)
            {
                return;
            }
        }
        source.Play();
        StartCoroutine(AudioFades.FadeIn(source, 1f));
    }
    public void SoftStop()
    {
        if (source == null)
        {
            return;
        }
        StartCoroutine(AudioFades.FadeOut(source, 3f));

    }
    public void HardStop()
    {
        if (source == null)
        {
            return;
        }
        source.Stop();
    }

    public static class AudioFades
    {

        public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
        {
            float startVolume = audioSource.volume;

            while (audioSource.volume > 0)
            {
                audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

                yield return null;
            }

            audioSource.Stop();
            audioSource.volume = startVolume;
        }
        public static IEnumerator FadeIn(AudioSource audioSource, float FadeTime)
        {
            float startVolume = 0.1f;

            while (audioSource.volume < 0.8f)
            {
                audioSource.volume += startVolume * Time.deltaTime / FadeTime;

                yield return null;
            }
        }

    }
}
