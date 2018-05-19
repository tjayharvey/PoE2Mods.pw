﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game;
using Game.UI;
using Patchwork;
using UnityEngine;

namespace TyrannyMods.pw
{
    [ModifiesType]
    public class CipherFocus : FocusTrait
    {        
        private float BaseFocusNew
        {
            [ModifiesMember("get_BaseFocus")]
            get
            {
                float num = 0f;
		        if (base.OwnerStats != null)
		        {
		        	num = (float)base.OwnerStats.Level * 5f;
		        	num += 10f;  // was 10f
		        	//num /= 2f; //just checking
		        }
		        return num;

                //switch(IEModOptions.CipherStartingFocusSetting) {
                //    case IEModOptions.CipherStartingFocus.Half:
                //        multiplier = 1f;
                //        break;
                //
                //    case IEModOptions.CipherStartingFocus.ThreeQuarter:
                //        multiplier = 1.5f;
                //        break;
                //
                //    case IEModOptions.CipherStartingFocus.Max:
                //        multiplier = 2f;
                //        break;
                //
                //    case IEModOptions.CipherStartingFocus.Quarter:
                //    default:
                //        multiplier = .5f;
                //        break;
                //
                //    case IEModOptions.CipherStartingFocus.None:
                //        multiplier = 0f;
                //        break;
                //}
                //
                //level = level * multiplier;
                //
                //return level;
            }
        }

        public float MaxFocusBonusNew
        {
            [ModifiesMember("get_MaxFocusBonus")]
            get
	           {
	           	return (!(base.OwnerStats != null)) ? 0f : base.OwnerStats.StatusEffectManager.CalculateValueForStat(Game.GameData.StatusEffectType.MaxFocus, null);
	           }
        }
        
        //public float MaxFocusNew
        //{
        //    [ModifiesMember("get_MaxFocus")]
        //    get
        //    {
        //        float result;
        //        float level;
        //
        //        switch (IEModOptions.CipherStartingFocusSetting)
        //        {
        //            case IEModOptions.CipherStartingFocus.Half:
        //                result = this.BaseFocus * 2f;
        //                break;
        //
        //            //Normally would be figured from BaseFocus, but since we'd have an irrational number it would
        //            //  mess up on rounding. Just recalculating it.
        //            case IEModOptions.CipherStartingFocus.ThreeQuarter:
        //                level = 0f;
        //                if (this.m_ownerStats != null)
        //                {
        //                    level = (float)this.m_ownerStats.Level * 5f;
        //                    level = level + 10f;
        //                }
        //                result = level * 2f;
        //                break;
        //
        //            case IEModOptions.CipherStartingFocus.Max:
        //                result = this.BaseFocus;
        //                break;
        //
        //            case IEModOptions.CipherStartingFocus.Quarter:
        //            default:
        //                result = this.BaseFocus * 4f;
        //                break;
        //
        //            //Normally would be figured from BaseFocus, but we have no base focus so we have to calculate it here
        //            case IEModOptions.CipherStartingFocus.None:
        //                level = 0f;
        //                if (this.m_ownerStats != null)
        //                {
        //                    level = (float)this.m_ownerStats.Level * 5f;
        //                    level = level + 10f;
        //                }
        //                result = level * 2f;
        //                break;
        //        }
        //
        //
        //        return result + this.MaxFocusBonus;
        //
        //    }
        //}
    }
}


