using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class XMLParser : MonoBehaviour {

    //public TextMesh fileDataTextbox;
    private string path;
    private string fileInfo;
    private XmlDocument xmlDoc;
    private WWW www;
    private TextAsset textXml;
    private List<Player> Players;
    private string fileName;

    // Structure for mainitaing the player information
    struct Player
    {   
        public string question;
        public string option;
        public string answer;
    };

    void Awake()
    {
        fileName = "level1";
        Players = new List<Player>(); // initalize player list
        //fileDataTextbox.text = "";
    }

    void Start()
    {
        loadXMLFromAssest();
        readXml();
    }

    // Following method load xml file from resouces folder under Assets
    private void loadXMLFromAssest()
    {
        xmlDoc = new XmlDocument();
        if (System.IO.File.Exists(getPath()))
        {
            xmlDoc.LoadXml(System.IO.File.ReadAllText(getPath()));
        }
        else
        {
            textXml = (TextAsset)Resources.Load(fileName, typeof(TextAsset));
            xmlDoc.LoadXml(textXml.text);
        }
    }

    void Update()
    {
        
    }

    // Following method reads the xml file and display its content
    private void readXml()
    {
        foreach (XmlElement node in xmlDoc.SelectNodes("options/level1"))
        {
            Player tempPlayer = new Player();
            tempPlayer.question = node.SelectSingleNode("Question").InnerText;
            tempPlayer.option = node.SelectSingleNode("Options").InnerText;
            tempPlayer.answer = node.SelectSingleNode("Answer").InnerText;
            Players.Add(tempPlayer);
            //displayPlayeData(tempPlayer);
        }

		print(Players.Count);
		Player first = Players[0];
		print(first.answer);
		print(first.option);
		print(first.question);

		string[] strQuestions = first.question.Split(' ');

		foreach (string theWord in strQuestions) {
			print(theWord);
		}

    }

    
    // Following method is used to retrive the relative path as device platform
    private string getPath()
    {
        #if UNITY_EDITOR
            return Application.dataPath + "/Resources/" + fileName;
        #elif UNITY_ANDROID
            return Application.persistentDataPath+fileName;
        #elif UNITY_IPHONE
            return GetiPhoneDocumentsPath()+"/"+fileName;
        #else
            return Application.dataPath +"/"+ fileName;
        #endif
    }

    private string GetiPhoneDocumentsPath()
    {
        // Strip "/Data" from path
        string path = Application.dataPath.Substring(0, Application.dataPath.Length - 5);
        // Strip application name
        path = path.Substring(0, path.LastIndexOf('/'));
        return path + "/Documents";
    }
}
