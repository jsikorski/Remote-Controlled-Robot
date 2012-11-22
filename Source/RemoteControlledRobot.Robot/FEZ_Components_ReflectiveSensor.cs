/*
Copyright 2010 GHI Electronics LLC
Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the License. You may obtain a copy of the License at
http://www.apache.org/licenses/LICENSE-2.0
Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the specific language governing permissions and limitations under the License. 
*/

using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using GHIElectronics.NETMF.Hardware;
using GHIElectronics.NETMF.FEZ;

namespace GHIElectronics.NETMF.FEZ
{
    public static partial class FEZ_Components
    {
        public class ReflectiveSensor : IDisposable
        {
            AnalogIn adc;
            int trigger_level;
			
            public void Dispose()
            {
                adc.Dispose();
            }

            public ReflectiveSensor(FEZ_Pin.AnalogIn pin, int reflection_detection_trigger_percentage)
            {
                adc = new AnalogIn((AnalogIn.Pin)pin);
                adc.SetLinearScale(0, 100);
                if (reflection_detection_trigger_percentage < 100 && reflection_detection_trigger_percentage >= 0)
                    trigger_level = reflection_detection_trigger_percentage;
                else
                    throw new Exception("You must enter a percentage value betweeon 0 and 100");
            }

            public enum DetectingState : byte
            {
                ReflectionNotDetected = 0,
                ReflectionDeteced = 1,
            }

            public DetectingState GetState()
            {
                Debug.Print(adc.Read().ToString());
                return (adc.Read() >= trigger_level) ? DetectingState.ReflectionDeteced : DetectingState.ReflectionNotDetected;
            }
        }
    }
}