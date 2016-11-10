using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	private int score;
	private Text text; 
	
	public int GetScore(){
		return score;
	}
	public void Add(int lineNum){
		switch (lineNum){
			case 1:
				score += 10;
				break;
			case 2:
				score += 30;
				break;
			case 3:
				score += 60;
				break;
			case 4:
				score += 100;
				break;
		}
	}

	void Start(){
		text = GetComponent<Text>();
	}
	void Update(){
		score = Stage.score.GetScore();
		text.text = "Score:"+score.ToString();
	}
}
