using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextDisplay : MonoBehaviour {
    GamePlayManager gamePlayManager;

    [SerializeField]
    private Text incidentText;

    // Use this for initialization
    void Start () {

        gamePlayManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GamePlayManager>();

    }
	
	// Update is called once per frame
	void Update () {

        incidentText.text = gamePlayManager.incidentDesc;

    }
}
