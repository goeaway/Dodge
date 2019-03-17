using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;

namespace Dodge
{
    public class DodgeController
    {
        private readonly DodgeState _state;
        private AutomationFocusChangedEventHandler _focusHandler;

        #region - External Items/Models -

        [DllImport("user32.dll")]
        private static extern IntPtr WindowFromPoint(System.Drawing.Point point);

        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr handle, out uint threadProcessId);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern bool IsIconic(IntPtr handle);

        [DllImport("user32.dll")]
        private static extern bool IsZoomed(IntPtr handle);

        [DllImport("user.dll")]
        private static extern bool IsWindowVisible(IntPtr handle);

        [DllImport("user.dll")]
        private static extern bool GetWindowRect(IntPtr handle, out RECT rect);

        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr handle, IntPtr handleInsertAfter, int left, int top, int right,
            int bottom, uint flags);

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        #endregion

        public DodgeController(DodgeState state)
        {
            _state = state;
        }

        public void StartDodging()
        {
            if (_state.TryingToDodge)
                throw new InvalidOperationException("cannot start dodging when already dodging");

            if(_state.WaitingForUserToAssignDodge)
                throw new InvalidOperationException("cannot start dodging when already waiting for user to assign a dodge");

            // create a listener to listen to when the user clicks somewhere on the screen, 
            Hook.GlobalEvents().MouseDown += OnGlobalMouseDown;

            _state.ToggleWaiting();
        }

        private void OnGlobalMouseDown(object sender, MouseEventArgs e)
        {
            var pHandle = WindowFromPoint(e.Location);

            _state.UpdateProcess(pHandle);

            _focusHandler = new AutomationFocusChangedEventHandler(OnWindowsFocusChanged);
            Automation.AddAutomationFocusChangedEventHandler(_focusHandler);
        }

        private void OnWindowsFocusChanged(object sender, AutomationFocusChangedEventArgs e)
        {
            // if dodge process is minimised, don't go further
            if (IsIconic(_state.DodgingProcess))
                return;

            // figure if whatever is being focused is actually a window

            // get the PID of the focused window
            var focusedHandle = GetForegroundWindow();
            GetWindowThreadProcessId(focusedHandle, out var focusedPID);
            GetWindowThreadProcessId(_state.DodgingProcess, out var dodgingPID);

            // if the PID is the same as what we're watching, don't go further
            if (focusedPID == dodgingPID)
                return;

            // find out if the process we're watching is hidden from view
            // if not, don't go further
            if (IsWindowVisible(_state.DodgingProcess))
                return;

            // find out if focused window is maximised
            var focusedMaximised = IsZoomed(focusedHandle);

            if (focusedMaximised)
            {
                // if we have multiple screens to use, just translate the dodge window to the second one
                if (Screen.AllScreens.Length > 1)
                {
                    // get the current bounds of the dodging process

                    // move it to screen 2 ([1]) keeping the same dimensions
                    // https://www.tek-tips.com/viewthread.cfm?qid=1416749

                    //SetWindowPos(_state.DodgingProcess, focusedHandle, 1, 1, 1, 1, 0);
                }
                // if there's only one screen we can't do anything
            }
            else
            {
                // find out the bounds of the focused process
                GetWindowRect(focusedHandle, out var focusedBounds);

                var screen = Screen.FromHandle(focusedHandle);

                // if we can fit the dodge window on the same screen, we should use the same screen

                // if we can't but we have more, just move it to the second screen (like if max)

                // if no other screens, do nothing
            }

            // the user should set an option for the minimum window they want to view 
            // find a location on the screen where we can fit the watched window

            // move the window to that location
        }

        public void StopDodging()
        {
            try
            {
                if (!_state.TryingToDodge)
                    throw new InvalidOperationException("could not stop dodging as the application was not dodging");

                _state.StopDodging();

                if(_state.WaitingForUserToAssignDodge)
                    _state.ToggleWaiting();

                Hook.GlobalEvents().MouseDown -= OnGlobalMouseDown;
                Automation.RemoveAutomationFocusChangedEventHandler(_focusHandler);
                _focusHandler = null;
            }
            catch (Exception e)
            {
                
                throw e;
            }
        }
    }
}
