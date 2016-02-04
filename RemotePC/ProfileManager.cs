using System.Collections.Generic;
using System.Diagnostics;
using RemotePC.Profiles;
using RemotePC.Profiles.Media;

namespace RemotePC
{
    static class ProfileManager
    {
        private static ProfileMapper mapper = new ProfileMapper();

        internal static Profile GetActiveProfile()
        {
            var activeProcess = ProcessManager.GetActiveProcess();
            return GetProfileForProcess(activeProcess);
        }

        private static Profile GetProfileForProcess(Process proc)
        {
            return mapper[proc.ProcessName];
        }

        private class ProfileMapper
        {
            private Dictionary<string, Profile> mapping = new Dictionary<string, Profile>();
            private static DefaultProfile defaultProfile = new DefaultProfile();

            internal ProfileMapper()
            {
                mapping.Add("mpc-hc64", new MediaPlayerClassic());
            }

            internal Profile this[string processName]
            {
                get
                {
                    if (!mapping.ContainsKey(processName))
                    {
                        return defaultProfile;
                    }
                    return mapping[processName];
                }
            }
        }
    }
}