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

        public DodgeContext()
        {
            InitialiseContext();
        }

        private void InitialiseContext()
        {
            _components = new Container();
            _notifyIcon = new NotifyIcon(_components)
            {
                ContextMenuStrip = new ContextMenuStrip(),
                Icon = new Icon(@"C:\Users\Joe\source\repos\Dodge\Dodge\favicon.ico"),
                Text = "Dodge",
                Visible = true,
            };

            _notifyIcon.ContextMenuStrip.Opening += ContextMenuStrip_Opening;
            _notifyIcon.DoubleClick += NotifyIconOnDoubleClick;
            _notifyIcon.MouseUp += NotifyIconOnMouseUp;
        }

        private void NotifyIconOnMouseUp(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void NotifyIconOnDoubleClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
