using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	public Text level;	//, rulesMain, quitMain,
				//wOption, bOption, 
				//backRules, 
				//continuePause, rulesPause, quitGamePause;

	public GameObject mainMenu, pauseMenu, rulesMenu, newGameMenu, checkMenu;

	Animator animMain, animPause, animRules, animNewGame, animCheckMenu;
	List<GameObject> menus;
	public enum MenuType{none, main, newGame, pause, rules};
	public MenuType menuType;
	int menu;
	

	
    public GameObject player1, player2,cpu1, cpu2;
	
	public Text arrow1, arrow2, arrow3, arrow4, victoryMsg;
	public Button bt1, bt2, bt3, bt4, bt_ok; 

	public RawImage img_alto_falante;
    public RawImage img_sem_alto_falante;
   

	//guarda opçao na hora de escolher jogador. 
	bool cpuw, cpub; 

	public static string mode, p1, p2; 
	static bool sound = true;

	public AudioSource menuMusic, gameMusic, soundMenuIn, soundMenuOut, soundClick;

	Scene sc;
	// Use this for initialization
	void Start () {
		sc = SceneManager.GetActiveScene();
		menu =(int) MenuType.none;
		animMain = mainMenu.GetComponent<Animator>();
		animPause = pauseMenu.GetComponent<Animator>();
		animRules = rulesMenu.GetComponent<Animator>();
		animNewGame = newGameMenu.GetComponent<Animator>();
		animCheckMenu = checkMenu.GetComponent<Animator>();
		if(sc.name == "Menu"){
			animMain.Play("slideIn");
			soundMenuIn.Play();
			mode = "facil";
		}
		else if(sc.name == "Demo")
		{
			pauseMenu.SetActive(false);
			rulesMenu.SetActive(false);
			checkMenu.SetActive(false);
		}

		cpuw = false;
		cpub = true;

		img_alto_falante.enabled = sound;
		img_sem_alto_falante.enabled = !sound;
		if(sound)
		{
			menuMusic.Play();
			gameMusic.Play();
		}
		else
		{
			menuMusic.Stop();
			gameMusic.Stop();
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		 if(sc.name == "Demo"){
			 if(Input.GetKeyDown(KeyCode.Escape)){
				pauseMenu.SetActive(true);
				Time.timeScale = 0;
				img_alto_falante.enabled = sound;
				img_sem_alto_falante.enabled = !sound;
				gameMusic.Pause();
			 }

			 if(GameManager.checkMate){
				 checkMenu.SetActive(true);
				 victoryMsg.text = GameManager.whiteWon? "BRANCAS GANHARAM" : "PRETAS GANHARAM";
				 animCheckMenu.Play("slideIn");
				 GameManager.checkMate = false;
			 }
		 }
	}

	public void continueGame()
	{
		pauseMenu.SetActive(false);
		Time.timeScale = 1;
		if(sound)
			gameMusic.Play();
		soundClick.Play();
	}

	public void toRules()
	{
		
		soundClick.Play();
		pauseMenu.SetActive(false);
		rulesMenu.SetActive(true);
	}

	public void backToPause()
	{
		
		soundClick.Play();
		pauseMenu.SetActive(true);
		rulesMenu.SetActive(false);
	}

	public void quitGame()
	{
		
		soundClick.Play();
		Time.timeScale = 1;
		SceneManager.LoadScene("Menu");
	}

	
	

	public void onClick(int menuOut)//}, int menuIn)
	{
		
		soundClick.Play();
	
		switch(menuOut)
		{
			case (int)MenuType.main:
			animMain.Play("slideOut");
			soundMenuOut.Play();
			break;

			case (int)MenuType.rules:
			animRules.Play("slideOut");
			soundMenuOut.Play();
			break;

			case (int)MenuType.newGame:
			animNewGame.Play("slideOut");
			soundMenuOut.Play();
			break;

			case (int)MenuType.pause:
			animPause.Play("slideOut");
			soundMenuOut.Play();
			break;
		}
	}

	public void toMenu(int menuIn){
		
		soundClick.Play();
		this.menu = menuIn;
	}

	public void Quit(){
		
		soundClick.Play();
		Application.Quit();
	}

	public void soundOnOff()
	{
		
		soundClick.Play();
		sound = !sound;
		img_alto_falante.enabled = sound;
		img_sem_alto_falante.enabled = !sound;
		if(sound)
		{
			menuMusic.Play();
			gameMusic.Play();
		}
		else
		{
			menuMusic.Stop();
			gameMusic.Stop();
		}
		
	}

	public void choosePlayer(int arrow)
	{
		
		soundClick.Play();
		if(arrow == 1 || arrow == 2)
		{
			cpuw = !cpuw;
			cpu1.SetActive(cpuw);
			player1.SetActive(!cpuw);
		}

		else if(arrow == 3 || arrow == 4)
		{
			cpub = !cpub;
			cpu2.SetActive(cpub);
			player2.SetActive(!cpub);
		}
		
	}

	public void changeLevel(){
		
		soundClick.Play();
		if(level.text == "FÁCIL")
		{
			level.text = "DIFÍCIL";
			mode = "dificil";
		}
		else
		{
			level.text = "FÁCIL";
			mode = "facil";
		}
	}

	public void mouseOverButton(Text button){
		
		button.fontStyle = FontStyle.Bold;
	}

	public void mouseExitButton(Text button){

		
		button.fontStyle = FontStyle.Normal;
	}

/// <summary>
	/// OnTriggerEnter is called when the Collider other enters the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerEnter(Collider other)
	{
		switch (menu)
		{
			case (int)MenuType.none:
			
			break;
			case (int)MenuType.main:
			
				animMain.Play("slideIn");
				soundMenuIn.Play();
			
			break;
			case (int)MenuType.rules:
			
				animRules.Play("slideIn");
				soundMenuIn.Play();
			
			break;
			case (int)MenuType.newGame:
			
				animNewGame.Play("slideIn");
				soundMenuIn.Play();
			break;
			case (int)MenuType.pause:
		
				animPause.Play("slideIn");
				soundMenuIn.Play();
			
			break;

			//menu = -1
			default:
			p1 = cpuw? "IA" : "Player";
			p2 = cpub? "IA" : "Player";
			SceneManager.LoadScene("Demo");
			break;
		}

		
	}

	
}
