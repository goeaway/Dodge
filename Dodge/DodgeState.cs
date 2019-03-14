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
        public int DodgingProcess { get; private set; } = -1;

        /// <summary>
        /// Gets if the application is currently trying to keep a process in view
        /// </summary>
        public bool TryingToDodge => DodgingProcess != -1;

        public void UpdateProcess(int process)
        {
            if (process < 0)
                throw new ArgumentOutOfRangeException(nameof(process));

            DodgingProcess = process;
        }

        public void StopDodging()
        {
            DodgingProcess = -1;
        }
    }
}
