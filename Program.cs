using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace showmsg
{
    class Program
    {
        #region strings
        static readonly string inv_arg = "Invalid arguments, display usage with --help";
        static readonly string help_msg = @"
======SHOWMSG PARAMETERS======

   -m Sets the message

   -t Sets the message box title
 
   -b Selects the buttons to be shown in the message box
    Possible values: [ok(default), okcancel, retrycancel, yesno, yesnocancel, abortretryignore]

   -i Selects the message box icon and sound
    Possible values: [error, info, warn, question, none(default)]

   -d Sets the default button
    Possible values: [1 (default), 2, 3]
";
        #endregion
        #region arg_dictionaries
        static readonly string[] validArgs = new string[] { "-b", "-m", "-t", "-i", "-d" };
        static readonly Dictionary<String, MessageBoxButtons> btn_params = new Dictionary<String, MessageBoxButtons>() { {"abortretryignore", MessageBoxButtons.AbortRetryIgnore }, {"ok", MessageBoxButtons.OK }, { "okcancel", MessageBoxButtons.OKCancel }, { "yesno", MessageBoxButtons.YesNo }, { "yesnocancel", MessageBoxButtons.YesNoCancel }, { "retrycancel", MessageBoxButtons.RetryCancel } };
        static readonly Dictionary<String, MessageBoxIcon> ico_params = new Dictionary<string, MessageBoxIcon>() { { "info", MessageBoxIcon.Information }, { "error", MessageBoxIcon.Error }, { "warn", MessageBoxIcon.Warning }, { "question", MessageBoxIcon.Question }, { "none", MessageBoxIcon.None } };
        #endregion
        static void Main(string[] args)
        {
            MessageBoxIcon ico = MessageBoxIcon.None;
            MessageBoxButtons btn = MessageBoxButtons.OK;
            MessageBoxDefaultButton def = MessageBoxDefaultButton.Button1;
            String message = "", title = "";
            bool iconSet = false, btnSet = false, defSet = false;
            if (args.Length > 0)
            {
                try
                {
                    for (int i = 0; i < args.Length; i++)
                    {
                        switch (args[i])
                        {

                            case "--help":
                                Console.WriteLine(help_msg);
                                Environment.Exit(0);
                                break;

                            case "-m":

                                for(int a = i + 1; a < args.Length; a++)
                                {
                                    if(validArgs.Contains(args[a]))
                                    {
                                        message = String.Join(" ",
                                            args.Skip(i + 1)
                                            .Take(a - i - 1));
                                                       // Takes the range of arguments from the first one after "-m" to the one before the next parameter and joins them together to create the message string

                                        i += a - i - 1;             // Skip over the message text 
                                        break;

                                    } else if(a + 1 == args.Length)
                                    {
                                        message = String.Join(" ", args.Skip(i + 1).Take(a - i)); 
                                        i += a - i;
                                        break;
                                    }
                                }
                                break;

                            case "-t":

                                for (int o = i + 1; o < args.Length; o++)
                                {
                                    
                                    if (validArgs.Contains(args[o])) // Same as -m
                                    {
                                         title = 
                                            String.Join(" ", args.Skip(i + 1)
                                            .Take(o - i - 1));     
                                        
                                        i += o - i - 1;
                                        break;

                                    } else if (o + 1 == args.Length)
                                    {
                                        title = String.Join(" ", args.Skip(i + 1).Take(o - i));
                                        i += o - i;
                                        break;
                                    }
                                }
                                break;

                            case "-i":

                                if (ico_params.ContainsKey(args[i + 1]) && !iconSet)
                                {
                                    ico = ico_params[args[i + 1]];
                                    iconSet = true;
                                    i += 1;
                                }
                                else
                                {
                                    Console.WriteLine(inv_arg);
                                    Environment.Exit(-1);
                                }

                                break;

                            case "-b":

                                if (btn_params.ContainsKey(args[i + 1]) && !btnSet)
                                {
                                    btn = btn_params[args[i + 1]];
                                    btnSet = true;
                                    i += 1;
                                }
                                else
                                {
                                    Console.WriteLine(inv_arg);
                                    Environment.Exit(-1);
                                }
                                
                                break;

                            case "-d":
                                int def_number;
                                if(int.TryParse(args[i+1], out def_number) && def_number > 0 && def_number < 4 && !defSet)
                                {
                                    def = (MessageBoxDefaultButton)((def_number - 1) * 256); // The values in the DefaultButton enum are defined as multiples of 256
                                    defSet = true;
                                    i++;
                                } else
                                {
                                    Console.WriteLine(inv_arg);
                                    Environment.Exit(-1);
                                }
                                break;
                                

                            default:
                                Console.WriteLine(inv_arg);
                                Environment.Exit(-1);
                                break;

                        }
                    }
                } catch(ArgumentOutOfRangeException)
                {
                    Console.WriteLine(inv_arg);
                    Environment.Exit(-1);
                }
                DialogResult rs = MessageBox.Show(message, title, btn, ico, def);
                Console.Write(rs.ToString().ToLower());
                Environment.Exit(0);
            } else
            {
                Console.WriteLine("No arguments, display usage with --help");
                Environment.Exit(-1);
            }
        }

    }
}
