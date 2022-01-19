using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ArmyOfOne
{
    public class HostagesManager : MonoBehaviour
    {
        /// <summary>
        /// Need to Identify the Base we are rescuing the Hostages from
        /// </summary>


        public void OnReleaseHostages(GameObject[] Hostages)
        {
            StartCoroutine(ReleaseHostages(Hostages));
        }

        public IEnumerator ReleaseHostages(GameObject[] Hostages)
        {
            if (Hostages.Length > 0)
            {
                for (int i = 0; i < Hostages.Length; i++)
                {
                    Hostages[i].SetActive(true);
                    yield return new WaitForSeconds(1f);
                }
            }
        }

    }
}

