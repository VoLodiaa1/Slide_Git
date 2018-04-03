using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour {
	public AnimationCurve In, Out, StrechVertical,StrechHorizontal;
	// Use this for initialization
	SkinnedMeshRenderer DeformerRenderer;
	float transitionspeed;

	public enum AnimState {Chute,Normal,Right,Left};
	public AnimState CurrentState;

	public bool IsFalling;
	Rigidbody rb;
	[Range (0, 30)]
	public float Smooth;
	public float AplatiDeformTime,MoveDeformTime,OutMoveDeformTime;
	
	//private float timer;
	
//	[Range(0,1)]

	//private float x;

	void Start () {
		rb =GetComponent<Rigidbody>();
		DeformerRenderer = GetComponent<SkinnedMeshRenderer>();
		
	}

	 IEnumerator Arrival(int select){
		
		float timer = Time.time;
		
	
		while(Time.time -timer < AplatiDeformTime)
		{
			float x = (Time.time - timer)/AplatiDeformTime;
			DeformerRenderer.SetBlendShapeWeight(select, StrechHorizontal.Evaluate(x)*100f);
	   		yield return null;
		}
	
	}

	 IEnumerator OneShootIn(int select){
		
		float timer = Time.time;
	
		if(select==0)
		{
			print("AnimRight");
		}
		else if(select==1)
		{
			print("AnimGauche");
		}
		while(Time.time -timer < MoveDeformTime)
		{
			float x = (Time.time - timer)/MoveDeformTime;
			DeformerRenderer.SetBlendShapeWeight(select, In.Evaluate(x)*100f);
	   		yield return null;
		}
	
	}
	 IEnumerator OneShootOut(){
		
		float timer = Time.time;
	
	
		while(Time.time -timer < OutMoveDeformTime)
		{
			float x = (Time.time - timer)/OutMoveDeformTime;
			if(DeformerRenderer.GetBlendShapeWeight(0)>0f)
			{
			DeformerRenderer.SetBlendShapeWeight(0, Out.Evaluate(x)*100f);
			}

				if(DeformerRenderer.GetBlendShapeWeight(1)>0f)
			{
			DeformerRenderer.SetBlendShapeWeight(1, Out.Evaluate(x)*100f);
			}



	   		yield return null;
		}
	
	}

	
	
	
	void OnTriggerEnter(Collider col)
	{
		if(col.tag == "feuille")
		{
			
			//IsFalling=false;
			//StopCoroutine(FallingBall());
			
			StartCoroutine(Arrival(4));
			

			
		}				
	}

	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.tag == "feuille")
		{
			IsFalling=false;

		}
	}

	void OnTriggerExit(Collider col)
	{
		if(col.tag == "feuille")
		{
			CurrentState= AnimState.Chute;
			IsFalling=true;
			StopAllCoroutines();
			StartCoroutine(OneShootOut());
			//StopCoroutine(ResetBall());
			//StartCoroutine(FallingBall());

			

		}				
	}

	void LerpRotationOut()
	{
		Quaternion PositionZero = Quaternion.Euler (0,0,0);
		transform.rotation = Quaternion.Slerp (transform.rotation, PositionZero, Time.deltaTime*Smooth);
	}
	// Update is called once per frame
	void FixedUpdate () {

		

		if(IsFalling==false)
		{
			if(rb.velocity.x<-0.5f)
			{
				CurrentState= AnimState.Left;
				StopCoroutine(OneShootIn(0));
				DeformerRenderer.SetBlendShapeWeight(0,0);
				StartCoroutine(OneShootIn(1));
				//AnimState.Left

			}
			else if (rb.velocity.x>0.5f) 
			{
				CurrentState= AnimState.Right;
				StopCoroutine(OneShootIn(1));
				DeformerRenderer.SetBlendShapeWeight(1,0);
				StartCoroutine(OneShootIn(0));
				
			}
			else
			{
				CurrentState= AnimState.Normal;
			}
		}else{
			LerpRotationOut ();
			
		}
	}		
}
