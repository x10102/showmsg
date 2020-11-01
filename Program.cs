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
        static string help_msg = @"
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
        static string[] validArgs = {"-b", "-m", "-t", "-i", "-d"};
        static void Main(string[] args)
        {
            MessageBoxIcon ico = MessageBoxIcon.None;
            MessageBoxButtons btn = MessageBoxButtons.OK;
            MessageBoxDefaultButton def = MessageBoxDefaultButton.Button1;
            String message = "";
            String title = "";
            bool iconSet = false;
            bool btnSet = false;
            bool defSet = false;
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
                                            .Take(a - i - 1)
                                            .ToArray());            // Takes the range of arguments from the first one after "-m" to the one before the next parameter and joins them together to create the message string

                                        i += a - i - 1;
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
                                    
                                    if (validArgs.Contains(args[o]))
                                    {
                                         title = String.Join(" ",
                                            args.Skip(i + 1)
                                            .Take(o - i - 1)
                                            .ToArray());            // Same as -m
                                        i += o - i - 1;
                                        break;
                                    } else if (o + 1 == args.Length)
                                    {
                                        title = String.Join(" ", args.Skip(i + 1).Take(2));
                                        i += o - i;
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

                                    case "question":
                                        if (!iconSet)
                                        {
                                            ico = MessageBoxIcon.Question;
                                            iconSet = true;
                                        }
                                        break;

                                    case "none":
                                        if (!iconSet)
                                        {
                                            ico = MessageBoxIcon.None;
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

                            case "-d":
                                switch(args[i+1])
                                {
                                    case "1":
                                        if (!defSet)
                                        {
                                            def = MessageBoxDefaultButton.Button1;
                                            defSet = true;
                                        }
                                        break;

                                    case "2":
                                        if (!defSet)
                                        {
                                            def = MessageBoxDefaultButton.Button2;
                                            defSet = true;
                                        }
                                        break;

                                    case "3":
                                        if (!defSet)
                                        {
                                            def = MessageBoxDefaultButton.Button3;
                                            defSet = true;
                                        }
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
                } catch(ArgumentOutOfRangeException e)
                {
                    Console.WriteLine(e);
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
