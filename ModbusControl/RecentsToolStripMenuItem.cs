// Recently used files manager
// From: https://www.codeproject.com/Tips/680088/Recent-Items-Tool-Strip-Menu-Item
//
using System;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel;

namespace ModbusControl
{
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]

    /// <summary>
    /// This is a menu item that manages a list of recently opened documents.
    /// </summary>
    [Description("This is a menu item that manages a list of recently opened documents.")]
    public class RecentsToolStripMenuItem : ToolStripMenuItem
    {
        private int mMaxItems = 5;
        private bool mIgnoreCase = true;
        private List<string> mRecentList = new List<string>();

        /// <summary>
        /// constructor
        /// </summary>
        public RecentsToolStripMenuItem()
        {
            this.Enabled = false;
        }

        /// <summary>
        /// This is the function that is executed when a file name is selected from the list.
        /// </summary>
        /// <remarks>
        /// ((ToolStripDropDownItem)sender).Text can be used to find the value of the one that was clicked.
        /// </remarks>
        /// <example>
        /// <code>
        /// recentToolStripMenuItem.ItemClick += new System.EventHandler(this.recentItem_Click);
        /// </code>
        /// </example>
        [Category("Action")]
        [Description("This is the function that is executed when a file name is selected from the list. ((ToolStripDropDownItem)sender).Text can be used to find the value of the one that was clicked.")]
        public event EventHandler? ItemClick;

        /// <summary>
        /// Maximum number of items in the list.
        /// </summary>
        /// <value>5</value>
        [Category("Layout")]
        [Description("Maximum number of items in the list.")]
        [DefaultValue(5)]
        public int MaxItems
        {
            get { return mMaxItems; }
            set
            {
                mMaxItems = value;
                // Limit max number of items
                while (mRecentList.Count > MaxItems)
                {
                    mRecentList.RemoveRange(MaxItems, mRecentList.Count - MaxItems);
                }
            }
        }

        /// <summary>
        /// Ignore case.
        /// </summary>
        /// <value>true</value>
        [Category("Layout")]
        [Description("Ignore case when comparing pathnames.")]
        [DefaultValue(true)]
        public bool IgnoreCase
        {
            get { return mIgnoreCase; }
            set { mIgnoreCase = value; }
        }

        /// <summary>
        /// Load a list of files.
        /// </summary>
        /// <remarks>
        /// Use after initializing.
        /// </remarks>
        public void LoadList(List<string> list)
        {
            this.DropDownItems.Clear();
            if (list.Count == 0)
            {
                this.Enabled = false;
            }
            else
            {
                this.Enabled = true;
                for (int i = 0; i < Math.Min(list.Count, MaxItems); ++i)
                {
                    mRecentList.Add(list[i]);
                    ToolStripMenuItem item = new ToolStripMenuItem();
                    item.Name = (1 + i).ToString();
                    item.Size = new System.Drawing.Size(156, 22);
                    item.Text = list[i];
                    if (this.ItemClick == null)
                    {
                        item.Enabled = false;
                    }
                    else
                    {
                        item.Click += this.ItemClick;
                    }
                    this.DropDownItems.Add(item);
                }
            }
        }

        /// <summary>
        /// Get the recent files list as List<string>
        /// </summary>
        public List<string> GetList()
        {
            return mRecentList;
        }

        /// <summary>
        /// Add a file path to the recents list
        /// </summary>
        /// <param name="filePath">
        /// The path to the file.
        /// </param>
        public void AddRecentItem(string filePath)
        {
            // Already in list?
            for (int i = 0; i < mRecentList.Count; ++i)
            {
                // compare, ignore case (windows)
                if (string.Compare(mRecentList[i], filePath, mIgnoreCase) == 0)
                {
                    mRecentList.RemoveAt(i);
                    break;
                }
            }
            // Insert new at top
            mRecentList.Insert(0, filePath);

            // Limit max number of items
            while (mRecentList.Count > MaxItems)
            {
                mRecentList.RemoveRange(MaxItems, mRecentList.Count - MaxItems);
            }

            // Update menu
            UpdateMenu();
        }

        /// <summary>
        /// Remove a file path to the recents list
        /// </summary>
        /// <param name="filePath">
        /// The path to the file.
        /// </param>
        public void RemoveRecentItem(string filePath)
        {
            for (int i = 0; i < mRecentList.Count; ++i)
            {
                // compare, ignore case (windows)
                // keep looping to remove possible duplicates
                if (string.Compare(mRecentList[i], filePath, mIgnoreCase) == 0)
                {
                    mRecentList.RemoveAt(i);
                }
            }

            // Update menu
            UpdateMenu();
        }

        /// <summary>
        /// Update the menu items according to the list
        /// </summary>
        private void UpdateMenu()
        {
            this.DropDownItems.Clear();

            if (mRecentList.Count == 0)
            {
                this.Enabled = false;
            }
            else
            {
                this.Enabled = true;
                for (int i = 0; i < mRecentList.Count; ++i)
                {
                    ToolStripMenuItem item = new ToolStripMenuItem();
                    item.Name = (1 + i).ToString();
                    item.Size = new System.Drawing.Size(156, 22);
                    item.Text = mRecentList[i];
                    if (this.ItemClick == null)
                    {
                        item.Enabled = false;
                    }
                    else
                    {
                        item.Click += this.ItemClick;
                    }
                    this.DropDownItems.Add(item);
                }
            }
        }


    }
}
