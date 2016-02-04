using System;
using System.Linq;
using RemotePC.Attributes;
using RemotePC.Extensions;

namespace RemotePC
{
    internal static class SignalManager
    {
        private static RemoteButton _lastButton;

        private static RemoteButton ParseSignal(string signal)
        {            
            int intSignal;
            var parsed = int.TryParse(signal, out intSignal);

            if (parsed && Enum.IsDefined(typeof (RemoteButton), intSignal))
            {
                return (RemoteButton)Enum.Parse(typeof (RemoteButton), signal);
            }                       

            return RemoteButton.Undefined;
        }

        private static SpecialSignal PraseSpecialSignal(string signal)
        {
            signal = signal.Trim();

            var type = typeof (SpecialSignal);            
            var enumValues = type.GetFields();            

            foreach (var member in enumValues)
            {
                var attributes = member.GetCustomAttributes(typeof(CodeAttribute), false);
                var attribute = attributes.FirstOrDefault();                

                var codeAttribute = attribute as CodeAttribute;
                if (codeAttribute != null && codeAttribute.Code == signal)
                {
                    return (SpecialSignal) member.GetRawConstantValue();
                }
            }

            return SpecialSignal.Undefined;
        }


        public static RemoteButton GetButtonFromSignal(string signal)
        {
            var button = ParseSignal(signal);
            if (button == RemoteButton.Undefined)
            {
                var special = PraseSpecialSignal(signal);
                if (special == SpecialSignal.FollowUp && _lastButton.IsFollowUpEnabled())
                {
                    return _lastButton;
                }
            }

            _lastButton = button;
            return button;
        }
    }
}