using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

	private bool filled;
	private GameObject Prefab;

	public void Fill(){
		filled = true;
	}
	public void UnFill(){
		filled = false;
	}
	public bool GetFill(){
		return filled;
	}
	public GameObject GetPrefab(){
		return Prefab;
	}
	public void SetPrefab(GameObject prefab){
		this.Prefab = prefab;
	}
	public void UnSetPrefab(){
		this.Prefab = null;
	}

}
