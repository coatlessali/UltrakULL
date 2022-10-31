﻿using HarmonyLib;
using UltrakULL.json;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

namespace UltrakULL.Harmony_Patches
{
    //@Override
    //Overrides the Start method to intercept and localize string in the intermissions
    [HarmonyPatch(typeof(IntermissionController), "Start")]
    public static class Localize_Intermission
    {
        [HarmonyPrefix]
        public static bool Start_MyPatch(IntermissionController __instance, ref string ___preText, ref string ___fullString, ref Text ___txt)
        {
            ___txt = __instance.GetComponent<Text>();
            ___fullString = ___txt.text;
            ___txt.text = "";

            IntermissionStrings intStrings = new IntermissionStrings();
            ___fullString = intStrings.getIntermissionString(___fullString);
            ___txt.text = ___fullString;

            //Section for 2-S Mirage's names.
            if (SceneManager.GetActiveScene().name == "Level 2-S")
            {
                string openingTag = "<color=grey>";
                string closingTag = "</color>";
                string mirageName = Regex.Replace(___preText, @"<[^>]*>", "");
                //Console.WriteLine(mirageName);
                switch (mirageName)
                {
                    case ("???:"): { break; }
                    case ("JUST SOMEONE:"): { ___preText = openingTag + LanguageManager.CurrentLanguage.visualnovel.visualnovel_mirageName1 + closingTag + ":"; break; }
                    case ("THE PRETTIEST GIRL IN TOWN:"): { ___preText = openingTag + LanguageManager.CurrentLanguage.visualnovel.visualnovel_mirageName2 + closingTag + ":"; break; }
                    case ("MIRAGE:"): { ___preText = openingTag + LanguageManager.CurrentLanguage.visualnovel.visualnovel_mirageName3 + closingTag + ":"; break; }
                    default: { break; }
                }
            }
            return true;
        }
    }
}
