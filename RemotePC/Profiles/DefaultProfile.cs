using System.Diagnostics;

namespace RemotePC.Profiles
{
    public class DefaultProfile : Profile
    {
        public void Execute(RemoteButton btn)
        {
            switch (btn)
            {
                case RemoteButton.Close:
                    PressClose();
                    break;
                case RemoteButton.Play:
                    PressPlay();
                    break;
                case RemoteButton.VolumeDown:
                    PressVolumeDown();
                    break;
                case RemoteButton.VolumeUp:
                    PressVolumeUp();
                    break;
                case RemoteButton.OFF:
                    ShutDown();
                    break;
                case RemoteButton.Audio:
                    PressAudio();
                    break;
                case RemoteButton.Subtitle:
                    PressSubtitle();
                    break;
                case RemoteButton.Backward:
                    PressBackward();
                    break;
                case RemoteButton.Forward:
                    PressForward();
                    break;
                case RemoteButton.Three:
                    PressThree();
                    break;
                case RemoteButton.Five:
                    PressFive();
                    break;
                case RemoteButton.Seven:
                    PressSeven();
                    break;
                case RemoteButton.Zoom:
                    PressZoom();
                    break;
                case RemoteButton.Left:
                    PressLeft();
                    break;
                case RemoteButton.Right:
                    PressRight();
                    break;
                case RemoteButton.Up:
                    PressUp();
                    break;
                case RemoteButton.Down:
                    PressDown();
                    break;
            }
        }

        internal void PressKey(string key)
        {
            System.Windows.Forms.SendKeys.SendWait(key);
        }

        private void ShutDown()
        {
            var psi = new ProcessStartInfo("shutdown", "/s /t 0") { CreateNoWindow = false, UseShellExecute = false };
            Process.Start(psi);
        }

        protected virtual void PressPlay() { }

        protected virtual void PressClose() { }

        protected virtual void PressVolumeDown() { }

        protected virtual void PressVolumeUp() { }

        protected virtual void PressAudio() { }

        protected virtual void PressSubtitle() { }

        protected virtual void PressBackward() { }

        protected virtual void PressForward() { }

        protected virtual void PressThree() { }

        protected virtual void PressFive() { }

        protected virtual void PressSeven() { }

        protected virtual void PressZoom() { }

        protected virtual void PressLeft() { }

        protected virtual void PressRight() { }

        protected virtual void PressUp() { }

        protected virtual void PressDown() { }
    }
}