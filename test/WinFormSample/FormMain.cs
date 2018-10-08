using Newtonsoft.Json;
using Sprintor.CheckedTextBox;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinFormSample.Models;

namespace WinFormSample
{

    public partial class FormMain
        : Form
    {

        private const int ControlWidth = 100;
        private const int ControlHeight = 40;

        private List<CheckedTextBox> _checkItems;

        public FormMain()
        {
            this.InitializeComponent();

            var itemCount = 16;
            this._checkItems = new List<CheckedTextBox>();
            for (var i = 0; i < itemCount; i++)
            {
                var item = new CheckItem()
                {
                    Label = i.ToString(),
                    Checked = i % 2 == 0,
                    Text = $"Text-{i}"
                };
                var ctrl = new CheckedTextBox()
                {
                    DataContext = item,
                    Tag = item,
                    Size = new Size(ControlWidth, ControlHeight)
                };
                this._checkItems.Add(ctrl);
            }
            this.Controls.AddRange(this._checkItems.ToArray());
            this.UpdateLayout();

            this.Resize += this.FormMain_Resize;
            this.Click += this.FormMain_Click;
        }

        private void FormMain_Click(object sender, System.EventArgs e)
        {
            var items = this._checkItems.Select(x => x.Tag as CheckItem).ToList();
            var json = JsonConvert.SerializeObject(items, Formatting.Indented);
            MessageBox.Show("Json string has been copied to the System Clipboard.");
            Clipboard.SetText(json);
        }

        private void FormMain_Resize(object sender, System.EventArgs e)
        {
            this.UpdateLayout();
        }

        protected void UpdateLayout()
        {
            var startPosition = new Point(12, 12);

            var containerWidth = this.Size.Width - startPosition.X * 2;
            var containerHeight = this.Size.Height - startPosition.Y * 2;

            var line = 0;
            var itemsPerLine = containerWidth / ControlWidth;
            var lines = this._checkItems.Count / itemsPerLine + 1;

            var lineSpace = (containerHeight - ControlHeight) / lines - ControlHeight;
            var columnSpace = containerWidth / itemsPerLine - ControlWidth;

            for (var i = 0; i < this._checkItems.Count; i += itemsPerLine)
            {
                var y = startPosition.Y + line * (ControlHeight + lineSpace);
                var items = this._checkItems.Skip(i).Take(itemsPerLine).ToList();
                for (var j = 0; j < items.Count; j++)
                {
                    var item = items[j];
                    var model = (CheckItem)item.Tag;
                    model.Position = new Point(line, j);
                    model.Checked = j % 2 == 0;
                    model.Text = $"Text-{i + i}-{j}";
                    item.Location = new Point(
                        startPosition.X + j * (ControlWidth + columnSpace), y);
                    Debug.Assert(item.Location.X + ControlWidth <= this.Size.Width);
                    Debug.Assert(y <= this.Size.Height);
                }
                line++;
            }
        }

    }

}