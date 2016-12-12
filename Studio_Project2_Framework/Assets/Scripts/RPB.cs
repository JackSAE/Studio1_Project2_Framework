using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RPB : MonoBehaviour {
    GamePlayManager gamePlayManager;
    public Transform LoadingBar;
    public Text TextIndicator;

    [SerializeField]
    private float currentAmount;
    [SerializeField]
    private float speed;
	
    //Fix this before Wedesnday!!!

    void Start()
    {
        gamePlayManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GamePlayManager>();
    }

	// Update is called once per frame
	void Update () {
        if (currentAmount < 100 && gamePlayManager.startTimer == true)
        {
            currentAmount += speed * Time.deltaTime;
            TextIndicator.text = ((int)currentAmount / 2.5f).ToString() + "";

        }
        LoadingBar.GetComponent<Image>().fillAmount = currentAmount / 100;
	}
}
