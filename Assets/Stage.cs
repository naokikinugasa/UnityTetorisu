using UnityEngine;
using System.Collections;

public class Stage : MonoBehaviour {

	public static readonly int FIELD_HEIGHT = 15;
	public static readonly int FIELD_WIDTH = 11;

	private Grid[,] grid = new Grid[FIELD_HEIGHT+1,FIELD_WIDTH+1];
	private Tetorimino[] tetorimino = new Tetorimino[7];
	private Tetorimino[] nexttetorimino = new Tetorimino[7];
	private int num;
	private int nextnum;
	public static Score score = new Score();
	public static Line line = new Line();
	public static double speed = 1.0;

	public Stage(){
		for(int i=0;i<=FIELD_HEIGHT;i++){
			for(int j=0;j<=FIELD_WIDTH;j++){
				grid[i,j] = new Grid();
			}
		}
	}

	public void Init(){
		num = nextnum;
		nextnum = (int)Random.Range(0, 6);
		tetorimino[num].SetPosition(1,2);
		tetorimino[num].InitBlock();
	}
	
	public void CreateTetorimino(){
		tetorimino[0] = new Tetorimino1();
		tetorimino[1] = new Tetorimino2();
		tetorimino[2] = new Tetorimino3();
		tetorimino[3] = new Tetorimino4();
		tetorimino[4] = new Tetorimino5();
		tetorimino[5] = new Tetorimino6();
		tetorimino[6] = new Tetorimino7();
		nexttetorimino[0] = new Tetorimino1();
		nexttetorimino[1] = new Tetorimino2();
		nexttetorimino[2] = new Tetorimino3();
		nexttetorimino[3] = new Tetorimino4();
		nexttetorimino[4] = new Tetorimino5();
		nexttetorimino[5] = new Tetorimino6();
		nexttetorimino[6] = new Tetorimino7();
	}

	public bool IsGameover(){
		for(int i=0;i<4;i++){
			if(grid[tetorimino[num].block[i].GetY(),tetorimino[num].block[i].GetX()].GetFill()){
				return true;
			}
		}
		return false;
	}
	public void CreateCubeObject(){
		DestroyCubeObject();
		for(int i=0;i<=FIELD_HEIGHT;i++){
			for(int j=0;j<=FIELD_WIDTH;j++){
				if(grid[i,j].GetFill()){
					Instantiate(grid[i,j].GetPrefab(),new Vector3(j*1.0f, i*1.0f, 0),Quaternion.identity);
				}
			}
		}
		for(int i=0; i<4; i++){
			Instantiate(nexttetorimino[nextnum].GetPrefab(),new Vector3(nexttetorimino[nextnum].block[i].GetX()*1.0f+(FIELD_WIDTH+3), nexttetorimino[nextnum].block[i].GetY()*1.0f+FIELD_HEIGHT-5, 0),Quaternion.identity);
		}
	}
	private void DestroyCubeObject(){
		var clones = GameObject.FindGameObjectsWithTag ("Tetorimino");
		foreach (var clone in clones){
		    Destroy(clone);
		}
	}
	public void Fill(){
		for(int i=0;i<4;i++){
			grid[tetorimino[num].block[i].GetY(),tetorimino[num].block[i].GetX()].Fill();
			grid[tetorimino[num].block[i].GetY(),tetorimino[num].block[i].GetX()].SetPrefab(tetorimino[num].GetPrefab());
		}
	}
	public void UnFill(){
		for(int i=0;i<4;i++){
			grid[tetorimino[num].block[i].GetY(),tetorimino[num].block[i].GetX()].UnFill();
		}
	}

	public bool IsFloor(){
		for(int i=0;i<4;i++){
			if(tetorimino[num].block[i].GetY() == FIELD_HEIGHT){
				return true;
			}
		}
		return false;
	}

	public bool CanDown(){
		UnFill();
		for(int i=0;i<4;i++){
			if(grid[(tetorimino[num].block[i].GetY()+1),tetorimino[num].block[i].GetX()].GetFill()){
				return false;
			}
		}
		return true;
	}
	private bool IsRange(){
		return tetorimino[num].IsRange();
	}
	private bool IsOverlap(){
		for(int i=0;i<4;i++){
			if(grid[tetorimino[num].block[i].GetY(),tetorimino[num].block[i].GetX()].GetFill()){
				return true;
			}
		}
		return false;
	}
	private bool CanMove(int rl){
		UnFill();
		tetorimino[num].Move(rl);
		if(IsRange()){
			if(!IsOverlap()){
				tetorimino[num].Move(-rl);
				return true;
			}
		}
		tetorimino[num].Move(-rl);
		return false;
	}
	private bool CanTurn(int rl){
		UnFill();
		tetorimino[num].Turn(rl,tetorimino[num].GetDirection());
		if(IsRange()){
			if(!IsOverlap()){
				tetorimino[num].Turn(-rl,tetorimino[num].GetDirection());
				return true;
			}
		}
		tetorimino[num].Turn(-rl,tetorimino[num].GetDirection());
		return false;
		
	}
	public void Turn(int rl){
		if(!IsFloor() && CanTurn(rl)){
			tetorimino[num].Turn(rl,tetorimino[num].GetDirection());
		}
		Fill();
		CreateCubeObject();
	}
	public void Move(int rl){
		if(!IsFloor() && CanMove(rl)){
			tetorimino[num].Move(rl);
		}
		Fill();
		CreateCubeObject();
	}
	public void Down(){
		tetorimino[num].Down();
		Fill();
		CreateCubeObject();
	}

	public void SearchLine(){
		int zero_count = 0;
		int clear_flag = 0;
		int[] clear_lines_point = new int[4];
		int clear_line_index = 0;
		int[] remain_lines_point = new int[FIELD_HEIGHT];
		int remain_line_index = 0;
		int SEARCH_START_Y = FIELD_HEIGHT;
		int SEARCH_START_X = 1;

		for(int i=SEARCH_START_Y;i>0;i--){
			for(int j=SEARCH_START_X;j<=FIELD_WIDTH;j++){
				if(!grid[i,j].GetFill()){
					zero_count++;
				}
			}
			if(zero_count == 0){
				clear_flag++;
				clear_lines_point[clear_line_index] = i;
				clear_line_index++;
			}else{
				remain_lines_point[remain_line_index] = i;
				remain_line_index++;
				zero_count = 0;
			}

		}

		if(clear_flag != 0){
			int lineNum = clear_line_index;
			for(clear_line_index=0;clear_line_index<4;clear_line_index++){
				if(clear_lines_point[clear_line_index] != 0){
					for(int j=SEARCH_START_X;j<=FIELD_WIDTH;j++){
						grid[clear_lines_point[clear_line_index],j].UnFill();
					}
				}
			}
			score.Add(lineNum);
			line.Add(lineNum);
			speed -= 0.1;

			remain_line_index = 0;
			for(int i=SEARCH_START_Y;i>0;i--){
				for(int j=SEARCH_START_X;j<=FIELD_WIDTH;j++){
					if(grid[remain_lines_point[remain_line_index],j].GetFill()){
						grid[i,j].Fill();
						grid[i,j].SetPrefab(grid[remain_lines_point[remain_line_index],j].GetPrefab());
					}else if(!grid[remain_lines_point[remain_line_index],j].GetFill()){
						grid[i,j].UnFill();
						grid[i,j].UnSetPrefab();
					}
				}
				remain_line_index++;
			}
		}
	}
	public int GetNum(){
		return num;
	}
	public void SetNum(int num){
		this.num = num;
	}
	public int GetNextNum(){
		return nextnum;
	}
	public void SetNextNum(int nextnum){
		this.nextnum = nextnum;
	}

}
