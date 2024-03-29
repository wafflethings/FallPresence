﻿using DiscordRPC;
using Ookii.Dialogs.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FallPresence
{
    public partial class FallPresence : Form
    {
        //the SemVer of the version just without decimals
        int version = 130;

        //get the path of the app, so we can do appPath + "/Resources/" later
        string appPath = Application.StartupPath;

        //define the button images, will later be set but not now since we cant use apppath before the actual code starts
        Image startButton;
        Image startButtonPressed;
        Image startButtonDisabled;
        Image logButton;
        Image logButtonPressed;
        Image logButtonDisabled;
        Image stopButton;
        Image stopButtonPressed;

        //if the log path or fg name cant be found, this bool wont let you start
        bool startBtnEnabled = false;

        //while started, you cant edit fg name or log path
        bool logBtnEnabled = true;

        //self explanatory
        bool usernameNull = true;
        bool pathIsGood = false;
        bool inStart = false;
        Thread rpcThread_;
        string currentlyInRound;
        string rpcLogo;
        int currentRoundPlayerCount = -1;
        string showName;
        string currentArche;

        //the player's state, e.g spectating, eliminated, etc 
        string playerState = "";

        //the actual variables to be used by the rp thread
        string logPath;
        string usernameStr;
        private RichPresence presence;
        private Timestamps timestamps;
        private DiscordRpcClient client;
        private Assets assets;
        private DateTime? startDateTime;

        public FallPresence()
        {
            //default winforms stuff, no stretching or maximizing, you know the drill
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            InitializeComponent();

            //setting the previous null image definitions to the resource
            startButton = Properties.Resources.btnstart;
            startButtonPressed = Properties.Resources.btnstart_pressed;
            startButtonDisabled = Properties.Resources.btnstart_disabled;
            logButton = Properties.Resources.btnpicker;
            logButtonPressed = Properties.Resources.btnpicker_pressed;
            logButtonDisabled = Properties.Resources.btnpicker_disabled;
            stopButton = Properties.Resources.btnstop;
            stopButtonPressed = Properties.Resources.btnstop_pressed;

            //give the pictureboxes their images
            picboxButtonLog.Image = logButton;
            picboxButtonStart.Image = startButtonDisabled;

            //tell the pictureboxes what to do when i click them
            //these are all seperate, so that when clicking the event happens just once and when holding the colour doesnt just change for 1 tick
            picboxButtonLog.Click += new EventHandler(picboxButtonLog_Click);
            picboxButtonStart.Click += new EventHandler(picboxButtonStart_Click);
            tutorialPicbox.Click += new EventHandler(tutorial_Click);

            picboxButtonLog.MouseUp += new MouseEventHandler(picboxButtonLog_MouseUp);
            picboxButtonStart.MouseUp += new MouseEventHandler(picboxButtonStart_MouseUp);

            picboxButtonLog.MouseDown += new MouseEventHandler(picboxButtonLog_MouseDown);
            picboxButtonStart.MouseDown += new MouseEventHandler(picboxButtonStart_MouseDown);

            //this is called when the textbox updates, so that i can make sure nothing is left null and we can do the thing
            username.TextChanged += new EventHandler(username_TextChanged);

            //create the folder with config/save files, nothing will happen if it already exists
            System.IO.Directory.CreateDirectory(appPath + @"\Config");
            //this creates the roundid and roundname path, then tries to download it from github. dlfromgh also checks the version
            System.IO.Directory.CreateDirectory(appPath + @"\Resources");
            downloadFromGithub();

            //this void checks if config files exist, if they are valid, and sets them to the variables
            TryLoadFromConfig();

            //when the app is closed, so we dont keep the thread running
            this.FormClosing += new FormClosingEventHandler(FormCloseThreadDisposal);
        }

        public void FormCloseThreadDisposal(object sender, EventArgs e)
        {
            rpcThread_.Abort();
        }

        public void downloadFromGithub()
        {
            WebClient client = new WebClient();
            string ids = client.DownloadString("https://raw.githubusercontent.com/wafflethings/fallpresencestringhost/main/roundid");
            string names = client.DownloadString("https://raw.githubusercontent.com/wafflethings/fallpresencestringhost/main/roundname");
            string versionGh = client.DownloadString("https://raw.githubusercontent.com/wafflethings/fallpresencestringhost/main/version");
            string shids = client.DownloadString("https://raw.githubusercontent.com/wafflethings/fallpresencestringhost/main/showids");
            string shnames = client.DownloadString("https://raw.githubusercontent.com/wafflethings/fallpresencestringhost/main/shownames");
            string arclvlids = client.DownloadString("https://raw.githubusercontent.com/wafflethings/fallpresencestringhost/main/ids4archetype");
            string arclvlarcs = client.DownloadString("https://raw.githubusercontent.com/wafflethings/fallpresencestringhost/main/archetypes4archetype");
            rpcLogo = client.DownloadString("https://raw.githubusercontent.com/wafflethings/fallpresencestringhost/main/iconname");

            if (int.Parse(versionGh) > version)
            {
                UpdateForm updateForm = new UpdateForm();
                updateForm.Show();
                this.Hide();
            }
            //download strings, set to variable, then save it

            FileInfo fi = new FileInfo(appPath + @"\Resources\roundid");

            if (fi.Exists)
            {
                fi.Delete();
            }

            using (StreamWriter sw = fi.CreateText())
            {
                sw.Write(ids);
            }

            FileInfo fi2 = new FileInfo(appPath + @"\Resources\roundname");

            if (fi2.Exists)
            {
                fi2.Delete();
            }

            using (StreamWriter sw2 = fi2.CreateText())
            {
                sw2.Write(names);
            }

            FileInfo fi3 = new FileInfo(appPath + @"\Resources\showids");

            if (fi3.Exists)
            {
                fi3.Delete();
            }

            using (StreamWriter sw3 = fi3.CreateText())
            {
                sw3.Write(shids);
            }

            FileInfo fi4 = new FileInfo(appPath + @"\Resources\shownames");

            if (fi4.Exists)
            {
                fi4.Delete();
            }

            using (StreamWriter sw4 = fi4.CreateText())
            {
                sw4.Write(shnames);
            }

            FileInfo fi5 = new FileInfo(appPath + @"\Resources\ids4archetype");

            if (fi5.Exists)
            {
                fi5.Delete();
            }

            using (StreamWriter sw5 = fi5.CreateText())
            {
                sw5.Write(arclvlids);
            }

            FileInfo fi6 = new FileInfo(appPath + @"\Resources\archetypes4archetype");

            if (fi6.Exists)
            {
                fi6.Delete();
            }

            using (StreamWriter sw6 = fi6.CreateText())
            {
                sw6.Write(arclvlarcs);
            }
        }

        public void tutorial_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://youtu.be/Hzy0f5Qjozo");
        }

        public void picboxButtonLog_Click(object sender, EventArgs e)
        {
            using (VistaFolderBrowserDialog folderBrowserDialog = new VistaFolderBrowserDialog())
            {
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    pathIsGood = false;

                    if (folderBrowserDialog.SelectedPath.Contains("FallGuys_client") &&
                    folderBrowserDialog.SelectedPath.Contains("Mediatonic") &&
                    folderBrowserDialog.SelectedPath.Contains("LocalLow") &&
                    folderBrowserDialog.SelectedPath.Contains("AppData") &&
                    !folderBrowserDialog.SelectedPath.Contains("Unity") &&
                    !folderBrowserDialog.SelectedPath.Contains("DlcItems"))
                    {
                        //these are pretty much some key things that the path should contain if it's the actual log path
                        pathIsGood = true;
                    }

                    if (pathIsGood)
                    {
                        //this will only happen when the folder is successfully opened
                        FileInfo fi = new FileInfo(appPath + @"\Config\" + "logPath");

                        if (fi.Exists)
                        {
                            //this is pretty obvious, if you're using the button to choose a path then you're replacing the old one.
                            fi.Delete();
                        }

                        using (StreamWriter sw = fi.CreateText())
                        {
                            //writing the log path to a stream so i can save it later and you dont have to reenter it
                            sw.Write(folderBrowserDialog.SelectedPath);
                        }

                        logPath = folderBrowserDialog.SelectedPath;

                        verifyForButton();
                    }
                    else
                    {
                        MessageBox.Show("That doesn't seem right, please try again.", "Wrong folder", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        picboxButtonLog_Click(sender, e);
                    }
                }
            }
        }

        public void TryLoadFromConfig()
        {
            FileInfo fi = new FileInfo(appPath + @"\Config\" + "logPath");
            if (fi.Exists)
            {
                //logPath exists check done
                FileInfo fi2 = new FileInfo(appPath + @"\Config\" + "username");
                if (fi2.Exists)
                {
                    //username exists check done
                    //same checks as opening the log
                    string configPath = System.IO.File.ReadAllText(appPath + @"\Config\" + "logPath");
                    if (configPath.Contains("FallGuys_client") &&
                    configPath.Contains("Mediatonic") &&
                    configPath.Contains("LocalLow") &&
                    configPath.Contains("AppData") &&
                    !configPath.Contains("Unity") &&
                    !configPath.Contains("DlcItems"))
                    {
                        //final check
                        string configUsername = System.IO.File.ReadAllText(appPath + @"\Config\" + "username");
                        if (configUsername != null)
                        {
                            //woo yeah, config loaded and verified to be correct
                            //unless you're a dumbass and have been tampering with the path which then you deserve it, only because i cant be bothered to program a check for that
                            usernameStr = configUsername;
                            logPath = configPath;
                            username.Text = usernameStr;
                            pathIsGood = true;
                            usernameNull = false;
                            verifyForButton();
                        }
                    }
                    else
                    {
                        //lol whoops you failed
                        fi2.Delete();
                    }
                }
            }
        }

        public void verifyForButton()
        {
            if (pathIsGood && !usernameNull)
            {
                //enable or disable the start button if the username is missing or the log path is wrong
                startBtnEnabled = true;
                picboxButtonStart.Image = startButton;
            }
            else
            {
                startBtnEnabled = false;
                picboxButtonStart.Image = startButtonDisabled;
            }
        }

        public void picboxButtonLog_MouseDown(object sender, EventArgs e)
        {
            //only happens when enabled
            if (logBtnEnabled)
            {
                picboxButtonLog.Image = logButtonPressed;
            }
        }

        public void picboxButtonLog_MouseUp(object sender, EventArgs e)
        {
            //only happens when enabled
            if (logBtnEnabled)
            {
                picboxButtonLog.Image = logButton;
            }
        }

        public void picboxButtonStart_Click(object sender, EventArgs e)
        {
            FileInfo fi = new FileInfo(appPath + @"\Config\" + "username");

            if (fi.Exists)
            {
                //this is pretty obvious, if you're using the button to choose a path then you're replacing the old one.
                fi.Delete();
            }

            using (StreamWriter sw = fi.CreateText())
            {
                //writing the log path to a stream so i can save it later and you dont have to reenter it
                sw.Write(username.Text);
            }
            if (startBtnEnabled == true)
            {
                inStart = !inStart;
                //disable or reenable all of the controls depending on if you have started or not
                if (inStart)
                {
                    logBtnEnabled = false;
                    username.Enabled = false;
                    picboxButtonLog.Image = logButtonDisabled;
                    logBtnEnabled = false;
                    ThreadDriver();
                    startDateTime = DateTime.Now;
                }
                else
                {
                    logBtnEnabled = true;
                    username.Enabled = true;
                    picboxButtonLog.Image = logButton;
                    logBtnEnabled = true;
                    client.Dispose();
                }
            }
        }

        public void picboxButtonStart_MouseDown(object sender, EventArgs e)
        {
            //only happens when enabled
            if (startBtnEnabled == true)
            {
                if (!inStart)
                {
                    picboxButtonStart.Image = startButtonPressed;
                }
                else
                {
                    picboxButtonStart.Image = stopButtonPressed;
                }
            }
        }

        public void picboxButtonStart_MouseUp(object sender, EventArgs e)
        {
            //only happens when enabled
            if (startBtnEnabled == true)
            {
                if (!inStart)
                {
                    picboxButtonStart.Image = startButton;
                }
                else
                {
                    picboxButtonStart.Image = stopButton;
                }
            }
        }

        public void username_TextChanged(object sender, EventArgs e)
        {
            if (username.TextLength != 0)
            {
                usernameNull = false;
                usernameStr = username.Text;
            }
            else
            {
                usernameNull = true;
            }
            verifyForButton();
        }

        public void ThreadDriver()
        {
            //this should only happen once, when the start button is pressed
            //checks for eac shutdown log to check if game is currently open or not

            string logs;
            var fs = new FileStream(logPath + @"\Player.log", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using (StreamReader r = new StreamReader(fs))
            {
                logs = r.ReadToEnd();
            }
            fs.Close();

            if (logs.Contains("[EAC Client] Shutdown"))
            {
                logBtnEnabled = true;
                username.Enabled = true;
                picboxButtonLog.Image = logButton;
                logBtnEnabled = true;
                inStart = false;
                MessageBox.Show("Game is not currently open, please open it.", "Game not open", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //verified that it's open, free to open now
                Thread rpcThread = new Thread(RpcThreadActions);
                rpcThread_ = rpcThread;
                rpcThread.Start();
            }
        }

        string GetLine(string text, int lineNo)
        {
            //yoinked from stackoverflow again, no idea what it does once again
            if (lineNo != -1)
            {
                string[] lines = text.Replace("\r", "").Split('\n');
                return lines.Length >= lineNo ? lines[lineNo - 1] : null;
            }
            else
            {
                return "Lobby";
            }
        }
        public void RpcThreadActions()
        {
            client = new DiscordRpcClient(
            "869316421199482921"
            );
            client.Initialize();
            //we only want one client or dispose wont work
            timestamps = new Timestamps();
            timestamps = Timestamps.Now;

            while (inStart)
            {
                Console.WriteLine("in true");
                string logs;
                string roundids;
                string roundnames;
                string showids;
                string shownames;
                string archetypes4archetype;
                string ids4archetype;
                //the log reading (pain)
                //credits to cochii, who's python code i shamelessly stole and ported to c#. thank you cochii, very cool
                var fs = new FileStream(logPath + @"\Player.log", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                using (StreamReader r = new StreamReader(fs))
                {
                    logs = r.ReadToEnd();
                }
                fs.Close();

                //yoinked this from stackoverflow, no idea what it does but all i know is that it works
                using (var reader = new StringReader(logs))
                {
                    for (string line = reader.ReadLine(); line != null; line = reader.ReadLine())
                    {
                        if (line.Contains("roundID"))
                        {
                            //some line contains round id, woo
                            //doesnt mean we could stop looping, this could just be the first roundid from the top
                            //there has to be a better way to do this but i can not be bothered whatsoever
                            var splitAtIndexLoadingScreen = line.IndexOf("loadingScreen");
                            var splitAtIndexRoundId = line.IndexOf("roundID=");
                            string roundId = line.Substring(splitAtIndexRoundId + 8, splitAtIndexLoadingScreen - splitAtIndexRoundId - 9);

                            //this reads the 2 dictionary files and turns them into a list, searches for the roundid in the roundid list and sets the presence
                            //this is, once again, unoptimised spaghetti code but this is fallguys rich presence so i dont care if it runs well
                            Console.WriteLine(roundId);
                            using (StreamReader r = new StreamReader(appPath + @"\Resources\roundid"))
                            {
                                roundids = r.ReadToEnd();
                            }

                            using (StreamReader r = new StreamReader(appPath + @"\Resources\roundname"))
                            {
                                roundnames = r.ReadToEnd();
                            }

                            using (StreamReader r = new StreamReader(appPath + @"\Resources\archetypes4archetype"))
                            {
                                archetypes4archetype = r.ReadToEnd();
                            }

                            using (StreamReader r = new StreamReader(appPath + @"\Resources\ids4archetype"))
                            {
                                ids4archetype = r.ReadToEnd();
                            }

                            using (var reader2 = new StringReader(roundids))
                            {
                                var i = 0;
                                var lineOfLevel = -1;
                                for (string line3 = reader2.ReadLine(); line3 != null; line3 = reader2.ReadLine())
                                {
                                    i++;
                                    if (roundId.Contains(line3))
                                    {
                                        //for every line, it checks if it has a round id till it gets to the end, which is the current round
                                        lineOfLevel = i;
                                    }
                                }
                                currentlyInRound = GetLine(roundnames, lineOfLevel);
                                using (var lineReader = new StringReader(ids4archetype))
                                {
                                    var i2 = 0;
                                    var lineOfArche = -1;
                                    for (string readLine = lineReader.ReadLine(); readLine != null; readLine = lineReader.ReadLine())
                                    {
                                        i2++;
                                        if (readLine == roundId)
                                        {
                                            lineOfArche = i2;
                                        }
                                    }
                                    currentArche = GetLine(archetypes4archetype, lineOfArche);
                                }
                            }
                            //ok so, here we get the archetypes from yet another list of all the levels i have to generate

                        } else if (line.Contains("Loading scene MainMenu"))
                        {
                            currentlyInRound = "Lobby";
                        }
                        if (line.Contains("currentParticipantCount="))
                        {
                            var splitAtIndexParticipantCount = line.IndexOf("currentParticipantCount=");
                            var splitAtIndexRandSeed = line.IndexOf(" randSeed");
                            string participantCount = line.Substring(splitAtIndexParticipantCount+24, splitAtIndexRandSeed - splitAtIndexParticipantCount - 24);

                            if (currentlyInRound == "Lobby")
                            {
                                currentRoundPlayerCount = -1;
                            }
                            else
                            {
                                currentRoundPlayerCount = int.Parse(participantCount);
                            }

                        }
                        if (line.Contains("[HandleSuccessfulLogin] Selected show is "))
                        {
                            var showId = line.Substring(line.IndexOf("[HandleSuccessfulLogin] Selected show is ") + "[HandleSuccessfulLogin] Selected show is ".Length);

                            using (StreamReader r = new StreamReader(appPath + @"\Resources\showids"))
                            {
                                showids = r.ReadToEnd();
                            }

                            using (StreamReader r = new StreamReader(appPath + @"\Resources\shownames"))
                            {
                                shownames = r.ReadToEnd();
                            }

                            using (var reader2 = new StringReader(showids))
                            {
                                var i = 0;
                                var lineOfShow = -1;
                                for (string line3 = reader2.ReadLine(); line3 != null; line3 = reader2.ReadLine())
                                {
                                    i++;
                                    if (line3.Contains(showId))
                                    {
                                        //for every line, it checks if it has a round id till it gets to the end, which is the current round
                                        lineOfShow = i;
                                    }
                                }
                                showName = GetLine(shownames, lineOfShow);
                            }
                        }
                        if (line.Contains("[StateWaitingForRewards] Init: waitingForRewards"))
                        {
                            currentlyInRound = "Results";
                        }
                        if (line.Contains("Changing local player state to: Playing")) {
                            playerState = "";
                        }
                        if (line.Contains("Changing local player state to: SpectatingEliminated"))
                        {
                            playerState = "(Eliminated)";
                        }
                    }
                }

                // the presence setting
                presence = new RichPresence();
                presence.Timestamps = timestamps;

                if(currentlyInRound == null)
                {
                    currentlyInRound = "Lobby";
                }
                assets = new Assets();
                //if you're in the lobby, dont show player or show info
                if (currentRoundPlayerCount == -1 || currentlyInRound == "Lobby" || currentlyInRound == "Results")
                {
                    presence.Details = "In " + currentlyInRound;
                    presence.State = usernameStr;
                    assets.SmallImageText = "";
                    currentArche = "";
                }
                else
                {
                    presence.Details = "In " + currentlyInRound + ", " + currentRoundPlayerCount + " players";
                    presence.State = usernameStr + " in " + showName + playerState;
                    Console.WriteLine(currentArche);
                    assets.SmallImageText = "In a " + char.ToUpper(currentArche.Substring(10)[0]) + currentArche.Substring(10).Substring(1) + " level";
                }
                
                Console.WriteLine(currentlyInRound);

                assets.LargeImageKey = rpcLogo;
                assets.SmallImageKey = currentArche;
                assets.LargeImageText = "Fall Guys: Ultimate Knockout via FallPresence";

                presence.Assets = assets;
                client.SetPresence(presence);
                Thread.Sleep(7500);
            }
        }
    }
}
