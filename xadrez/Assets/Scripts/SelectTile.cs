using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTile : MonoBehaviour {

    public Color change, enemyPiece, enemyPieceChanged;
	private Color ini;
	private SpriteRenderer[] children;
    private bool enemy;

    private PlayerScript ps;
    private BoardRules br;
    private SelectPiece sp;

	// Use this for initialization
	void Start () {
        enemy = false;
        ps = GameObject.FindObjectOfType(typeof(PlayerScript)) as PlayerScript;
        br = GameObject.FindObjectOfType(typeof(BoardRules)) as BoardRules;
        sp = GameObject.FindObjectOfType(typeof(SelectPiece)) as SelectPiece;

        ini = this.GetComponentInChildren<SpriteRenderer>().color;
        children = GetComponentsInChildren<SpriteRenderer>();
        verificaInimigo();
    }
	
	// Update is called once per frame
	void Update () {
      
    }

    // verifica se tem inimigo em cima do tile e moda a cor para a cor de ataque
    void verificaInimigo (){
        string respStr = this.name;
        string[] resp = respStr.Split(new char[] { ',' });
        float[] respf = new float[2];
        for (int i = 0; i < 2; i++)
        {
            respf[i] = float.Parse(resp[i]);
        }

        Vector2 vec = new Vector2(respf[0], respf[1]);
        if(br.verifyPosition(vec)){
            setEnemyColor();
        }
    }

    //coloca a cor de peça inimiga
    public void setEnemyColor(){
        enemy = true;
        foreach(SpriteRenderer c in children){
           c.color = enemyPiece;
        }
    }

    //muda a cor quando mouse entra
    void OnMouseEnter()
    {
        foreach(SpriteRenderer c in children){
            if(enemy){
                c.color = enemyPieceChanged;
            }else
            {
                c.color = change;
            } 
        }
    }
    //enquanto o mouse tiver em cima pode-se mover a peça para essa posição
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            sp.cleanSelection(false);
            ps.movePiece(null, this.gameObject);
        }
    }
    //volta a cor original quando o mouse sai
    void OnMouseExit()
    {
         foreach(SpriteRenderer c in children){
           if(enemy){
                c.color = enemyPiece;
            }else
            {
                c.color = ini;
            } 
        }
    }
}
