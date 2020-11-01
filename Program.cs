using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace showmsg
{
    class Program
    {

        static string inv_arg = "Invalid arguments, display usage with --help";
        static string help_msg = "TODO: Put helpful text here";
        static string[] validArgs = { "-b", "-m", "-t", "-i" };
        static void Main(string[] args)
        {
            MessageBoxIcon ico = MessageBoxIcon.None;
            MessageBoxButtons btn = MessageBoxButtons.OK;
            String message = "";
            String title = "";
            bool iconSet = false;
            bool btnSet = false;
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
                                    if(a+1 == args.Length)
                                    {
                                        message = String.Join(" ", args.Skip(i+1).Take(a));
                                        i += a - i;
                                        break;
                                    } else if(validArgs.Contains(args[a]))
                                    {
                                        message = String.Join(" ", args.Skip(i+1).Take(a-1));
                                        i += a - i - 1;
                                        break;
                                    }
                                }
                                break;

                            case "-t":

                                for (int a = i + 1; a < args.Length; a++)
                                {
                                    if (a + 1 == args.Length)
                                    {
                                        title = String.Join(" ", args.Skip(i + 1).Take(a));
                                        i += a - i;
                                        break;
                                    }
                                    else if (validArgs.Contains(args[a]))
                                    {
                                        title = String.Join(" ", args.Skip(i + 1).Take(a - 1));
                                        i += a - i - 1;
                                        break;
                                    }
                                }
                                break;

                            case "-i":

                                switch (args[i + 1])
                                {
                                    case "error":
                                        if (!iconSet)
                                        {
                                            ico = MessageBoxIcon.Error;
                                            iconSet = true;
                                        }
                                        break;

                                    case "info":
                                        if (!iconSet)
                                        {
                                            ico = MessageBoxIcon.Information;
                                            iconSet = true;
                                        }
                                        break;

                                    case "warn":
                                        if (!iconSet)
                                        {
                                            ico = MessageBoxIcon.Warning;
                                            iconSet = true;
                                        }
                                        break;

                                    default:
                                        Console.WriteLine(inv_arg);
                                        Environment.Exit(-1);
                                        break;

                                }
                                i += 1;
                                break;

                            case "-b":
                                switch (args[i + 1])
                                {
                                    case "abortretryignore":
                                        if (!btnSet)
                                        {
                                            btn = MessageBoxButtons.AbortRetryIgnore;
                                            btnSet = true;
                                        }
                                        break;

                                    case "okcancel":
                                        if (!btnSet)
                                        {
                                            btn = MessageBoxButtons.OKCancel;
                                            btnSet = true;
                                        }
                                        break;

                                    case "ok":
                                        if (!btnSet)
                                        {
                                            btn = MessageBoxButtons.OK;
                                            btnSet = true;
                                        }
                                        break;

                                    case "retrycancel":
                                        if (!btnSet)
                                        {
                                            btn = MessageBoxButtons.RetryCancel;
                                            btnSet = true;
                                        }
                                        break;

                                    case "yesno":
                                        if (!btnSet)
                                        {
                                            btn = MessageBoxButtons.YesNo;
                                            btnSet = true;
                                        }
                                        break;

                                    case "yesnocancel":
                                        if (!btnSet)
                                        {
                                            btn = MessageBoxButtons.YesNoCancel;
                                            btnSet = true;
                                        }
                                        break;

                                    default:
                                        Console.WriteLine(inv_arg);
                                        Environment.Exit(-1);
                                        break;

                                }
                                i += 1;
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
                DialogResult rs = MessageBox.Show(message, title, btn, ico);
                Console.Write(rs.ToString().ToLower());
            } else
            {
                Console.WriteLine("No arguments, display usage with --help");
                Environment.Exit(-1);
            }
        }

    }
}
