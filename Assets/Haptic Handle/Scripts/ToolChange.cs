using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
                
                if (SceneManager.GetActiveScene().name == "BeatDevil")
                {
                    GameManageBeatDevil.instance.HoldingWeapon = GameManageBeatDevil.WeaponType.Sword;
                }
                else if (SceneManager.GetActiveScene().name == "Tutorial")
                {
                    GameManageTutorial.instance.HoldingWeapon = GameManageTutorial.WeaponType.Sword;
                }
                else
                {
                    GameManage.instance.HoldingWeapon = GameManage.WeaponType.Sword;
                }
            }
            if (input.BButtonDown)
            {
                Debug.Log("B Button");
                if (SceneManager.GetActiveScene().name == "BeatDevil")
                {
                    GameManageBeatDevil.instance.HoldingWeapon = GameManageBeatDevil.WeaponType.Gun;
                }
                else if (SceneManager.GetActiveScene().name == "Tutorial")
                {
                    GameManageTutorial.instance.HoldingWeapon = GameManageTutorial.WeaponType.Gun;
                }
                else
                {
                    GameManage.instance.HoldingWeapon = GameManage.WeaponType.Gun;
                }
            }
            if (input.XButtonDown)
            {
                Debug.Log("X Button");

                if (SceneManager.GetActiveScene().name == "BeatDevil")
                {
                    GameManageBeatDevil.instance.HoldingWeapon = GameManageBeatDevil.WeaponType.Shield;
                }
                else if (SceneManager.GetActiveScene().name == "Tutorial")
                {
                    GameManageTutorial.instance.HoldingWeapon = GameManageTutorial.WeaponType.Shield;
                }
                else
                {
                    GameManage.instance.HoldingWeapon = GameManage.WeaponType.Shield;
                }
            }
            if (input.YButtonDown)
            {
                Debug.Log("Y Button");
                if (SceneManager.GetActiveScene().name == "BeatDevil")
                {
                    GameManageBeatDevil.instance.HoldingWeapon = GameManageBeatDevil.WeaponType.Hammer;
                }
                else if (SceneManager.GetActiveScene().name == "Tutorial")
                {
                    GameManageTutorial.instance.HoldingWeapon = GameManageTutorial.WeaponType.Hammer;
                }
                else
                {
                    GameManage.instance.HoldingWeapon = GameManage.WeaponType.Hammer;
                }
            }
        }
    }

}