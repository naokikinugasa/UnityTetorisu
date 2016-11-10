using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Line : MonoBehaviour {

	private int num;
	private Text text; 

	public int GetNum(){
		return num;
	}
	public void Add(int lineNum){
		num += lineNum;
	}

	void Start(){
		text = GetComponent<Text>();
	}
	void Update(){
		num = Stage.line.GetNum();
		text.text = "Line:"+num.ToString();
	}
}
