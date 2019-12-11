using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace DBManager.Application.Behaviors
{
    public class PasswordBoxBehavior : Behavior<PasswordBox>
    {
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register(nameof(Password), typeof(string), typeof(PasswordBoxBehavior), new PropertyMetadata(PasswordChangedCallback));

        private static void PasswordChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var passwordBox = ((PasswordBoxBehavior)d).AssociatedObject;
            passwordBox.Password = e.NewValue.ToString();
        }

        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        protected override void OnAttached()
        {
            AssociatedObject.PasswordChanged += PasswordValueChanged;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.PasswordChanged -= PasswordValueChanged;
        }

        private void PasswordValueChanged(object sender, RoutedEventArgs e)
        {
            Password = AssociatedObject.Password;
        }

    }
}

