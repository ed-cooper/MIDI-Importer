using NAudio.Midi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace MIDI_Importer
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// The name used for saving the converted data.
        /// </summary>
        string saveName = "Data";
        /// <summary>
        /// The open midi file.
        /// </summary>
        MidiFile midi;
        /// <summary>
        /// The list of channels within the midi file. (0 - 16, excluding drum tracks)
        /// </summary>
        List<int> Channels = new List<int>();
        /// <summary>
        /// The list of notes within the midi file.
        /// </summary>
        List<NoteOnEvent> Notes = new List<NoteOnEvent>();
        /// <summary>
        /// The list of tempo changes within the midi file.
        /// </summary>
        List<TempoEvent> TempoChanges = new List<TempoEvent>();

        /// <summary>
        /// The list of instruments supported by Scratch.
        /// </summary>
        string[] ScratchVoices = new string[21] {"Piano",
                                                 "Electric Piano",
                                                 "Organ",
                                                 "Guitar",
                                                 "Electric Guitar",
                                                 "Bass",
                                                 "Pizzicato",
                                                 "Cello",
                                                 "Trombone",
                                                 "Clarinet",
                                                 "Saxaphone",
                                                 "Flute",
                                                 "Wooden Flute",
                                                 "Bassoon",
                                                 "Choir",
                                                 "Vibraphone",
                                                 "Music Box",
                                                 "Steel Drum",
                                                 "Marimba",
                                                 "Synth Lead",
                                                 "Synth Pad"};

        /// <summary>
        /// Initialises the form.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Occurs when the load button is clicked.
        /// </summary>
        private void BtnLoad_Click(object sender, EventArgs e)
        {
            // Create and display dialog
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "MIDI Files (*.mid) | *.mid";
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                // File selected so load midi file
                LoadMidi(dialog.FileName);
            }
        }

        /// <summary>
        /// Loads the notes and tempo changes from the specified midi file.
        /// </summary>
        /// <param name="file">The file path of the midi file to upload.</param>
        void LoadMidi(string file)
        {
            // Display file name
            FileLbl.Text = Path.GetFileName(file);

            // Open midi file
            midi = new MidiFile(file, false);

            // Create save name for midi file
            saveName = Path.GetFileNameWithoutExtension(file);

            // Resets the list of channels
            Channels = new List<int>();
            ChannelTable.Rows.Clear();

            // The list of voices used in the midi file
            List<PatchChangeEvent> PatchChanges = new List<PatchChangeEvent>();

            // For every track/channel
            foreach (IList<MidiEvent> track in midi.Events)
            {
                // For every midi event (note, metadata, etc.)
                foreach (MidiEvent midiEvent in track)
                {
                    switch (midiEvent.CommandCode)
                    {
                        case MidiCommandCode.NoteOn: // Note
                            NoteOnEvent ne = (NoteOnEvent)midiEvent;
                            if (ne.OffEvent != null)
                            {
                                Notes.Add(ne);
                            }

                            if (!Channels.Contains(ne.Channel) && ne.Channel != 16 && ne.Channel != 10)
                            {
                                // New instument channel discovered so add to channel list
                                Channels.Add(ne.Channel);
                            }

                            break;
                        case MidiCommandCode.MetaEvent: // Metadata
                            MetaEvent me = (MetaEvent)midiEvent;
                            switch (me.MetaEventType)
                            {
                                case MetaEventType.SetTempo: // Tempo Change
                                    TempoChanges.Add((TempoEvent)me);
                                    break;
                            }
                            break;
                        case MidiCommandCode.PatchChange: // Patch (instrument)
                            PatchChanges.Add((PatchChangeEvent)midiEvent);
                            break;
                    }
                }
            }

            // Sort channels numerically
            Channels.Sort();

            // Add rows to ChannelTable (TODO: refactor code)

            // For each channel
            foreach (int channel in Channels)
            {
                // Identify MIDI voice (default is acoustic grand)
                string instrument = "Acoustic Grand";

                // For each midi voice
                foreach (PatchChangeEvent pe in PatchChanges)
                {
                    if (pe.Channel == channel) // If instrument channel matches current channel
                    {
                        // Use sneaky hacks to find instument name
                        instrument = pe.ToString().Split(':')[1].Replace(" " + pe.Channel + " ", "");
                    }
                }

                // Add row to ChannelTable
                ChannelTable.Rows.Add(channel, instrument, false, "Piano");
            }

            // Finally, sort notes by position in the piece
            Notes.Sort((x, y) => x.AbsoluteTime.CompareTo(y.AbsoluteTime));
        }

        /// <summary>
        /// Occurs when the save button is clicked.
        /// </summary>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            processAndSave();
        }

        /// <summary>
        /// Processes and saves the midi data.
        /// </summary>
        void processAndSave()
        {
            // Current time in beats of the current note
            double currentTime = -1;

            // Notes data
            List<string> NotesStart = new List<string>(); // Start times for each note (per bar)
            List<string> NotesPitch = new List<string>(); // Pitch of each note (60 = middle C)
            List<string> NotesDuration = new List<string>(); // Duration of each note
            List<string> NotesVoice = new List<string>(); // Instrument of each notes
            List<string> NotesVolume = new List<string>(); // Volume of each note
            List<string> NotesIndexes = new List<string>(); // Indexes for the start of each bar in the previous data

            // Drums data
            List<string> DrumsStart = new List<string>(); // Start times for each drum (per bar)
            List<string> DrumsDuration = new List<string>(); // Duration of each drum
            List<string> DrumsDrum = new List<string>(); // Percussion instrument to use
            List<string> DrumsVolume = new List<string>(); // Volume of each drum
            List<string> DrumsIndexes = new List<string>(); // Indexes for the start of each bar in the previous data

            // Tempo of each bar
            List<string> BarTempo = new List<string>();

            // Convert notes data

            // For each note event
            foreach (NoteOnEvent ne in Notes)
            {
                // Detect if new bar has started
                double currentTimeOld = currentTime;
                currentTime = ne.AbsoluteTime / (double)midi.DeltaTicksPerQuarterNote;
                if (Math.Floor(currentTime / 4) > Math.Floor(currentTimeOld / 4))
                {
                    // New bar, so add spacers and index
                    NotesStart.Add("-");
                    NotesPitch.Add("-");
                    NotesDuration.Add("-");
                    NotesVoice.Add("-");
                    NotesVolume.Add("-");
                    NotesIndexes.Add((NotesStart.Count + 1).ToString());

                    DrumsStart.Add("-");
                    DrumsDuration.Add("-");
                    DrumsDrum.Add("-");
                    DrumsVolume.Add("-");
                    DrumsIndexes.Add((DrumsStart.Count + 1).ToString());

                    // Find tempo of bar (could be more efficient)
                    int tempoEvent = 0;
                    while (tempoEvent < TempoChanges.Count - 1 && (TempoChanges[tempoEvent].AbsoluteTime / (double)midi.DeltaTicksPerQuarterNote) < currentTime)
                    {
                        tempoEvent++;
                    }

                    BarTempo.Add(TempoChanges[tempoEvent].Tempo.ToString());
                }

                if (ne.Channel != 16 && ne.Channel != 10)
                {
                    // Instrument track

                    // Find ChannelTable row relating to this channel
                    int rowIndex = Array.IndexOf(Channels.ToArray(), ne.Channel);
                    if (rowIndex > -1)
                    {
                        DataGridViewRow row = ChannelTable.Rows[rowIndex];

                        // Add data
                        NotesStart.Add(Math.Round(currentTime % 4, 2).ToString()); // Modulus because start values are per bar
                        NotesPitch.Add(ne.NoteNumber.ToString());
                        NotesDuration.Add(Math.Round(ne.NoteLength / (double)midi.DeltaTicksPerQuarterNote, 2).ToString());

                        if ((bool)row.Cells[2].Value == false)
                        {
                            NotesVoice.Add((Array.IndexOf(ScratchVoices, row.Cells[3].Value) + 1).ToString());
                        }
                        else
                        {
                            // Track has been muted, but we're too lazy to delete the note so we just give a voice of -1
                            NotesVoice.Add("-1");
                        }
                        NotesVolume.Add(Math.Round(ne.Velocity / (double)127 * 100, 2).ToString());
                    }
                }
                else
                {
                    // Drum track

                    // Add data
                    DrumsStart.Add(Math.Round(currentTime % 4, 2).ToString());
                    DrumsDrum.Add(ScratchDrum(ne.NoteNumber).ToString());
                    DrumsDuration.Add(Math.Round(ne.NoteLength / (double)midi.DeltaTicksPerQuarterNote, 2).ToString());
                    DrumsVolume.Add(Math.Round(ne.Velocity / (double)127 * 100, 2).ToString());
                }
            }

            // Add closing data to ensure correct playback (same as adding a new bar)
            NotesStart.Add("-");
            NotesPitch.Add("-");
            NotesDuration.Add("-");
            NotesVoice.Add("-");
            NotesVolume.Add("-");
            NotesIndexes.Add((NotesStart.Count + 1).ToString());

            DrumsStart.Add("-");
            DrumsDuration.Add("-");
            DrumsDrum.Add("-");
            DrumsVolume.Add("-");
            DrumsIndexes.Add((DrumsStart.Count + 1).ToString());

            // Save Files

            // Create directory
            string dir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MIDI Importer\\" + saveName + "\\";
            Directory.CreateDirectory(dir);

            // Save notes
            SaveFile(dir + "Notes - Start.txt", NotesStart);
            SaveFile(dir + "Notes - Pitch.txt", NotesPitch);
            SaveFile(dir + "Notes - Duration.txt", NotesDuration);
            SaveFile(dir + "Notes - Voice.txt", NotesVoice);
            SaveFile(dir + "Notes - Volume.txt", NotesVolume);
            SaveFile(dir + "Notes - Indexes.txt", NotesIndexes);

            if (DrumCheckBox.Checked == true)
            {
                // Save drums
                SaveFile(dir + "Drums - Start.txt", DrumsStart);
                SaveFile(dir + "Drums - Duration.txt", DrumsDuration);
                SaveFile(dir + "Drums - Drum.txt", DrumsDrum);
                SaveFile(dir + "Drums - Volume.txt", DrumsVolume);
                SaveFile(dir + "Drums - Indexes.txt", DrumsIndexes);
            }

            // Save bar tempos
            SaveFile(dir + "Bar Tempo.txt", BarTempo);
        }

        /// <summary>
        /// Returns the Scratch drum equivalent for the specified midi drum. -1 indicates no equivalent.
        /// </summary>
        /// <param name="midiDrum">The midi drum note to find the equivalent of.</param>
        /// <returns>The Scratch drum equivalent for the specified midi drum. -1 indicates no equivalent.</returns>
        private int ScratchDrum(int midiDrum)
        {
            switch (midiDrum)
            {
                case 35: return 2;  // Acoustic Bass Drum -> Bass Drum
                case 36: return 2;  // Bass Drum 1        -> Bass Drum
                case 37: return 3;  // Side Stick         -> Side Stick
                case 38: return 1;  // Acoustic Snare     -> Snare Drum
                case 39: return 8;  // Hand Clap          -> Hand Clap
                case 40: return 1;  // Electric Snare     -> Snare Drum
                case 41: return -1; // Low Floor Tom      -> Unknown
                case 42: return 6;  // Closed Hi-Hat      -> Closed Hi-Hat
                case 43: return -1; // High Floor Tom     -> Unknown
                case 44: return 5;  // Pedal Hi-Hat       -> Open Hi-Hat
                case 45: return -1; // Low Tom            -> Unknown
                case 46: return 5;  // Open Hi-Hat        -> Open Hi-Hat
                case 47: return -1; // Low-Mid Tom        -> Unknown
                case 48: return -1; // Hi-Mid Tom         -> Unknown
                case 49: return 4;  // Crash Cymbal 1     -> Crash Cymbal
                case 50: return -1; // High Tom           -> Unknown
                case 51: return 4;  // Ride Cymbal 1      -> Crash Cymbal
                case 52: return 4;  // Chinese Cymbal     -> Crash Cymbal
                case 53: return -1; // Ride Bell          -> Unknown
                case 54: return 7;  // Tambourine         -> Tambourine
                case 55: return 4;  // Splash Cymbal      -> Crash Cymbal
                case 56: return 11; // Cowbell            -> Cowbell
                case 57: return 4;  // Crash Cymbal 2     -> Crash Cymbal
                case 58: return 17; // Vibraslap          -> Vibraslap
                case 59: return 4;  // Ride Cymbal 2      -> Crash Cymbal
                case 60: return 13; // Hi Bongo           -> Bongo
                case 61: return 13; // Low Bongo          -> Bongo
                case 62: return -1; // Mute Hi Conga      -> Unknown
                case 63: return -1; // Open Hi Conga      -> Unknown
                case 64: return -1; // Low Conga          -> Unknown
                case 65: return -1; // High Timbale       -> Unknown
                case 66: return -1; // Low Timbale        -> Unknown
                case 67: return -1; // High Agogo         -> Unknown
                case 68: return -1; // Low Agogo          -> Unknown
                case 69: return 15; // Cabasa             -> Cabasa
                case 70: return -1; // Maracas            -> Unknown
                case 71: return -1; // Short Whistle      -> Unknown
                case 72: return -1; // Long Whistle       -> Unknown
                case 73: return 16; // Short Guiro        -> Guiro
                case 74: return 16; // Long Guiro         -> Guiro
                case 75: return 9;  // Claves             -> Claves
                case 76: return 10; // Hi Wood Block      -> Wood Block
                case 77: return 10; // Low Wood Block     -> Wood Block
                case 78: return 18; // Mute Cuica         -> Open Cuica
                case 79: return 18; // Open Cuica         -> Open Cuica
                case 80: return 12; // Mute Triangle      -> Triangle
                case 81: return 12; // Open Triangle      -> Triangle
                default: return -1; // Unknown            -> Unknown
            }
        }

        /// <summary>
        /// Saves the specified data to the specified file.
        /// </summary>
        /// <param name="path">The file path to save to.</param>
        /// <param name="data">The list containing the data to save.</param>
        private void SaveFile(string path, List<string> data)
        {
            StreamWriter Writer = File.CreateText(path);
            Writer.Write(string.Join("\r\n", data.ToArray()));
            Writer.Close();
            Writer.Dispose();
        }
    }
}
