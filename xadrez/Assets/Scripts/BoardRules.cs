using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardRules : MonoBehaviour {

	public GameObject brancas;
	public GameObject pretas;

	// false se nunca andou, true se já
	// peão n posição 0 = peão 1. peão na posição 1 = peão 2 e por ai vai
	public bool[] peoesPretos;
	public bool[] peoesBrancos;

	// posição 0 é o rei branco, posição 1 é o rei preto
	// false se nunca andou, true se já
	public bool[] reiAndou;
	// posição 0 é a torre mais perto, posilão 1 é torre mais longa
	public bool[] torresBrancas;
	public bool[] torresPretas;

	private GameObject[][] tabuleiro;
	private Vector2 reiBranco;
	private Vector2 reiPreto;
	private bool reiBrancoEmCheck;
	private bool reiPretoEmCheck;
	Vector2 posPeca = new Vector2(), pretaEP = new Vector2(-1,-1), brancaEP = new Vector2(-1,-1);
	private GameManager gm;
	private bool roque;

	private int qtdBrancas;
	private int qtdPretas;
	
	// Use this for initialization
	void Start () {
		gm = FindObjectOfType(typeof(GameManager)) as GameManager;
		tabuleiro = new GameObject[8][];
		reiBrancoEmCheck = false;
		reiPretoEmCheck = false;
		peoesBrancos = new bool[8];
		peoesPretos = new bool[8];
		reiAndou = new bool[2];
		torresBrancas = new bool[2];
		torresPretas = new bool[2];
		qtdBrancas = qtdPretas = 16;
		for(int i = 0; i < tabuleiro.Length; i++) {
			tabuleiro[i] = new GameObject[8];
			for(int j = 0; j < tabuleiro[i].Length; j++) {
				
				if(i == 1) {
					string nome = "White Pawn ";
					nome += (j+1).ToString();
					tabuleiro[i][j] = brancas.transform.Find(nome).gameObject;
				} else if(i == 6) {
					string nome = "Black Pawn ";
					nome += (j+1).ToString();
					tabuleiro[i][j] = pretas.transform.Find(nome).gameObject;
				} else if(i == 0) {
					switch(j) {
						case 0:
							tabuleiro[i][j] = brancas.transform.Find("White Rook 2").gameObject;
							break;
						case 1:
							tabuleiro[i][j] = brancas.transform.Find("White Knight 2").gameObject;
							break;
						case 2:
							tabuleiro[i][j] = brancas.transform.Find("White Bishop 2").gameObject;
							break;
						case 3:
							tabuleiro[i][j] = brancas.transform.Find("White King").gameObject;
							reiBranco = new Vector2(i, j);
							break;
						case 4:
							tabuleiro[i][j] = brancas.transform.Find("White Queen").gameObject;
							break;
						case 5:
							tabuleiro[i][j] = brancas.transform.Find("White Bishop 1").gameObject;
							break;
						case 6:
							tabuleiro[i][j] = brancas.transform.Find("White Knight 1").gameObject;
							break;
						case 7: 
							tabuleiro[i][j] = brancas.transform.Find("White Rook 1").gameObject;
							break;
					}
				} else if(i == 7) {
					switch(j) {
						case 0:
							tabuleiro[i][j] = pretas.transform.Find("Black Rook 2").gameObject;
							break;
						case 1:
							tabuleiro[i][j] = pretas.transform.Find("Black Knight 2").gameObject;
							break;
						case 2:
							tabuleiro[i][j] = pretas.transform.Find("Black Bishop 2").gameObject;
							break;
						case 3:
							tabuleiro[i][j] = pretas.transform.Find("Black King").gameObject;
							reiPreto = new Vector2(i, j);
							break;
						case 4:
							tabuleiro[i][j] = pretas.transform.Find("Black Queen").gameObject;
							break;
						case 5:
							tabuleiro[i][j] = pretas.transform.Find("Black Bishop 1").gameObject;
							break;
						case 6:
							tabuleiro[i][j] = pretas.transform.Find("Black Knight 1").gameObject;
							break;
						case 7: 
							tabuleiro[i][j] = pretas.transform.Find("Black Rook 1").gameObject;
							break;
					}
				} else {
					tabuleiro[i][j] = null;

				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public bool getRoque() {
		return this.roque;
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
	public List<Vector2> MovimentosPossiveis(GameObject peca) {
		return MovimentosPossiveis(this.tabuleiro, peca);
	}

	private List<Vector2> rook_moves(GameObject[][] tabuleiro, string adv_color, Vector2 posPeca, List<Vector2> resultado){
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

	private List<Vector2> bishop_moves(GameObject[][] tabuleiro, string adv_color, Vector2 posPeca, List<Vector2> resultado){
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

	private List<Vector2> knight_moves(GameObject[][] tabuleiro, string adv_color, Vector2 posPeca, List<Vector2> resultado){
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

	private List<Vector2> king_moves(GameObject[][] tabuleiro, string color, string adv_color, Vector2 posPeca, List<Vector2> resultado, bool definitivo){
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
		// condições para o roque
		if((color.Equals("White") && reiAndou[0] == false) || (color.Equals("Black") && reiAndou[1] == false))  {
			// rei não andou
			if((torresBrancas[0] == false || torresPretas[0] == false) && yPeca - 1 >= 0 && tabuleiro[xPeca][yPeca - 1] == null && yPeca - 2 >= 0 && tabuleiro[xPeca][yPeca - 2] == null) {
				// rei pode fazer o roque com a torre mais proxima
				resultado.Add(new Vector2(xPeca, yPeca - 2));
				if(definitivo) {
					this.roque = true;
				}
			} else if((torresBrancas[1] == false || torresPretas[1] == false) && yPeca + 1 < tabuleiro.Length && tabuleiro[xPeca][yPeca + 1] == null && yPeca + 2 < tabuleiro.Length && tabuleiro[xPeca][yPeca + 2] == null  && yPeca + 3 < tabuleiro.Length && tabuleiro[xPeca][yPeca + 3] == null) {
				// rei pode fazer o roque com a torre mais longe
				resultado.Add(new Vector2(xPeca, yPeca + 2));
				if(definitivo) {
					this.roque = true;
				}
			}
		}
		return resultado;
	}

	private List<Vector2> queen_moves(GameObject[][] tabuleiro, string adv_color, Vector2 posPeca, List<Vector2> resultado) {
		resultado = bishop_moves(tabuleiro, adv_color, posPeca, resultado);
		resultado = rook_moves(tabuleiro, adv_color, posPeca, resultado);
		return resultado;
	}

	public List<Vector2> MovimentosPossiveis(GameObject[][] tabuleiro, GameObject peca) {
		Vector2 aux = new Vector2();
		for(int i = 0; i < tabuleiro.Length; i++) {
			for(int j = 0; j < tabuleiro[i].Length; j++) {
				//acha a peça
				if(peca.Equals(tabuleiro[i][j])) {
					aux.x = i;
					aux.y = j;
				}
			}
		} 
		return MovimentosPossiveis(tabuleiro, peca, aux, true);
	}
	public List<Vector2> MovimentosPossiveis(GameObject[][] tab, GameObject peca, Vector2 pos, bool definitivo) {
		return MovimentosPossiveis(tab, peca, pos, definitivo, false);
	}
	public List<Vector2> MovimentosPossiveis(GameObject[][] tab, GameObject peca, Vector2 pos, bool definitivo, bool checkmate) {
		if(definitivo) {
			this.roque = false;
		}
		posPeca = pos;
		List<Vector2> resultado = new List<Vector2>();
		if(tab == null) {
			Debug.Log("tab == null em MovimentosPossiveis()");
		}

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
					(tab[(int)posPeca.x + 1][(int)posPeca.y + 1].name.StartsWith("Black") || tab[(int)posPeca.x + 1][(int)posPeca.y + 1].name.StartsWith("Ghost"))) {
					// Comer para diagonal esquerda
					resultado.Add(new Vector2(posPeca.x + 1, posPeca.y + 1));
				}
				if((int)posPeca.y - 1 >= 0 && 
					tab[(int)posPeca.x + 1][(int)posPeca.y - 1] != null && 
					(tab[(int)posPeca.x + 1][(int)posPeca.y - 1].name.StartsWith("Black") || tab[(int)posPeca.x + 1][(int)posPeca.y - 1].name.StartsWith("Ghost"))) {
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
					(tab[(int)posPeca.x - 1][(int)posPeca.y + 1].name.StartsWith("White") || tab[(int)posPeca.x - 1][(int)posPeca.y + 1].name.StartsWith("Ghost"))) {
					// Comer para diagonal direita em relação as peças pretas
					resultado.Add(new Vector2(posPeca.x - 1, posPeca.y + 1));
				}
				if((int)posPeca.y - 1 >= 0 && 
					tab[(int)posPeca.x - 1][(int)posPeca.y - 1] != null && 
					(tab[(int)posPeca.x - 1][(int)posPeca.y - 1].name.StartsWith("White") || tab[(int)posPeca.x - 1][(int)posPeca.y - 1].name.StartsWith("Ghost"))) {
					// Comer para diagonal esquerda em relação as peças pretas
					resultado.Add(new Vector2(posPeca.x - 1, posPeca.y - 1));
				} 
			}
		}

		// Movimento das torres
		else if(peca.name.StartsWith("White Rook")) {
			resultado = rook_moves(tab, "Black", posPeca, resultado);
		} else if(peca.name.StartsWith("Black Rook")) {
			resultado = rook_moves(tab, "White", posPeca, resultado);
		}

		// Movimento dos bispos
		else if(peca.name.StartsWith("White Bishop")) {
			resultado = bishop_moves(tab, "Black", posPeca, resultado);
		} else if(peca.name.StartsWith("Black Bishop")) {
			resultado = bishop_moves(tab, "White", posPeca, resultado);
		}

		// Movimento dos cavalos
		else if(peca.name.Contains("Knight")) {
			if (peca.name.StartsWith("White")){
				resultado = knight_moves(tab, "Black", posPeca, resultado);
			}
			else{
				resultado = knight_moves(tab, "White", posPeca, resultado);
			}
		}

		// Movimento do Rei
		else if(peca.name.Contains("King")) {
			if (peca.name.StartsWith("White")){
				resultado = king_moves(tab, "White", "Black", posPeca, resultado, definitivo);
			}
			else{
				resultado = king_moves(tab, "Black", "White", posPeca, resultado, definitivo);
			}
		}

		// Movimento da Rainha
		else if(peca.name.Contains("Queen")) {
			if (peca.name.StartsWith("White")){
				resultado = queen_moves(tab, "Black", posPeca, resultado);
			}
			else{
				resultado = queen_moves(tab, "White", posPeca, resultado);
			}
		}
		if(definitivo || checkmate) {
			if(peca.name.StartsWith("White")) {
				resultado = FiltrarMovimentos(tab, "Black", posPeca, resultado);
			} else {
				resultado = FiltrarMovimentos(tab, "White", posPeca, resultado);
			}
		}
		return resultado;
	}

	// pos é a posição da peça
	public List<Vector2> FiltrarMovimentos(GameObject[][] tab, string adv_color, Vector2 pos, List<Vector2> mov) {
		List<Vector2> mov_aux;
		int contPecas = 0;
		Vector2 posRei = new Vector2();
		bool xeque = false;
		List<Vector2> resp = new List<Vector2>();
		bool ehORei = tab[(int)pos.x][(int)pos.y].name.Contains("King");
		
		if (!ehORei) {
			if(adv_color.Equals("White")) {
				posRei = reiPreto;
			} else {
				posRei = reiBranco;
			}
		}

		foreach (Vector2 m in mov) {
			contPecas = 0;
			GameObject[][] tab_aux = copiar(tab);
			tab_aux = Mover_tab_aux(tab_aux, pos, m);
			if(ehORei) {
				posRei.x = m.x;
				posRei.y = m.y;
			}
			xeque = false;
			for(int i = 0; i < tab.Length; i++) {
				if(adv_color.Equals("White") && contPecas == qtdBrancas) {break;}
				else if(adv_color.Equals("Pretas") && contPecas == qtdPretas) {break;}
				for(int j = 0; j < tab.Length; j++) {
					if(adv_color.Equals("White") && contPecas == qtdBrancas) {break;}
					else if(adv_color.Equals("Pretas") && contPecas == qtdPretas) {break;}
					else if(tab_aux[i][j] && tab_aux[i][j].name.StartsWith(adv_color)) {
						contPecas++;
						mov_aux = MovimentosPossiveis(tab_aux, tab_aux[i][j], new Vector2(i, j), false);
						foreach (Vector2 ma in mov_aux) {
							if((int)ma.x == (int)posRei.x && (int)ma.y == (int)posRei.y) {
								/* 
									Se entrou aqui significa que com o movimento 'm' da peca original, a peça adv na posição 
									(i,j) conseguiu chegar no rei, então esse movimento m é invalido e precisa ser removido.
									Não há mais necessidade de checar -> cair fora do loop e testar o proximo movimento.
								*/
								// Debug.Log("IR PARA " + m + " DA XEQUE PELA " + tab_aux[i][j]);
								// Debug.Log("POR CAUSA DO MOVIMENTO " + ma);
								xeque = true;
								break;
							}
						}
					}
					if(xeque) break;
				}
				if(xeque) break;
			}
			if(!xeque) {
				resp.Add(m);
			}			
		}
		return resp;
	}
	private void imprimir(GameObject[][] t) {
		string resp = "";
		for (int i = 0; i < t.Length; i++) {
			for (int j = 0; j < t.Length; j++) {
				if(t[i][j])
					resp += " || " + t[i][j].name ;
				else 
					resp += "|| null ";
			}
			resp += "\n";
		}
		Debug.Log(resp);
	}

	public bool checkmate(GameObject[][] t, string cor) {
		List<Vector2> aux;
		for (int i = 0; i < t.Length; i++) {
			for (int j = 0; j < t.Length; j++) {
				if(t[i][j] && t[i][j].name.StartsWith(cor)) {
					aux = MovimentosPossiveis(t, t[i][j], new Vector2(i, j), false, true);
					if(aux.Count > 0) return false;
				}
			}
		}
		return true;
	}

	public GameObject[][] Mover_tab_aux(GameObject[][] tab, Vector2 posDaPeca, Vector2 posDestino) {
		return AtualizaPosicoes(tab, posDaPeca, posDestino, false);
	}
	public GameObject[][] AtualizaPosicoes(GameObject peca, Vector2 pos) {
		return AtualizaPosicoes(this.tabuleiro, peca, pos, true);
	}

	public GameObject[][] AtualizaPosicoes(GameObject[][] tab, GameObject peca, Vector2 pos, bool definitivo){
		Vector2 pp = new Vector2();
		for (int i = 0; i < tab.Length; i++){
			for (int j = 0; j < tab.Length; j++){
				if(tab[i][j] && tab[i][j].name.Equals(peca.name)) {
					pp.x = i;
					pp.y = j;
				}
			}
		}
		return AtualizaPosicoes(tab, pp, pos, definitivo);
	}

	public GameObject[][] AtualizaPosicoes(GameObject[][] tab, Vector2 posPeca, Vector2 posDestino, bool definitivo) {
		if(definitivo && tab[(int)posDestino.x][(int)posDestino.y]) {
			if(tab[(int)posDestino.x][(int)posDestino.y].name.StartsWith("White")) {
				this.qtdBrancas--;
				Destroy(tab[(int)posDestino.x][(int)posDestino.y]);
			} else if(tab[(int)posDestino.x][(int)posDestino.y].name.StartsWith("Black")) {
				this.qtdPretas--;
				Destroy(tab[(int)posDestino.x][(int)posDestino.y]);
			} else {
				if(tab[(int)posPeca.x][(int)posPeca.y].name.StartsWith("White")) {
					Destroy(tab[(int)brancaEP.x-1][(int)brancaEP.y]);
				} else {
					Destroy(tab[(int)pretaEP.x+1][(int)pretaEP.y]);
				}	
			}
		}
		GameObject peca = tab[(int)posPeca.x][(int)posPeca.y].gameObject;
		if(pretaEP.x != -1) {
			// elimina o fantasma
			tab[(int)pretaEP.x][(int)pretaEP.y] = null;
			if((int)posDestino.x == (int)pretaEP.x && (int)posDestino.y == (int)pretaEP.y) {
				// elimina a peça
				tab[(int)pretaEP.x+1][(int)pretaEP.y] = null;
			}
			if(definitivo) {
				// remove o direito da preta de fazer o enpassant
				pretaEP.x = -1;
				pretaEP.y = -1;
			}
		} else if(brancaEP.x != -1) {
			// elimina o fantasma
			tab[(int)brancaEP.x][(int)brancaEP.y] = null;
			if((int)posDestino.x == (int)brancaEP.x && (int)posDestino.y == (int)brancaEP.y) {
				// elimina a peça
				tab[(int)brancaEP.x-1][(int)brancaEP.y] = null;
			}
			if(definitivo) {
				// remove o direito da branca de fazer o enpassant
				brancaEP.x = -1;
				brancaEP.y = -1;
			}
		}
		
		if(peca.tag == "whitePiece") {
			tab[(int)posDestino.x][(int)posDestino.y] = brancas.transform.Find(peca.name).gameObject;
			if(definitivo && peca.name.Contains("Pawn")) {
				if(Mathf.Abs(posDestino.x - posPeca.x) == 2) {
					// se a peca que você ta movendo é um peão e vc quer andar dois pra frente
					pretaEP = new Vector2(posDestino.x-1, posDestino.y);
					// da o direito da preta fazer o enpassant
					tab[(int)pretaEP.x][(int)pretaEP.y] = brancas.transform.Find("Ghost").gameObject;
					// ^ coloca um "fantasma" na posição que precisa ser limpo na proxima rodada
				}
				int numeroPeao = (int)peca.name.ToCharArray()[11] - 49;
				peoesBrancos[numeroPeao] = true;
			} else if(definitivo && peca.name.Equals("White Rook 2")) {
				// torre mais proxima do rei branco
				torresBrancas[0] = true;
			} else if(definitivo && peca.name.Equals("White Rook 1")) {
				// torre mais longe do rei branco
				torresBrancas[1] = true;
			} else if(peca.name.Contains("King")) {
				// rei branco andou
				if(Mathf.Abs((int)posDestino.y - (int)posPeca.y) >= 2) {
					if((int)posDestino.y > (int)posPeca.y && tab[(int)posPeca.x][tab.Length - 1] && tab[(int)posPeca.x][tab.Length - 1].name.Contains("White Rook")) {
						// significa que está fazendo o roque longo
						tab[(int)posPeca.x][(int)posPeca.y + 1] = brancas.transform.Find(tab[(int)posPeca.x][tab.Length - 1].name).gameObject;
						tab[(int)posPeca.x][tab.Length - 1] = null;
						// torre mais longe do rei branco
						if(definitivo) {
							torresBrancas[1] = true;	
						}
						
					} else if(tab[(int)posPeca.x][0] && tab[(int)posPeca.x][0].name.Contains("White Rook")){
						// significa que está fazendo o roque curto
						tab[(int)posPeca.x][(int)posPeca.y - 1] = brancas.transform.Find(tab[(int)posPeca.x][0].name).gameObject;
						tab[(int)posPeca.x][0] = null;
						// torre mais perto do rei branco
						if(definitivo) {
							torresBrancas[0] = true;	
						}	
					}
				}
				if(definitivo) {
					reiBranco.x = posDestino.x;
					reiBranco.y = posDestino.y;
					reiAndou[0] = true;
				}
			}
		} else {
			tab[(int)posDestino.x][(int)posDestino.y] = pretas.transform.Find(peca.name).gameObject;
			if(definitivo && peca.name.Contains("Pawn")) {
				if(Mathf.Abs(posDestino.x - posPeca.x) == 2) {
					// se a peca que você ta movendo é um peão e vc quer andar dois pra frente
					brancaEP = new Vector2(posDestino.x+1, posDestino.y);
					// da o direito da preta fazer o enpassant
					tab[(int)brancaEP.x][(int)brancaEP.y] = brancas.transform.Find("Ghost").gameObject;
					// ^ coloca um "fantasma" na posição que precisa ser limpo na proxima rodada
				}
				int numeroPeao = (int)peca.name.ToCharArray()[11] - 49;
				peoesPretos[numeroPeao] = true;
			} else if(definitivo && peca.name.Equals("Black Rook 2")) {
				// torre mais proxima do rei preto
				torresPretas[0] = true;
			} else if(definitivo && peca.name.Equals("Black Rook 1")) {
				// torre mais longe do rei pretos
				torresPretas[1] = true;
			} else if(peca.name.Contains("King")) {
				// rei preto andou
				if(Mathf.Abs((int)posDestino.y - (int)posPeca.y) >= 2) {
					if((int)posDestino.y > (int)posPeca.y && tab[(int)posPeca.x][tab.Length - 1] && tab[(int)posPeca.x][tab.Length - 1].name.Contains("Black Rook")) {
						// significa que está fazendo o roque longo
						tab[(int)posPeca.x][(int)posPeca.y + 1] = pretas.transform.Find(tab[(int)posPeca.x][tab.Length - 1].name).gameObject;
						tab[(int)posPeca.x][tab.Length - 1] = null;
						// torre mais longe do rei preto
						if(definitivo) {
							torresPretas[1] = true;	
						}
					} else if(tab[(int)posPeca.x][0] && tab[(int)posPeca.x][0].name.Contains("Black Rook")){
						// significa que está fazendo o roque curto
						tab[(int)posPeca.x][(int)posPeca.y - 1] = pretas.transform.Find(tab[(int)posPeca.x][0].name).gameObject;
						tab[(int)posPeca.x][0] = null;
						// torre mais perto do rei preto
						if(definitivo) {
							torresPretas[0] = true;
						}	
					}
				}
				if(definitivo) {
					reiPreto.x = posDestino.x;
					reiPreto.y = posDestino.y;
					reiAndou[1] = true;
				}
			}
		}
		
		tab[(int)posPeca.x][(int)posPeca.y] = null;
		this.posPeca.x = posDestino.x;
		this.posPeca.y = posDestino.y;
	
		if(definitivo) {
			if(peca.name.StartsWith("White")) {
				reiPretoEmCheck = checkmate(tab, "Black");
				if(reiPretoEmCheck) gm.endGame();
			} else if (peca.name.StartsWith("Black")) {
				reiBrancoEmCheck = checkmate(tab, "White");
				if(reiBrancoEmCheck) gm.endGame();
			}
		 	return null;
		} else {
			return tab;
		}
	}

	public GameObject verifyPosition (Vector2 pos){
		return tabuleiro[(int)pos.x][(int)pos.y];
	}

	public GameObject[][] GetTabuleiro() {
		return tabuleiro;
	}

	// promove a ultima peça selecionada para o objeto passado como parametro
	public void Promover(GameObject peao, GameObject promo) {
		bool achou = false;
		for (int i = 0; i <= 1; i++) {
			for (int j = 0; j < tabuleiro.Length; j++) {
				if(tabuleiro[i*(tabuleiro.Length-1)][j] && tabuleiro[i*(tabuleiro.Length-1)][j].name.Equals(peao.name)) {
					posPeca.x = i*(tabuleiro.Length-1);
					posPeca.y = j;
					achou = true;
					break;
				}
			}
			if(achou) {
				break;
			}
		}
		if(promo.name.StartsWith("White")) {
			tabuleiro[(int)posPeca.x][(int)posPeca.y] = brancas.transform.Find(promo.name).gameObject;
			reiPretoEmCheck = checkmate(tabuleiro, "Black");
			if(reiPretoEmCheck) gm.endGame();
		} else if(promo.name.StartsWith("Black")) {
			tabuleiro[(int)posPeca.x][(int)posPeca.y] = pretas.transform.Find(promo.name).gameObject;
			reiBrancoEmCheck = checkmate(tabuleiro, "White");
			if(reiBrancoEmCheck) gm.endGame();
		}
	}

	public GameObject[][] copiar(GameObject[][] tab){
		GameObject[][] tab_aux = new GameObject[8][];
		for(int i = 0; i < tab_aux.Length; i++) {
			tab_aux[i] = new GameObject[8];
			for(int j = 0; j < tab_aux[i].Length; j++) {
				if (tab[i][j] && tab[i][j].name.StartsWith("Black")){
					tab_aux[i][j] = pretas.transform.Find(tab[i][j].name).gameObject;
				}
				else if (tab[i][j] && tab[i][j].name.StartsWith("White")){
					tab_aux[i][j] = brancas.transform.Find(tab[i][j].name).gameObject;
				}
			}
		}
		return tab_aux;
	}
}