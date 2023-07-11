using System.Text;

namespace GenFile
{
    public static class Program
    {
        private static int FileSize { get; set; } = 1024; //in kb
        private static string? FilePath { get; set; }

        public static void Main(string[] args)
        {
            if (SetArgs(args.ToList()))
                return;

            GenerateFile();
            Console.WriteLine($"Created file with {FileSize}kb size");
        }

        //returns: wether should stop
        //params: args: input args
        private static bool SetArgs(List<string> args)
        {
            if (args.Contains("h") || args.Contains("help"))
            {
                Console.WriteLine("s = file size in kb, default is 1024kb");
                Console.WriteLine("p = specify file location and name");
                Console.WriteLine("v = display application's version");
                Console.WriteLine("h = display this text");

                return true;
            }

            if (args.Contains("v"))
            {
                Console.Write("version 0.1");
                return true;
            }

            if (args.Contains("s"))
            {
                var index = args.IndexOf("s");

                if (index >= args.Count - 1)
                {
                    Console.WriteLine("must provide value for s argument");
                    return true;
                }

                if (int.TryParse(args[index + 1], out int size))
                    FileSize = size;

                else
                {
                    Console.WriteLine("value for s argument must be a number");
                    return true;
                }
            }

            if (args.Contains("p"))
            {
                var index = args.IndexOf("p");

                if (index >= args.Count - 1)
                {
                    Console.WriteLine("must provide value for p argument");
                    return true;
                }

                FilePath = args[index + 1];
            }

            return false;
        }

        private static void GenerateFile()
        {
            var currentDir = FilePath != null ? FilePath : $"{Directory.GetCurrentDirectory()}/generated-file-{DateTime.Now.Ticks}.txt";
            FileStream stream;

            try
            {
                stream = File.OpenWrite(currentDir);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Could not create file: \n{e.Message}");
                return;
            }

            var bytes = Encoding.UTF8.GetBytes("#")[0];
            var fileSize = FileSize * 1024;
            Console.WriteLine("generating...");

            for (int i = 1; i < fileSize + 1; i++)
                stream.WriteByte(bytes);
        }

    }
}