using Sce.Atf.Applications;
using Sce.Atf.Controls.Timelines;
using Sce.Atf.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace RobotDirector
{
    public partial class TimelineCommands
    {
        private static System.Timers.Timer aTimer;
        WMPLib.WindowsMediaPlayer Player;
        private void SetTimer()
        {
            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(0.02);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }
        private  void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if (Player.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                //consoleOutputWriter.Write(OutputMessageType.Info, Player.controls.currentPosition.ToString());
                document.m_scrubberManipulator.Position = Convert.ToSingle(Player.controls.currentPosition);
            }
            // Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}",e.SignalTime);
        }

        private bool Do_Media_Command(object commandTag, bool doing)
        {
            switch (commandTag)
            {
                case Command.PauseMedia:
                    if (doing)
                    {
                        if (Player.controls.get_isAvailable("pause"))
                        {
                            Player.controls.pause();
                        }
                    }
                    return true;
                case Command.StopMedia:
                    if (doing)
                    {
                        if (Player.controls.get_isAvailable("stop"))
                        {
                            Player.controls.stop();
                            document.m_scrubberManipulator.Position = 0;
                        }
                    }
                    return true;
                case Command.PlayMedia:
                    if (doing)
                    {
                        Player.controls.currentPosition = document.m_scrubberManipulator.Position;
                        if (Player.controls.get_isAvailable("play"))
                        {
                            Player.controls.play();
                            IEnumerable<TimelinePath> AllTracks = document.TimelineControl.AllTracks;
                            IEnumerable<TimelinePath> AllGroups = document.TimelineControl.AllGroups;
                            IEnumerable<TimelinePath> AllEvents = document.TimelineControl.AllEvents;
                            IEnumerable<TimelinePath> AllMarkers = document.TimelineControl.AllMarkers;
                        }
                    }
                    return true;
                default:
                    return false;
            }
        }


        private void Player_Initialize()
        {

            m_commandService.RegisterCommand(
                 Command.PlayMedia, StandardMenu.Edit,
                 null, "Play", "Play", Keys.P,
                 null, CommandVisibility.Toolbar, this);

            m_commandService.RegisterCommand(
                 Command.StopMedia, StandardMenu.Edit,
                 null, "Stop", "Stop", Keys.P,
                 null, CommandVisibility.Toolbar, this);
            m_commandService.RegisterCommand(
                 Command.PauseMedia, StandardMenu.Edit,
                 null, "Pause", "Pause", Keys.P,
                 null, CommandVisibility.Toolbar, this);


            SetTimer();
            Player = new WMPLib.WindowsMediaPlayer();
            Player.PlayStateChange += new WMPLib._WMPOCXEvents_PlayStateChangeEventHandler(Player_PlayStateChange);
            Player.MediaError += new WMPLib._WMPOCXEvents_MediaErrorEventHandler(Player_MediaError);
            Player.settings.autoStart = false;
            Player.URL = @"D:\Desktop\media\A Love Theme.mp3";
            //  WMPLib.IWMPControls3 controls = (WMPLib.IWMPControls3)Player.controls;
            //document.m_scrubberManipulator.Moved
            
        }

        private void Player_PlayStateChange(int NewState)
        {
            if ((WMPLib.WMPPlayState)NewState == WMPLib.WMPPlayState.wmppsStopped)
            {
                Player.close();
            }
        }

        private void Player_MediaError(object pMediaObject)
        {
            //MessageBox.Show("Cannot play media file.");
        }
    }
}
