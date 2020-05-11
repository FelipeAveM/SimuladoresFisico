using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class File1Physical : MonoBehaviour
{
    [System.Serializable]
    public class File1 : System.Object
    {

        [SerializeField]
        private string business;

        public string Business
        {
            get { return business; }
            set { business = value; }
        }
    }
}
