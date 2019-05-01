using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public GameObject brancas;
	public GameObject pretas;
    public float speed;
    public float heigth;
    private GameObject piece;
    private GameObject tile;

     private GameObject pieceToMove;
    private Vector3 posToGo;
     private GameObject rookToMove;
    private Vector3 rookPosToGo;
    bool roque;
    private bool podeMudarTurno;

    private GameManager gm;
    private BoardMapping bm;
    private BoardRules br;

    public bool podeJogar;

    public AudioSource toc;

    private bool ctrlMove;
	// Use this for initialization
	void Start () {
        gm = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager;
        bm = GameObject.FindObjectOfType(typeof(BoardMapping)) as BoardMapping;
        br = GameObject.FindObjectOfType(typeof(BoardRules)) as BoardRules;

        piece = null;
        pieceToMove = null;
        tile = null;

        ctrlMove = false;
        podeJogar = false;
        podeMudarTurno = true;
    }

    public void setPodeMudarTurno(bool resp){
        podeMudarTurno = resp;
    }

    //guarda qual peça está selecionada no momento
    public void setSelected(GameObject p) {
        piece = p;
    }

    //movimenta a peça para a posição do tile desejado
    public void movePiece(GameObject p, GameObject t)
    {
        gm.disableColliders();
        if (p != null) piece = p;
        if (t != null) tile = t;
        string[] resp = null;
        Vector2 vec = new Vector2();
        if(piece != null && tile != null)
        {
            pieceToMove = piece;
            posToGo = tile.transform.position;
            bm.clearTiles();
            
            string respStr = tile.name;
            resp = respStr.Split(new char[] { ',' });
            float[] respf = new float[2];
            for (int i = 0; i < 2; i++)
            {
                respf[i] = float.Parse(resp[i]);
            }
            
            vec = new Vector2(respf[0], respf[1]);

            br.AtualizaPosicoes(piece, vec);
            
            ctrlMove = true;
            //toc.Play();

            if(piece.name.Contains("Pawn") && (vec.x == 0 || vec.x == 7)){
                podeMudarTurno = false;
                gm.setPromoteToChange(piece);
                //podeMudarTurno = true;
            }

            if(pieceToMove.name.Contains("King") && br.getRoque()) {
                if(respf[1] == (float)1) {
                    // roque curto
                    if(piece.name.StartsWith("White")) {
                        // roque curto do rei branco
                        GameObject torre = brancas.transform.Find("White Rook 2").gameObject;
                        GameObject tileDaTorre = GameObject.Find("0,2");
                        rookToMove =  torre;
                        rookPosToGo = tileDaTorre.transform.position;
                    } else {
                        // roque curto do rei preto
                        GameObject torre = pretas.transform.Find("Black Rook 2").gameObject;
                        GameObject tileDaTorre = GameObject.Find("7,2");
                        rookToMove =  torre;
                        rookPosToGo = tileDaTorre.transform.position;
                    }
                    roque = true;
                } else if(respf[1] == (float)5) {
                    // roque longo
                    if(piece.name.StartsWith("White")) {
                        // roque longo do rei branco
                        GameObject torre = brancas.transform.Find("White Rook 1").gameObject;
                        GameObject tileDaTorre = GameObject.Find("0,4");
                        rookToMove =  torre;
                        rookPosToGo = tileDaTorre.transform.position;
                    } else {
                        // roque longo do rei preto
                        GameObject torre = pretas.transform.Find("Black Rook 1").gameObject;
                        GameObject tileDaTorre = GameObject.Find("7,4");
                        rookToMove =  torre;
                        rookPosToGo = tileDaTorre.transform.position;
                    }
                    roque = true;
                }
            }
        } 
    }

    // Update is called once per frame
    void Update () {
        // faz a animação de movimentação da peça
        if (ctrlMove)
        {
            if(roque) {
                if(Vector3.Distance(rookToMove.transform.position, rookPosToGo) > 0.01f) {
                    rookToMove.transform.position =  Vector3.Lerp(rookToMove.transform.position, rookPosToGo, speed);
                } else {
                    roque = false;
                    rookPosToGo = rookToMove.transform.position;
                    rookToMove = null;
                }
            }
            if (Vector3.Distance(pieceToMove.transform.position, posToGo) > 0.01f)
            {
                pieceToMove.transform.position =  Vector3.Lerp(pieceToMove.transform.position, posToGo, speed);
                
            }else
            {
                ctrlMove = false;
                posToGo = pieceToMove.transform.position;
                pieceToMove = null;
                podeJogar = true;
                if(podeMudarTurno){
                    gm.mudaTurno();
                }
            }
        } 
    }
}
