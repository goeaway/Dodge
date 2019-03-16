using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dodge
{
    public class DodgeState
    {
        /// <summary>
        /// Gets the PID of the process the app is trying to keep in view
        /// </summary>
        public IntPtr DodgingProcess { get; private set; }

        /// <summary>
        /// Gets if the application is currently trying to keep a process in view
        /// </summary>
        public bool TryingToDodge => DodgingProcess != IntPtr.Zero;

        /// <summary>
        /// Gets if the application is waiting for the user to click on a window to dodge
        /// </summary>
        public bool WaitingForUserToAssignDodge { get; private set; }

        public void ToggleWaiting()
        {
            WaitingForUserToAssignDodge = !WaitingForUserToAssignDodge;
        }

        public void UpdateProcess(IntPtr processHandle)
        {
            DodgingProcess = processHandle;
        }

        public void StopDodging()
        {
            DodgingProcess = IntPtr.Zero;
        }
    }
}
