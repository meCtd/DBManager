using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace DBManager.Application.Behaviors
{
    public class MouseWheelClickTrigger : TriggerAction<DependencyObject>
    {
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(MouseWheelClickTrigger), new UIPropertyMetadata(null));

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register(
            "CommandParameter", typeof(object), typeof(MouseWheelClickTrigger), new PropertyMetadata(default(object)));

        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        protected override void Invoke(object parameter)
        { 
            if (AssociatedObject != null && parameter is MouseButtonEventArgs mouseArgs && mouseArgs.ChangedButton == MouseButton.Middle)
            {
                ICommand command = Command;
                if ((command != null) && command.CanExecute(CommandParameter))
                {
                    command.Execute(CommandParameter);
                }
            }
        }
    }
}

