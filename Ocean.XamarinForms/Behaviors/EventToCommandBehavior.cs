namespace Ocean.XamarinForms.Behaviors {
    using System;
    using System.Reflection;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class EventToCommandBehavior : BehaviorBase<View> {

        Delegate _eventHandler;
        public static readonly BindableProperty EventNameProperty = BindableProperty.Create("EventName", typeof(String), typeof(EventToCommandBehavior), null, propertyChanged: OnEventNameChanged);
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create("CommandParameter", typeof(Object), typeof(EventToCommandBehavior));
        public static readonly BindableProperty CommandProperty = BindableProperty.Create("Command", typeof(ICommand), typeof(EventToCommandBehavior));
        public static readonly BindableProperty InputConverterProperty = BindableProperty.Create("Converter", typeof(IValueConverter), typeof(EventToCommandBehavior));

        public ICommand Command {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public Object CommandParameter {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public IValueConverter Converter {
            get { return (IValueConverter)GetValue(InputConverterProperty); }
            set { SetValue(InputConverterProperty, value); }
        }

        public String EventName {
            get { return (String)GetValue(EventNameProperty); }
            set { SetValue(EventNameProperty, value); }
        }

        protected override void OnAttachedTo(View bindable) {
            base.OnAttachedTo(bindable);
            RegisterEvent(EventName);
        }

        protected override void OnDetachingFrom(View bindable) {
            DeRegisterEvent(EventName);
            base.OnDetachingFrom(bindable);
        }

        void DeRegisterEvent(String name) {
            if (String.IsNullOrWhiteSpace(name)) {
                return;
            }

            if (_eventHandler == null) {
                return;
            }
            var eventInfo = AssociatedObject.GetType().GetRuntimeEvent(name);
            if (eventInfo == null) {
                throw new ArgumentException($"{this.GetType().Name}: Can't de-register the '{this.EventName}' event.");
            }
            eventInfo.RemoveEventHandler(AssociatedObject, _eventHandler);
            _eventHandler = null;
        }

        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once UnusedParameter.Local
        void OnEvent(Object sender, Object eventArgs) {
            if (Command == null) {
                return;
            }

            Object resolvedParameter;
            if (CommandParameter != null) {
                resolvedParameter = CommandParameter;
            } else if (Converter != null) {
                resolvedParameter = Converter.Convert(eventArgs, typeof(Object), null, null);
            } else {
                resolvedParameter = eventArgs;
            }

            if (Command.CanExecute(resolvedParameter)) {
                Command.Execute(resolvedParameter);
            }
        }

        static void OnEventNameChanged(BindableObject bindable, Object oldValue, Object newValue) {
            var behavior = (EventToCommandBehavior)bindable;
            if (behavior.AssociatedObject == null) {
                return;
            }

            var oldEventName = (String)oldValue;
            var newEventName = (String)newValue;

            behavior.DeRegisterEvent(oldEventName);
            behavior.RegisterEvent(newEventName);
        }

        void RegisterEvent(String name) {
            if (String.IsNullOrWhiteSpace(name)) {
                return;
            }

            var eventInfo = AssociatedObject.GetType().GetRuntimeEvent(name);
            if (eventInfo == null) {
                throw new ArgumentException($"{this.GetType().Name}: Can't register the '{this.EventName}' event.");
            }
            var methodInfo = typeof(EventToCommandBehavior).GetTypeInfo().GetDeclaredMethod("OnEvent");
            _eventHandler = methodInfo.CreateDelegate(eventInfo.EventHandlerType, this);
            eventInfo.AddEventHandler(AssociatedObject, _eventHandler);
        }

    }
}
