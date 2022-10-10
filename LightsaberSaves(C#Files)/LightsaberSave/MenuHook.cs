
using ThunderRoad;
using UnityEngine;
using TOR;


namespace LightsaberSave
{


    public class MenuHook : MonoBehaviour

    {

        public SaveMenu myMenu;


        void Update() {

            Refresh();
        
        
        }




        void Refresh() {



            int iterative = 0;
            if (Player.local.creature.handRight.grabbedHandle.item is Item item)
            {

                if (item.GetComponent<ItemLightsaber>() is ItemLightsaber saber)
                {
                    foreach (LightsaberBlade crystal in saber.module.lightsaberBlades)
                    {

                        switch (iterative)
                        {
                            case 0:

                                myMenu.hilt.text = "Hilt: " + item.name;
                                myMenu.kyberCrystal1.text = "Crystal: " + crystal.kyberCrystal;
                                myMenu.bladeLength.text = "Length: " + crystal.bladeLength;
                                break;
                            case 1:
                                myMenu.hilt.text = "Hilt: " + item.name;
                                myMenu.kyberCrystal2.text = "Crystal: " + crystal.kyberCrystal;
                                myMenu.bladeLength.text = "Length: " + crystal.bladeLength;
                                break;
                            case 2:
                                myMenu.hilt.text = "Hilt: " + item.name;
                                myMenu.kyberCrystal3.text = "Crystal: " + crystal.kyberCrystal;
                                myMenu.bladeLength.text = "Length: " + crystal.bladeLength;
                                break;
                            default:

                                break;
                        }
                        iterative++;

                        if (iterative > saber.module.lightsaberBlades.Length)
                        {

                            iterative = 0;


                        }
                    }
                }
            }


        }


    }
}