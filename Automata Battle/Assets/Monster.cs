using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Monster : MonoBehaviour {

	public enum State {
		Initial,
		Annoyed,
		Frustrated,
		Furious
	}

	public State state;

	public Sprite initialSprite;
	public Sprite annoyedSprite;
	public Sprite frustratedSprite;
	public Sprite furiousSprite;

	public GameObject stateIcon;

	public bool isAttacking;

	public int misses;
	public int hitsTaken;

	public void Awake(){
		isAttacking = false;
	}

	public void TakeDamage (){
		Debug.Log("Monster took damage!");	
		hitsTaken++;
		GetComponent<SpriteRenderer>().material.DOFloat(1f, "_FlashAmount", 0.1f).OnComplete(()=>{
			GetComponent<SpriteRenderer>().material.DOFloat(0f, "_FlashAmount", 0.1f);
		});
	}

	public void Update(){
		SpriteRenderer iconRenderer = stateIcon.GetComponent<SpriteRenderer>();

		switch(state){
			case State.Initial:
				iconRenderer.sprite = initialSprite;
				break;
			case State.Annoyed:
				iconRenderer.sprite = annoyedSprite;
				break;
			case State.Frustrated:
				iconRenderer.sprite = frustratedSprite;
				break;
			case State.Furious:
				iconRenderer.sprite = furiousSprite;
				break;
		}

		if (hitsTaken >= 9){
			state = State.Furious;
		} else{
			if (misses > 5){
				state = State.Annoyed;
			} else{
				if (misses > 2){
					state = State.Frustrated;
				} else{
					state = State.Initial;
				}
			}
		}


	}

	public void Think(){
		// Will consider what to do depending on current state.


		int random = Random.Range(0,100);
		if (state != State.Furious){
			if (random != 0) return;
		}

		switch (state){
			case State.Initial:		
				random = Random.Range(0, 3);
				if (random == 0) AttackCenter();
				if (random == 1) AttackTop();
				if (random == 2) AttackBottom();
				break;			
			case State.Annoyed:
				random = Random.Range(0, 2);
				if (random == 0) AttackTop();
				if (random == 1) AttackBottom();
				break;
			case State.Frustrated:				
				if (random == 0) AttackCenter();
				break;
			case State.Furious:
				if (random == 0) AttackCenter();	
				if (random == 1) AttackTop();
				if (random == 2) AttackBottom();
				break;			
		}
	}

	public void AttackCenter(){
		if (!isAttacking){
			isAttacking = true;
			transform.DOMoveX(-1f, 0.2f).SetRelative(true);
			transform.DOMoveX(5f, 0.3f).SetDelay(1).OnComplete(()=>{
				if (GameManager.Instance.hero.state == Hero.State.Neutral || GameManager.Instance.hero.state == Hero.State.Attacking){
					GameManager.Instance.hero.TakeDamage();
				} else misses++;
				transform.DOMoveX(-4f, 0.8f).OnComplete(()=>{
					isAttacking = false;
				});
			});
		}
	}

	public void AttackTop(){
		if (!isAttacking){
			isAttacking = true;
			transform.DOMoveX(-1f, 0.2f).SetRelative(true);
			transform.DOMoveY(2f, 0.2f).SetRelative(true);
			transform.DOMove(new Vector3(5f, 2f), 0.3f).SetDelay(1).OnComplete(()=>{
				if (GameManager.Instance.hero.state == Hero.State.Up) Debug.Log("Lol");
				if (GameManager.Instance.hero.state == Hero.State.Up || GameManager.Instance.hero.state == Hero.State.Attacking){
					GameManager.Instance.hero.TakeDamage();
				} else misses++;
				transform.DOMove(new Vector3(-4f, 0f), 0.8f).OnComplete(()=>{
					isAttacking = false;
				});
			});
		}
	}

	public void AttackBottom(){
		if (!isAttacking){
			isAttacking = true;
			transform.DOMoveX(-1f, 0.2f).SetRelative(true);
			transform.DOMoveY(-2f, 0.2f).SetRelative(true);
			transform.DOMove(new Vector3(5f, -2f), 0.3f).SetDelay(1).OnComplete(()=>{
				if (GameManager.Instance.hero.state == Hero.State.Down) Debug.Log("Lol");
				if (GameManager.Instance.hero.state == Hero.State.Down || GameManager.Instance.hero.state == Hero.State.Attacking){
					GameManager.Instance.hero.TakeDamage();
				} else misses++;
				transform.DOMove(new Vector3(-4f, 0f), 0.8f).OnComplete(()=>{
					isAttacking = false;
				});
			});
		}
	}

}
