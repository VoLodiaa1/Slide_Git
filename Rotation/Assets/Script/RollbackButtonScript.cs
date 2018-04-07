using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Rotation
{
    public class RollbackButtonScript : MonoBehaviour
    {
      public List<GameObject> ObjToRollback;

        public void UpdateRollbackObj(List<GameObject> ObjToInject)
        {
            ObjToRollback = new List<GameObject>(ObjToInject);
        }
       public void StartRollback()
        {
            foreach (var item in ObjToRollback)
            {
                item.GetComponent<PropertiesObj>().timer = 9999;
            }
        }

        public void EndRollback()
        {
            foreach (var item in ObjToRollback)
            {
                item.GetComponent<PropertiesObj>().StopAllCoroutines();
                item.GetComponent<PropertiesObj>().timer = 0;
                item.GetComponent<PropertiesObj>().RollbackInEffect = false;
            }
        }
    }
}
