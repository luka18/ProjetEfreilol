using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinTurn : MonoBehaviour {
    Vector2 v2 = new Vector2(0, 180);
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(v2 * Time.deltaTime);
	}

}
