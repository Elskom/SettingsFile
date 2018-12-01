// Copyright (c) 2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Elskom.Generic.Libs
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using XmlAbstraction;

    /// <summary>
    /// A class that handles the settings for any application.
    /// </summary>
    public static class SettingsFile
    {
        /// <summary>
        /// Gets or sets the settings file XMLObject instance.
        ///
        /// This is designed so there is globally only
        /// a single instance to save time, and memory.
        /// </summary>
        /// <value>
        /// The settings file XMLObject instance.
        ///
        /// This is designed so there is globally only
        /// a single instance to save time, and memory.
        /// </value>
        public static XmlObject Settingsxml { get; set; }

        /// <summary>
        /// Gets the path to the Application Settings file.
        ///
        /// Creates the folder if needed.
        /// </summary>
        /// <value>
        /// The path to the Application Settings file.
        ///
        /// Creates the folder if needed.
        /// </value>
        public static string Path
        {
            get
            {
                // We cannot use System.Windows.Forms.Application.LocalUserAppDataPath as it would
                // Create annoying folders, and throw annoying Exceptions making it harder to
                // debug as it spams the debugger. Also then we would not need to Replace
                // everything added to the path obtained from System.Environment.GetFolderPath.
                var localPath = Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData);
                var thisProcess = Process.GetCurrentProcess();
                localPath += Path.DirectorySeparatorChar + thisProcess.ProcessName;
                thisProcess.Dispose();
                if (!Directory.Exists(localPath))
                {
                    Directory.CreateDirectory(localPath);
                }

                // do not create settings file, just pass this path to XmlObject.
                // if we create it ourselves the new optimized class will fail
                // to work right if it is empty.
                localPath += Path.DirectorySeparatorChar + "Settings.xml";
                return localPath;
            }
        }

        /// <summary>
        /// Gets the path to the Application Error Log file.
        ///
        /// Creates the Error Log file if needed.
        /// </summary>
        public static string ErrorLogPath
        {
            get
            {
                var localPath = Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData);
                var thisProcess = Process.GetCurrentProcess();
                localPath += Path.DirectorySeparatorChar + thisProcess.ProcessName;
                localPath += Path.DirectorySeparatorChar + thisProcess.ProcessName + "-" + thisProcess.Id.ToString() + ".log";
                thisProcess.Dispose();
                return localPath;
            }
        }

        /// <summary>
        /// Gets the path to the Application Mini-Dump file.
        /// </summary>
        public static string MiniDumpPath
        {
            get
            {
                var localPath = Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData);
                var thisProcess = Process.GetCurrentProcess();
                localPath += Path.DirectorySeparatorChar + thisProcess.ProcessName;
                localPath += Path.DirectorySeparatorChar + thisProcess.ProcessName + "-" + thisProcess.Id.ToString() + ".mdmp";
                thisProcess.Dispose();
                return localPath;
            }
        }
    }
}
