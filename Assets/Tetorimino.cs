using UnityEngine;
using System.Collections;

public abstract class Tetorimino  {
	protected GameObject blockPrefab;
	protected int position_y=1;
	protected int position_x=1;
	protected int direction = 0;
	public Block[] block = new Block[4];
	// Use this for initialization	
	public Tetorimino(){
	}
	public int GetY(){
		return position_y;
	}
	public void SetPosition(int y,int x){
		position_y = y;
		position_x = x;
	}


	public void Down(){
		position_y++;
		for(int i=0;i<4;i++){
			block[i].PlusPosition(1,0);
		}	
	}
	public void Move(int rl){
		position_x += rl;
		for(int i=0;i<4;i++){
			block[i].PlusPosition(0,rl);
		}
	}
	public int GetDirection(){
		return this.direction;
	}
	public GameObject GetPrefab(){
		return this.blockPrefab;
	}
	public abstract void InitBlock();
	public abstract void Turn(int rl,int direction);

	public bool IsRange(){
		for(int i=0;i<4;i++){
			if(block[i].GetX() > Stage.FIELD_WIDTH || block[i].GetX() < 1){
				return false;
			}
		}
		return true;
	}
	
}
