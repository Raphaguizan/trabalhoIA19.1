using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BoardMapping : MonoBehaviour {

    public GameObject greenTile;

    public List<Vector2> movimentos;

    public List<GameObject> tiles;

    private int pos;
    private List<GameObject> auxList;
    private BoardRules br;


    // Use this for initialization
    void Start() {
        auxList = new List<GameObject>();
        br = GameObject.FindObjectOfType(typeof(BoardRules)) as BoardRules;
    }

    // Update is called once per frame
    void Update() {
        
    }

    //função que faz o posicionamento dos movimentos possíveis
    public void makeTiles(GameObject peca){
        movimentos = br.MovimentosPossiveis(peca);
        GameObject temp;
        foreach (Vector2 v in movimentos)
        {
            pos = findInList(int.Parse(v.x + ""), int.Parse(v.y + ""));
            //instancia o objeto tile na posição certa da cena
            temp = Instantiate(greenTile, tiles[pos].transform.position, tiles[pos].transform.localRotation);
            //ajusta o nome para ser a posição em que o objeto foi colocado na matriz
            temp.name = v.x+","+v.y;
            auxList.Add(temp);
        }
        
        
    }

    //limpa todos os tiles que estavam na cena
    public void clearTiles()
    {
        foreach (GameObject i in auxList)
        {
            Destroy(i);
        }
        auxList.Clear();
    }

    //busca a posição na lista pela posição da matriz
    int findInList(int i, int j)
    {
        return i * 8 + j;
    }
}
