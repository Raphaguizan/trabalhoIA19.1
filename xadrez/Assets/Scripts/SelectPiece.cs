using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPiece : MonoBehaviour {

    public GameObject board;

    private Component[] comp;

    private GameManager gm;
    private PlayerScript ps;
    private BoardMapping bm;


    // Use this for initialization
    void Start () {
        gm = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager;
        ps = GameObject.FindObjectOfType(typeof(PlayerScript)) as PlayerScript;
        bm = GameObject.FindObjectOfType(typeof(BoardMapping)) as BoardMapping;

    }

    // Update is called once per frame
    void Update()
    {
       
    }
    //metodo que limpa a seleção de todas as peças. A variável booleana indica se tira somente a borda (false)
    // ou se limpa a seleção do objeto da peça também (true)
    public void cleanSelection(bool p)
    {
        comp = board.GetComponentsInChildren<Outline>();

        foreach (Outline c in comp)
        {
            c.enabled = false;
            if(p){
                ps.setSelected(null);
            } 
        }
    }

    void OnMouseDown()
    {
        if (gm.getTurno() == 1)
        {
            //verifica se é o truno da peça branca
            if (this.tag == "whitePiece")
            {
                bm.clearTiles();
                cleanSelection(true);
                //liga o outline da peça
                this.GetComponent<Outline>().enabled = true;
                //passa para o scrip do player qual peça esta selecionada
                ps.setSelected(this.gameObject);
                //faz as posições possiveis de movimentação dessa peça
                bm.makeTiles(this.gameObject);
            }
        }
        else
        {
            //verifica se é o truno da peça preta
            if (this.tag == "blackPiece")
            {
                bm.clearTiles();
                cleanSelection(true);
               //liga o outline da peça
                this.GetComponent<Outline>().enabled = true;
                //passa para o scrip do player qual peça esta selecionada
                ps.setSelected(this.gameObject);
                //faz as posições possiveis de movimentação dessa peça
                bm.makeTiles(this.gameObject);
            }
        } 
    }
}
