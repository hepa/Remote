namespace RemotePC.Profiles.Media
{
    class MediaPlayerClassic : Media
    {
        protected override void PressAudio()
        {
            PressKey("A");
        }

        protected override void PressSubtitle()
        {
            PressKey("S");
        }

        protected override void PressBackward()
        {
            PressKey("^{LEFT}");
        }

        protected override void PressForward()
        {
            PressKey("^{RIGHT}");
        }

        protected override void PressThree()
        {
            PressKey("{9}");
        }

        protected override void PressFive()
        {
            PressKey("{5}");
        }

        protected override void PressSeven()
        {
            PressKey("{1}");
        }

        protected override void PressLeft()
        {
            PressKey("{F1}");
        }

        protected override void PressRight()
        {
            PressKey("{F2}");
        }
    }
}