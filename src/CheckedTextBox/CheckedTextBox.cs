using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Sprintor.CheckedTextBox
{

    public partial class CheckedTextBox
        : UserControl, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private string _labelText = "0";
        private bool _checked = false;
        private string _text = "";
        private BindingViewModel _dataContext;

        public bool Checked
        {
            get
            {
                return this._checked;
            }
            set
            {
                if (this._checked != value)
                {
                    this._checked = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string Label
        {
            get
            {
                return this._labelText;
            }
            set
            {
                if (this._labelText != value)
                {
                    this._labelText = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public override string Text
        {
            get
            {
                return this._text;
            }
            set
            {
                if (this._text != value)
                {
                    this._text = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public BindingViewModel DataContext
        {
            get
            {
                return this._dataContext;
            }
            set
            {
                if (this._dataContext != value)
                {
                    if (this._dataContext != null)
                    {
                        this._dataContext.PropertyChanged -= this._dataContext_PropertyChanged;
                    }
                    this._dataContext = value;
                    this._dataContext.PropertyChanged += this._dataContext_PropertyChanged;
                }
            }
        }

        private void _dataContext_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(BindingViewModel.Checked):
                    this.Checked = this._dataContext.Checked;
                    break;
                case nameof(BindingViewModel.Label):
                    this.Label = this._dataContext.Label;
                    break;
                case nameof(BindingViewModel.Text):
                    this.Text = this._dataContext.Text;
                    break;
            }
        }

        public CheckedTextBox()
        {
            this.InitializeComponent();

            this._checkBox.Checked = this._checked;
            this._checkBox.Text = this._labelText;
            this._textBox.Text = this._text;

            this._checkBox.CheckedChanged += this._checkBox_CheckedChanged;
            this._textBox.TextChanged += this._textBox_TextChanged;

            this.Resize += this.CheckedTextBox_Resize;
        }

        private void CheckedTextBox_Resize(object sender, EventArgs e)
        {
            this.Size = new Size(this.Size.Width, (int)(this._checkBox.Height * 2.1));
        }

        private void _checkBox_CheckedChanged(object sender, EventArgs e)
        {
            this._checked = this._checkBox.Checked;
            if (this._dataContext != null)
            {
                this._dataContext.Checked = this._checked;
            }
        }

        private void _textBox_TextChanged(object sender, EventArgs e)
        {
            this._text = this._textBox.Text;
            if (this._dataContext != null)
            {
                this._dataContext.Text = this._text;
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (propertyName != null)
            {
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                switch (propertyName)
                {
                    case nameof(BindingViewModel.Checked):
                        if (this._checkBox != null)
                        {
                            this._checkBox.Checked = this.Checked;
                        }
                        break;
                    case nameof(BindingViewModel.Label):
                        if (this._checkBox != null)
                        {
                            this._checkBox.Text = this.Label;
                        }
                        break;
                    case nameof(BindingViewModel.Text):
                        if (this._textBox != null)
                        {
                            this._textBox.Text = this.Text;
                        }
                        break;
                }
            }
        }

    }

}