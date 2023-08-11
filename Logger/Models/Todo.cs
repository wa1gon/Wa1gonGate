namespace Logger.Models;

using System.ComponentModel;
public class Todo : INotifyPropertyChanged
{
    private int _id;
    public int Id
    {
        get => _id;
        set
        {
            if (_id == value)
                return;

            _id = value;
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(nameof(Id)));

        }
    }
    public string Name { get ; set; }

    public event PropertyChangedEventHandler PropertyChanged;
}
