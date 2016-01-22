using RemotePC.Attributes;

namespace RemotePC
{
    public enum SpecialSignal
    {
        Undefined = 0,

        [Code("4294967295")]
        FollowUp = 1
    }
}