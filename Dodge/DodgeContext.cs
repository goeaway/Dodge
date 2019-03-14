using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Automation;

namespace Dodge
{
    public class DodgeContext : ApplicationContext
    {
        private const string STOP_DODGING_ITEM_NAME = "stopDItem";
        private const string START_DODGING_ITEM_NAME = "startDItem";

        private IContainer _components;
        private NotifyIcon _notifyIcon;
        private ContextMenuStrip _menu;
        private readonly DodgeState _state;

        
        private readonly DodgeController _controller;

        public DodgeContext()
        {
            InitialiseContext();
            _state = new DodgeState();
            _controller = new DodgeController(_state);
        }

        private void InitialiseContext()
        {
            _components = new Container();

            _menu = new ContextMenuStrip();
            var exitItem = new ToolStripMenuItem() {Text = "Exit", Name = "exitItem"};
            var stopDodgingItem = new ToolStripMenuItem() { Text = "Stop Dodging", Name = STOP_DODGING_ITEM_NAME };
            var startDodgingItem = new ToolStripMenuItem() { Text = "Start Dodging", Name = START_DODGING_ITEM_NAME };

            exitItem.Click += (_, e) => QuitApplication();
            stopDodgingItem.Click += StopDodgingItemOnClick; 
            startDodgingItem.Click += StartDodgingItemOnClick;

            _menu.Items.AddRange(new ToolStripItem[] { startDodgingItem, stopDodgingItem, exitItem });

            _notifyIcon = new NotifyIcon(_components)
            {
                ContextMenuStrip = _menu,
                Icon = new Icon(@"C:\Users\Joe\source\repos\Dodge\Dodge\favicon.ico"),
                Text = "Dodge",
                Visible = true,
            };

            _notifyIcon.ContextMenuStrip.Opening += ContextMenuStrip_Opening;
        }

        private void StartDodgingItemOnClick(object sender, EventArgs e)
        {
            _controller.StartDodging();
        }

        private void StopDodgingItemOnClick(object sender, EventArgs e)
        {
            _controller.StopDodging();
        }

        private void ContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            var startItemFoundArray = _menu.Items.Find(START_DODGING_ITEM_NAME, false);

            if(startItemFoundArray.Length != 1)
                throw new InvalidOperationException("could not find the start dodging menu item");

            var startItem = startItemFoundArray[0];

            startItem.Enabled = !_state.TryingToDodge;

            var stopItemFoundArray = _menu.Items.Find(STOP_DODGING_ITEM_NAME, false);

            if (stopItemFoundArray.Length != 1)
                throw new InvalidOperationException("could not find the stop dodging menu item");

            var stopItem = stopItemFoundArray[0];
            
            stopItem.Enabled = _state.TryingToDodge;
        }

        private void QuitApplication()
        {
            _notifyIcon.Visible = false;

            if (_state.TryingToDodge)
            {
                _controller.StopDodging();
            }

            Application.Exit();
        }
    }
}
