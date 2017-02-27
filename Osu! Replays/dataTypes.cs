using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Osu__Replays
{
    class IntDouble
    {
        int intVar;
        double doubleVar;
        public IntDouble(byte[] pair)
        {
            intVar = BitConverter.ToInt32(pair, 1);
            doubleVar = BitConverter.ToDouble(pair,6);
        }
    }

    class TimingPoint
    {
        double bpm;
        double offset;
        byte inherited;
        public TimingPoint(byte[] pair)
        {
            bpm = BitConverter.ToDouble(pair, 0);
            offset = BitConverter.ToDouble(pair, 8);
            inherited = pair[16];
        }
    }
    class OsuMods
    {
        public static string ToString(int mods)
        {
            string outstring = "";
            BitArray b = new BitArray(new int[] { mods });
            if (b[0]) { outstring += "NF"; }
            if (b[1]) { outstring += "EZ"; }
            if (b[2]) { outstring += "NV"; }
            if (b[3]) { outstring += "HD"; }
            if (b[4]) { outstring += "HR"; }
            if (b[5]) { outstring += "SD"; }
            if (b[6] && !b[9]) { outstring += "DT"; }
            if (b[7]) { outstring += "RL"; }
            if (b[8]) { outstring += "HT"; }
            if (b[9]) { outstring += "NC"; }
            if (b[10]) { outstring += "FL"; }
            if (b[11]) { outstring += "AUTO"; }
            if (b[12]) { outstring += "SO"; }
            if (b[13]) { outstring += "AP"; }
            if (b[14]) { outstring += "PF"; }
            if (b[15]) { outstring += "4K"; }
            if (b[16]) { outstring += "5K"; }
            if (b[17]) { outstring += "6K"; }
            if (b[18]) { outstring += "7K"; }
            if (b[19]) { outstring += "8K"; }
            if (b[20]) { outstring += "FI"; }
            if (b[21]) { outstring += "RD"; }
            if (b[22]) { outstring += "CM"; }
            if (b[23]) { outstring += "TP"; }
            if (b[24]) { outstring += "9K"; }
            if (b[25]) { outstring += "COOP"; }
            if (b[26]) { outstring += "1K"; }
            if (b[27]) { outstring += "3K"; }
            if (b[28]) { outstring += "2K"; }
            if (b[29]) { outstring += "V2"; }
            if (outstring == ""){outstring = "None";}
            return outstring;
        }
    }
}