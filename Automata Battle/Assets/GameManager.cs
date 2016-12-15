using UnityEngine;
using System.Collections;
using Gamelogic.Extensions;

public class GameManager : Singleton<GameManager> {

	public Hero hero;
	public Monster monster;

	// Update is called once per frame
	void Update () {
	
		if (Input.GetKeyDown(KeyCode.DownArrow)){
			hero.MoveDown();
		}
		else if (Input.GetKeyDown(KeyCode.UpArrow)){
			hero.MoveUp();
		}
		else if (Input.GetKeyDown(KeyCode.RightArrow)){
			hero.MoveBack();
		}
		else if (Input.GetKeyDown(KeyCode.LeftArrow)){
			hero.Attack();
		}



		monster.Think();

	}



}
