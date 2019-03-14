using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace Dodge
{
    public class DodgeController
    {
        private readonly DodgeState _state;
        private AutomationFocusChangedEventHandler _focusHandler;

        public DodgeController(DodgeState state)
        {
            _state = state;
        }

        public void StartDodging()
        {
            if (_state.TryingToDodge)
                throw new InvalidOperationException("cannot start dodging when already dodging");

            // code that guides the user to select a window

            // code that gets the PID from the window selected
            var pidToWatch = 1;

            _state.UpdateProcess(pidToWatch);

            _focusHandler = new AutomationFocusChangedEventHandler(OnWindowsFocusChanged);
            Automation.AddAutomationFocusChangedEventHandler(_focusHandler);
        }

        private void OnWindowsFocusChanged(object sender, AutomationFocusChangedEventArgs e)
        {
            // figure if whatever is being focused is actually a window

            // get the PID of the focused window

            // if the PID is the same as what we're watching, don't go further

            // find out if the process we're watching is hidden from view

            // if not, don't go further

            // if minimised, don't go further

            // find out the bounds of the focused process

            // if it's taking up 100% of screen, don't go further

            // the user should set an option for the minimum window they want to view 
            // find a location on the screen where we can fit the watched window

            // move the window to that location

            var child = e.ChildId;
        }

        public void StopDodging()
        {
            if (!_state.TryingToDodge)
                throw new InvalidOperationException("could not stop dodging as the application was not dodging");

            _state.StopDodging();

            Automation.RemoveAutomationFocusChangedEventHandler(_focusHandler);
            _focusHandler = null;
        }
    }
}
