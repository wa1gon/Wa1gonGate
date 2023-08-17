

namespace HamDotNetToolkit;

public interface IRigControl : IDisposable
{
    public enum ErrorCode
    {
        Success, Unsupported, InvalidParams, NoResponse,
        Timeout, Unspecified
    }

    public List<string>? ValidModes { get; set; }

    public ErrorCode SetFrequency(long frequency);
    public (ErrorCode, long) GetFrequency();

    public void Connect(string host, int port);
    public void Disconnect();
    public string? SendCommand(string command);
    public ErrorCode SetRit(int frequency);
    public (ErrorCode, long) GetRit();
    public ErrorCode SetXit(int frequency);
    public (ErrorCode, long) GetXit();

    public ErrorCode SetMode(string mode, int PassBand);
    public (ErrorCode, string) GetMode();

    public List<string> GetValidModes();

    public ErrorCode SetVfo(string vfo);
    public (ErrorCode, string) GetVfo();


    public string? Mode { get; set; }
    public string? VFO { get; set; }
    public long Frequency { get; set; }
    public bool IsConnected { get; set; }

}
