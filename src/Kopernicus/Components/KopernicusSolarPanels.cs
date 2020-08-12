﻿/**
 * Kopernicus Planetary System Modifier
 * ------------------------------------------------------------- 
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 3 of the License, or (at your option) any later version.
 *
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston,
 * MA 02110-1301  USA
 * 
 * This library is intended to be used as a plugin for Kerbal Space Program
 * which is copyright of TakeTwo Interactive. Your usage of Kerbal Space Program
 * itself is governed by the terms of its EULA, not the license above.
 * 
 * https://kerbalspaceprogram.com
 */

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using UnityEngine;
using KSP.Localization;

namespace Kopernicus.Components
{

    /// <summary>
    /// This is the replacement Kopernicus Solar Panel extension.
    /// </summary>
    public class KopernicusSolarPanels : ModuleDeployableSolarPanel
    {
        //Strings for Localization
        private static string SP_status_DirectSunlight = Localizer.Format("#Kopernicus_UI_DirectSunlight");  // "Direct Sunlight"
        private static string SP_status_Underwater = Localizer.Format("#Kopernicus_UI_Underwater");          // "Underwater"
        private static string button_Auto = Localizer.Format("#Kopernicus_UI_AutoTracking");                 // "Auto"
        private static string SelectBody = Localizer.Format("#Kopernicus_UI_SelectBody");                    // "Select Tracking Body"
        private static string SelectBody_Msg = Localizer.Format("#Kopernicus_UI_SelectBody_Msg");            // "Please select the Body you want to track with this Solar Panel."

        [KSPField(guiActive = true, guiActiveEditor = false, guiName = "#Kopernicus_UI_TrackingBody", isPersistant = true)]
        [SuppressMessage("ReSharper", "NotAccessedField.Global")]
        public String trackingBodyName;

        [KSPField(isPersistant = true)]
        private Boolean _manualTracking;

        //declare internal float curves
        private static FloatCurve AtmosphericAttenutationAirMassMultiplier = new FloatCurve();
        private static FloatCurve AtmosphericAttenutationSolarAngleMultiplier = new FloatCurve();

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (HighLogic.LoadedSceneIsFlight)
            {
                if (deployState == ModuleDeployablePart.DeployState.EXTENDED)
                {
                    KopernicusStar trackingStar = KopernicusStar.CelestialBodies[trackingBody];

                    Double bestFlux = vessel.solarFlux * 1360 / PhysicsGlobals.SolarLuminosityAtHome;
                    KopernicusStar bestStar = trackingStar;
                    Double totalFlux = 0;
                    Double totalFlow = 0;
                    flowRate = 0;
                    _flowRate = 0;
                    for (Int32 s = 0; s < KopernicusStar.Stars.Count; s++)
                    {
                        KopernicusStar star = KopernicusStar.Stars[s];
                        // Use this star
                        star.shifter.ApplyPhysics();

                        // Set Tracking Speed to zero
                        Single oldTrackingSpeed = trackingSpeed;
                        trackingSpeed = 0;

                        // Change the tracking body
                        trackingBody = star.sun;
                        GetTrackingBodyTransforms();
                        CalculateTracking();

                        //Calculate flux
                        double starFlux = star.CalculateFluxAt(vessel) * (1360 / PhysicsGlobals.SolarLuminosityAtHome);

                        //Check if star has better flux
                        if (bestFlux < starFlux)
                        {
                            bestFlux = starFlux;
                            bestStar = star;
                        }

                        // Add to TotalFlux and EC tally
                        totalFlux += starFlux;
                        float panelEffectivness = 0;
                        //Now for some fancy atmospheric math
                        float atmoDensityMult = 1;
                        float atmoAngleMult = 1;
                        float tempMult = 1;
                        Vector3d localSpace = ScaledSpace.ScaledToLocalSpace(star.target.position);
                        if (this.vessel.atmDensity > 0)
                        {
                            float horizonAngle = (float)Math.Acos(FlightGlobals.currentMainBody.Radius / (FlightGlobals.currentMainBody.Radius + FlightGlobals.ship_altitude));
                            Vector3 horizonVector = new Vector3(0, Mathf.Sin(Mathf.Deg2Rad * horizonAngle), Mathf.Cos(Mathf.Deg2Rad * horizonAngle));
                            float sunZenithAngleDeg = Vector3.Angle(FlightGlobals.upAxis, star.sun.position);

                            Double gravAccelParameter = (vessel.mainBody.gravParameter / Math.Pow(vessel.mainBody.Radius + FlightGlobals.ship_altitude, 2));
                            float massOfAirColumn = (float)(FlightGlobals.getStaticPressure() / gravAccelParameter);

                            tempMult = this.temperatureEfficCurve.Evaluate((float)this.vessel.atmosphericTemperature);
                            atmoDensityMult = AtmosphericAttenutationAirMassMultiplier.Evaluate(massOfAirColumn);
                            atmoAngleMult = AtmosphericAttenutationSolarAngleMultiplier.Evaluate(sunZenithAngleDeg);
                        }

                        panelEffectivness = (chargeRate / 24.4f) / 56.37091313591871f * sunAOA * tempMult * atmoAngleMult * atmoDensityMult;  //56.blabla is a weird constant we use to turn flux into EC
                        totalFlow += (starFlux * panelEffectivness) / (1360 / PhysicsGlobals.SolarLuminosityAtHome);

                        // Restore Tracking Speed
                        trackingSpeed = oldTrackingSpeed;

                    }
                    // Restore the starting star
                    trackingStar.shifter.ApplyPhysics();

                    // Restore the old tracking body
                    trackingBody = trackingStar.sun;
                    GetTrackingBodyTransforms();
                    CalculateTracking();

                    //see if tracked star is blocked or not
                    if (sunAOA > 0)
                    {
                        //this ensures the "blocked" GUI option is set right, if we're exposed to you we're not blocked
                        vessel.directSunlight = true;
                    }
                    // We got the best star to use
                    if ((bestStar != null && bestStar.sun != trackingBody) && (!_manualTracking))
                    {
                        trackingBody = bestStar.sun;
                        GetTrackingBodyTransforms();
                        CalculateTracking();
                    }
                    vessel.solarFlux = totalFlux;
                    //Add to new output
                    flowRate = (float)totalFlow;
                    _flowRate = totalFlow / chargeRate;
                    resHandler.UpdateModuleResourceOutputs(_flowRate);
                }
            }
            // Restore The Current Star
            KopernicusStar.Current.shifter.ApplyPhysics();
        }

        public override void PostCalculateTracking(bool trackingLOS, Vector3 trackingDirection)
        {
            // Calculate sun AOA
            sunAOA = 0f;
            Vector3 trackDir = (trackingBody.transform.position - panelRotationTransform.position).normalized;
            if (!trackingLOS)
            {
                sunAOA = 0f;
                status = Localizer.Format("#Kopernicus_UI_PanelBlocked", blockingObject);
            }
            else
            {
                status = "Direct Sunlight";
                if (panelType == PanelType.FLAT)
                {
                    sunAOA = Mathf.Clamp(Vector3.Dot(trackingDotTransform.forward, trackDir), 0f, 1f);
                }
                else if (panelType != PanelType.CYLINDRICAL)
                {
                    sunAOA = 0.25f;
                }
                else
                {
                    Vector3 direction;
                    if (alignType == PanelAlignType.PIVOT)
                    {
                        direction = trackingDotTransform.forward;
                    }
                    else if (alignType != PanelAlignType.X)
                    {
                        direction = alignType != PanelAlignType.Y ? part.partTransform.forward : part.partTransform.up;
                    }
                    else
                    {
                        direction = part.partTransform.right;
                    }
                    sunAOA = (1f - Mathf.Abs(Vector3.Dot(direction, trackDir))) * 0.318309873f;
                }
            }
        }

        void EarlyLateUpdate()
        {
            if (deployState == ModuleDeployablePart.DeployState.EXTENDED)
            {
                // Update the name
                trackingBodyName = trackingBody.bodyDisplayName.Replace("^N", "");

                if (!_manualTracking)
                {
                    trackingBodyName = Localizer.Format("#Kopernicus_UI_AutoTrackingBodyName", trackingBodyName);
                }
            }
        }

        [KSPEvent(active = true, guiActive = false, guiName = "#Kopernicus_UI_SelectBody")]
        public void ManualTracking()
        {
            // Assemble the buttons
            Int32 stars = KopernicusStar.Stars.Count;
            DialogGUIBase[] options = new DialogGUIBase[stars + 1];
            options[0] = new DialogGUIButton(button_Auto, () => { _manualTracking = false; }, true);//Auto
            for (Int32 i = 0; i < stars; i++)
            {
                CelestialBody body = KopernicusStar.Stars[i].sun;
                options[i + 1] = new DialogGUIButton
                (
                    body.bodyDisplayName.Replace("^N", ""),
                    () => SetTrackingBody(body),
                    true
                );
            }

            PopupDialog.SpawnPopupDialog(new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new MultiOptionDialog(
                "SelectTrackingBody",
                SelectBody_Msg,//Please select the Body you want to track with this Solar Panel.
                SelectBody,//Select Tracking Body
                UISkinManager.GetSkin("MainMenuSkin"),
                options), false, UISkinManager.GetSkin("MainMenuSkin"));
        }

        public void SetTrackingBody(CelestialBody sun)
        {
            _manualTracking = true;
            trackingBody = sun;
            GetTrackingBodyTransforms();
        }
        public override void OnStart(StartState state)
        {
            //Setup Floatcurves
            AtmosphericAttenutationAirMassMultiplier.Add(0f, 1f, 0f, 0f);
            AtmosphericAttenutationAirMassMultiplier.Add(5f, 0.953f, -0.018f, -0.018f);
            AtmosphericAttenutationAirMassMultiplier.Add(10f, 0.835f, -0.032f, -0.032f);
            AtmosphericAttenutationAirMassMultiplier.Add(15f, 0.699f, -0.020f, -0.020f);
            AtmosphericAttenutationAirMassMultiplier.Add(20f, 0.616f, -0.014f, -0.014f);
            AtmosphericAttenutationAirMassMultiplier.Add(30f, 0.515f, -0.0076f, -0.0076f);
            AtmosphericAttenutationAirMassMultiplier.Add(40f, 0.454f, -0.0050f, -0.0050f);
            AtmosphericAttenutationAirMassMultiplier.Add(50f, 0.411f, -0.0036f, -0.0036f);
            AtmosphericAttenutationAirMassMultiplier.Add(60f, 0.380f, -0.0028f, -0.0028f);
            AtmosphericAttenutationAirMassMultiplier.Add(80f, 0.335f, -0.0018f, -0.0018f);
            AtmosphericAttenutationAirMassMultiplier.Add(100f, 0.303f, -0.0013f, -0.0013f);
            AtmosphericAttenutationAirMassMultiplier.Add(150f, 0.254f, -0.00074f, -0.00074f);
            AtmosphericAttenutationAirMassMultiplier.Add(200f, 0.224f, -0.00049f, -0.00049f);
            AtmosphericAttenutationAirMassMultiplier.Add(300f, 0.187f, -0.00027f, -0.00027f);
            AtmosphericAttenutationAirMassMultiplier.Add(500f, 0.149f, -0.00013f, -0.00013f);
            AtmosphericAttenutationAirMassMultiplier.Add(800f, 0.121f, -0.00007f, -0.00007f);
            AtmosphericAttenutationAirMassMultiplier.Add(1200f, 0.102f, -0.00004f, 0f);
            AtmosphericAttenutationSolarAngleMultiplier.Add(0f, 1f, 0f, 0f);
            AtmosphericAttenutationSolarAngleMultiplier.Add(15f, 0.985f, -0.0020f, -0.0020f);
            AtmosphericAttenutationSolarAngleMultiplier.Add(30f, 0.940f, -0.0041f, -0.0041f);
            AtmosphericAttenutationSolarAngleMultiplier.Add(45f, 0.862f, -0.0064f, -0.0064f);
            AtmosphericAttenutationSolarAngleMultiplier.Add(60f, 0.746f, -0.0092f, -0.0092f);
            AtmosphericAttenutationSolarAngleMultiplier.Add(75f, 0.579f, -0.0134f, -0.0134f);
            AtmosphericAttenutationSolarAngleMultiplier.Add(90f, 0.336f, -0.0185f, -0.0185f);
            AtmosphericAttenutationSolarAngleMultiplier.Add(105f, 0.100f, -0.008f, -0.008f);
            AtmosphericAttenutationSolarAngleMultiplier.Add(120f, 0.050f, 0f, 0f);
            if (HighLogic.LoadedSceneIsFlight)
            {
                TimingManager.LateUpdateAdd(TimingManager.TimingStage.Early, EarlyLateUpdate);

                Fields["trackingBodyName"].guiActive = true;
                Events["ManualTracking"].guiActive = true;

                if (_manualTracking)
                {
                    CelestialBody trackingBody = FlightGlobals.Bodies.FirstOrDefault(b => b.bodyDisplayName.Replace("^N", "") == trackingBodyName);

                    if (trackingBody != null)
                    {
                        SetTrackingBody(trackingBody);
                    }
                    else
                    {
                        _manualTracking = false;
                    }
                }
            }
            base.OnStart(state);
        }

        public void OnDestroy()
        {
            TimingManager.LateUpdateRemove(TimingManager.TimingStage.Early, EarlyLateUpdate);
        }
    }
}
