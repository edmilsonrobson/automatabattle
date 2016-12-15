using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Hero : MonoBehaviour {

	SpriteRenderer spriteRenderer;


	public float amountToMove = 2f;

	public enum State {
		Neutral,
		Up,
		Down,
		Back,
		Attacking
	}

	public State state;

	void Awake(){
		spriteRenderer = GetComponent<SpriteRenderer>();

		state = Hero.State.Neutral;
	}

	public void TakeDamage (){
		Debug.Log("Took damage!");
		GetComponent<SpriteRenderer>().material.DOFloat(1f, "_FlashAmount", 0.1f).OnComplete(()=>{
			GetComponent<SpriteRenderer>().material.DOFloat(0f, "_FlashAmount", 0.1f);
		});
	}

	public void Attack (){
		if (state == State.Neutral){
			state = State.Attacking;
			transform.DOMoveX(-4, 0.3f).OnComplete(()=>{				
				if (GameManager.Instance.monster.isAttacking == false){
					GameManager.Instance.monster.TakeDamage();
				}
				transform.DOMoveX(5, 0.5f).SetDelay(0.2f).OnComplete(()=>{
					state = State.Neutral;
				});
			}); 
		}
	}

	public void MoveUp(){
		if (state == State.Neutral){
			state = State.Up;
			transform.DOMoveY(amountToMove, 0.2f).SetRelative(true).OnComplete(()=>{				
				transform.DOMoveY(-amountToMove, 0.3f).SetRelative(true).SetDelay(0.2f).OnComplete(()=>{					
					state = State.Neutral;
				});
			}); 
		}
	}

	public void MoveDown(){
		if (state == State.Neutral){
			state = State.Down;
			transform.DOMoveY(-amountToMove, 0.2f).SetRelative(true).OnComplete(()=>{				
				transform.DOMoveY(amountToMove, 0.3f).SetRelative(true).SetDelay(0.2f).OnComplete(()=>{
					state = State.Neutral;
				});
			}); 
		}
	}

	public void MoveBack(){
		if (state == State.Neutral){
			state = State.Back;
			transform.DOMoveX(amountToMove, 0.2f).SetRelative(true).OnComplete(()=>{				
				transform.DOMoveX(-amountToMove, 0.8f).SetRelative(true).SetDelay(0.2f).OnComplete(()=>{
					state = State.Neutral;
				});
			}); 
		}
	}

}
