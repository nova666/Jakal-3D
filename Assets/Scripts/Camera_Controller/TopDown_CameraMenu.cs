using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace JackelGame
{
    public class TopDown_CameraMenu : MonoBehaviour
    {
        [MenuItem("TopDownCamera Menu")]
        public static void CreateCamera()
        {
            GameObject[] selectedGO = Selection.gameObjects;
            //foreach (var selected in selectedGO)
            //{
            //    Debug.Log("selected go");
            //}
            if (selectedGO.Length > 0 && selectedGO[0].GetComponent<Camera>())
            {
                if (selectedGO.Length < 2)
                {
                    AttachTopDownScript(selectedGO[0].gameObject, null);

                }
                else if (selectedGO.Length == 2)
                {
                    AttachTopDownScript(selectedGO[0].gameObject, selectedGO[1].transform);
                }else if(selectedGO.Length == 3)
                {
                    EditorUtility.DisplayDialog("Camera Tools", "You Need to slect a gameobject in thw sceen to assign to!", "ok");

                }
            }
            else
            {
                EditorUtility.DisplayDialog("Camera Tools", "You Need to slect a gameobject in thw sceen to assign to!", "ok");
            }
        }

        static void AttachTopDownScript(GameObject aCamera, Transform aTarget)
        {
            Top_DownCamera cameraScript = null;
            if (aCamera)
            {
                cameraScript = aCamera.AddComponent<Top_DownCamera>();
                if (cameraScript && aTarget)
                {
                    cameraScript.m_Target = aTarget;
                }

                Selection.activeGameObject = aCamera;
            }

            
        }
    }

}
