using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Osu__Replays
{
    class beatmapInfo
    {
        public int size;
        public string artist;
        public string artistUnicode;
        public string songTitle;
        public string songTitleUnicode;
        public string creator;
        public string difficultyName;
        public string audioFile;
        public string beatmapHash;
        public string osuFile;
        public byte rankedStatus;
        public short hitCircles;
        public short sliders;
        public short spinners;
        public long modificationTime;
        public Single approachRate;
        public Single circleSize;
        public Single hpDrain;
        public Single difficultyScore;
        public double sliderVelocity;
        public List<IntDouble> starRatingStandard;
        public List<IntDouble> starRatingTaiko;
        public List<IntDouble> starRatingCTB;
        public List<IntDouble> starRatingMania;
        public int drainTime;
        public int totalTime;
        public int previewTime;
        public List<TimingPoint> timingPoints;
        public int id;
        public int setId;
        public int threadId;
        public byte gradeStandard;
        public byte gradeTaiko;
        public byte gradeCTB;
        public byte gradeMania;
        public short localOffset;
        public Single stackLeniency;
        public byte gameMode;
        public string source;
        public string tags;
        public short onlineOffset;
        public string font;
        public bool unplayed;
        public long lastPlayed;
        public bool osz2;
        public string folder;
        public long repoTime;
        public bool ignoreSounds;
        public bool ignoreSkin;
        public bool disableStoryboard;
        public bool disableVideo;
        public bool visualOverride;
        public int lastModificationTime;
        public byte maniaScrollSpeed;
    }
}
