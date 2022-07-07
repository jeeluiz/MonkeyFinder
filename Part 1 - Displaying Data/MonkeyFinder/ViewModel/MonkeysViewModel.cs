using IntelliJ.Lang.Annotations;
using MonkeyFinder.Services;

namespace MonkeyFinder.ViewModel;

public partial class MonkeysViewModel : BaseViewModel
{
    MonkeyService monkeyService;
    public ObservableCollection<Monkey> Monkeys { get; } = new(); 
    public Command GetMonkeysCommand { get; }
    public MonkeysViewModel()
    {
        Title = "Monkey Finder";
        this.monkeyService = monkeyService;
    }

    async Task GetMonkeysAsync()
    {
        if(IsBusy)
            return;
        try
        {
            IsBusy = true;
            var monkeys = await monkeyService.GetMonkeys();

            if (Monkeys.Count != 0)
                Monkeys.Clear();
            foreach (var monkey in monkeys)
                Monkeys.Add(monkey);
        }
        catch(Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error", $"Unable to get monkeys:{ex.Message}", "OK");
}
finally
        {
            IsBusy = false;
        }
    }
}

