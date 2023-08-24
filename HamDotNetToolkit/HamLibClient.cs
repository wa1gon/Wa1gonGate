using System.Net.Sockets;
using System.Text;
using static HamDotNetToolkit.IRigControl;

namespace HamDotNetToolkit
{
    public class HamLibClient : IRigControl
    {
        private TcpClientWrapper? client;
        private StreamReader? reader;
        private StreamWriter? writer;

        private const string rprt0 = "RPRT 0";
        private const string rprt1 = "RPRT 1";
        private const string rprt2 = "RPRT 2";
        private const string rprt3 = "RPRT 3";
        private const string rprt4 = "RPRT 4";
        private const string rprt5 = "RPRT 5";
        public const int PassbandDefault = 0;
        public const int PassbandNoChange = -1;

        public bool IsConnected { get; set; }
        public string? Mode { get; set; }
        public long Frequency { get; set; }
        public string? VFO { get; set; } = string.Empty;
        public int Rit { get; set; }


        public List<string>? ValidModes { get; set; } = new List<string>();
        public HamLibClient()
        {

        }

        public void Connect(string host, int port)
        {
            client = new TcpClientWrapper();
            client.Connect(host, port);
            var networkStream = client.GetStream();
            reader = new StreamReader(networkStream, Encoding.ASCII);
            writer = new StreamWriter(networkStream, Encoding.ASCII);
            IsConnected = true;
        }

        public void Disconnect()
        {
            writer?.Dispose();
            reader?.Dispose();
            client?.Close();
            IsConnected = false;
        }

        public List<string> GetValidModes()
        {
            string modeList = SendCommand("M ?\n");
            ValidModes = ParseSpaceDelimitedStringToList(modeList);
            return ValidModes;
        }
        public string SendCommand(string command)
        {
            if (!IsConnected)
            {
                throw new InvalidOperationException("Not connected to rigctld.");
            }

            writer?.WriteLine(command);
            writer?.FlushAsync();

            StringBuilder responseBuilder = new StringBuilder();
            char[] buffer = new char[1024];

            while (true)
            {
                int bytesRead = reader!.Read(buffer, 0, buffer.Length);
                if (bytesRead == 0)
                {
                    // End of stream, exit the loop
                    break;
                }

                responseBuilder.Append(buffer, 0, bytesRead);

                // Check if the response ends with a newline
                if (responseBuilder.ToString().EndsWith("\n"))
                {
                    break;
                }
            }

            string rc = responseBuilder.ToString().Trim();
            return rc;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Disconnect();
                client?.Dispose();
            }
        }

        public ErrorCode SetFrequency(long frequency)
        {
            ErrorCode eCode;
            string rc = SendCommand($"F {frequency}\n");
            eCode = GetErrorCode(rc);
            if (eCode == ErrorCode.Success)
                Frequency = frequency;
            return eCode;
        }

        public (ErrorCode, long) GetFrequency()
        {

            string rc = SendCommand($"f\n");
            Frequency = Int64.Parse(rc);
            return (ErrorCode.Success, Frequency);
        }
        public ErrorCode SetRit(int frequency)
        {
            ErrorCode eCode;
            string rc = SendCommand($"J {frequency}\n");
            eCode = GetErrorCode(rc);
            if (eCode == ErrorCode.Success)
                Rit = frequency;
            return eCode;
        }

        public (ErrorCode, long) GetRit()
        {
            string rc = SendCommand($"j\n");
            Rit = int.Parse(rc);
            return (ErrorCode.Success, Rit);
        }

        public ErrorCode SetXit(int frequency)
        {
            ErrorCode eCode;
            string rc = SendCommand($"Z {frequency}\n");
            eCode = GetErrorCode(rc);
            if (eCode == ErrorCode.Success)
                Rit = frequency;
            return eCode;
        }

        public (ErrorCode, long) GetXit()
        {
            string rc = SendCommand($"z\n");
            Rit = int.Parse(rc);
            return (ErrorCode.Success, Rit);
        }

        public ErrorCode SetMode(string mode, int PassBand = PassbandNoChange)
        {
            ErrorCode eCode;
            if (ValidModes!.Count == 0)
                ValidModes = GetValidModes();
            if (ValidModes.Contains(mode) == false)
            {
                return ErrorCode.InvalidParams;
            }
            string rc = SendCommand($"M {mode} {PassBand}\n");
            eCode = GetErrorCode(rc);
            if (eCode == ErrorCode.Success)
                Mode = mode;
            return eCode;
        }

        public (ErrorCode, string) GetMode()
        {
            string rc = SendCommand($"m\n");

            var items = ParseSpaceDelimitedStringToList(rc);
            if (items.Count == 2)
            {

            }

            return (ErrorCode.Success, items[0]);
        }
        public ErrorCode SetVfo(string vfo)
        {
            ErrorCode eCode = ErrorCode.Success;

            string rc = SendCommand($"V {vfo}\n");
            eCode = GetErrorCode(rc);
            return eCode;
        }

        public (ErrorCode, string) GetVfo()
        {
            string rc = SendCommand($"v\n");

            return (ErrorCode.Success, rc);
        }
        static private ErrorCode GetErrorCode(string rprtMessage)
        {
            ErrorCode code = rprtMessage switch
            {

                rprt0 => ErrorCode.Success,
                rprt1 => ErrorCode.Unsupported,
                rprt2 => ErrorCode.InvalidParams,
                rprt3 => ErrorCode.NoResponse,
                rprt4 => ErrorCode.Timeout,
                rprt5 => ErrorCode.Unspecified,
                _ => ErrorCode.Unspecified
            };
            return code;
        }
        private static List<string> ParseSpaceDelimitedStringToList(string input)
        {
            input = TrimToFirstNewline(input);
            string[] stringArray = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            List<string> stringList = new List<string>(stringArray);
            return stringList;
        }
        public static string TrimToFirstNewline(string input)
        {
            int indexOfNewline = input.IndexOf('\n');
            if (indexOfNewline >= 0)
                return input.Substring(0, indexOfNewline);

            return input;
        }

    }
}
