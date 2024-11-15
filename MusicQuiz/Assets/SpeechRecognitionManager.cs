//#nullable enable

//using HuggingFace.API;
//using MusicQuiz.Exceptions;
//using System;
//using System.IO;
//using TMPro;
//using UnityEngine;
//using UnityEngine.UI;

//namespace MusicQuiz
//{
//    public class SpeechRecognitionManager : MonoBehaviour
//    {
//        [SerializeField]
//        private Button? startListeningButton;

//        [SerializeField]
//        private Button? stopListeningButton;

//        [SerializeField]
//        private TMP_Text? recognizedText;

//        private AudioClip? recordingClip;
//        private byte[]? recordedBytes;
//        private bool isRecording;

//        private Button StartListeningButton => SerializeFieldNotAssignedException.ThrowIfNull(startListeningButton);

//        private Button StopListeningButton => SerializeFieldNotAssignedException.ThrowIfNull(stopListeningButton);

//        private TMP_Text RecognizedText => SerializeFieldNotAssignedException.ThrowIfNull(recognizedText);

//        private void Start()
//        {
//            StartListeningButton.onClick.AddListener(StartListening);
//            StopListeningButton.onClick.AddListener(StopListening);
//        }

//        private void StartListening()
//        {
//            recordingClip = Microphone.Start(null, false, 10, 44100);
//            isRecording = true;
//        }

//        private void Update()
//        {
//            if (isRecording && recordingClip != null && Microphone.GetPosition(null) >= recordingClip.samples)
//            {
//                StopListening();
//            }
//        }

//        private void StopListening()
//        {
//            if (!isRecording || recordingClip == null)
//            {
//                return;
//            }

//            var position = Microphone.GetPosition(null);
//            Microphone.End(null);
//            var samples = new float[position * recordingClip.channels];
//            recordingClip.GetData(samples, 0);
//            recordedBytes = EncodeAsWAV(samples, recordingClip.frequency, recordingClip.channels);
//            isRecording = false;
//            SendRecording();
//        }

//        private void SendRecording()
//        {
//            HuggingFaceAPI.AutomaticSpeechRecognition(recordedBytes, response =>
//            {
//                RecognizedText.text = response;
//            }, error =>
//            {
//                RecognizedText.text = error;
//            });
//        }

//        private byte[]? EncodeAsWAV(float[] samples, int frequency, int channels)
//        {
//            using var memoryStream = new MemoryStream(44 + samples.Length * 2);
//            using var writer = new BinaryWriter(memoryStream);
//            writer.Write("RIFF".ToCharArray());
//            writer.Write(36 + samples.Length * 2);
//            writer.Write("WAVE".ToCharArray());
//            writer.Write("fmt ".ToCharArray());
//            writer.Write(16);
//            writer.Write((ushort)1);
//            writer.Write((ushort)channels);
//            writer.Write(frequency);
//            writer.Write(frequency * channels * 2);
//            writer.Write((ushort)(channels * 2));
//            writer.Write((ushort)16);
//            writer.Write("data".ToCharArray());
//            writer.Write(samples.Length * 2);

//            foreach (var sample in samples)
//            {
//                writer.Write((short)(sample * short.MaxValue));
//            }

//            return memoryStream.ToArray();
//        }
//    }
//}
