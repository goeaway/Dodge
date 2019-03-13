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
        public int DodgingProcess { get; } = -1;

        /// <summary>
        /// Gets if the application is currently trying to keep a process in view
        /// </summary>
        public bool TryingToDodge => DodgingProcess != -1;
    }
}
