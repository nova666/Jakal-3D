using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArmyOfOne {
    public class Vehicle_Input : MonoBehaviour
    {
        #region Variables
        [Header("Input Camera")]
        public Camera camera;
        #endregion

        #region Properties
        private Vector3 reticlePosition;
        public Vector3 ReticlePosition
        {
            get { return reticlePosition; }
        }

        private Vector3 reticleNormal;
        public Vector3 ReticleNormal
        {
            get { return reticleNormal; }
        }

        private float forwardInput;
        public float ForwardInput
        {
            get { return forwardInput; }
        }

        private float rotationInput;
        public float RotationInput
        {
            get { return rotationInput; }
        }

        private bool shoot;
        public bool Shoot
        {
            get { return shoot; }
        }
        #endregion

        #region BuiltinMethods
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            if (camera)
            {
                HandleInputs();
            }

        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(ReticlePosition, 0.05f);
        }
        #endregion

        #region
        protected virtual void HandleInputs()
        {
            Ray screenRay = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(screenRay, out hit))
            {
                reticlePosition = hit.point;
                reticleNormal = hit.normal;
            }

            forwardInput = Input.GetAxis("Vertical");
            rotationInput = Input.GetAxis("Horizontal");           
            shoot = Input.GetKeyDown(KeyCode.Space);  
        }
        #endregion
    }
}


