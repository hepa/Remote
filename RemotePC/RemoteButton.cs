using RemotePC.Attributes;

namespace RemotePC
{
    public enum RemoteButton
    {
        Undefined = 0,
        
        Play = 33431775,

        Stop = 33433815,

        [FollowUp]
        VolumeDown = 33454215,

        [FollowUp]
        VolumeUp = 33446055,

        Audio = 33466455,

        Subtitle = 33482775,

        Close = 33439935,

        OFF = 33472575,

        [FollowUp]
        Backward = 33478695,

        [FollowUp]
        Forward = 33462375,

        One = 33480735,

        Two = 33464415,

        Three = 33448095,

        Four = 33476655,

        Five = 33460335,

        Six = 33444015,

        Seven = 33484815,

        Eight = 33468495,

        Nine = 33452175,

        Zoom = 33474615
    }
}