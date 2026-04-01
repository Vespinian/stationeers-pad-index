using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using StationeersMods.Interface;
using Assets.Scripts.Objects.Electrical;
using Objects.Electrical;

namespace SatellitePadIndex
{
    [StationeersMod("SatellitePadIndex", "SatellitePadIndex", "1.0.0")]
    class SatellitePadIndex : ModBehaviour
    {

        public override void OnLoaded(ContentHandler contentHandler)
        {
            base.OnLoaded(contentHandler);

            Harmony harmony = new Harmony("SatellitePadIndex");
            harmony.PatchAll();
            UnityEngine.Debug.Log("Satellite Pad Index Loaded!");
        }


        [HarmonyPatch(typeof(SatelliteDish), "GetTargetLandingPad")]
        public class GetTargetLandingPad
        {
            static void Postfix(ref ITraderDestination __result, List<ITraderDestination> ____foundPads, int ____targetPadIndex)
            {
                if (____foundPads.Count == 0 || ____targetPadIndex <= 10)
                    return;

                var match = ____foundPads.FirstOrDefault(pad =>
                    pad.LandingPadNetwork?.StructureList != null &&
                    pad.LandingPadNetwork.StructureList
                        .OfType<LandingPadDataPowerConnection>()
                        .Any(s => s.ReferenceId == ____targetPadIndex));
                if (match != null)
                {
                    __result = match;
                }
            }
        }
    }
}
