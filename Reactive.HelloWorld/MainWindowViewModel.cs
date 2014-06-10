using System;
using System.Windows;
using ReactiveUI;

namespace Reactive.HelloWorld
{
    public class MainWindowViewModel : ReactiveObject
    {
        public MainWindowViewModel()
        {
            ChangePasswordCommand = new ReactiveCommand(this.WhenAny(x=>x.Password, x=>x.ConfirmedPassword, 
                (p, c) => string.IsNullOrWhiteSpace(p.Value) && p.Value == c.Value));
            ChangePasswordCommand.Subscribe(_ => MessageBox.Show("You clicked on DisplayCommand: Name is " + Password));

        }

        private string password;
        public string Password
        {
            get { return password; }
            set { this.RaiseAndSetIfChanged(ref password, value); }
        }

        private string confirmedPassword;

        public string ConfirmedPassword
        {
            get
            {
                return confirmedPassword;
            } 
            set
            {
                this.RaiseAndSetIfChanged(ref confirmedPassword, value);
            }
        }

        public IReactiveCommand ChangePasswordCommand { get; protected set; }
    }
}
