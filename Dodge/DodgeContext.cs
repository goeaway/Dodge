using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dodge
{
    public class DodgeContext : ApplicationContext
    {
        private IContainer _components;
        private NotifyIcon _notifyIcon;
        private readonly DodgeState _state;

        public DodgeContext()
        {
            InitialiseContext();
            _state = new DodgeState();
        }

        private void InitialiseContext()
        {
            _components = new Container();

            var menu = new ContextMenuStrip();
            var exitItem = new ToolStripMenuItem() {Text = "Exit", Name = "exitItem"};
            var stopDodgingItem= new ToolStripMenuItem() { Text = "Stop Dodging", Name = "stopDItem"};
            var startDodgingItem = new ToolStripMenuItem() { Text = "Start Dodging", Name = "startDItem"};
            
            exitItem.Click += (_, e) => QuitApplication();
            stopDodgingItem.Click += StopDodgingItemOnClick; 
            startDodgingItem.Click += StartDodgingItemOnClick;

            menu.Items.AddRange(new ToolStripItem[] { startDodgingItem, stopDodgingItem, exitItem });

            _notifyIcon = new NotifyIcon(_components)
            {
                ContextMenuStrip = menu,
                Icon = new Icon(@"C:\Users\Joe\source\repos\Dodge\Dodge\favicon.ico"),
                Text = "Dodge",
                Visible = true,
            };

            _notifyIcon.ContextMenuStrip.Opening += ContextMenuStrip_Opening;
        }

        private void StartDodgingItemOnClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void StopDodgingItemOnClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            // if state.TryingToDodge we need to enable the stop dodging item
            // vice versa
        }

        private void QuitApplication()
        {
            _notifyIcon.Visible = false;
            Application.Exit();
        }
    }
}
