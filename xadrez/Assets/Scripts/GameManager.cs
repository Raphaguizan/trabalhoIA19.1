using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject white, black;
    public string player1, player2, mode;
    public int turno = 1;
    private GameObject promoteToChange;
    public GameObject wPromotePlat, bPromotePlat;
    private SelectPiece sp;
    private Collider[] coll;
    private IA ia;
    private CameraMove cm;

    public static bool checkMate, whiteWon;
    // Use this for initialization
    void Start()
    {
        mode = MenuManager.mode;
        player1 = MenuManager.p1;
        player2 = MenuManager.p2;
        sp = GameObject.FindObjectOfType(typeof(SelectPiece)) as SelectPiece;
        ia =  GameObject.FindObjectOfType(typeof(IA)) as IA;
        cm = GameObject.FindObjectOfType(typeof(CameraMove)) as CameraMove;

        // ajustes de camera e jogadas de acordo com a escolha de jogadores
        if(player1.Equals("IA")){
            StartCoroutine(RunIA(1, "White", mode));
        }
        if(player1.Equals("Player")&& player2.Equals("Player")){
            cm.active = true;
        }else if(player1.Equals("IA")&& player2.Equals("Player")){
            cm.setPlayer2();
        }  
     }
    
     // tenta iniciar o script da IA em corotina / thread mas ainda não funciona
     IEnumerator RunIA(float waitTime, string color, string mode)
     {
         yield return new WaitForSeconds(waitTime);
         
        //Debug.Log("ia rodando em coroutine");
       // Debug.Log(color);
        ia.buscar(color, mode);
        
     }
    
    // Update is called once per frame
    void Update()
    {

    }

    // função que ativa a visualização da promoção de peça
    public void setPromoteToChange(GameObject o){
        promoteToChange = o;
        if (o.name.Contains("White"))
        {
            wPromotePlat.SetActive(true);
        }else if (o.name.Contains("Black")){
            bPromotePlat.SetActive(true);
        }
    }

    // função para terminar o jogo quando o rei for eliminado
    public void endGame(){
        StartCoroutine(endGameWait(1));
        whiteWon = (turno == 1)? true : false;
        checkMate = true;
        turno = 0;
    }

    // wait assincrono para esperar a animação acabar
    IEnumerator endGameWait(float waitTime)
     {
         yield return new WaitForSeconds(waitTime);
        
        //SceneManager.LoadScene("Menu");
     }

    //função que remove a visualização da promoção de peça
    public void cleanPromotePlat(){
        wPromotePlat.SetActive(false);
        bPromotePlat.SetActive(false);
    }
    public GameObject getPromoteToChange(){
        return promoteToChange;;
    }
    public int getTurno()
    {
        return turno;
    }

    // desabilita a colisão das peças
    public void disableColliders(){
        coll = white.GetComponentsInChildren<Collider>();
        foreach(Collider c in coll){
            c.enabled = false;
        }
        coll = black.GetComponentsInChildren<Collider>();
        foreach(Collider c in coll){
            c.enabled = false;
        }
    }

    public void mudaTurno()
    {
        if(turno == 2)
        {
            turno = 1;
            // Debug.Log("IA pensando");
            // ia.buscar();
            coll = white.GetComponentsInChildren<Collider>();
            foreach(Collider c in coll){
                c.enabled = true;
            }
            coll = black.GetComponentsInChildren<Collider>();
            foreach(Collider c in coll){
                c.enabled = false;
            }
            if(player1.Equals("IA")){
                StartCoroutine(RunIA(0, "White", mode));
            } 
        
        }else if (turno == 1)
        {
            turno = 2;
             coll = white.GetComponentsInChildren<Collider>();
            foreach(Collider c in coll){
                c.enabled = false;
            }
            coll = black.GetComponentsInChildren<Collider>();
            foreach(Collider c in coll){
                c.enabled = true;
            }
            if(player2.Equals("IA")){
                StartCoroutine(RunIA(0, "Black", mode));
            } 
        }
        sp.cleanSelection(true);
        cleanPromotePlat();
    }
}