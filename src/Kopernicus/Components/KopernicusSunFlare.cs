/**
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
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Kopernicus.Components
{
    /// <summary>
    /// Modifications for the SunFlare component
    /// </summary>
    public class KopernicusSunFlare : SunFlare
    {
        private class MapObjectData
        {
            public CelestialBody body;
            public Vector3d targetDistance;
            public double num2;
        }

        private static List<MapObjectData> mapObjectCache = new List<MapObjectData>(50);
        private static int mapObjectCount;
        private static int cacheLastFrame = 0;

        protected override void Awake()
        {
            Camera.onPreCull += PreCull;
        }

        [SuppressMessage("ReSharper", "Unity.IncorrectMethodSignature")]
        private void PreCull(Camera camera)
        {
            Vector3d scaledSpace = target.transform.position - ScaledSpace.LocalToScaledSpace(sun.position);
            sunDirection = scaledSpace.normalized;
            if (sunDirection != Vector3d.zero)
            {
                transform.forward = sunDirection;
            }
        }

        [SuppressMessage("ReSharper", "DelegateSubtraction")]
        protected override void OnDestroy()
        {
            Camera.onPreCull -= PreCull;
            base.OnDestroy();
        }

        // Overload the stock LateUpdate function
        private void LateUpdate()
        {
            Vector3d position = target.position;
            Vector3d sunPosition = ScaledSpace.LocalToScaledSpace(sun.position);
            sunDirection = position - sunPosition;
            double sunDistance = sunDirection.magnitude;
            sunDirection /= sunDistance; // normalize;
            transform.forward = sunDirection;
            sunFlare.brightness = brightnessMultiplier
                                  * brightnessCurve.Evaluate((float)(1.0 / (sunDistance / (AU * ScaledSpace.InverseScaleFactor))));

            if (PlanetariumCamera.fetch.target == null
                || HighLogic.LoadedScene != GameScenes.TRACKSTATION
                && HighLogic.LoadedScene != GameScenes.FLIGHT)
            {
                return;
            }

            int frameCount = Time.frameCount;

            if (cacheLastFrame != frameCount)
            {
                cacheLastFrame = frameCount;

                int cacheSize = mapObjectCache.Count;
                int mapObjectIndex = 0;

                Vector3 planetariumPosition = PlanetariumCamera.fetch.transform.position;

                for (int i = PlanetariumCamera.fetch.targets.Count; i-- > 0;)
                {
                    MapObject mapTarget = PlanetariumCamera.fetch.targets[i];

                    if (mapTarget.IsNullOrDestroyed())
                        continue;

                    if (mapTarget.type != MapObject.ObjectType.CelestialBody)
                        continue;

                    SphereCollider collider = mapTarget.GetComponent<SphereCollider>();
                    if (collider.IsNullOrDestroyed())
                        continue;

                    if (!mapTarget.GetComponent<MeshRenderer>().enabled)
                        continue;

                    if (mapTarget.transform.localScale.x < 1.0 || mapTarget.transform.localScale.x >= 3.0)
                        continue;

                    MapObjectData mapObjectData;
                    if (mapObjectIndex < cacheSize)
                    {
                        mapObjectData = mapObjectCache[mapObjectIndex];
                    }
                    else
                    {
                        mapObjectData = new MapObjectData();
                        mapObjectCache.Add(mapObjectData);
                        cacheSize++;
                    }

                    mapObjectIndex++;

                    mapObjectData.body = mapTarget.celestialBody;
                    mapObjectData.targetDistance = planetariumPosition - mapTarget.transform.position;
                    double radius = collider.radius;
                    mapObjectData.num2 = Vector3d.Dot(mapObjectData.targetDistance, mapObjectData.targetDistance) - radius * radius;
                }

                mapObjectCount = mapObjectIndex;
            }

            bool state = true;
            for (int i = mapObjectCount; i-- > 0;)
            {
                MapObjectData mapObjectData = mapObjectCache[i];

                if (mapObjectData.body.RefEquals(sun))
                    continue;

                double num1 = 2.0 * Vector3d.Dot(-sunDirection, mapObjectData.targetDistance);
                double d = num1 * num1 - 4.0 * mapObjectData.num2;
                if (d < 0)
                    continue;

                double dSqrt = Math.Sqrt(d);
                double num3 = (-num1 + dSqrt) * 0.5;
                double num4 = (-num1 - dSqrt) * 0.5;
                if (num3 >= 0.0 && num4 >= 0.0)
                {
                    state = false;
                    break;
                }
            }

            SunlightEnabled(state);
        }
    }
}
