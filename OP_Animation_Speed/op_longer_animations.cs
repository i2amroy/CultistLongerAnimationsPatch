using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using SecretHistories.Manifestations;
using UnityEngine;

public class op_longer_animations : MonoBehaviour
{
    public static void Initialise()
    {
        NoonUtility.Log("op_longer_animations: Initialising");

        Harmony harmony = new Harmony("opanimations");
        try
        {
            var doAnim_original = typeof(CardManifestation).GetMethod("DoAnim", BindingFlags.NonPublic | BindingFlags.Instance);
            if (doAnim_original == null)
            {
                NoonUtility.Log("Cannot find DoAnim method");
            }
            var prefix = typeof(op_doAnim_Patch).GetMethod("Prefix");
            if (prefix == null)
            {
                NoonUtility.Log("Cannot find Prefix method");
            }
            harmony.Patch(doAnim_original, prefix: new HarmonyMethod(prefix));
        }
        catch (Exception e)
        {
            NoonUtility.Log(e.ToString());
            NoonUtility.LogException(e);
        }
    }
}

static class op_doAnim_Patch
{
    public static void Prefix(ref float duration, int frameCount, int frameIndex)
    {
        duration = duration * 2.5f;
    }
}