using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Rotation
{
    public class PropertiesObj : MonoBehaviour
    {
        public bool CanRollBack;
        public bool CanPaint;


        public float TimerAmount;
        public float timer;
        public float rotateSpeed = 2f;
        public bool RollbackInEffect;
       public bool RollbackInEffectClamped;
        public Vector3 rotationBase;
        public List<Vector3> RotaArray;
        //public Vector3[] RotaArray;
        

        RotationObject Cam;
        public bool IsHolding;
        // Use this for initialization
        void Start()
        {
            rotationBase = transform.rotation.eulerAngles;
            Cam = FindObjectOfType<Camera>().GetComponent<RotationObject>();
        }

        // Update is called once per frame
        void Update()
        {
            if (CanRollBack == true)
            {
                Rollback();
            }
        }

        public void Rollback()
        {
            if (IsHolding == true)
            {
                
                 RotaArray.Add(transform.rotation.eulerAngles);

            }
            if (IsHolding == false/* && RotaArray.Length != 0*/)
            {
                if (timer <= TimerAmount)
                {
                    //timer += Time.deltaTime;
                }
                else
                {
                    RollbackInEffect = true;
                }
                if (RollbackInEffect == true)
                {
                    //transform.rotation = Quaternion.Lerp(transform.rotation, rotationBase, Time.deltaTime * rotateSpeed);
                    StartCoroutine(RollBackActivated());
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
            if (transform.rotation.eulerAngles == rotationBase && RollbackInEffect == true)
            {

                RollbackInEffect = false;
                
            }
        }









        IEnumerator RollBackActivated()
        {
            for (int i = RotaArray.Count - 1; i >= 0; i--)
            {
               // transform.Rotate(RotaArray[i]);

                transform.rotation =Quaternion.Euler(RotaArray[i].x, RotaArray[i].y, RotaArray[i].z);
                RotaArray.RemoveAt(i);

                if (i == 0)
                {
                    RotaArray = new List<Vector3>();
                    yield break;
                }
                yield return null;
            }
            
        }
    }
}
