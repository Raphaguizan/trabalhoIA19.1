using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class RulesTest {

    //gera a matriz para o teste
    public GameObject[][] m01(){
        //criacao base da matriz
        GameObject[][] tabuleiro = new GameObject[8][];
        for(int i = 0; i < tabuleiro.Length; i++) {
			tabuleiro[i] = new GameObject[8];
            for(int j = 0; j < tabuleiro[i].Length; j++) {
                tabuleiro[i][j] = null;
            }
        }
        tabuleiro[0][0] = GameObject.CreatePrimitive(PrimitiveType.Cube);
        tabuleiro[0][0].name = "White Queen";
        return tabuleiro;   
    }

    //uma peca da mesma cor
    [Test]
    public void TestNumPecasTime01() {
        GameObject[][] g = m01();
        int n = NumPecasTime(g,"White");
        Assert.AreEqual(n, 1);
    }

    //gera a matriz para o teste
    public GameObject[][] m02(){
        //criacao base da matriz
        GameObject[][] tabuleiro = new GameObject[8][];
        for(int i = 0; i < tabuleiro.Length; i++) {
			tabuleiro[i] = new GameObject[8];
            for(int j = 0; j < tabuleiro[i].Length; j++) {
                tabuleiro[i][j] = null;
            }
        }
        tabuleiro[0][0] = GameObject.CreatePrimitive(PrimitiveType.Cube);
        tabuleiro[0][0].name = "White Queen";
        tabuleiro[1][1] = GameObject.CreatePrimitive(PrimitiveType.Cube);
        tabuleiro[1][1].name = "White Bishop 1";
        return tabuleiro;   
    }

    //duas pecas da mesma cor
    [Test]
    public void TestNumPecasTime02() {
        GameObject[][] g = m02();
        int n = NumPecasTime(g,"White");
        Assert.AreEqual(n, 2);
    }


    //gera a matriz para o teste
    public GameObject[][] m03(){
        //criacao base da matriz
        GameObject[][] tabuleiro = new GameObject[8][];
        for(int i = 0; i < tabuleiro.Length; i++) {
			tabuleiro[i] = new GameObject[8];
            for(int j = 0; j < tabuleiro[i].Length; j++) {
                tabuleiro[i][j] = null;
            }
        }
        tabuleiro[0][0] = GameObject.CreatePrimitive(PrimitiveType.Cube);
        tabuleiro[0][0].name = "White Rook";
        tabuleiro[0][1] = GameObject.CreatePrimitive(PrimitiveType.Cube);
        tabuleiro[0][1].name = "White Bishop 01";
        tabuleiro[2][0] = GameObject.CreatePrimitive(PrimitiveType.Cube);
        tabuleiro[2][0].name = "White Bishop 02";
        return tabuleiro;   
    }

    //uma posicao livre e duas pecas do mesmo time
    [Test]
    public void TestRook01() {
        GameObject[][] g = m03();
        Vector2 v = new Vector2(0,0);
        System.Collections.Generic.List<Vector2> r = new System.Collections.Generic.List<Vector2>();
        System.Collections.Generic.List<Vector2> exp = new System.Collections.Generic.List<Vector2>();
        exp.Add(new Vector2(1,0));
        r = rook_moves(g,"Black",v,r);
        Assert.AreEqual(r, exp);
    }

    //gera a matriz para o teste
    public GameObject[][] m04(){
        //criacao base da matriz
        GameObject[][] tabuleiro = new GameObject[8][];
        for(int i = 0; i < tabuleiro.Length; i++) {
			tabuleiro[i] = new GameObject[8];
            for(int j = 0; j < tabuleiro[i].Length; j++) {
                tabuleiro[i][j] = null;
            }
        }
        tabuleiro[0][0] = GameObject.CreatePrimitive(PrimitiveType.Cube);
        tabuleiro[0][0].name = "Black Rook";
        tabuleiro[0][1] = GameObject.CreatePrimitive(PrimitiveType.Cube);
        tabuleiro[0][1].name = "White Bishop 01";
        tabuleiro[2][0] = GameObject.CreatePrimitive(PrimitiveType.Cube);
        tabuleiro[2][0].name = "White Bishop 02";
        return tabuleiro;   
    }

    //uma posicao vazia e 2 inimigos
    [Test]
    public void TestRook02() {
        GameObject[][] g = m04();
        Vector2 v = new Vector2(0,0);
        System.Collections.Generic.List<Vector2> r = new System.Collections.Generic.List<Vector2>();
        System.Collections.Generic.List<Vector2> exp = new System.Collections.Generic.List<Vector2>();
        exp.Add(new Vector2(1,0));
        exp.Add(new Vector2(2,0));
        exp.Add(new Vector2(0,1));
        r = rook_moves(g,"White",v,r);
        Assert.AreEqual(r, exp);
    }

    //gera a matriz para o teste
    public GameObject[][] m05(){
        //criacao base da matriz
        GameObject[][] tabuleiro = new GameObject[8][];
        for(int i = 0; i < tabuleiro.Length; i++) {
			tabuleiro[i] = new GameObject[8];
            for(int j = 0; j < tabuleiro[i].Length; j++) {
                tabuleiro[i][j] = null;
            }
        }
        tabuleiro[0][0] = GameObject.CreatePrimitive(PrimitiveType.Cube);
        tabuleiro[0][0].name = "Black Rook";
        return tabuleiro;   
    }

    //linha e coluna vazias da torre
    [Test]
    public void TestRook03() {
        GameObject[][] g = m05();
        Vector2 v = new Vector2(0,0);
        System.Collections.Generic.List<Vector2> r = new System.Collections.Generic.List<Vector2>();
        System.Collections.Generic.List<Vector2> exp = new System.Collections.Generic.List<Vector2>();
        for (int i = 1; i < 8; i++){
            exp.Add(new Vector2(i,0));
        }
        for (int i = 1; i < 8; i++){
            exp.Add(new Vector2(0,i));
        }
        r = rook_moves(g,"White",v,r);
        Assert.AreEqual(r, exp);
    }

    //gera a matriz para o teste
    public GameObject[][] m06(){
        //criacao base da matriz
        GameObject[][] tabuleiro = new GameObject[8][];
        for(int i = 0; i < tabuleiro.Length; i++) {
			tabuleiro[i] = new GameObject[8];
            for(int j = 0; j < tabuleiro[i].Length; j++) {
                tabuleiro[i][j] = null;
            }
        }
        tabuleiro[0][0] = GameObject.CreatePrimitive(PrimitiveType.Cube);
        tabuleiro[0][0].name = "White Bishop";
        tabuleiro[2][2] = GameObject.CreatePrimitive(PrimitiveType.Cube);
        tabuleiro[2][2].name = "White Rook";
        return tabuleiro;   
    }

    //somente uma diagonal livre, bloqueado por uma peca do mesmo time
    [Test]
    public void TestBishop01() {
        GameObject[][] g = m06();
        Vector2 v = new Vector2(0,0);
        System.Collections.Generic.List<Vector2> r = new System.Collections.Generic.List<Vector2>();
        System.Collections.Generic.List<Vector2> exp = new System.Collections.Generic.List<Vector2>();
        exp.Add(new Vector2(1,1));
        r = bishop_moves(g,"Black",v,r);
        Assert.AreEqual(r, exp);
    }

    //gera a matriz para o teste
    public GameObject[][] m07(){
        //criacao base da matriz
        GameObject[][] tabuleiro = new GameObject[8][];
        for(int i = 0; i < tabuleiro.Length; i++) {
			tabuleiro[i] = new GameObject[8];
            for(int j = 0; j < tabuleiro[i].Length; j++) {
                tabuleiro[i][j] = null;
            }
        }
        tabuleiro[0][0] = GameObject.CreatePrimitive(PrimitiveType.Cube);
        tabuleiro[0][0].name = "White Bishop";
        tabuleiro[2][2] = GameObject.CreatePrimitive(PrimitiveType.Cube);
        tabuleiro[2][2].name = "Black Rook";
        return tabuleiro;   
    }

    //somente uma diagonal livre, bloqueado por uma peca do mesmo time
    [Test]
    public void TestBishop02() {
        GameObject[][] g = m07();
        Vector2 v = new Vector2(0,0);
        System.Collections.Generic.List<Vector2> r = new System.Collections.Generic.List<Vector2>();
        System.Collections.Generic.List<Vector2> exp = new System.Collections.Generic.List<Vector2>();
        exp.Add(new Vector2(1,1));
        exp.Add(new Vector2(2,2));
        r = bishop_moves(g,"Black",v,r);
        Assert.AreEqual(r, exp);
    }

    //gera a matriz para o teste
    public GameObject[][] m08(){
        //criacao base da matriz
        GameObject[][] tabuleiro = new GameObject[8][];
        for(int i = 0; i < tabuleiro.Length; i++) {
			tabuleiro[i] = new GameObject[8];
            for(int j = 0; j < tabuleiro[i].Length; j++) {
                tabuleiro[i][j] = null;
            }
        }
        tabuleiro[0][0] = GameObject.CreatePrimitive(PrimitiveType.Cube);
        tabuleiro[0][0].name = "White Bishop";
        return tabuleiro;   
    }

    //somente uma diagonal livre, bloqueado por uma peca do mesmo time
    [Test]
    public void TestBishop03() {
        GameObject[][] g = m08();
        Vector2 v = new Vector2(0,0);
        System.Collections.Generic.List<Vector2> r = new System.Collections.Generic.List<Vector2>();
        System.Collections.Generic.List<Vector2> exp = new System.Collections.Generic.List<Vector2>();
        for (int i = 1;i<8;i++){
            exp.Add(new Vector2(i,i));
        }
        r = bishop_moves(g,"Black",v,r);
        Assert.AreEqual(r, exp);
    }

    //gera a matriz para o teste
    public GameObject[][] m09(){
        //criacao base da matriz
        GameObject[][] tabuleiro = new GameObject[8][];
        for(int i = 0; i < tabuleiro.Length; i++) {
			tabuleiro[i] = new GameObject[8];
            for(int j = 0; j < tabuleiro[i].Length; j++) {
                tabuleiro[i][j] = null;
            }
        }
        tabuleiro[0][0] = GameObject.CreatePrimitive(PrimitiveType.Cube);
        tabuleiro[0][0].name = "White Knight";
        tabuleiro[2][1] = GameObject.CreatePrimitive(PrimitiveType.Cube);
        tabuleiro[2][1].name = "White Knight";
        return tabuleiro;   
    }

    //somente uma diagonal livre, bloqueado por uma peca do mesmo time
    [Test]
    public void TestKnight01() {
        GameObject[][] g = m09();
        Vector2 v = new Vector2(0,0);
        System.Collections.Generic.List<Vector2> r = new System.Collections.Generic.List<Vector2>();
        System.Collections.Generic.List<Vector2> exp = new System.Collections.Generic.List<Vector2>();
        exp.Add(new Vector2(1,2));
        r = knight_moves(g,"Black",v,r);
        Assert.AreEqual(r, exp);
    }

    //gera a matriz para o teste
    public GameObject[][] m10(){
        //criacao base da matriz
        GameObject[][] tabuleiro = new GameObject[8][];
        for(int i = 0; i < tabuleiro.Length; i++) {
			tabuleiro[i] = new GameObject[8];
            for(int j = 0; j < tabuleiro[i].Length; j++) {
                tabuleiro[i][j] = null;
            }
        }
        tabuleiro[0][0] = GameObject.CreatePrimitive(PrimitiveType.Cube);
        tabuleiro[0][0].name = "White Knight";
        tabuleiro[2][1] = GameObject.CreatePrimitive(PrimitiveType.Cube);
        tabuleiro[2][1].name = "Black Knight";
        return tabuleiro;   
    }

    //somente uma diagonal livre com um inimigo
    [Test]
    public void TestKnight02() {
        GameObject[][] g = m10();
        Vector2 v = new Vector2(0,0);
        System.Collections.Generic.List<Vector2> r = new System.Collections.Generic.List<Vector2>();
        System.Collections.Generic.List<Vector2> exp = new System.Collections.Generic.List<Vector2>();
        exp.Add(new Vector2(1,2));
        exp.Add(new Vector2(2,1));
        r = knight_moves(g,"Black",v,r);
        Assert.AreEqual(r, exp);
    }

    //gera a matriz para o teste
    public GameObject[][] m11(){
        //criacao base da matriz
        GameObject[][] tabuleiro = new GameObject[8][];
        for(int i = 0; i < tabuleiro.Length; i++) {
			tabuleiro[i] = new GameObject[8];
            for(int j = 0; j < tabuleiro[i].Length; j++) {
                tabuleiro[i][j] = null;
            }
        }
        tabuleiro[3][3] = GameObject.CreatePrimitive(PrimitiveType.Cube);
        tabuleiro[3][3].name = "White Knight";
        return tabuleiro;   
    }

    //cavalo no meio do tabuleiro com todos os movimentos possiveis
    [Test]
    public void TestKnight03() {
        GameObject[][] g = m11();
        Vector2 v = new Vector2(3,3);
        System.Collections.Generic.List<Vector2> r = new System.Collections.Generic.List<Vector2>();
        System.Collections.Generic.List<Vector2> exp = new System.Collections.Generic.List<Vector2>();
        exp.Add(new Vector2(4,5));
        exp.Add(new Vector2(4,1));
        exp.Add(new Vector2(2,5));
        exp.Add(new Vector2(2,1));
        exp.Add(new Vector2(5,4));
        exp.Add(new Vector2(5,2));
        exp.Add(new Vector2(1,4));
        exp.Add(new Vector2(1,2));
        r = knight_moves(g,"Black",v,r);
        Assert.AreEqual(r, exp);
    }

    //gera a matriz para o teste
    public GameObject[][] m12(){
        //criacao base da matriz
        GameObject[][] tabuleiro = new GameObject[8][];
        for(int i = 0; i < tabuleiro.Length; i++) {
			tabuleiro[i] = new GameObject[8];
            for(int j = 0; j < tabuleiro[i].Length; j++) {
                tabuleiro[i][j] = null;
            }
        }
        tabuleiro[0][0] = GameObject.CreatePrimitive(PrimitiveType.Cube);
        tabuleiro[0][0].name = "White King";
        tabuleiro[1][0] = GameObject.CreatePrimitive(PrimitiveType.Cube);
        tabuleiro[1][0].name = "White Queen 01";
        tabuleiro[0][1] = GameObject.CreatePrimitive(PrimitiveType.Cube);
        tabuleiro[0][1].name = "White Queen 02";
        return tabuleiro;   
    }

    //rei no canto com somente um movimento possivel
    [Test]
    public void TestKing01() {
        GameObject[][] g = m12();
        Vector2 v = new Vector2(0,0);
        System.Collections.Generic.List<Vector2> r = new System.Collections.Generic.List<Vector2>();
        System.Collections.Generic.List<Vector2> exp = new System.Collections.Generic.List<Vector2>();
        exp.Add(new Vector2(1,1));
        r = king_moves(g,"White","Black",v,r,false);
        Assert.AreEqual(r, exp);
    }

    public GameObject[][] m13(){
        //criacao base da matriz
        GameObject[][] tabuleiro = new GameObject[8][];
        for(int i = 0; i < tabuleiro.Length; i++) {
			tabuleiro[i] = new GameObject[8];
            for(int j = 0; j < tabuleiro[i].Length; j++) {
                tabuleiro[i][j] = null;
            }
        }
        tabuleiro[3][3] = GameObject.CreatePrimitive(PrimitiveType.Cube);
        tabuleiro[3][3].name = "White King";
        return tabuleiro;   
    }

    //rei no meio com todos os movimentos possiveis
    [Test]
    public void TestKing02() {
        GameObject[][] g = m13();
        Vector2 v = new Vector2(3,3);
        System.Collections.Generic.List<Vector2> r = new System.Collections.Generic.List<Vector2>();
        System.Collections.Generic.List<Vector2> exp = new System.Collections.Generic.List<Vector2>();
        exp.Add(new Vector2(4,3));
        exp.Add(new Vector2(4,4));
        exp.Add(new Vector2(4,2));
        exp.Add(new Vector2(2,3));
        exp.Add(new Vector2(2,4));
        exp.Add(new Vector2(2,2));
        exp.Add(new Vector2(3,4));
        exp.Add(new Vector2(3,2));
        r = king_moves(g,"White","Black",v,r,false);
        Assert.AreEqual(r, exp);
    }

    public GameObject[][] m14(){
        //criacao base da matriz
        GameObject[][] tabuleiro = new GameObject[8][];
        for(int i = 0; i < tabuleiro.Length; i++) {
			tabuleiro[i] = new GameObject[8];
            for(int j = 0; j < tabuleiro[i].Length; j++) {
                tabuleiro[i][j] = null;
            }
        }
        tabuleiro[1][0] = GameObject.CreatePrimitive(PrimitiveType.Cube);
        tabuleiro[1][0].name = "White Pawn 1";
        return tabuleiro;   
    }

    //peao que nunca andou
    [Test]
    public void TestPawn01() {
        GameObject[][] g = m14();
        Vector2 v = new Vector2(1,0);
        bool[] pb = {false,false,false,false,false,false,false,false};
        bool[] pp = {false,false,false,false,false,false,false,false};
        System.Collections.Generic.List<Vector2> r = new System.Collections.Generic.List<Vector2>();
        System.Collections.Generic.List<Vector2> exp = new System.Collections.Generic.List<Vector2>();
        exp.Add(new Vector2(2,0));
        exp.Add(new Vector2(3,0));
        r = pawn_moves(g,g[1][0],v,r,pb,pp);
        Assert.AreEqual(r, exp);
    }

    public GameObject[][] m15(){
        //criacao base da matriz
        GameObject[][] tabuleiro = new GameObject[8][];
        for(int i = 0; i < tabuleiro.Length; i++) {
			tabuleiro[i] = new GameObject[8];
            for(int j = 0; j < tabuleiro[i].Length; j++) {
                tabuleiro[i][j] = null;
            }
        }
        tabuleiro[2][0] = GameObject.CreatePrimitive(PrimitiveType.Cube);
        tabuleiro[2][0].name = "White Pawn 1";
        return tabuleiro;   
    }

    //peao que ja andou
    [Test]
    public void TestPawn02() {
        GameObject[][] g = m15();
        Vector2 v = new Vector2(2,0);
        bool[] pb = {true,false,false,false,false,false,false,false};
        bool[] pp = {false,false,false,false,false,false,false,false};
        System.Collections.Generic.List<Vector2> r = new System.Collections.Generic.List<Vector2>();
        System.Collections.Generic.List<Vector2> exp = new System.Collections.Generic.List<Vector2>();
        exp.Add(new Vector2(3,0));
        r = pawn_moves(g,g[2][0],v,r,pb,pp);
        Assert.AreEqual(r, exp);
    }

    public GameObject[][] m16(){
        //criacao base da matriz
        GameObject[][] tabuleiro = new GameObject[8][];
        for(int i = 0; i < tabuleiro.Length; i++) {
			tabuleiro[i] = new GameObject[8];
            for(int j = 0; j < tabuleiro[i].Length; j++) {
                tabuleiro[i][j] = null;
            }
        }
        tabuleiro[1][0] = GameObject.CreatePrimitive(PrimitiveType.Cube);
        tabuleiro[1][0].name = "White Pawn 1";
        tabuleiro[2][1] = GameObject.CreatePrimitive(PrimitiveType.Cube);
        tabuleiro[2][1].name = "Black Pawn 1";
        return tabuleiro;   
    }

    //peao que pode comer um
    [Test]
    public void TestPawn03() {
        GameObject[][] g = m16();
        Vector2 v = new Vector2(1,0);
        bool[] pb = {true,false,false,false,false,false,false,false};
        bool[] pp = {false,false,false,false,false,false,false,false};
        System.Collections.Generic.List<Vector2> r = new System.Collections.Generic.List<Vector2>();
        System.Collections.Generic.List<Vector2> exp = new System.Collections.Generic.List<Vector2>();
        exp.Add(new Vector2(2,0));
        exp.Add(new Vector2(2,1));
        r = pawn_moves(g,g[1][0],v,r,pb,pp);
        Assert.AreEqual(r, exp);
    }

    public GameObject[][] m17(){
        //criacao base da matriz
        GameObject[][] tabuleiro = new GameObject[8][];
        for(int i = 0; i < tabuleiro.Length; i++) {
			tabuleiro[i] = new GameObject[8];
            for(int j = 0; j < tabuleiro[i].Length; j++) {
                tabuleiro[i][j] = null;
            }
        }
        tabuleiro[1][1] = GameObject.CreatePrimitive(PrimitiveType.Cube);
        tabuleiro[1][1].name = "White Pawn 1";
        tabuleiro[2][0] = GameObject.CreatePrimitive(PrimitiveType.Cube);
        tabuleiro[2][0].name = "Black Pawn 1";
        tabuleiro[2][2] = GameObject.CreatePrimitive(PrimitiveType.Cube);
        tabuleiro[2][2].name = "Black Pawn 2";
        return tabuleiro;   
    }

    //peao que nunca andou e pode comer 2
    [Test]
    public void TestPawn04() {
        GameObject[][] g = m17();
        Vector2 v = new Vector2(1,1);
        bool[] pb = {false,false,false,false,false,false,false,false};
        bool[] pp = {false,false,false,false,false,false,false,false};
        System.Collections.Generic.List<Vector2> r = new System.Collections.Generic.List<Vector2>();
        System.Collections.Generic.List<Vector2> exp = new System.Collections.Generic.List<Vector2>();
        exp.Add(new Vector2(2,1));
        exp.Add(new Vector2(3,1));
        exp.Add(new Vector2(2,2));
        exp.Add(new Vector2(2,0));
        r = pawn_moves(g,g[1][1],v,r,pb,pp);
        Assert.AreEqual(r, exp);
    }



// ------------------------------------------ FIM DOS CASOS TESTE -----------------------------------------------------------


    private System.Collections.Generic.List<Vector2> pawn_moves(GameObject[][] tab, GameObject peca, Vector2 posPeca, System.Collections.Generic.List<Vector2> resultado, bool[] peoesBrancos, bool[] peoesPretos){
        // Movimento do peões
		if(peca.name.StartsWith("White Pawn")) {
			int numeroPeao = (int)peca.name.ToCharArray()[11] - 49;
			if((int)posPeca.x + 1 < tab.Length) {
				if(tab[(int)posPeca.x + 1][(int)posPeca.y] == null) {
					// Só pode andar pra frente se estiver vazio
					resultado.Add(new Vector2(posPeca.x + 1, posPeca.y));
					if(peca.name.StartsWith("White") && peoesBrancos[numeroPeao] == false && tab[(int)posPeca.x + 2][(int)posPeca.y] == null) {
						resultado.Add(new Vector2(posPeca.x + 2, posPeca.y));
					} 
				}
				if((int)posPeca.y + 1 < tab.Length && 
					tab[(int)posPeca.x + 1][(int)posPeca.y + 1] != null && 
					tab[(int)posPeca.x + 1][(int)posPeca.y + 1].name.StartsWith("Black")) {
					// Comer para diagonal esquerda
					resultado.Add(new Vector2(posPeca.x + 1, posPeca.y + 1));
				}
				if((int)posPeca.y - 1 >= 0 && 
					tab[(int)posPeca.x + 1][(int)posPeca.y - 1] != null && 
					tab[(int)posPeca.x + 1][(int)posPeca.y - 1].name.StartsWith("Black")) {
					// Comer para diagonal direita
					resultado.Add(new Vector2(posPeca.x + 1, posPeca.y - 1));
				}
			}
			
		} else if(peca.name.StartsWith("Black Pawn")) {
			int numeroPeao = (int)peca.name.ToCharArray()[11] - 49;
			if((int)posPeca.x - 1 >= 0) {
				if(tab[(int)posPeca.x - 1][(int)posPeca.y] == null) {
					// Só pode andar pra frente se estiver vazio
					resultado.Add(new Vector2(posPeca.x - 1, posPeca.y));
					if(peca.name.StartsWith("Black") && peoesPretos[numeroPeao] == false && tab[(int)posPeca.x - 2][(int)posPeca.y] == null) {
						resultado.Add(new Vector2(posPeca.x - 2, posPeca.y));
					} 
				}
				if((int)posPeca.y + 1 < tab.Length && 
					tab[(int)posPeca.x - 1][(int)posPeca.y + 1] != null && 
					tab[(int)posPeca.x - 1][(int)posPeca.y + 1].name.StartsWith("White")) {
					// Comer para diagonal direita em relação as peças pretas
					resultado.Add(new Vector2(posPeca.x - 1, posPeca.y + 1));
				}
				if((int)posPeca.y - 1 >= 0 && 
					tab[(int)posPeca.x - 1][(int)posPeca.y - 1] != null && 
					tab[(int)posPeca.x - 1][(int)posPeca.y - 1].name.StartsWith("White")) {
					// Comer para diagonal esquerda em relação as peças pretas
					resultado.Add(new Vector2(posPeca.x - 1, posPeca.y - 1));
				} 
			}
		}
        return resultado;
    }

    private System.Collections.Generic.List<Vector2> king_moves(GameObject[][] tabuleiro, string color, string adv_color, Vector2 posPeca, System.Collections.Generic.List<Vector2> resultado, bool definitivo){
        int xPeca = (int)posPeca.x, yPeca = (int)posPeca.y;
		if(xPeca + 1 < tabuleiro.Length) {
			// Pode subir
			if(tabuleiro[xPeca + 1][yPeca] == null || 
			(tabuleiro[xPeca + 1][yPeca].name.StartsWith(adv_color))) {
				// Para cima na matriz está vazio ou contem peça inimiga
				resultado.Add(new Vector2(xPeca + 1, yPeca));
			}
			if(yPeca + 1 < tabuleiro.Length && 
			(tabuleiro[xPeca + 1][yPeca + 1] == null || 
			(tabuleiro[xPeca + 1][yPeca + 1].name.StartsWith(adv_color)))) {
				// Para cima e para esquerda em relação a matriz está vazio ou contem peça inimiga
				resultado.Add(new Vector2(xPeca + 1, yPeca + 1));
			}
			if(yPeca - 1 >= 0 && 
			(tabuleiro[xPeca + 1][yPeca - 1] == null || 
			(tabuleiro[xPeca + 1][yPeca - 1].name.StartsWith(adv_color)))) {
				// Para cima e para direita em relação a matriz está vazio ou contem peça inimiga
				resultado.Add(new Vector2(xPeca + 1, yPeca - 1));
			}
		}
		if(xPeca - 1 >= 0) {
			// Pode descer
			if(tabuleiro[xPeca - 1][yPeca] == null || 
			(tabuleiro[xPeca - 1][yPeca].name.StartsWith(adv_color))) {
				// Para baixo na matriz está vazio ou contem peça inimiga
				resultado.Add(new Vector2(xPeca - 1, yPeca));
			}
			if(yPeca + 1 < tabuleiro.Length && 
			(tabuleiro[xPeca - 1][yPeca + 1] == null || 
			(tabuleiro[xPeca - 1][yPeca + 1].name.StartsWith(adv_color)))) {
				// Para baixo e para esquerda em relação a matriz está vazio ou contem peça inimiga
				resultado.Add(new Vector2(xPeca - 1, yPeca + 1));
			}
			if(yPeca - 1 >= 0 && 
			(tabuleiro[xPeca - 1][yPeca - 1] == null || 
			(tabuleiro[xPeca - 1][yPeca - 1].name.StartsWith(adv_color)))) {
				// Para baixo e para direita em relação a matriz está vazio ou contem peça inimiga
				resultado.Add(new Vector2(xPeca - 1, yPeca - 1));
			}
		}
		if(yPeca + 1 < tabuleiro.Length &&
			(tabuleiro[xPeca][yPeca + 1] == null|| 
			(tabuleiro[xPeca][yPeca + 1].name.StartsWith(adv_color)))) {
				// Para esquerda em relação a matriz está vazio ou contem peça inimiga
				resultado.Add(new Vector2(xPeca, yPeca + 1));
		}
		if(yPeca - 1 >= 0 &&
			(tabuleiro[xPeca][yPeca - 1] == null || 
			(tabuleiro[xPeca][yPeca - 1].name.StartsWith(adv_color)))) {
				// Para direita em relação a matriz está vazio ou contem peça inimiga
				resultado.Add(new Vector2(xPeca, yPeca - 1));
		}
		return resultado;
	}

    private System.Collections.Generic.List<Vector2> rook_moves(GameObject[][] tabuleiro, string adv_color, Vector2 posPeca, System.Collections.Generic.List<Vector2> resultado){
		// Os 2 'for' seguintes são para subir e descer no tabuleiro
		for(int i = (int)posPeca.x + 1; i < tabuleiro.Length; i++) {
			if(tabuleiro[i][(int)posPeca.y] == null ) {
				//Se o lugar ta vazio pode mover
				resultado.Add(new Vector2(i, (int)posPeca.y));
			} else if (tabuleiro[i][(int)posPeca.y].name.StartsWith(adv_color)) {
				// Se o lugar possui uma peça preta pode mover, mas se sabe que a posições seguintes são invalidas
				resultado.Add(new Vector2(i, (int)posPeca.y));
				break;
			} else {
				// Se o lugar não ta vazio e nem é uma peça preta significa que é uma peça branca, logo não pode mover
				break;
			}
		}
		for(int i = (int)posPeca.x - 1; i >= 0; i--) {
			if(tabuleiro[i][(int)posPeca.y] == (null)) {
				// Se o lugar ta vazio pode mover
				resultado.Add(new Vector2(i, (int)posPeca.y));
			} else if (tabuleiro[i][(int)posPeca.y].name.StartsWith(adv_color)) {
				// Se o lugar possui uma peça preta pode mover, mas se sabe que as posições seguintes são invalidas
				resultado.Add(new Vector2(i, (int)posPeca.y));
				break;
			} else {
				// Se o lugar não ta vazio e nem é uma peça preta significa que é uma peça branca, logo não pode mover
				break;
			}
		}

		// Os proximos dois 'for' são para os movimentos laterais
		for(int i = (int)posPeca.y + 1; i < tabuleiro[(int)posPeca.x].Length; i++) {
			if(tabuleiro[(int)posPeca.x][i] == (null)) {
				//Se o lugar ta vazio pode mover
				resultado.Add(new Vector2((int)posPeca.x, i));
			} else if(tabuleiro[(int)posPeca.x][i].name.StartsWith(adv_color)) {
				// Se o lugar possui uma peça preta pode mover, mas se sabe que as posições seguintes são invalidas
				resultado.Add(new Vector2((int)posPeca.x, i));
				break;
			} else {
				// Se o lugar não ta vazio e nem é uma peça preta significa que é uma peça branca, logo não pode mover
				break;
			}
		}
		for(int i = (int)posPeca.y - 1; i >= 0; i--) {
			if(tabuleiro[(int)posPeca.x][i] == (null)) {
				//Se o lugar ta vazio pode mover
				resultado.Add(new Vector2((int)posPeca.x, i));
			} else if(tabuleiro[(int)posPeca.x][i].name.StartsWith(adv_color)) {
				// Se o lugar possui uma peça preta pode mover, mas se sabe que as posições seguintes são invalidas
				resultado.Add(new Vector2((int)posPeca.x, i));
				break;
			} else {
				// Se o lugar não ta vazio e nem é uma peça preta significa que é uma peça branca, logo não pode mover
				break;
			}
		}
		return resultado;
	}

    private System.Collections.Generic.List<Vector2> knight_moves(GameObject[][] tabuleiro, string adv_color, Vector2 posPeca, System.Collections.Generic.List<Vector2> resultado){
		int xPeca = (int)posPeca.x, yPeca = (int)posPeca.y;
		//Debug.Log(peca.name + ": (" + xPeca + "," + yPeca + ")");
		for(int i = 1; i < 3; i++) {
			for(int j = 1; j < 3; j++) {
				if(i == j) continue;
				if(xPeca + i < tabuleiro.Length) {
					// Pode subir
					if(yPeca + j < tabuleiro.Length) {
						// Pode ir para a esquerda da matriz
						if((tabuleiro[xPeca + i][yPeca + j] == null) || 
							tabuleiro[xPeca + i][yPeca + j].name.StartsWith(adv_color)) {
							// Se a peça clicada é branca e a posição desejada contem uma peça preta ou nenhuma peça
							resultado.Add(new Vector2(xPeca + i, yPeca + j));
						}
					}
					if(yPeca - j >= 0) {
						// Pode ir para a direita da matriz
						if((tabuleiro[xPeca + i][yPeca - j] == null) || 
							tabuleiro[xPeca + i][yPeca - j].name.StartsWith(adv_color)) {
							// Se a peça clicada é branca e a posição desejada contem uma peça preta ou nenhuma peça
							resultado.Add(new Vector2(xPeca + i, yPeca - j));
						}
					}
				}
				if(xPeca - i >= 0) {
					// Pode descer!! LEMBRANDO: SEMPRE EM RELAÇÃO A MATRIZ
					if(yPeca + j < tabuleiro.Length) {
						// Pode ir para a esquerda da matriz
						if((tabuleiro[xPeca - i][yPeca + j] == null) || 
							tabuleiro[xPeca - i][yPeca + j].name.StartsWith(adv_color)) {
							// Se a peça clicada é branca e a posição desejada contem uma peça preta ou nenhuma peça
							resultado.Add(new Vector2(xPeca - i, yPeca + j));
						}
					}
					if(yPeca - j >= 0) {
						// Pode ir para a direita da matriz
						if((tabuleiro[xPeca - i][yPeca - j] == null) || 
							tabuleiro[xPeca - i][yPeca - j].name.StartsWith(adv_color)) {
							// Se a peça clicada é branca e a posição desejada contem uma peça preta ou nenhuma peça
							resultado.Add(new Vector2(xPeca - i, yPeca - j));
						}
					}
				}
			}
		}
		return resultado;
	}
    
    private System.Collections.Generic.List<Vector2> bishop_moves(GameObject[][] tabuleiro, string adv_color, Vector2 posPeca, System.Collections.Generic.List<Vector2> resultado){
		bool parouDir = false, parouEsq = false;
		// Primeiro 'for' é para os bispos brancos subirem no tabuleiro
		int j = 1;
		for(int i = (int)posPeca.x + 1; i < tabuleiro.Length; i++) {
			if(!parouEsq || !parouDir) {
				// Se entrou aqui ainda da pra subir pra algum lado

				// Os 'ifs' abaixo são pra saber se saiu do tabuleiro
				if((int)posPeca.y + j >= tabuleiro.Length) {
					parouEsq = true;
				}
				if((int)posPeca.y - j < 0) {
					parouDir = true;
				}

				if(!parouEsq) {
					// Se entrou aqui ainda da pra subir pra esquerda
					if(tabuleiro[i][(int)posPeca.y + j] == null) {
						// Se o lugar ta vazio
						resultado.Add(new Vector2(i, (int)posPeca.y + j));
					} else if(tabuleiro[i][(int)posPeca.y + j].name.StartsWith(adv_color)) {
						// Se o lugar possui uma peça preta pode mover, mas se sabe as posições seguintes são invalidas
						resultado.Add(new Vector2(i, (int)posPeca.y + j));
						parouEsq = true;
					} else {
						parouEsq = true;
					} 
				}
				if(!parouDir) {
					// Se entrou aqui ainda da pra subir pra direita
					if(tabuleiro[i][(int)posPeca.y - j] == null) {
						// Se o lugar ta vazio
						resultado.Add(new Vector2(i, (int)posPeca.y - j));
					} else if(tabuleiro[i][(int)posPeca.y - j].name.StartsWith(adv_color)) {
						// Se o lugar possui uma peça preta pode mover, mas se sabe as posições seguintes são invalidas
						resultado.Add(new Vector2(i, (int)posPeca.y - j));
						parouDir = true;
					} else {
						parouDir = true;
					} 
				}
				j++;
			} else {
				// parouEsq && parouDir
				break;
			}
		}
		parouDir = false; parouEsq = false;
		//Segundo for é para os bispos brancos descerem no tabuleiro
		j = 1;
		for(int i = (int)posPeca.x - 1; i >= 0; i--) {
			if(!parouDir || !parouEsq) {
				// Se entrou aqui ainda da para descer para algum lado

				// Os 'ifs' abaixo são pra saber se saiu do tabuleiro
				if((int)posPeca.y + j >= tabuleiro.Length) {
					parouEsq = true;
				}
				if((int)posPeca.y - j < 0) {
					parouDir = true;
				}
				if(!parouEsq) {
					// Se entrou aqui ainda da pra descer pra esquerda
					if(tabuleiro[i][(int)posPeca.y + j] == null) {
						// Se o lugar ta vazio
						resultado.Add(new Vector2(i, (int)posPeca.y + j));
					} else if(tabuleiro[i][(int)posPeca.y + j].name.StartsWith(adv_color)) {
						// Se o lugar possui uma peça preta pode mover, mas se sabe as posições seguintes são invalidas
						resultado.Add(new Vector2(i, (int)posPeca.y + j));
						parouEsq = true;
					} else {
						parouEsq = true;
					} 
				}
				if(!parouDir) {
					// Se entrou aqui ainda da pra descer pra direita
					if(tabuleiro[i][(int)posPeca.y - j] == null) {
						// Se o lugar ta vazio
						resultado.Add(new Vector2(i, (int)posPeca.y - j));
					} else if(tabuleiro[i][(int)posPeca.y - j].name.StartsWith(adv_color)) {
						// Se o lugar possui uma peça preta pode mover, mas se sabe as posições seguintes são invalidas
						resultado.Add(new Vector2(i, (int)posPeca.y - j));
						parouDir = true;
					} else {
						parouDir = true;
					} 
				}
				j++;
			} else {
				// parouEsq && parouDir
				break;
			}
		}
		return resultado;
	}

    public int NumPecasTime(GameObject[][]tab, string time){
		int countPeca = 0;
		for(int i=0; i< tab.Length;i++){
			for(int j=0;j<tab.Length;j++){
				if(tab[i][j]!= null){
					GameObject peca = tab[i][j];
					if(peca.name.StartsWith(time)){
						countPeca+=1;
					}
				}
			}
		}
		return countPeca;
	}
}
