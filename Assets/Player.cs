using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
	private Stage stage = new Stage();
	private float timeElapsed;
	
	//private tetorimino[num]Factory factory = tetorimino[num]Factory.GetInstance();

	// Use this for initialization
	void Start () {
		//bool flag = false;
		stage.CreateTetorimino();
		stage.SetNum((int)Random.Range(0, 6));
		stage.SetNextNum((int)Random.Range(0, 6));
		stage.Fill();
		stage.CreateCubeObject();
	}
	
	// Update is called once per frame
	void Update () {
		timeElapsed += Time.deltaTime;
		    if(timeElapsed >= Stage.speed) {
		    	if(!stage.IsFloor() && stage.CanDown()){
		    		stage.Down();
					timeElapsed = 0.0f;
		    	}else{
		    		stage.Fill();
					stage.CreateCubeObject();
		    		stage.SearchLine();
		    		stage.Init();
		    		if(stage.IsGameover()){
						SceneManager.LoadScene("gameover");
					}
		    		stage.Fill();
					stage.CreateCubeObject();
		    	}
				
			}
		
		if(Input.GetKeyDown("right")){
			stage.Move(1);
		}
		if(Input.GetKeyDown("left")){
			stage.Move(-1);
		}
		if(Input.GetKeyDown("up")){
			stage.Turn(1);
		}
		if(Input.GetKeyDown("space")){
			stage.Turn(-1);
		}
		if(Input.GetKeyDown("down")){
			if(!stage.IsFloor() && stage.CanDown()){
				stage.Down();
			}
		}
	}
	

}