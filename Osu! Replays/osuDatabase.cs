using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Osu__Replays
{
    class osuDatabase
    {
        int osuVersion;
        int folderCount;
        bool accountUnlocked;
        string playerName;
        int numberOfBeatmaps;
        public List<beatmapInfo> beatmaps;
        int validatorByte;

        public osuDatabase(string filename)
        {
            using (BinaryReader b = new BinaryReader(File.Open(filename, FileMode.Open)))
            {
                osuVersion = b.ReadInt32();
                folderCount = b.ReadInt32();
                accountUnlocked = b.ReadBoolean();
                b.ReadBytes(9); // Read past unlock DateTime
                playerName = b.ReadString();
                numberOfBeatmaps = b.ReadInt32();
                beatmaps = new List<beatmapInfo>();
                for (int i = 0; i < numberOfBeatmaps; i++)
                {
                    beatmapInfo beatmap = new beatmapInfo();
                    beatmap.size = b.ReadInt32();
                    if (b.ReadByte() == 0x0B)
                    {
                        beatmap.artist = b.ReadString();
                    }
                    if (b.ReadByte() == 0x0B)
                    {
                        beatmap.artistUnicode = b.ReadString();
                    }
                    if (b.ReadByte() == 0x0B)
                    {
                        beatmap.songTitle = b.ReadString();
                    }
                    if (b.ReadByte() == 0x0B)
                    {
                        beatmap.songTitleUnicode = b.ReadString();
                    }
                    if (b.ReadByte() == 0x0B)
                    {
                        beatmap.creator = b.ReadString();
                    }
                    if (b.ReadByte() == 0x0B)
                    {
                        beatmap.difficultyName = b.ReadString();
                    }
                    if (b.ReadByte() == 0x0B)
                    {
                        beatmap.audioFile = b.ReadString();
                    }
                    if (b.ReadByte() == 0x0B)
                    {
                        beatmap.beatmapHash = b.ReadString();
                    }
                    if (b.ReadByte() == 0x0B)
                    {
                        beatmap.osuFile = b.ReadString();
                    }
                    beatmap.rankedStatus = b.ReadByte();
                    beatmap.hitCircles = b.ReadInt16();
                    beatmap.sliders = b.ReadInt16();
                    beatmap.spinners = b.ReadInt16();
                    beatmap.modificationTime = b.ReadInt64();
                    beatmap.approachRate = b.ReadSingle();
                    beatmap.circleSize = b.ReadSingle();
                    beatmap.hpDrain = b.ReadSingle();
                    beatmap.difficultyScore = b.ReadSingle();
                    beatmap.sliderVelocity = b.ReadDouble();
                    int amountOfPairs = b.ReadInt32();
                    beatmap.starRatingStandard = new List<IntDouble>();
                    for (int j = 0; j < amountOfPairs; j++)
                    {
                        IntDouble pair = new IntDouble(b.ReadBytes(14));
                        beatmap.starRatingStandard.Add(pair);
                    }
                    amountOfPairs = b.ReadInt32();
                    beatmap.starRatingTaiko = new List<IntDouble>();
                    for (int j = 0; j < amountOfPairs; j++)
                    {
                        IntDouble pair = new IntDouble(b.ReadBytes(14));
                        beatmap.starRatingTaiko.Add(pair);
                    }
                    amountOfPairs = b.ReadInt32();
                    beatmap.starRatingCTB = new List<IntDouble>();
                    for (int j = 0; j < amountOfPairs; j++)
                    {
                        IntDouble pair = new IntDouble(b.ReadBytes(14));
                        beatmap.starRatingCTB.Add(pair);
                    }
                    amountOfPairs = b.ReadInt32();
                    beatmap.starRatingMania = new List<IntDouble>();
                    for (int j = 0; j < amountOfPairs; j++)
                    {
                        IntDouble pair = new IntDouble(b.ReadBytes(14));
                        beatmap.starRatingMania.Add(pair);
                    }
                    beatmap.drainTime = b.ReadInt32();
                    beatmap.totalTime = b.ReadInt32();
                    beatmap.previewTime = b.ReadInt32();
                    beatmap.timingPoints = new List<TimingPoint>();
                    int amountOfTimingPoints = b.ReadInt32();
                    for (int j = 0; j < amountOfTimingPoints; j++)
                    {
                        TimingPoint timingPoint = new TimingPoint(b.ReadBytes(17));
                        beatmap.timingPoints.Add(timingPoint);
                    }
                    beatmap.id = b.ReadInt32();
                    beatmap.setId = b.ReadInt32();
                    beatmap.threadId = b.ReadInt32();
                    beatmap.gradeStandard = b.ReadByte();
                    beatmap.gradeTaiko = b.ReadByte();
                    beatmap.gradeCTB = b.ReadByte();
                    beatmap.gradeMania = b.ReadByte();
                    beatmap.localOffset = b.ReadInt16();
                    beatmap.stackLeniency = b.ReadSingle();
                    beatmap.gameMode = b.ReadByte();
                    if (b.ReadByte() == 0x0B)
                    {
                        beatmap.source = b.ReadString();
                    }
                    if (b.ReadByte() == 0x0B)
                    {
                        beatmap.tags = b.ReadString();
                    }
                    beatmap.onlineOffset = b.ReadInt16();
                    if (b.ReadByte() == 0x0B)
                    {
                        beatmap.font = b.ReadString();
                    }
                    beatmap.unplayed = b.ReadBoolean();
                    beatmap.lastPlayed = b.ReadInt64();
                    beatmap.osz2 = b.ReadBoolean();
                    if (b.ReadByte() == 0x0B)
                    {
                        beatmap.folder = b.ReadString();
                    }
                    beatmap.repoTime = b.ReadInt64();
                    beatmap.ignoreSounds = b.ReadBoolean();
                    beatmap.ignoreSkin = b.ReadBoolean();
                    beatmap.disableStoryboard = b.ReadBoolean();
                    beatmap.disableVideo = b.ReadBoolean();
                    beatmap.visualOverride = b.ReadBoolean();
                    beatmap.lastModificationTime = b.ReadInt32();
                    beatmap.maniaScrollSpeed = b.ReadByte();
                    beatmaps.Add(beatmap);
                }
                validatorByte = b.ReadByte();
                b.Close();
            }
        }

        public beatmapInfo findByHash(string hash)
        {
            foreach(beatmapInfo beatmap in beatmaps)
            {
                if (hash == beatmap.beatmapHash)
                {
                    return beatmap;
                }
            }
            beatmapInfo fakeoutput = new beatmapInfo();
            fakeoutput.songTitle = "Not found";
            return fakeoutput;
        }
    }
}
