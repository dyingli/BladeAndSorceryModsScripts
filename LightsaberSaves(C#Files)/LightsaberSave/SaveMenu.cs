using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThunderRoad;
using UnityEngine;
using UnityEngine.Events;
using TOR;

namespace LightsaberSave
{

    
    public class SaveMenu : MenuModule
    {

        Item item;

        public MenuHook menuHook;

        private UnityEngine.UI.Button saveButton1;
        private UnityEngine.UI.Text titleL;
        private UnityEngine.UI.Text titleR;
        private UnityEngine.UI.Button saveButton2;
        private UnityEngine.UI.Button saveButton3;
        internal UnityEngine.UI.Text hilt;
        internal UnityEngine.UI.Text kyberCrystal1;
        internal UnityEngine.UI.Text kyberCrystal2;
        internal UnityEngine.UI.Text kyberCrystal3;
        internal UnityEngine.UI.Text bladeLength;


        public override void Init(MenuData menuData, Menu menu)
        {
            base.Init(menuData, menu);


            menuHook = menu.gameObject.AddComponent<MenuHook>();

            menuHook.myMenu = this;
            

            this.titleL = menu.GetCustomReference("TitleL").GetComponent<UnityEngine.UI.Text>();
            this.titleR = menu.GetCustomReference("TitleR").GetComponent<UnityEngine.UI.Text>();
            this.hilt = menu.GetCustomReference("Hilt").GetComponent<UnityEngine.UI.Text>();
            this.kyberCrystal1 = menu.GetCustomReference("KyberCrystal1").GetComponent<UnityEngine.UI.Text>();
            this.kyberCrystal2 = menu.GetCustomReference("KyberCrystal2").GetComponent<UnityEngine.UI.Text>();
            this.kyberCrystal3 = menu.GetCustomReference("KyberCrystal3").GetComponent<UnityEngine.UI.Text>();
            this.bladeLength = menu.GetCustomReference("BladeLength").GetComponent<UnityEngine.UI.Text>();


            this.titleL.text = "Save";
            this.titleR.text = "Saber";


            this.saveButton1 = menu.GetCustomReference("SaveButton1").GetComponent<UnityEngine.UI.Button>();
            this.saveButton1.onClick.AddListener((UnityAction)(() =>
            {

                SetupJson(1);


            }
            ));

            this.saveButton2 = menu.GetCustomReference("SaveButton2").GetComponent<UnityEngine.UI.Button>();
            this.saveButton2.onClick.AddListener((UnityAction)(() =>
            {

                SetupJson(2);


            }
            ));

            this.saveButton3 = menu.GetCustomReference("SaveButton3").GetComponent<UnityEngine.UI.Button>();
            this.saveButton3.onClick.AddListener((UnityAction)(() =>
            {

                SetupJson(3);

            }
            ));


            void SetupJson(int saveNum) {

                JSONParser parser = new JSONParser();

                parser.item = Player.local.creature.handRight.grabbedHandle.item;


                this.hilt.text = parser.item.name;


                parser.CreateItemData();

                if (parser.itemData.category == "Lightsabers")
                {
                    parser.GenerateJson(saveNum);


                }
                


            }



        }

    }
}
