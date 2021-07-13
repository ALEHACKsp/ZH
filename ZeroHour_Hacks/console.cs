using System.IO;

namespace ZeroHour_Hacks
{
    public class ConsoleWriter
    {
        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
        private static extern bool AllocConsole();

        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool FreeConsole();
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool AttachConsole(int dwProcessId);
        private const int ATTACH_PARENT_PROCESS = -1;
        private readonly StreamWriter _stdOutWriter;
        private readonly StreamReader _stdInReader;
        public ConsoleWriter()
        {
            AllocConsole();
            Stream stdout = System.Console.OpenStandardOutput();
            Stream stdin = System.Console.OpenStandardInput();
            _stdOutWriter = new StreamWriter(stdout);
            _stdInReader = new StreamReader(stdin);
            _stdOutWriter.AutoFlush = true;

            AttachConsole(ATTACH_PARENT_PROCESS);
        }

        public void release()
        {
            FreeConsole();
        }

        public string getLine()
        {
            return _stdInReader.ReadLine();
        }
        public void WriteLine(string line)
        {
            _stdOutWriter.WriteLine(line);
            System.Console.WriteLine(line);
        }

    }
}
