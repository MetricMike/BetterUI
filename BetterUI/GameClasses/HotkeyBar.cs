﻿using BetterUI.Patches;
using HarmonyLib;

namespace BetterUI.GameClasses
{
    [HarmonyPatch]
    public static class BetterHotkeyBar
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(HotkeyBar), "UpdateIcons")]
        private static void PatchHotkeyBar(ref HotkeyBar __instance, ref Player player)
        {
            if (!player || player.IsDead())
            {
                return;
            }

            foreach (HotkeyBar.ElementData element in __instance.m_elements)
            {
                // would be better if this could be done reliably in an Awake/ Start, but I'm not in the mood to look for one
                if (element.m_icon.gameObject.GetComponent<ItemIconUpdater>() == null)
                {
                    var origScaleComp = element.m_icon.gameObject.AddComponent<ItemIconUpdater>();
                    origScaleComp.Setup(element.m_icon);
                }

                ElementHelper.UpdateElement(element.m_durability);
            }
        }
    }
}