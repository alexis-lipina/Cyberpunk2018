using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EquipmentSystem : MonoBehaviour
{
    [SerializeField] InventoryWindow inventoryWindow;

    //equipped implants
    [SerializeField] private Implant equipmentSlot1;
    [SerializeField] private Implant equipmentSlot2;
    [SerializeField] private Implant equipmentSlot3;

    [SerializeField] Implant jump;

    /// <summary>
    /// Disables an implant
    /// </summary>
    /// <param name="implant">Name of implant to disable</param>
    public void DisableImplant(string implant, int equipmentSlot)
    {
        //get a reference to the component to disable
        Behaviour toDisable = ((Behaviour)transform.GetComponent(Type.GetType(implant)));

        //ensure base implants overriden by the disabled implant are re enabled after it is removed
        foreach (Type type in ((Implant)toDisable).IncompatibleTypes)
        {
            if (type == typeof(Jump) && toDisable != (Behaviour)jump)
            {
                jump.enabled = true;
            }
            //etc...
        }

        ////remove from the list of enabled implants
        //switch(equipmentSlot)
        //{
        //    case 1:
        //        equipmentSlot1 = null;
        //        break;
        //    case 2:
        //        equipmentSlot2 = null;
        //        break;
        //    case 3:
        //        equipmentSlot3 = null;
        //        break;
        //}
        if(equipmentSlot1 != null && equipmentSlot1 == ((Implant)toDisable))
        {
            equipmentSlot1 = null;
        }
        if (equipmentSlot2 != null && equipmentSlot2 == ((Implant)toDisable))
        {
            equipmentSlot2 = null;
        }
        if (equipmentSlot3 != null && equipmentSlot3 == ((Implant)toDisable))
        {
            equipmentSlot3 = null;
        }

        //disable the implant
        toDisable.enabled = false;
    }

    /// <summary>
    /// Enables and implant
    /// </summary>
    /// <param name="implant">Name of implant to enable</param>
    public void ActivateImplant(string implant, int equipmentSlot)
    {
        //get a reference to the component to enable
        Behaviour toEnable = ((Behaviour)transform.GetComponent(Type.GetType(implant)));

        foreach (Type type in ((Implant)toEnable).IncompatibleTypes)
        {
            //for(int i = 0; i < enabledImplants.Count; i++)
            //{
            //    if(enabledImplants[i].IncompatibleTypes.Contains(type))
            //    {
            //        enabledImplants[i].enabled = false;
            //        enabledImplants.RemoveAt(i);
            //        i--;
            //    }
            //}
            switch (equipmentSlot)
            {
                case 1:
                    if(equipmentSlot2 != null && equipmentSlot2.IncompatibleTypes.Contains(type))
                    {
                        Unequip(2);
                    }
                    if(equipmentSlot3 != null && equipmentSlot3.IncompatibleTypes.Contains(type))
                    {
                        Unequip(3);
                    }
                    break;

                case 2:
                    if(equipmentSlot1 != null && equipmentSlot1.IncompatibleTypes.Contains(type))
                    {
                        Unequip(1);
                    }
                    if (equipmentSlot3 != null && equipmentSlot3.IncompatibleTypes.Contains(type))
                    {
                        Unequip(3);
                    }
                    break;
                case 3:
                    if(equipmentSlot1 != null && equipmentSlot1.IncompatibleTypes.Contains(type))
                    {
                        Unequip(1);
                    }
                    if (equipmentSlot2 != null && equipmentSlot2.IncompatibleTypes.Contains(type))
                    {
                        Unequip(2);
                    }
                    break;
            }
        }


        switch (equipmentSlot)
        {
            case 1:
                equipmentSlot1 = (Implant)toEnable;
                break;

            case 2:
                equipmentSlot2 = (Implant)toEnable;
                break;
            case 3:
                equipmentSlot3 = (Implant)toEnable;
                break;
        }

        //add to the list of enabled implants
        //enabledImplants.Add((Implant)toEnable);

        //enable the implant
        toEnable.enabled = true;
    }

    private void Unequip(int slot)
    {

        Behaviour implant;
        if(slot == 1)
        {
            implant = equipmentSlot1;
            implant.enabled = false;
            equipmentSlot1 = null;   
            
        }
        if(slot == 2)
        {
            implant = equipmentSlot2;
            implant.enabled = false;
            equipmentSlot2 = null;
        }
        if(slot == 3)
        {
            implant = equipmentSlot3;
            implant.enabled = false;
            equipmentSlot3 = null;
        }

        inventoryWindow.SendBackToInventory(slot);
    }
}
