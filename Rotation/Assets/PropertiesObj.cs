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
        float timer;
        public float rotateSpeed = 2f;
        public bool RollbackInEffect;
       public bool RollbackInEffectClamped;
        public Quaternion rotationBase;
        public Quaternion[] RotaArray;

        RotationObject Cam;
        // Use this for initialization
        void Start()
        {
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
            if (Cam.IsHolding == true)
            {
                //  RotaArray = Extensions.AddItemToArray(RotaArray, ObjetController.transform.rotation);

            }
            if (Cam.IsHolding == false/* && RotaArray.Length != 0*/)
            {
                if (timer <= TimerAmount)
                {
                    timer += Time.deltaTime;
                }
                else
                {
                    RollbackInEffect = true;
                }
                if (RollbackInEffect == true)
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, rotationBase, Time.deltaTime * rotateSpeed);
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
            if (transform.rotation == rotationBase && RollbackInEffect == true)
            {

                RollbackInEffect = false;
            }
        }









        IEnumerator RollBackActivated()
        {
            for (int i = RotaArray.Length - 1; i >= 0; i--)
            {
                transform.Rotate(RotaArray[i].eulerAngles - transform.rotation.eulerAngles);

                //ObjetController.transform.rotation = RotaArray[i];

                if (i == 0)
                {
                    RotaArray = new Quaternion[0];
                }
            }
            yield return null;
        }
    }
}
