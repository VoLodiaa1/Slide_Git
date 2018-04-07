using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour {
	public AnimationCurve In, Out, StrechVertical,StrechHorizontal,Fall,Etirement;
	// Use this for initialization
	SkinnedMeshRenderer DeformerRenderer;
	float transitionspeed;

	public enum AnimState {Chute,Normal,Right,Left};
	public AnimState CurrentState;

	public bool IsFalling;
	Rigidbody rb;
	[Range (0, 30)]
	public float Smooth;
	public float AplatiDeformTime,MoveDeformTime,OutMoveDeformTime,Putain, EtirementTime;

	public ParticleSystem Splash;
	
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

			if(DeformerRenderer.GetBlendShapeWeight(0)>0f)
			{
			DeformerRenderer.SetBlendShapeWeight(0, Out.Evaluate(x)*100f);
			}

				if(DeformerRenderer.GetBlendShapeWeight(1)>0f)
			{
			DeformerRenderer.SetBlendShapeWeight(1, Out.Evaluate(x)*100f);
			}

				if(DeformerRenderer.GetBlendShapeWeight(2)>0f)
			{
			DeformerRenderer.SetBlendShapeWeight(2, Out.Evaluate(x)*100f);
			}
				if(DeformerRenderer.GetBlendShapeWeight(3)>0f)
			{
			DeformerRenderer.SetBlendShapeWeight(3, Out.Evaluate(x)*100f);
			}
				if(DeformerRenderer.GetBlendShapeWeight(5)>0f)
			{
			DeformerRenderer.SetBlendShapeWeight(5, Out.Evaluate(x)*100f);
			}
	   		yield return null;
		}
	
	}

	 IEnumerator Leave(int select){
		
		float timer = Time.time;
		print("arrondis toi");
	
		while(Time.time -timer < Putain)
		{
			float x = (Time.time - timer)/Putain;
			DeformerRenderer.SetBlendShapeWeight(select, StrechVertical.Evaluate(x)*100f);
	   		yield return null;
		}
	
	}

	 IEnumerator Falling(int select){
		
		float timer = Time.time;
		print("forme gouttedeau");
	
		while(Time.time -timer < Putain)
		{
			float x = (Time.time - timer)/Putain;
			DeformerRenderer.SetBlendShapeWeight(select, Fall.Evaluate(x)*100f);
			
	   		yield return null;
		}
	
	}

	IEnumerator Etire(){
		
		float timer = Time.time;
		print("étire toi");
	
		while(Time.time -timer < EtirementTime)
		{
			float x = (Time.time - timer)/EtirementTime;
				DeformerRenderer.SetBlendShapeWeight(3, Etirement.Evaluate(x)*100f);
			
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
			Splash.time=0;
			Splash.Play();

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
			StartCoroutine(Leave(5));
			StartCoroutine(Falling(2));

			//StopCoroutine(ResetBall());
			//StartCoroutine(FallingBall());
		}				
	}

	void OnCollisionExit(Collision col)
	{
		if(col.gameObject.tag == "feuille")
		{
			//IsFalling=false;
			

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
			if(rb.velocity.x<-0.25f)
			{
				CurrentState= AnimState.Left;
				StopCoroutine(OneShootIn(0));

				DeformerRenderer.SetBlendShapeWeight(0,0);
				StartCoroutine(OneShootIn(1));
				//AnimState.Left

			}
			else if (rb.velocity.x>0.25f) 
			{
				CurrentState= AnimState.Right;
				StopCoroutine(OneShootIn(1));
				DeformerRenderer.SetBlendShapeWeight(1,0);
				StartCoroutine(OneShootIn(0));
				
			}
			else
			{
				CurrentState= AnimState.Normal;
				StopCoroutine(OneShootIn(1));
				StopCoroutine(OneShootIn(0));
				StartCoroutine(OneShootOut());
			}
		}else{
			LerpRotationOut ();
			
		}
	}		
}
