using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationObject : MonoBehaviour {
    
	Quaternion Orientationaudepart;

	public Camera camerabase;

	public GameObject ObjetController;

	float RotationToMakeX;
	float RotationToMakeY;


	public float VitesseDeRotation;
	public float ClampingRotation;
	public int TempsdeRecul;
	public bool freerotation = false;
    public float SpeedFreeRotation;
    float pointer_x;
    float pointer_y;
    public float DureeAppuie;

    Vector2 firstPressPos;
	Vector2 secondPressPos;
	Vector2 currentSwipe;

	public bool bougeable = false;
	bool ismoving = false;
	bool demarrageList = false;
	bool retourdansletemps = false;
	bool oneframe = false;
	bool playeable;

    //painting
    public GameObject MatAPeindre;
    

    //rollback
    public float TimerAmount;
    float timer;
    public float rotateSpeed = 2f;
    bool RollbackInEffect;
    bool RollbackInEffectClamped;
    public Quaternion rotationBase;
    public Quaternion[] RotaArray;
    
    public bool IsHolding;
    public bool RevertBool;


    public Vector2 PositionFinger;
    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(gameObject);
		Orientationaudepart = ObjetController.transform.rotation;
		
	}
	
	// Update is called once per frame
	void Update () {
        
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    DureeAppuie = 0;
                    break;

            }
            DureeAppuie += Time.deltaTime;
            PositionFinger = Input.touches[0].position;
            pointer_x = Input.touches[0].deltaPosition.x;
            pointer_y = Input.touches[0].deltaPosition.y;
        }
        if (ObjetController.GetComponent<PropertiesObj>().CanRollBack == true)
        {
            Rollback();
        }
		if (Input.GetKeyDown (KeyCode.R)) {
			ObjetController.transform.rotation = Orientationaudepart;
		}

		oneframe = false;

		if (Input.GetKeyDown (KeyCode.F)) {
			if (freerotation == false && oneframe == false) {
				Debug.Log ("true");
				freerotation = true;
				oneframe = true;

			}
		}
		if (Input.GetKeyDown (KeyCode.F)) {
			if (freerotation == true && oneframe == false) {
				//Debug.Log ("false");
				freerotation = false;
				oneframe = true;

			}
		}

		if (freerotation == true) {
			ControleLibre ();
		} else {
			VitesseDeRotation = 3;
		}
			
		if (ismoving == true) {
			demarrageList = true;
		}


		

		RaycastHit hit;
		Ray ray	= camerabase.ScreenPointToRay (Input.mousePosition);
		bougeable = false;
		if (Physics.Raycast (ray, out hit) && playeable == false && ismoving == false) {
			//Debug.Log ("OK");
			if (hit.transform.tag == "ObjetFree") {
				freerotation = true;
				ObjetController = hit.transform.gameObject;
				bougeable = true;
			}
			if (hit.transform.tag == "ObjetIncrementation") {
				freerotation = false;
				ObjetController = hit.transform.gameObject;
				bougeable = true;
			}
		}

		if (ismoving == false && freerotation == false) {
			Swipe ();
		}

		if (RotationToMakeX != 0 || RotationToMakeY != 0) {
			ismoving = true;
			RotationMovement ();
            if(RollbackInEffectClamped == false)
            {
                rotationBase = ObjetController.transform.rotation;
            }
            else
            {
                RollbackInEffectClamped = false;
            }
        } else {
			ismoving = false;
            RollbackInEffectClamped = false;
		}

		if (Input.GetKeyDown (KeyCode.Z)) {
			RotationToMakeX -= ClampingRotation;
		}
		if (Input.GetKeyDown (KeyCode.S)) {
			RotationToMakeX += ClampingRotation;
		}
		if (Input.GetKeyDown (KeyCode.Q)) {
			RotationToMakeY -= ClampingRotation;
		}
		if (Input.GetKeyDown (KeyCode.D)) {
			RotationToMakeY += ClampingRotation;
		}
	}


	void RotationMovement () {
		if (RotationToMakeX < 0) {
			float depassementX ;
			depassementX = RotationToMakeX;
			ObjetController.transform.Rotate (VitesseDeRotation, 0, 0,Space.World);
			RotationToMakeX += VitesseDeRotation;
			if (depassementX > 0) {
				RotationToMakeX -= depassementX;
			}
		}

		if (RotationToMakeX > 0) {
			float depassementX ;
			depassementX = RotationToMakeX;
			ObjetController.transform.Rotate (-VitesseDeRotation, 0, 0,Space.World);
			RotationToMakeX -= VitesseDeRotation;
			if (depassementX < 0) {
				RotationToMakeX += depassementX;
			}
		}

		if (RotationToMakeY < 0) {
			float depassementY ;
			depassementY = RotationToMakeY;
			ObjetController.transform.Rotate (0, VitesseDeRotation, 0,Space.World);
			RotationToMakeY += VitesseDeRotation;
			if (depassementY > 0) {
				RotationToMakeY -= depassementY;
			}
		}

		if (RotationToMakeY > 0) {
			float depassementY ;
			depassementY = RotationToMakeY;
			ObjetController.transform.Rotate (0, -VitesseDeRotation, 0,Space.World);
			RotationToMakeY -= VitesseDeRotation;
			if (depassementY < 0) {
				RotationToMakeY += depassementY;
			}
		}

	}


	public void Swipe()
	{
		


			

		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && bougeable == true) {
			//save began touch 2d point
			firstPressPos = new Vector2 (Input.touches[0].deltaPosition.x, Input.touches[0].deltaPosition.y);
			//Debug.Log ("activable");
		}
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) {
			//save ended touch 2d point
			secondPressPos = new Vector2(Input.touches[0].deltaPosition.x, Input.touches[0].deltaPosition.y);

            //create vector from the two points
            currentSwipe = new Vector2 (secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

			//normalize the 2d vector
			currentSwipe.Normalize ();

			//swipe upwards
			if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) {
				Debug.Log ("up swipe");
				RotationToMakeX -= ClampingRotation;


			}
			//swipe down
			if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) {
				Debug.Log ("down swipe");
				RotationToMakeX += ClampingRotation;
			}
			//swipe left
			if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) {
				Debug.Log ("left swipe");
				RotationToMakeY -= ClampingRotation;
			}
			//swipe right
			if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) {
				Debug.Log ("right swipe");
				RotationToMakeY += ClampingRotation;
			}

		}

	}

	void ControleLibre () {

		RaycastHit hit;
		Ray ray	= camerabase.ScreenPointToRay (Input.mousePosition);
		bougeable = false;
		if (Physics.Raycast (ray, out hit)) {
			//Debug.Log ("OK");
			if (hit.transform.tag == "ObjetFree") {
				bougeable = true;
			}
		}

		Debug.Log ("controlelibre");
		VitesseDeRotation = 20;


		if (bougeable == true && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {

			playeable = true;
            if(RollbackInEffect == false)
            {
                rotationBase = ObjetController.transform.rotation;
                
            }
            else
            {
                RollbackInEffect = false;
            }
		}
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) {
			playeable = false;
            timer = 0;
		}

		if (playeable == true ) {
            IsHolding = true;
            
			ObjetController.transform.Rotate (0, -pointer_x * SpeedFreeRotation, 0, Space.World);
			ObjetController.transform.Rotate (-pointer_y * SpeedFreeRotation, 0, 0, Space.World);
            float red = MatAPeindre.GetComponent<Renderer>().material.color.r;
            

            if (ObjetController.GetComponent<PropertiesObj>().CanPaint == true)
            {
                if (pointer_x < 0 && pointer_y < (-2*pointer_x) && pointer_y >= (2 * pointer_x))
                {
                   MatAPeindre.GetComponent<Renderer>().material.color = new Color(MatAPeindre.GetComponent<Renderer>().material.color.r, MatAPeindre.GetComponent<Renderer>().material.color.g, Mathf.Lerp(MatAPeindre.GetComponent<Renderer>().material.color.b,0,-0.1f));
                    Debug.Log("bleu");
                }

                if (pointer_x > 0 && pointer_y < (-2 * pointer_x) && pointer_y >= (2 * pointer_x))
                {
                    MatAPeindre.GetComponent<Renderer>().material.color = new Color(MatAPeindre.GetComponent<Renderer>().material.color.r,Mathf.Lerp(MatAPeindre.GetComponent<Renderer>().material.color.g, 0,-0.1f), MatAPeindre.GetComponent<Renderer>().material.color.b);
                    Debug.Log("green");
                }
                if (pointer_y < 0/* && pointer_x < (-2 * pointer_y) && pointer_x >= (2 * pointer_y)*/)
                {
                    MatAPeindre.GetComponent<Renderer>().material.color = new Color(Mathf.Lerp(MatAPeindre.GetComponent<Renderer>().material.color.r, 0,-0.1f), MatAPeindre.GetComponent<Renderer>().material.color.g, MatAPeindre.GetComponent<Renderer>().material.color.b);
                    Debug.Log("Red");
                }

                if (pointer_y > 0 /*&& pointer_x < (-2 * pointer_y) && pointer_x >= (2 * pointer_y)*/)
                {
                    MatAPeindre.GetComponent<Renderer>().material.color = Color.Lerp(MatAPeindre.GetComponent<Renderer>().material.color, Color.white, 0.1f);
                    Debug.Log("white");
                }
            }
        }

        else
        {
            IsHolding = false;
        }
        

	}


    public void Rollback()
    {
        if(IsHolding == true)
        {
         //  RotaArray = Extensions.AddItemToArray(RotaArray, ObjetController.transform.rotation);
          
        }
        if(IsHolding == false/* && RotaArray.Length != 0*/)
        {
            if(timer <= TimerAmount)
            {
                timer += Time.deltaTime;
            }
            else
            {
                RollbackInEffect = true;
            }
            if (RollbackInEffect == true)
            {
                ObjetController.transform.rotation = Quaternion.Lerp(ObjetController.transform.rotation, rotationBase, Time.deltaTime * rotateSpeed);
                //StartCoroutine(RollBackActivated());
            }
            /*for (int i = RotaArray.Length - 1; i >= 0; i--)
            {
                ObjetController.transform.Rotate(RotaArray[i].eulerAngles-ObjetController.transform.rotation.eulerAngles );
                
               //ObjetController.transform.rotation = RotaArray[i];
               
                if (i== 0)
                {
                    RotaArray = new Quaternion[0];
                }
            }*/

        }
        if(ObjetController.transform.rotation == rotationBase && RollbackInEffect == true)
        {
            
            RollbackInEffect = false;
        }
    }

    
       

    




    IEnumerator RollBackActivated()
    {
        for (int i = RotaArray.Length - 1; i >= 0; i--)
        {
            ObjetController.transform.Rotate(RotaArray[i].eulerAngles - ObjetController.transform.rotation.eulerAngles);

            //ObjetController.transform.rotation = RotaArray[i];

            if (i == 0)
            {
                RotaArray = new Quaternion[0];
            }
        }
        yield return null;
    }

    }

