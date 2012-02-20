using System;
using System.Collections.Generic;
using System.Text;

namespace com.alexleekt.aideNotebook
{
    class FrequencyController
    {
        public static readonly KeyValuePair<int, String>[] kvpFrequencyPercentages = {
            new KeyValuePair<int, String>(50, "Power Save")
            , new KeyValuePair<int, String>(75, "Balanced")
            , new KeyValuePair<int, String>(99, "Performance")
            , new KeyValuePair<int, String>(100, "Turbo Boost")
        };

        public static KeyValuePair<int, String> GetNextFrequencyPercentage(int currentFrequencyPercentage)
        {
            foreach (KeyValuePair<int, String> kvpFrequencyPercentage in kvpFrequencyPercentages)
            {
                if (kvpFrequencyPercentage.Key > currentFrequencyPercentage)
                {
                    return kvpFrequencyPercentage;
                }
            }

            // next one not available, return the first
            return kvpFrequencyPercentages[0];
        }

        public static String GetDescription(int percentage)
        {
            foreach (KeyValuePair<int, String> kvpFrequencyPercentage in kvpFrequencyPercentages)
            {
                if (kvpFrequencyPercentage.Key == percentage)
                {
                    return kvpFrequencyPercentage.Value;
                }
            }

            //if we don't find it, return empty string
            return "";
        }
    }
}
