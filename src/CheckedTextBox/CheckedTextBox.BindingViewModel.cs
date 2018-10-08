using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Sprintor.CheckedTextBox
{

    public partial class CheckedTextBox
    {

        public class BindingViewModel
            : INotifyPropertyChanged
        {

            protected bool _checked;
            protected string _label;
            protected string _text;

            public event PropertyChangedEventHandler PropertyChanged;

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
                    return this._label;
                }
                set
                {
                    if (this._label != value)
                    {
                        this._label = value;
                        this.OnPropertyChanged();
                    }
                }
            }

            public string Text
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

            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                if (propertyName != null)
                {
                    this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                }
            }

        }

    }

}