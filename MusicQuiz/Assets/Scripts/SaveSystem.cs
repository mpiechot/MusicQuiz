public class SaveSystem
{
    //public static void SaveQuestions(Questions question)
    //{
    //    BinaryFormatter formatter = new BinaryFormatter();
    //    string path = Application.persistentDataPath + "/questionOrder.settings";
    //    FileStream stream = new FileStream(path, FileMode.OpenOrCreate);

    //    formatter.Serialize(stream, question);
    //    stream.Close();
    //}

    //public static Questions LoadQuestions()
    //{
    //    string path = Application.persistentDataPath + "/questionOrder.settings";
    //    if (File.Exists(path))
    //    {
    //        BinaryFormatter formatter = new BinaryFormatter();
    //        FileStream stream = new FileStream(path, FileMode.Open);
    //        if(stream.Length == 0)
    //        {
    //            stream.Close();
    //            return null;
    //        }
    //        Questions questions = formatter.Deserialize(stream) as Questions;
    //        stream.Close();

    //        return questions;
    //    }
    //    return null;
    //}
}
