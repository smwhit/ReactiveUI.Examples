using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using ISS.Client.WPF.Annotations;
using ISS.Common;
using ReactiveUI;

namespace ISS.Client.WPF
{
    public class MainWindowViewModel : ReactiveObject
    {
        public MainWindowViewModel()
        {
            ISSPasses = new ObservableCollection<ISSPassModel>();

            GetPassesCommand = new ReactiveCommand(_isBusyHelper);
            //GetPassesCommand = new ReactiveCommand(this.WhenAnyValue(x => x.IsBusy)

             //                                               .Select(isBusy => !isBusy));
            //GetPassesCommand.Subscribe(async _ =>
            //{
            //    var passes = await new FakeISSService().GetPasses(new ISSLocationRequest());

            //    ISSPasses.Clear();

            //    passes.response.ForEach(x => ISSPasses.Add(new ISSPassModel
            //    {
            //        Duration = x.Duration,
            //        AppearanceTime = UnixTimeStampToDateTime(x.RiseTime)
            //    }));
            //});

            GetPassesCommand
                .RegisterAsyncTask(_ =>
                {
                    ISSPasses.Clear();
                    return new FakeISSService().GetPasses(new ISSLocationRequest());
                })
                //.Do(_ => ISSPasses.Clear())
                //.SelectMany(_ => new FakeISSService().GetPasses(new ISSLocationRequest()).ToObservable())
                .ObserveOn(DispatcherScheduler.Current)
                .Do(passes => passes.response.ForEach(x => ISSPasses.Add(new ISSPassModel
                {
                    Duration = x.Duration,
                    AppearanceTime = UnixTimeStampToDateTime(x.RiseTime)
                })))
                .Subscribe();

            //GetPassesCommand.Subscribe(async _ =>
            //{
            //    Observable.Start(() => { ISSPasses.Clear(); }, DispatcherScheduler.Current);

            //    var passes = await new FakeISSService().GetPasses(new ISSLocationRequest());
                
            //    passes.response.ForEach(x => ISSPasses.Add(new ISSPassModel
            //    {
            //        Duration = x.Duration,
            //        AppearanceTime = UnixTimeStampToDateTime(x.RiseTime)
            //    }));
            //});
           
            _isBusyHelper = GetPassesCommand.IsExecuting
                .ToProperty(this, vm => vm.IsBusy);
        }

        private readonly ObservableAsPropertyHelper<bool> _isBusyHelper;//= new ObservableAsPropertyHelper<bool>();

        public IReactiveCommand GetPassesCommand { get; private set; }

        public bool IsBusy
        {
            get { return _isBusyHelper.Value; }
        }

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        public ObservableCollection<ISSPassModel> ISSPasses { get; protected set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    
}

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainWindowViewModel();
        }



        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            //IsBusy = true;
            //var passes = await new FakeISSService().GetPasses(new ISSLocationRequest());

            //ISSPasses.Clear();

            //passes.response.ForEach(x => ISSPasses.Add(new ISSPassModel
            //{
            //    Duration = x.Duration,
            //    AppearanceTime = UnixTimeStampToDateTime(x.RiseTime)
            //}));

            //IsBusy = false;
        }
    }
}
