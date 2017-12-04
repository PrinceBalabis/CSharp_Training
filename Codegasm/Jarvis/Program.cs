using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.Speech.Synthesis;

namespace Jarvis
{
    class Program
    {

        private static SpeechSynthesizer synth = new SpeechSynthesizer();

        /// <summary>
        /// Where all the magic happens
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //List of messages that will be selected at random when the CPU is hammered
            List<string> cpuMaxedOutMessages = new List<string>;
            cpuMaxedOutMessages.Add("WARNING HOLY CRAP YOUR CPU IS ABOUT TO CATCH FIRE");
            cpuMaxedOutMessages.Add("OMG YOU SHOULD NOT RUN YOUR CPU THAT HARD");
            cpuMaxedOutMessages.Add("WARNING STOP DOWNLOADING THE P*** ITS MAXING ME OUT");
            cpuMaxedOutMessages.Add("WARNING YOUR CPU IS OFICIALLY CHASING SQUIREELS");
            cpuMaxedOutMessages.Add("RED ALERT! RED ALERT! RED ALERT! I FARTED");
            Random rand = new Random();


            // This will greet the user in a default voice
            //synth.Speak("Welcome to Jarvis version one point O");

            #region My Performance Counters
            // This will pull the current CPU load in percentage
            PerformanceCounter perfCpuCount = new PerformanceCounter("Processor Information", "% Processor Time", "_Total");
            perfCpuCount.NextValue(); // need to call NextValue to initialize

            // This will pull the current available memory in Megabytes
            PerformanceCounter perfMemCount = new PerformanceCounter("Memory", "Available MBytes");
            perfMemCount.NextValue(); // need to call NextValue to initialize

            // This will get us the system uptime in seconds
            PerformanceCounter perfUptimeCount = new PerformanceCounter("System", "System Up Time");
            perfUptimeCount.NextValue(); // need to call NextValue to initialize
            #endregion

            TimeSpan uptimeSpan = TimeSpan.FromSeconds(perfUptimeCount.NextValue());
            string systemUptimeMessage = string.Format("The current system up time is {0} days and {1} hours {2} minutes {3} seconds",
                uptimeSpan.Days,
                uptimeSpan.Hours,
                uptimeSpan.Minutes,
                uptimeSpan.Seconds
                );
            // Tell the user what the current system uptime is
            JerrySpeak(systemUptimeMessage, VoiceGender.Male, 2);

            int speechSpeed = 1; // This will increment each time it runs and so the voice will be faster

            // Infinite While Loop
            while (true)
            {

                int currentCpuPercentage = (int)perfCpuCount.NextValue();
                int currentAvailableMemory = (int)perfMemCount.NextValue();

                //Every 1 second print the CPU load in percentage to the screen
                Thread.Sleep(1000);
                Console.WriteLine("CPU Load         : {0}%", currentCpuPercentage);
                Console.WriteLine("Available Memory : {0}MB", currentAvailableMemory);

                // Only tell us when the CPU usage is above 80%
                #region Logic
                if (currentCpuPercentage > 80)
                {
                    if (currentCpuPercentage == 100)
                    {
                        string cpuLoadVocalMessage = cpuMaxedOutMessages[rand.Next(5)];
                        JerrySpeak(cpuLoadVocalMessage, VoiceGender.Female, speechSpeed++);
                    }
                    else
                    {
                        string cpuLoadVocalMessage = String.Format("The current CPU load is {0}", currentCpuPercentage);
                        JerrySpeak(cpuLoadVocalMessage, VoiceGender.Male, 5);
                    }
                }
                #endregion

                // Only tell us when the memory is below one gigabyte
                if (currentAvailableMemory < 1024)
                {
                    string memAvailableVolcalMessage = String.Format("you currently have {0} megabytes of memory available", currentAvailableMemory);
                    JerrySpeak(memAvailableVolcalMessage, VoiceGender.Male, 10);
                }

                Thread.Sleep(1000);
            } // end of loop
        }

        /// <summary>
        /// Speaks a message with a selected voice
        /// </summary>
        /// <returns></returns>
        public static void JerrySpeak(string message, VoiceGender voiceGender)
        {
            synth.SelectVoiceByHints(voiceGender);
            synth.Speak(message);
        }

        /// <summary>
        /// Speaks with a selected voice at a selected speed
        /// </summary>
        /// <returns></returns>
        public static void JerrySpeak(string message, VoiceGender voiceGender, int rate)
        {
            synth.Rate = rate;
            JerrySpeak(message, voiceGender);
        }

        /// <summary>
        /// FUNCTIONCOMMENT
        /// </summary>
        /// <returns></returns>
        public static void OpenWebsite(string URL)
        {
            // Start a new window of Chrome
            Process p1 = new Process();
            p1.StartInfo.FileName = "chrome.exe";
            p1.StartInfo.Arguments = URL;
            p1.StartInfo.WindowStyle = ProcessWindowStyle.Maximized();
            p1.Start();

        }
    }
}
