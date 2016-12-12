using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GamePlayManager : MonoBehaviour {

    #region Serialized Fields
    [SerializeField]
    VideoController videoController;
    [SerializeField]
    UIController uiController;
    [SerializeField]
    public bool startTimer = false;
    [SerializeField]
    public bool textIncident = false;
    [SerializeField]
    public string incidentDesc; 
    #endregion

    #region Private Variables
    private int inputCounter;
    private List<string> inputStringList = new List<string>();
    private VideoInputLogic videoInputLogic = new VideoInputLogic();
    private IncidentInputLogic incidentInputLogic = new IncidentInputLogic();
    private ResponseInputLogic responseInputLogic = new ResponseInputLogic();
    #endregion

    #region Gameplay Modifiers
    //True if the players are asked to input incidents before each round
    bool inputIncidents;
    #endregion


    public List<Incident> Incidents;
    public List<Response> Responses;
    public Incident currentIncident;
    public Response currentResponse;
    public GameStates currentGameState;
    public Material defualtMat;

    public int incidentsInGame;
    public int incidentCounter;

    public float IncidentPlayTimer;
    public float ResponsePlayTimer;

    public enum GameStates
    {
        ChooseIncident,
        PlayIncident,
        ChooseResponse,
        PlayResolution
    }

    #region Singlton Pattern
    public static GamePlayManager instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    // Use this for initialization
    void Start ()
    {
        ChangeState(GameStates.ChooseIncident);
        incidentCounter = 0;
        videoController = GameObject.FindGameObjectWithTag("VideoController").GetComponent<VideoController>();
    }

    //Changes the current game state and adjusts input field visibility
    void ChangeState(GameStates _gameState)
    {
        currentGameState = _gameState;

        switch (_gameState)
        {
            case GameStates.ChooseIncident:
                uiController.ActivateIncidentInputField();
                break;
            case GameStates.ChooseResponse:
                uiController.ActivateResponseInputField();
                break;
            case GameStates.PlayIncident:
                uiController.DeActivateInputFields();
                break;
            case GameStates.PlayResolution:
                uiController.DeActivateInputFields();
                break;
        }
    }

    //Called when the player enters input to choose incident
    public void ActivateIncident()
    {
        if(uiController.inputField_Incident.text == "")
        {
            return;
        }

        if (textIncident)
        {
            currentIncident = incidentInputLogic.DetermineIncident(uiController.inputField_Incident.text, Incidents);
            currentIncident.Activate();
            incidentDesc = currentIncident.IncidentDescs;
            startTimer = true; // Start this after the full length of the video or when the the code is input!
            IncidentPlayTimer = 10;
            startTimer = true; // Start this after the full length of the video or when the the code is input!
            ChangeState(GameStates.PlayIncident);
            incidentCounter += 1;
        }
        else
        {
            currentIncident = incidentInputLogic.DetermineIncident(uiController.inputField_Incident.text, Incidents);
            currentIncident.Activate();
            videoController.PlayVideo(videoInputLogic.DetermineIncidentVideo(videoController.videoClips));
            //Sets the timer to the duration of the video
            IncidentPlayTimer = videoInputLogic.DetermineIncidentVideo(videoController.videoClips).duration;
            startTimer = true; // Start this after the full length of the video or when the the code is input!
            ChangeState(GameStates.PlayIncident);
            incidentCounter += 1;
        }
       
    }

    //Called when the player enters input to choose response
    public void ActivateResponse()
    {
        if (uiController.inputField_Response.text == "")
        {
            return;
        }

        incidentDesc = "";

        //5 different types of cards
        //8 different incidents
        //switch statement. if against and Sarcastic is played depending on the incident. Play that card. Enter a standard number

        // Sarcastic ID number: 11
        // For ID number: 22
        // DumbFounded ID number: 33
        // Against ID number: 44
        // Uncertain ID number: 55

        //Sorry...... Fix this some other time...

        if (currentIncident.name == "Incident_1")
        {
            switch(uiController.inputField_Response.text)
            {
                case "11":
                    currentResponse = responseInputLogic.DetermineResponse("11B1", Responses);
                    break;

                case "22":
                    currentResponse = responseInputLogic.DetermineResponse("11G1", Responses);
                    break;

                case "33":
                    currentResponse = responseInputLogic.DetermineResponse("11N1", Responses);
                    break;

                case "44":
                    currentResponse = responseInputLogic.DetermineResponse("11B1", Responses);
                    break;

                case "55":
                    currentResponse = responseInputLogic.DetermineResponse("11N1", Responses);
                    break;

            }
        }

        else if (currentIncident.name == "Incident_2")
        {
            switch (uiController.inputField_Response.text)
            {
                case "11": // Sarcastic ID number: 11
                    currentResponse = responseInputLogic.DetermineResponse("12G2", Responses);
                    break;

                case "22": // For ID number: 22
                    currentResponse = responseInputLogic.DetermineResponse("12B1", Responses);
                    break;

                case "33": // DumbFounded ID number: 33
                    currentResponse = responseInputLogic.DetermineResponse("12N1", Responses);
                    break;

                case "44": // Against ID number: 44
                    currentResponse = responseInputLogic.DetermineResponse("12G2", Responses);
                    break;

                case "55": // Uncertain ID number: 55
                    currentResponse = responseInputLogic.DetermineResponse("12G1", Responses);
                    break;

            }
        }
        else if (currentIncident.name == "Incident_3")
        {
            switch (uiController.inputField_Response.text)
            {
                case "11": // Sarcastic ID number: 11
                    currentResponse = responseInputLogic.DetermineResponse("13G1", Responses);
                    break;

                case "22": // For ID number: 22
                    currentResponse = responseInputLogic.DetermineResponse("13B1", Responses);
                    break;

                case "33": // DumbFounded ID number: 33
                    currentResponse = responseInputLogic.DetermineResponse("13G1", Responses);
                    break;

                case "44": // Against ID number: 44
                    currentResponse = responseInputLogic.DetermineResponse("13G2", Responses);
                    break;

                case "55": // Uncertain ID number: 55
                    currentResponse = responseInputLogic.DetermineResponse("13G2", Responses);
                    break;


            }
        }
        else if (currentIncident.name == "Incident_4")
        {
            switch (uiController.inputField_Response.text)
            {
                case "11": // Sarcastic ID number: 11
                    currentResponse = responseInputLogic.DetermineResponse("14G2", Responses);
                    break;

                case "22": // For ID number: 22
                    currentResponse = responseInputLogic.DetermineResponse("14G2", Responses);
                    break;

                case "33": // DumbFounded ID number: 33
                    currentResponse = responseInputLogic.DetermineResponse("14B1", Responses);
                    break;

                case "44": // Against ID number: 44
                    currentResponse = responseInputLogic.DetermineResponse("14B1", Responses);
                    break;

                case "55": // Uncertain ID number: 55
                    currentResponse = responseInputLogic.DetermineResponse("14G1", Responses);
                    break;


            }
        }
        else if (currentIncident.name == "Incident_5")
        {
            switch (uiController.inputField_Response.text)
            {
                case "11": // Sarcastic ID number: 11
                    currentResponse = responseInputLogic.DetermineResponse("15B2", Responses);
                    break;

                case "22": // For ID number: 22
                    currentResponse = responseInputLogic.DetermineResponse("15G1", Responses);
                    break;

                case "33": // DumbFounded ID number: 33
                    currentResponse = responseInputLogic.DetermineResponse("15B2", Responses);
                    break;

                case "44": // Against ID number: 44
                    currentResponse = responseInputLogic.DetermineResponse("15G2", Responses);
                    break;

                case "55": // Uncertain ID number: 55
                    currentResponse = responseInputLogic.DetermineResponse("15N1", Responses);
                    break;


            }
        }
        else if (currentIncident.name == "Incident_6")
        {
            switch (uiController.inputField_Response.text)
            {
                case "11": // Sarcastic ID number: 11
                    currentResponse = responseInputLogic.DetermineResponse("16B1", Responses);
                    break;

                case "22": // For ID number: 22
                    currentResponse = responseInputLogic.DetermineResponse("16G1", Responses);
                    break;

                case "33": // DumbFounded ID number: 33
                    currentResponse = responseInputLogic.DetermineResponse("16N1", Responses);
                    break;

                case "44": // Against ID number: 44
                    currentResponse = responseInputLogic.DetermineResponse("16N1", Responses);
                    break;

                case "55": // Uncertain ID number: 55
                    currentResponse = responseInputLogic.DetermineResponse("16B1", Responses);
                    break;


            }
        }
        else if (currentIncident.name == "Incident_7")
        {
            switch (uiController.inputField_Response.text)
            {
                case "11": // Sarcastic ID number: 11
                    currentResponse = responseInputLogic.DetermineResponse("17B1", Responses);
                    break;

                case "22": // For ID number: 22
                    currentResponse = responseInputLogic.DetermineResponse("17G1", Responses);
                    break;

                case "33": // DumbFounded ID number: 33
                    currentResponse = responseInputLogic.DetermineResponse("17B1", Responses);
                    break;

                case "44": // Against ID number: 44
                    currentResponse = responseInputLogic.DetermineResponse("17B1", Responses);
                    break;

                case "55": // Uncertain ID number: 55
                    currentResponse = responseInputLogic.DetermineResponse("17N1", Responses);
                    break;


            }
        }
        else if (currentIncident.name == "Incident_8")
        {
            switch (uiController.inputField_Response.text)
            {
                case "11": // Sarcastic ID number: 11
                    currentResponse = responseInputLogic.DetermineResponse("18B1", Responses);
                    break;

                case "22": // For ID number: 22
                    currentResponse = responseInputLogic.DetermineResponse("18G1", Responses);
                    break;

                case "33": // DumbFounded ID number: 33
                    currentResponse = responseInputLogic.DetermineResponse("18G2", Responses);
                    break;

                case "44": // Against ID number: 44
                    currentResponse = responseInputLogic.DetermineResponse("18G2", Responses);
                    break;

                case "55": // Uncertain ID number: 55
                    currentResponse = responseInputLogic.DetermineResponse("18N1", Responses);
                    break;


            }
        }

        //currentResponse = responseInputLogic.DetermineResponse(uiController.inputField_Response.text, Responses);
        currentResponse.Activate();
        videoController.PlayVideo(videoInputLogic.DetermineResponseVideo(videoController.videoClips));
        //Sets the timer to the duration of the video
        ResponsePlayTimer = videoInputLogic.DetermineResponseVideo(videoController.videoClips).duration;
        //ResponsePlayTimer = 40;
        ChangeState(GameStates.PlayResolution);

        //Reset to default material
        defualtMat = videoController.screenMat;
    }

    // Update is called once per frame
    void Update ()
    {
        UpdateTimers();

	}

    //Controls the timers that control playing input between response and incident video playbacks
    void UpdateTimers()
    {
        if(IncidentPlayTimer > 0)
        {
            IncidentPlayTimer -= Time.deltaTime;
        }
        else
        {
            if(currentGameState == GameStates.PlayIncident)
            {
                ChangeState(GameStates.ChooseResponse);
            }
        }

        if(ResponsePlayTimer > 0)
        {
            ResponsePlayTimer -= Time.deltaTime;
        }
        else
        {
            if (currentGameState == GameStates.PlayResolution)
            {
                if(incidentCounter == incidentsInGame)
                {
                    EndGame();
                }
                else
                {
                    ChangeState(GameStates.ChooseIncident);
                }
            }
        }
    }

    //Called when all the incidents have been responded to
    void EndGame()
    {
        GameManager.instance.ChangeScene(GameManager.instance.tag_OutroScene);
    }
}
