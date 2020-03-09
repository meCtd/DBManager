using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace DBManager.Application.Behaviors
{
    public class PasswordBoxBehavior : Behavior<PasswordBox>
    {
        private bool _isSuspended;

        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register(nameof(Password), typeof(string), typeof(PasswordBoxBehavior), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, PasswordChanged));

        private static void PasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var behaviour = (PasswordBoxBehavior)d;

            if (behaviour._isSuspended)
                return;

            behaviour.AssociatedObject.Password = (string)e.NewValue;
            UpdateCaret(behaviour.AssociatedObject);
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
            try
            {
                _isSuspended = true;

                Password = AssociatedObject.Password;
            }
            finally
            {
                _isSuspended = false;
            }
        }

        private static void UpdateCaret(PasswordBox passwordBox)
        {
            passwordBox.GetType().GetMethod("Select", BindingFlags.Instance | BindingFlags.NonPublic)
                .Invoke(passwordBox, new object[] { passwordBox.Password.Length, 1 });
        }
    }
}

