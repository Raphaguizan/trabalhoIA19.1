using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public Text text_novo_jogo;
    public Text text_tutorial;
    public Text text_sair;

    public Text checkW, checkB, backToMenu, jogar, facil, medio, dificil;
    public RawImage img_alto_falante;
    public RawImage img_sem_alto_falante;
    bool som;
    Ray ray;

	//level pode ser "facil", "medio" ou "dificil", black e white podem ser "player" ou "cpu". 
    public static string level, black, white;

    RaycastHit hit;
    Camera cam;
    string menu;
    Animator anim;

	
    public GameObject canvasTutorial, canvasPrincipal, canvasNovoJogo;
    // Use this for initialization
    void Start()
    {

        DontDestroyOnLoad(this.gameObject);
        som = true;
        anim = GetComponent<Animator>();
        //anim.SetBool("menuEnter", true);
        anim.Play("MenuAnimation");
        //cam = GetComponent<Camera>();
        menu = "principal";
        anim.SetBool("boardReturn", true);
        canvasNovoJogo.SetActive(false);
        canvasTutorial.SetActive(false);
        canvasPrincipal.SetActive(true);
        medio.fontStyle = FontStyle.Bold;
		medio.fontSize = 17;
        level = "medio";

    }

    // Update is called once per frame
    void Update()
    {




        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            switch (hit.collider.name)
            {
                case "novo jogo":
                    text_novo_jogo.fontStyle = FontStyle.Bold;
                    text_sair.fontStyle = FontStyle.Normal;
                    text_tutorial.fontStyle = FontStyle.Normal;
                    break;

                case "tutorial":
                    text_novo_jogo.fontStyle = FontStyle.Normal;
                    text_sair.fontStyle = FontStyle.Normal;
                    text_tutorial.fontStyle = FontStyle.Bold;
                    break;

                case "sair":
                    text_novo_jogo.fontStyle = FontStyle.Normal;
                    text_sair.fontStyle = FontStyle.Bold;
                    text_tutorial.fontStyle = FontStyle.Normal;
                    break;

                case "backToMenu":
                    backToMenu.fontStyle = FontStyle.Bold;
                    break;


                case "jogar":
                    jogar.fontStyle = FontStyle.Bold;
                    break;



                default:

                    checkB.fontStyle = FontStyle.Normal;
                    backToMenu.fontStyle = FontStyle.Normal;
                    text_novo_jogo.fontStyle = FontStyle.Normal;
                    text_sair.fontStyle = FontStyle.Normal;
                    text_tutorial.fontStyle = FontStyle.Normal;
                    jogar.fontStyle = FontStyle.Normal;

                    break;

            }

            if (som)
            {
                img_alto_falante.enabled = true;
                img_sem_alto_falante.enabled = false;
            }
            else
            {
                img_alto_falante.enabled = false;
                img_sem_alto_falante.enabled = true;
            }


            if (Input.GetMouseButtonUp(0))
            {

                Debug.Log(hit.collider.name);
                switch (hit.collider.name)
                {


                    //------------- CANVAS PRINCIPAL----------------------
                    case "somCollider":
                        som = !som;
                        text_novo_jogo.fontStyle = FontStyle.Normal;
                        text_sair.fontStyle = FontStyle.Normal;
                        text_tutorial.fontStyle = FontStyle.Normal;
                        break;

                    case "novo jogo":
                        anim.SetBool("boardReturn", true);
                        anim.Play("MenuExitAnimation");
                        menu = "novo jogo";
                        //
                        break;

                    case "tutorial":
                        anim.SetBool("boardReturn", true);
                        anim.Play("MenuExitAnimation");
                        menu = "tutorial";
                        break;

                    case "sair":
                        Application.Quit();
                        break;

                    //------------------------- CANVAS NOVO JOGO-----------------

                    case "checkBoxW":
                        checkW.enabled = !checkW.enabled;
                        break;

                    case "checkBoxB":
                        checkB.enabled = !checkB.enabled;

                        break;

                    case "backToMenu":
                        anim.Play("MenuExitAnimation");
                        menu = "principal";
                        break;

                    case "jogar":
                        anim.SetBool("boardReturn", false);
                        anim.Play("MenuExitAnimation");
                        white = checkW.enabled ? "player" : "cpu";
                        black = checkB.enabled ? "player" : "cpu";
                        SceneManager.LoadScene("Demo");

                        break;

                    case "facil":
						facil.fontSize = 17;
                        facil.fontStyle = FontStyle.Bold;
						dificil.fontSize = 12;
                        dificil.fontStyle = FontStyle.Normal;
						medio.fontSize = 12;
                        medio.fontStyle = FontStyle.Normal;
                        level = "facil";
                        break;

                    case "medio":
						medio.fontSize = 17;
						facil.fontSize = 12;
						dificil.fontSize = 12;
                        medio.fontStyle = FontStyle.Bold;
                        facil.fontStyle = FontStyle.Normal;
                        dificil.fontStyle = FontStyle.Normal;
                        level = "medio";
                        break;

                    case "dificil":
						medio.fontSize = 12;
						facil.fontSize = 12;
						dificil.fontSize = 17;
                        facil.fontStyle = FontStyle.Normal;
                        dificil.fontStyle = FontStyle.Bold;
                        medio.fontStyle = FontStyle.Normal;
                        level = "dificil";
                        break;

                    default:

                        break;
                }
            }

        }

    }

    private void OnTriggerEnter(Collider other)
    {

        switch (menu)
        {
            case "novo jogo":
                canvasNovoJogo.SetActive(true);
                canvasTutorial.SetActive(false);
                canvasPrincipal.SetActive(false);
                break;

            case "principal":
                canvasNovoJogo.SetActive(false);
                canvasTutorial.SetActive(false);
                canvasPrincipal.SetActive(true);

                break;
            case "tutorial":

                canvasNovoJogo.SetActive(false);
                canvasTutorial.SetActive(true);
                canvasPrincipal.SetActive(false);

                break;

            default:


                break;
        }
    }


}
