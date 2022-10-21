using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Esoterics.PspspsInterpreter;
using Esoterics.InstructionSets;

namespace runtime
{
    public static class Commands
    {
        private static Dictionary<string, Action<string[], string[]>> commands = new Dictionary<string, Action<string[], string[]>>()
        {
            { "psp.run", RunPspsps },
            { "help", Help },
            { "psp.convert", PspConvert }
        };

        private static Dictionary<string, string> commandsHelp = new Dictionary<string, string>()
        {
            { "psp.run", "Runs a pspsps code file (using the included interpreter), arguments :\n\t* the file itself (can be written with quotes : \"path/file.psp\")\n\t* -d : enables debug mode\n\t* -t : returns the execution time\n\t* -m : shows the memory at the end of the program" },
            { "help", "Shows every command or help on a specific command, arguments :\n\t* optionally the command you want to get help for" },
            { "psp.convert", "Converts a pspsps code file from one style (asm or psp) to its opposite style, arguments :\n\t* A first to read code from\n\t* (Optional) A second file to write converted code to" }
        };

        public static void TryInvokeCommand(string line)
        {
            string[] commandArgs = line.Trim().Split(' ', 2);
            string command = commandArgs[0];

            if (command == "exit")
                return;

            if (commandArgs.Length == 1)
            {
                try
                {
                    commands[command].Invoke(new string[0], new string[0]);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Command failed : {ex.Message}");
                }
                return;
            }

            List<string> cargs = new List<string>(4);
            List<string> args = new List<string>(4);

            List<string> current = args;

            bool inStringDecl = false;

            char c;
            for (int i = 0; i < commandArgs[1].Length; i++)
            {
                c = commandArgs[1][i];
                if (inStringDecl)
                {
                    if (c == '\"')
                        inStringDecl = false;
                    else
                        args[args.Count - 1] += c;

                    continue;
                }

                if (c == ' ')
                {
                    if (commandArgs[1][i + 1] == '-')
                        current = cargs;
                    else
                        current = args;
                    current.Add("");
                    continue;
                }
                else if (c == '-')
                {
                    current = cargs;
                }
                else if (c == '\"')
                    inStringDecl = true;
                else
                {
                    if (current.Count == 0)
                        current.Add("");
                    current[current.Count - 1] += c;

                }
            }

            try
            {
                commands[command].Invoke(cargs.ToArray(), args.ToArray());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Command failed : {ex.Message}");
            }
        }

        private static void RunPspsps(string[] cargs, string[] args)
        {
            if (args.Length < 1)
                throw new Exception("No arguments recieved for file to run");
            bool timeit = cargs.Contains("t"), debug = cargs.Contains("d"), showmemory = cargs.Contains("m");

            string code = File.ReadAllText(args[0]);
            PspspsHeaderSettings settings = PspspsParser.GetSettingsFromCodeStringHeader(code);

            PspspsVM vm = new PspspsVM(settings);

            Console.WriteLine($"\nInterpreter : {settings.InstructionSet.Name}, V{settings.InstructionSet.Version}, {settings.type} style instructions");
            Console.WriteLine($"Allocated Memory : {settings.MemSize}, Function Stack Capacity : {settings.FSS}");

            Console.WriteLine($"Parsing program...");

            PspspsCode parsedCode = PspspsParser.CodeFromString(code, settings.InstructionSet);
            Stopwatch sw = Stopwatch.StartNew();

            Console.WriteLine($"Succesfully parsed.\n\nRunning program :\n#####\n");

            if (timeit)
                sw.Restart();

            if (debug)
                vm.ExecuteDebug(parsedCode);
            else
                vm.Execute(parsedCode);

            Console.Write("\n\n#####\nProgram terminated.");
            if (timeit)
            {
                sw.Stop();
                Console.Write($" Execution took {sw.Elapsed.TotalMilliseconds}ms");
            }
            Console.WriteLine('\n');
            if (showmemory)
            {
                string[] mems = new string[vm.Memory.Count()];
                for (int i = 0; i < mems.Length; i++)
                    mems[i] = vm.Memory.Peek(i).ToString();
                
                Console.WriteLine($"Program memory after termination : [{string.Join(", ", mems)}]\n");
            }
        }
        
        private static void Help(string[] cargs, string[] args)
        {
            if (args.Length > 0)
            {
                Console.WriteLine($"Help for command {args[0]} :\n{commandsHelp[args[0]]}");
                return;
            }
            Console.WriteLine("Showing help for every command :");
            foreach (string command in commandsHelp.Keys)
            {
                Console.WriteLine($"   Command `{command}` {commandsHelp[command]}");
            }
            Console.WriteLine();
        }

        public static void PspConvert(string[] cargs, string[] args)
        {

            if (args.Length < 1)
                throw new Exception("At least one file is needed in arguments for conversion to happen");

            string file = args[0], targetFile;

            if (args.Length < 2 || args[0] == args[1])
            {
                Console.WriteLine("Are you sure you want to convert the file and override it with the new version (All comments will be lost) (y?) : ");
                if (Console.ReadKey().Key != ConsoleKey.Y)
                {
                    Console.WriteLine("\nCancelled conversion.\n");
                    return;
                }
                Console.WriteLine();
                targetFile = args[0];
            }
            else
            {
                targetFile = args[1];

                Console.WriteLine($"The file {targetFile} will be overriden, are you sure ? (y?)");
                if (Console.ReadKey().Key != ConsoleKey.Y)
                {
                    Console.WriteLine("\nCancelled conversion.\n");
                    return;
                }
                Console.WriteLine();
            }

            string code = File.ReadAllText(args[0]);
            PspspsHeaderSettings settings = PspspsParser.GetSettingsFromCodeStringHeader(code);

            Console.WriteLine($"Parsing code...");

            PspspsCode parsedCode = PspspsParser.CodeFromString(code, settings.InstructionSet);

            Console.WriteLine("Successfuly parsed.");

            string targetType = settings.type == "asm" ? "psp" : "asm";
            PspspsInstructionSet targetSet = settings.type == "asm" ? PspspsV1.GetPspspsPspspsSet() : PspspsV1.Set;

            PspspsHeaderSettings newSettings = new PspspsHeaderSettings(targetSet, settings.TargetVersion, settings.MemSize, settings.FSS, targetType);

            File.WriteAllText(targetFile, PspspsUtils.GetCodeAsText(parsedCode, newSettings));

            Console.WriteLine($"Conversion done, written converted code to {targetFile} in {targetType}\n");
        }

    }
}
