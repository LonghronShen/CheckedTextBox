using Sprintor.CheckedTextBox;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace WinFormSample.Models
{

    public class CheckItem
        : CheckedTextBox.BindingViewModel
    {

        public Point Position { get; set; }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            Debug.WriteLine($"Property {propertyName} has been changed.");
        }

    }

}