﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading;
using System.Windows.Forms;
using System.Media;

//
//  Application Name: Drunk PC
//  Description: Applcation that generates erratic mouse and keyboard movements and input and generates system sounds and fake dialogs to confuse the user
//

namespace DrunkPC
{
    class Program
    {
        public static Random _random = new Random();

        public static int _startupDelaySeconds = 10;
        public static int _totalDurationSeconds = 10;

        /// <summary>
        /// Entry point for prank application
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("DrunkPC Prank Application by: Jerry (aka. Barnacules)");

            //Check for command line arguments and assign the new values
            if(args.Length >= 2)
            {
                _startupDelaySeconds = Convert.ToInt32(args[0]);
                _totalDurationSeconds = Convert.ToInt32(args[1]);
            }

            //Create all threads that manipulates all of the inputs and outputs of the system
            Thread drunkMouseThread = new Thread(new ThreadStart(DrunkMouseThread));
            Thread drunkKeyboardThread = new Thread(new ThreadStart(DrunkKeyboardThread));
            Thread drunkSoundThread = new Thread(new ThreadStart(DrunkSoundThread));
            Thread drunkPopupThread = new Thread(new ThreadStart(DrunkPopupThread));

            DateTime future = DateTime.Now.AddSeconds(_startupDelaySeconds);
            Console.WriteLine("Waiting some seconds before starting threads");
            while (future > DateTime.Now)
            {
                Thread.Sleep(1000);
            }

            // Start all of the threads
            //drunkMouseThread.Start();
            //drunkKeyboardThread.Start();
            drunkSoundThread.Start();
            drunkPopupThread.Start();

            DateTime future2 = DateTime.Now.AddSeconds(_totalDurationSeconds);
            Console.WriteLine("Waiting some seconds before aborting threads");
            while (future2 > DateTime.Now)
            {
                Thread.Sleep(1000);
            }

            Console.WriteLine("Terminating all threads");

            // Kill all threads and exit application
            //drunkMouseThread.Abort();
            //drunkKeyboardThread.Abort();
            drunkSoundThread.Abort();
            drunkPopupThread.Abort();
        }

        #region Threads
        /// <summary>
        /// This thread will randomly affect the mouse movement to sc*** with the end user
        /// </summary>
        public static void DrunkMouseThread()
        {
            Console.WriteLine("DrunkMouseThread Started");

            int moveX = 0;
            int moveY = 0;

            while (true)
            {
                //Console.WriteLine(Cursor.Position.ToString());

                // Generate random numbers between -10 and 10
                moveX = _random.Next(20) - 10;
                moveY = _random.Next(20) - 10;

                // Change mouse cursor position to new random coordinates
                Cursor.Position = new System.Drawing.Point(
                    Cursor.Position.X + moveX,
                    Cursor.Position.Y + moveY);

                Thread.Sleep(500);
            }
        }

        /// <summary>
        /// This thread will generates random keyboard output to sc*** with the end user
        /// </summary>
        public static void DrunkKeyboardThread()
        {
            Console.WriteLine("DrunkKeyboardThread Started");

            while (true)
            {
                // Generate a random capitol letter
                char key = (char)(_random.Next(25) + 65);

                // 50/50 make it lower case
                if (_random.Next(2) == 0)
                {
                    key = Char.ToLower(key);
                }

                SendKeys.SendWait(key.ToString());

                Thread.Sleep(_random.Next(500));
            }
        }

        /// <summary>
        /// This will play system sounds at random to sc*** with the end user
        /// </summary>
        public static void DrunkSoundThread()
        {
            Console.WriteLine("DrunkSoundThread Started");
            while (true)
            {
                if (_random.Next(100) > 80)
                {
                    switch (_random.Next(5))
                    {
                        case 0:
                            {
                                SystemSounds.Asterisk.Play();
                                break;
                            }
                        case 1:
                            {
                                SystemSounds.Beep.Play();
                                break;
                            }
                        case 2:
                            {
                                SystemSounds.Exclamation.Play();
                                break;
                            }
                        case 3:
                            {
                                SystemSounds.Hand.Play();
                                break;
                            }
                        case 4:
                            {
                                SystemSounds.Question.Play();
                                break;
                            }
                    }
                }
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// This will generate random popups to sc*** with the user
        /// </summary>
        public static void DrunkPopupThread()
        {
            Console.WriteLine("DrunkPopupThread Started");

            while (true)
            {
                // Every 10% show a dialog
                if (_random.Next(100) > 90)
                {
                    // Determine which message to show user
                    switch (_random.Next(2))
                    {
                        case 0:
                            {
                                MessageBox.Show("Internet explorer has stopped working",
                                    "Internet Explorer",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                                break;
                            }
                        case 1:
                            {
                                MessageBox.Show("Your system is running low on resources",
                                    "Microsoft Windows",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                                break;
                            }
                    }
                }
                Thread.Sleep(500);
            }
        }
        #endregion
    }
}
