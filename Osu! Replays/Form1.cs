using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Osu__Replays
{
    public partial class Form1 : Form
    {
        osuDatabase database;

        public Form1()
        {
            InitializeComponent();
            if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\osu!"))
            {
                toolStripStatusLabel1.Text = "Loading osu! Replays";
                var watch = System.Diagnostics.Stopwatch.StartNew();
                loadDB(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\osu!\\osu!.db");
                int replays = loadReplays(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\osu!\\Data");
                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                toolStripStatusLabel1.Text = "Loaded Replays in " + elapsedMs + "ms (" + database.beatmaps.Count() + " beatmaps) (" + replays + " replays)";
            }
            if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\osu!"))
            {
                toolStripStatusLabel1.Text = "Loading osu! Replays";
                var watch = System.Diagnostics.Stopwatch.StartNew();
                loadDB(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\osu!\\osu!.db");
                int replays = loadReplays(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\osu!\\Data");
                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                toolStripStatusLabel1.Text = "Loaded Replays in " + elapsedMs + "ms (" + database.beatmaps.Count() + " beatmaps) (" + replays + " replays)";
            }
            if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\osu!"))
            {
                toolStripStatusLabel1.Text = "Loading osu! Replays";
                var watch = System.Diagnostics.Stopwatch.StartNew();
                loadDB(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\osu!\\osu!.db");
                int replays = loadReplays(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\osu!\\Data");
                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                toolStripStatusLabel1.Text = "Loaded Replays in " + elapsedMs + "ms (" + database.beatmaps.Count() + " beatmaps) (" + replays + " replays)";
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "osu!.db";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                loadDB(openFileDialog1.FileName);
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            dataGridView1.Width = this.Width - 16;
            dataGridView1.Height = this.Height - 77;
            statusStrip1.Top = this.Height - 22;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                loadReplays(folderBrowserDialog1.SelectedPath);
            }
        }

        private List<string> GetFiles(string path, string pattern)
        {
            var files = new List<string>();

            try
            {
                files.AddRange(Directory.GetFiles(path, pattern, SearchOption.TopDirectoryOnly));
                foreach (var directory in Directory.GetDirectories(path))
                    if (directory.Contains("Songs") == false)
                    {
                        files.AddRange(GetFiles(directory, pattern));
                    }
            }
            catch (UnauthorizedAccessException) { }

            return files;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            var rows = dataGridView1.SelectedRows;
            // Very bad piece of code, sorry!
            if (rows.Count == 1)
            {
                string file = rows[0].Cells[0].Value.ToString();
                saveFileDialog1.InitialDirectory = new FileInfo(file).Directory.FullName;
                string date = rows[0].Cells[1].Value.ToString().Replace(":", "-").Replace(" ", "_");
                string player = rows[0].Cells[2].Value.ToString();
                string title = rows[0].Cells[3].Value.ToString();
                string artist = rows[0].Cells[4].Value.ToString();
                string difficulty = rows[0].Cells[5].Value.ToString();
                string mods = rows[0].Cells[6].Value.ToString();
                saveFileDialog1.FileName = player + " - " + artist + " - " + title + " [" + difficulty + "] [" + mods + "] (" + date + ") Osu.osr";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    File.Copy(file, saveFileDialog1.FileName);
                }
            }
            else if (rows.Count > 1)
            {
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    for (int i = 0; i < rows.Count; i++)
                    {
                        string file = rows[i].Cells[0].Value.ToString();
                        string date = rows[i].Cells[1].Value.ToString().Replace(":", "-").Replace(" ", "_");
                        string player = rows[i].Cells[2].Value.ToString();
                        string title = rows[i].Cells[3].Value.ToString();
                        string artist = rows[i].Cells[4].Value.ToString();
                        string difficulty = rows[i].Cells[5].Value.ToString();
                        string mods = rows[i].Cells[6].Value.ToString();
                        string filename = player + " - " + artist + " - " + title + " [" + difficulty + "] [" + mods + "] (" + date + ") Osu.osr";
                        filename = filename.Replace(":", "_");
                        try
                        {
                            File.Copy(file, folderBrowserDialog1.SelectedPath + "\\" + filename);
                        }
                        catch (Exception) { }
                    }
                }
            }
        }
        void loadDB(string file)
        {
            toolStripStatusLabel1.Text = "Loading osu!.db";
            var watch = System.Diagnostics.Stopwatch.StartNew();
            database = new osuDatabase(file);
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            toolStripStatusLabel1.Text = "Loaded osu!.db in " + elapsedMs + "ms (" + database.beatmaps.Count() + " beatmaps), please load replay folder (you can just use the osu! folder for this)";
        }
        int loadReplays(string directory)
        {
            toolStripStatusLabel1.Text = "Loading osu! Replay files...";
            var watch = System.Diagnostics.Stopwatch.StartNew();
            string[] files = GetFiles(directory, "*.osr").ToArray<string>();
            foreach (string file in files)
            {
                using (BinaryReader b = new BinaryReader(File.Open(file, FileMode.Open)))
                {
                    int gamemode = b.ReadByte();        // (byte)   game mode
                    int gameversion = b.ReadInt32();    // (int)    game version
                    b.ReadByte();                       //          useless byte
                    string songmd5 = b.ReadString();    // (string) song md5 hash
                    b.ReadByte();                       //          useless byte
                    string player = b.ReadString();     // (string) username
                    b.ReadByte();                       //          useless byte
                    b.ReadString();                     //          replay hash (useless)
                    short s300 = b.ReadInt16();         // (short)  amount of 300s
                    short s100 = b.ReadInt16();         // (short)  amount of 100s
                    short s50 = b.ReadInt16();          // (short)  amount of 50s
                    short sGeki = b.ReadInt16();        // (short)  amount of Gekis (special 300s)
                    short sKatu = b.ReadInt16();        // (short)  amount of Katus (special 100s)
                    short misses = b.ReadInt16();       // (short)  amount of misses
                    int score = b.ReadInt32();          // (int)    score
                    short combo = b.ReadInt16();        // (short)  combo
                    byte fc = b.ReadByte();             // (byte)   full combo (0=no,1=yes)
                    int mods = b.ReadInt32();           // (int)    mods
                    b.Close();
                    beatmapInfo beatmap = database.findByHash(songmd5);
                    string date = File.GetCreationTime(file).ToString("yyyy-MM-dd HH:mm:ss");
                    float accuracy = (float)(s50 * 50 + s100 * 100 + s300 * 300) / ((float)(misses + s50 + s100 + s300) * 300) * 100;
                    string accuracyText = accuracy.ToString("N2") + "%";
                    dataGridView1.Rows.Add(new object[] { file, date, player, beatmap.songTitle, beatmap.artist, beatmap.difficultyName, OsuMods.ToString(mods), accuracyText,
                        score, combo, s300, sGeki, s100, sKatu, s50, misses,beatmap.creator});
                    dataGridView1.Sort(Timestamp, ListSortDirection.Descending);
                }
            }
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            toolStripStatusLabel1.Text = "Loaded " + files.Count() + " replays in " + elapsedMs + "ms!";
            return files.Count();
        }
    }
}