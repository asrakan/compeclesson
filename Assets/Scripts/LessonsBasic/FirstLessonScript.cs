using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LessonsBasic
{
    public class FirstLessonScript : MonoBehaviour
    {
        /// comment yani herhangi bir şey
        /// <summary>
        /// C# variables
        /// </summary>
        // Start is called before the first frame update
        public int tamSayi = 1;
        [SerializeField] int tamSayi2 = 100;
        float ondalikSayi = 0.25f;
        float ondalikSayi2 = 5.2342f;
        bool dogru = true;
        bool yanlis2 = false;
        [SerializeField] private Vector3 ucFloat = new Vector3(1, 2, 3);

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}