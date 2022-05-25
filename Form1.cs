using WindowsDesktop;


namespace WhichDesktop
{
    public partial class Form1 : Form
    {
        System.Windows.Forms.NotifyIcon ni;
        public Form1()
        {
            InitializeComponent();

            InitializeDesktops();
        }
        public void InitializeDesktops()
        {
            ni = new System.Windows.Forms.NotifyIcon();

            // Notification of desktop switching
            VirtualDesktop.CurrentChanged += (_, args) =>
            {
                //MessageBox.Show($"Switched: {GetDesktopIndex(args.NewDesktop.Id).ToString()}");
                SetIcon(args.NewDesktop.Id);


            };

            // Notification of desktop creating
            VirtualDesktop.Created += (_, desktop) =>
            {
                desktop.Switch();
            };

            VirtualDesktop.Destroyed += (_, args) =>
            {
                var currentId = VirtualDesktop.Current.Id;
                //MessageBox.Show($"Switched: {GetDesktopIndex(currentId).ToString()}");
                SetIcon(currentId);
            };

            //identify the desktop we are on when the program starts
            var currentId = VirtualDesktop.Current.Id;
            //MessageBox.Show($"Switched: {GetDesktopIndex(currentId).ToString()}");
            SetIcon(currentId);
        }

        int GetDesktopIndex(Guid guid)
        {
            var desktops = VirtualDesktop.GetDesktops();
            int i = 0;
            for (i = 0; i < desktops.Length; ++i)
            {
                if (guid == desktops[i].Id)
                {
                    return i;
                }
            }
            //MessageBox.Show("Error in GetDesktopIndex");
            return -1;
        }

        void SetIcon(Guid guid)
        {
            //ni.Dispose();
            int desktopIndex = GetDesktopIndex(guid);
            ni.Icon = new System.Drawing.Icon(@"icos\" + (desktopIndex+1).ToString() + ".ico");
            ni.Visible = true;
        }
    }
}