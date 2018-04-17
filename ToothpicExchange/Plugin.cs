using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental;

namespace ToothpicExchange
{
    /// <summary>
    /// This is a plugin class from OpenDental.
    /// </summary>
    public class Plugin : PluginBase
    {
        private Form host;

        /// <summary>
        /// This is an OpenDental method.
        /// </summary>
        public override Form Host
        {
            get
            {
                return host;
            }
            set
            {
                host = value;
            }
        }

        /// <summary>
        /// This is an OpenDental function to launch the main form when the toolbar button is clicked.
        /// </summary>
        /// <param name="patNum">This is the current patient number from OpenDental</param>
        public override void LaunchToolbarButton(long patNum)
        {
            // Opens a new instance of LaunchToothpicExchange and passes the current patient number.
            var form = new LaunchToothpicExchange();
            form.PatNum = patNum;
            form.ShowDialog();
        }

    }
}
