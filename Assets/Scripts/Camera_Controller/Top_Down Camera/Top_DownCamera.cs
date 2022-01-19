using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JackelGame
{
    public class Top_DownCamera : MonoBehaviour
    {
        #region Variables
        public Transform m_Target;
        public float m_Height = 10f;
        public float m_Distance = 20f;
        public float m_Angle = 40f;

        private Vector3 refVelocity;
        #endregion

        #region

        // Start is called before the first frame update
        void Start()
        {
            HandleCamera();
        }

        // Update is called once per frame
        void Update()
        {
            HandleCamera();
        }
        #endregion

        #region Helper Methods
        private void HandleCamera()
        {
            if (!m_Target)
            {
                return;
            }

            //Get World Position
            Vector3 worldPosition = (Vector3.forward * -m_Distance) + (Vector3.up * m_Height);
            Debug.DrawLine(m_Target.position, worldPosition, Color.red);

            // Build our Rotated vector
            Vector3 rotatedVector = Quaternion.AngleAxis(m_Angle, Vector3.up) * worldPosition;
            Debug.DrawLine(m_Target.position, rotatedVector, Color.green);

            // Build THe new Position
            Vector3 flatTargetPosition = m_Target.position;
            flatTargetPosition.y = 0;
            Vector3 finalPosition = flatTargetPosition + rotatedVector;

            Debug.DrawLine(m_Target.position, finalPosition, Color.blue);


            transform.position = Vector3.SmoothDamp(  transform.position,finalPosition,ref refVelocity, 0.5f);
            transform.LookAt(flatTargetPosition);
        }

        void OnDrawGizmos()
        {
            Gizmos.color = new Color(0f, 0f, 1f, 0.2f);
            if (m_Target)
            {
                Gizmos.DrawLine(transform.position, m_Target.position);
                Gizmos.DrawCube(m_Target.position, new Vector3(0.5f, 0.5f, 0.5f));
            }
            Gizmos.DrawCube(m_Target.position, new Vector3(0.5f, 0.5f, 0.5f));
        }
        #endregion
    }
}

