namespace RemotePC.Profiles.Media
{
    public class Media : DefaultProfile
    {
        protected override void PressPlay()
        {
            PressKey(" ");
        }

        protected override void PressClose()
        {
            PressKey("%{F4}");
        }

        protected override void PressVolumeUp()
        {
            PressKey("{UP}");
        }

        protected override void PressVolumeDown()
        {
            PressKey("{DOWN}");
        }

        protected override void PressZoom()
        {
            PressKey("%{ENTER}");
        }
    }
}