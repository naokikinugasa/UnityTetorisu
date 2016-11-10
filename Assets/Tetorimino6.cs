using UnityEngine;
using System.Collections;

public class Tetorimino6  : Tetorimino {
	// Use this for initialization	

	public Tetorimino6(){
		block[0] = new Block(position_y,position_x);
		block[1] = new Block(position_y,position_x+1);
		block[2] = new Block(position_y+1,position_x+1);
		block[3] = new Block(position_y+2,position_x+1);
		blockPrefab = (GameObject)Resources.Load ("Tetorimino5");
	}
	
	public override void Turn(int rl,int direction){
		if(rl == 1){
			switch(direction){
				case 0:
					block[0].PlusPosition(0,2);
					block[1].PlusPosition(1,-1);
					block[2].PlusPosition(0,0);
					block[3].PlusPosition(-1,1);
					this.direction++;
					break;
				case 1:
					block[0].PlusPosition(-1,-1);
					block[1].PlusPosition(-1,1);
					block[2].PlusPosition(0,0);
					block[3].PlusPosition(0,0);
					this.direction++;
					break;
				case 2:
					block[0].PlusPosition(1,0);
					block[1].PlusPosition(0,1);
					block[2].PlusPosition(-1,2);
					block[3].PlusPosition(0,-1);
					this.direction++;
					break;
				case 3:
					block[0].PlusPosition(0,-1);
					block[1].PlusPosition(0,-1);
					block[2].PlusPosition(1,-2);
					block[3].PlusPosition(1,0);
					this.direction = 0;
					break;
			}
		}else if(rl == -1){
			Turn(1,direction);
			Turn(1,this.direction);
			Turn(1,this.direction);
		}
	}
	public override void InitBlock(){
		block[0] = new Block(position_y,position_x);
		block[1] = new Block(position_y,position_x+1);
		block[2] = new Block(position_y+1,position_x+1);
		block[3] = new Block(position_y+2,position_x+1);
		this.direction = 0;
	}
}
