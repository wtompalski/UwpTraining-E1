using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UwpTraining_E1.Background
{
    public static class AppBackgroundState
    {
        public const string ApplicationTriggerTaskName = "ApplicationTriggerTask";
        public static string ApplicationTriggerTaskProgress = "";
        public static string ApplicationTriggerTaskResult = "";
        public static bool ApplicationTriggerTaskRegistered = false;
    }
}
