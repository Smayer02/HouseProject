using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GuiElements : MonoBehaviour
{
    public static float elapsedTime = 0;
    public bool isRunning = false;
    public bool isFinished = false;
    public FinishGoal finishKey;

    public Text timeDisplayText;
    public Text scoreDisplayText;
    public int displayTime;
    public Button startMessage;
    public Button leaderboardBox;
    public ButtonCommands buttonManager;
    public static bool gameStart = false;
    public int cutoff = 0;

    public string apiurl = "https://fvtcdp.azurewebsites.net/api/leaderboard";
    public List<Leaderboard> games;
    public string userName = "MAG";
    public Dropdown ddlGameList;
    public List<string> recentScores;

    // Start is called before the first frame update
    void Start()
    {
        isRunning = true;
        if (gameStart)
            startMessage.gameObject.SetActive(false);
        leaderboardBox.gameObject.SetActive(false);
        timeDisplayText.text = "Time: " + displayTime.ToString() + " seconds";
    }

    // Update is called once per frame
    void Update()
    {   
        if (isRunning) {
            elapsedTime = elapsedTime + Time.deltaTime;
            displayTime = (int)elapsedTime;
            timeDisplayText.text = "Time: " + displayTime.ToString() + " seconds";
        }

        if (Input.GetKeyDown(KeyCode.Escape) || buttonManager.buttonPressed || gameStart) {
            startMessage.gameObject.SetActive(false);
            Time.timeScale = 1;
            gameStart = true;
        }
    }

    //Added
    public void GetScores()
    {
         try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiurl);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string jsonResponse = reader.ReadToEnd();
                dynamic items = (JArray)JsonConvert.DeserializeObject(jsonResponse);
                games = items.ToObject<List<Leaderboard>>();
                ddlGameList.options.Clear();
                //Debug.Log("Games: " + games.Count);
                List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
                //ddlGameList = GameList.GetComponent<Dropdown>();
                Debug.Log(recentScores == null);
                //GameList.options.Clear();
                if (games.Count > 4)
                    cutoff = games.Count - 4;
                else
                    cutoff = games.Count;
                int listCount = 0;
                foreach(Leaderboard leaderboard in games)
                {
                    if (listCount > cutoff) {
                        recentScores.Add(leaderboard.ToString());
                    }
                    listCount++;
                }
                string completeScores = "";
                foreach(string score in recentScores) {
                    completeScores = completeScores + score + "\n";
                }
                scoreDisplayText.text = completeScores;
                //ddlGameList.options.Clear();
                //ddlGameList.AddOptions(options);
            }
            catch (System.Exception ex)
            {
                Debug.Log("Error " + ex.Message);
            }
    }
    
    public void SubmitLeaderboard()
	{   
		HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiurl);
		request.Method = "POST";

		Leaderboard leaderboard = new Leaderboard
		{
			Id = Guid.Empty,
			UserName = userName,
			Score = displayTime,
			GameTime = DateTime.Now
		};

		string serializedObject = JsonConvert.SerializeObject(leaderboard);
		byte[] byteArray = Encoding.UTF8.GetBytes(serializedObject);
		request.ContentType = "application/json";
		request.ContentLength = byteArray.Length;
        Debug.Log(serializedObject);

		Stream dataStream = request.GetRequestStream();
		dataStream.Write(byteArray,0,byteArray.Length);
		WebResponse response = request.GetResponse();

		// Display the status
		dataStream = response.GetResponseStream();
		StreamReader reader = new StreamReader(dataStream);
		string reponseFromServer = reader.ReadToEnd();
        
		reader.Close();
		dataStream.Close();
		response.Close();

        Debug.Log("Finished..." + reponseFromServer);
	}
}
