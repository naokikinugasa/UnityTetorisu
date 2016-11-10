using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {
	private int y,x;

	public Block(int y,int x){
		this.y = y;
		this.x = x;
	}
	public int GetY(){
		return this.y;
	}
	public int GetX(){
		return this.x;
	}
	public void SetPosition(int y,int x){
		this.y = y;
		this.x = x;
	}
	public void PlusPosition(int y,int x){
		this.y += y;
		this.x += x;
	}

}
