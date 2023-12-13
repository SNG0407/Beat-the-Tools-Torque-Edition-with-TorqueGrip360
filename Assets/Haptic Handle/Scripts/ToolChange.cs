using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BNG
{
    public class ToolChange : MonoBehaviour
    {
        protected InputBridge input;
        protected void Awake()
        {
            input = InputBridge.Instance;
        }
            // Start is called before the first frame update
            void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            //input.AButton
            if (input.AButtonDown)
            {
                Debug.Log("A Button");
                GameManageBeatDevil.instance.HoldingWeapon = GameManageBeatDevil.WeaponType.Sword; 
            }
            if (input.BButtonDown)
            {
                Debug.Log("B Button");
                GameManageBeatDevil.instance.HoldingWeapon = GameManageBeatDevil.WeaponType.Gun;

            }
            if (input.XButtonDown)
            {
                Debug.Log("X Button");
                GameManageBeatDevil.instance.HoldingWeapon = GameManageBeatDevil.WeaponType.Shield;

            }
            if (input.YButtonDown)
            {
                Debug.Log("Y Button");
                GameManageBeatDevil.instance.HoldingWeapon = GameManageBeatDevil.WeaponType.Hammer;

            }
        }
    }

}