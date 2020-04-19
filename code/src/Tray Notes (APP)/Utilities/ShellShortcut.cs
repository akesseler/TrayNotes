/*
 * MIT License
 * 
 * Copyright (c) 2020 plexdata.de
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Windows.Forms;

// I had too much time, didn't I?
namespace Plexdata.Shell
{
    /// <summary>
    /// The <see cref="Plexdata.Shell">Shell</see> namespace provides classes, structures 
    /// and interfaces that allow the usage of additional functionality of the Windows Shell.
    /// </summary>
    [System.Runtime.CompilerServices.CompilerGenerated]
    class NamespaceDoc { }

    /// <summary>
    /// This static class allows users to easily create and/or remove shortcuts either 
    /// on the <b>Desktop</b> or under the <b>Startup</b> menu.
    /// </summary>
    /// <remarks>
    /// Be aware, namespace <see cref="System.Windows.Forms"/> provides an enumeration 
    /// with name <see cref="System.Windows.Forms.Shortcut">Shortcut</see>. Therefore, 
    /// it is recommended to use this class only with its fully qualified namespace.
    /// <para>
    /// Special thanks to the guy of answer #8 under 
    /// <a href="http://stackoverflow.com/questions/4897655/create-shortcut-on-desktop-c-sharp" target="_blank">Create shortcut on desktop C#</a>
    /// </para>
    /// <para>
    /// For more information please see description of <c>IShellLink</c> under 
    /// <a href="http://msdn.microsoft.com/en-us/library/windows/desktop/bb774950(v=vs.85).aspx" target="_blank">http://msdn.microsoft.com/</a>
    /// </para>
    /// </remarks>
    public static class Shortcut
    {
        #region Startup related method implementation section.

        /// <summary>
        /// This method tries to determine if a shortcut for the current application exists 
        /// within the user's <b>Startup</b> menu.
        /// </summary>
        /// <remarks>
        /// The current application's executable name is used as default shortcut name.
        /// </remarks>
        /// <returns>
        /// This method returns <c>true</c> if the current application's shortcut exists 
        /// within the <b>Startup</b> menu and <c>false</c> otherwise.
        /// </returns>
        public static bool IsStartupShortcut()
        {
            return Shortcut.IsStartupShortcut(Path.GetFileNameWithoutExtension(Application.ExecutablePath));
        }

        /// <summary>
        /// This method tries to determine if the given shortcut name exists within the 
        /// user's <b>Startup</b> menu.
        /// </summary>
        /// <param name="shortcut">
        /// The shortcut name to be used. This parameter is <b>mandatory</b>.
        /// </param>
        /// <returns>
        /// This method returns <c>true</c> if the given shortcut name exists within the 
        /// <b>Startup</b> menu and <c>false</c> otherwise.
        /// </returns>
        public static bool IsStartupShortcut(string shortcut)
        {
            return Shortcut.IsShortcut(Environment.SpecialFolder.Startup, shortcut);
        }

        /// <summary>
        /// This method creates a shortcut under the user's <b>Startup</b> menu using 
        /// a default shortcut name.
        /// </summary>
        /// <remarks>
        /// The current application's executable name is used as default shortcut name.
        /// </remarks>
        public static void CreateStartupShortcut()
        {
            Shortcut.CreateStartupShortcut(String.Empty);
        }

        /// <summary>
        /// This method creates a shortcut under the user's <b>Startup</b> menu using 
        /// the given shortcut name.
        /// </summary>
        /// <remarks>
        /// The current application's executable name is used if parameter 
        /// <paramref name="shortcut"/> is not set.
        /// </remarks>
        /// <param name="shortcut">
        /// The shortcut name to be used. This parameter is <b>optional</b>.
        /// </param>
        public static void CreateStartupShortcut(string shortcut)
        {
            Shortcut.CreateStartupShortcut(shortcut, String.Empty);
        }

        /// <summary>
        /// This method creates a shortcut under the user's <b>Startup</b> menu using 
        /// given shortcut name and a description as well.
        /// </summary>
        /// <remarks>
        /// The current application's executable name is used if parameter 
        /// <paramref name="shortcut"/> is not set.
        /// </remarks>
        /// <param name="shortcut">
        /// The shortcut name to be used. This parameter is <b>optional</b>.
        /// </param>
        /// <param name="description">
        /// The description of the shortcut to create. This parameter is <b>optional</b>.
        /// </param>
        public static void CreateStartupShortcut(string shortcut, string description)
        {
            Shortcut.CreateStartupShortcut(shortcut, description, Application.ExecutablePath);
        }

        /// <summary>
        /// This method creates a shortcut under the user's <b>Startup</b> menu using 
        /// given shortcut name, description as well as the fully qualified executable 
        /// path.
        /// </summary>
        /// <remarks>
        /// The given executable's name is used if parameter <paramref name="shortcut"/> 
        /// is not set.
        /// </remarks>
        /// <param name="shortcut">
        /// The shortcut name to be used. This parameter is <b>optional</b>.
        /// </param>
        /// <param name="description">
        /// The description of the shortcut to create. This parameter is <b>optional</b>.
        /// </param>
        /// <param name="executable">
        /// The fully qualified executable path. This parameter is <b>mandatory</b>.
        /// </param>
        public static void CreateStartupShortcut(string shortcut, string description, string executable)
        {
            Shortcut.CreateStartupShortcut(shortcut, description, executable, String.Empty);
        }

        /// <summary>
        /// This method creates a shortcut under the user's <b>Startup</b> menu using 
        /// given shortcut name, description, the fully qualified executable path and 
        /// a space separated list of additional arguments.
        /// </summary>
        /// <remarks>
        /// The given executable's name is used if parameter <paramref name="shortcut"/> 
        /// is not set.
        /// </remarks>
        /// <param name="shortcut">
        /// The shortcut name to be used. This parameter is <b>optional</b>.
        /// </param>
        /// <param name="description">
        /// The description of the shortcut to create. This parameter is <b>optional</b>.
        /// </param>
        /// <param name="executable">
        /// The fully qualified executable path. This parameter is <b>mandatory</b>.
        /// </param>
        /// <param name="arguments">
        /// The space separated list of additional arguments. This parameter is <b>optional</b>.
        /// </param>
        public static void CreateStartupShortcut(string shortcut, string description, string executable, string arguments)
        {
            Shortcut.CreateStartupShortcut(shortcut, description, executable, arguments, String.Empty);
        }

        /// <summary>
        /// This method creates a shortcut under the user's <b>Startup</b> menu using 
        /// given shortcut name, description, the fully qualified executable path, a 
        /// space separated list of additional arguments as well as an application's 
        /// working directory.
        /// </summary>
        /// <remarks>
        /// The given executable's name is used if parameter <paramref name="shortcut"/> 
        /// is not set.
        /// </remarks>
        /// <param name="shortcut">
        /// The shortcut name to be used. This parameter is <b>optional</b>.
        /// </param>
        /// <param name="description">
        /// The description of the shortcut to create. This parameter is <b>optional</b>.
        /// </param>
        /// <param name="executable">
        /// The fully qualified executable path. This parameter is <b>mandatory</b>.
        /// </param>
        /// <param name="arguments">
        /// The space separated list of additional arguments. This parameter is <b>optional</b>.
        /// </param>
        /// <param name="working">
        /// The fully qualified working directory of the application. This parameter is 
        /// <b>optional</b>.
        /// </param>
        public static void CreateStartupShortcut(string shortcut, string description, string executable, string arguments, string working)
        {
            Shortcut.CreateShortcut(Environment.SpecialFolder.Startup, executable, shortcut, description, working, arguments);
        }

        /// <summary>
        /// This method removes the shortcut for the current application from the user's 
        /// <b>Startup</b> menu.
        /// </summary>
        /// <remarks>
        /// The current application's executable name is used as default shortcut name.
        /// </remarks>
        public static void RemoveStartupShortcut()
        {
            Shortcut.RemoveStartupShortcut(Path.GetFileNameWithoutExtension(Application.ExecutablePath));
        }

        /// <summary>
        /// This method removes the shortcut for the given shortcut name from the user's 
        /// <b>Startup</b> menu.
        /// </summary>
        /// <param name="shortcut">
        /// The shortcut name to be used. This parameter is <b>mandatory</b>.
        /// </param>
        public static void RemoveStartupShortcut(string shortcut)
        {
            Shortcut.RemoveShortcut(Environment.SpecialFolder.Startup, shortcut);
        }

        #endregion // Startup related method implementation section.

        #region Desktop related method implementation section.

        /// <summary>
        /// This method tries to determine if a shortcut for the current application exists 
        /// on the user's <b>Desktop</b>.
        /// </summary>
        /// <remarks>
        /// The current application's executable name is used as default shortcut name.
        /// </remarks>
        /// <returns>
        /// This method returns <c>true</c> if the current application's shortcut exists 
        /// on the user's <b>Desktop</b> and <c>false</c> otherwise.
        /// </returns>
        public static bool IsDesktopShortcut()
        {
            return Shortcut.IsDesktopShortcut(Path.GetFileNameWithoutExtension(Application.ExecutablePath));
        }

        /// <summary>
        /// This method tries to determine if the given shortcut name exists on the user's 
        /// <b>Desktop</b>.
        /// </summary>
        /// <param name="shortcut">
        /// The shortcut name to be used. This parameter is <b>mandatory</b>.
        /// </param>
        /// <returns>
        /// This method returns <c>true</c> if the given shortcut name exists on the user's 
        /// <b>Desktop</b> and <c>false</c> otherwise.
        /// </returns>
        public static bool IsDesktopShortcut(string shortcut)
        {
            return Shortcut.IsShortcut(Environment.SpecialFolder.DesktopDirectory, shortcut);
        }

        /// <summary>
        /// This method creates a shortcut on the user's <b>Desktop</b> using a default 
        /// shortcut name.
        /// </summary>
        /// <remarks>
        /// The current application's executable name is used as default shortcut name.
        /// </remarks>
        public static void CreateDesktopShortcut()
        {
            Shortcut.CreateDesktopShortcut(String.Empty);
        }

        /// <summary>
        /// This method creates a shortcut on the user's <b>Desktop</b> using the given 
        /// shortcut name.
        /// </summary>
        /// <remarks>
        /// The current application's executable name is used if parameter 
        /// <paramref name="shortcut"/> is not set.
        /// </remarks>
        /// <param name="shortcut">
        /// The shortcut name to be used. This parameter is <b>optional</b>.
        /// </param>
        public static void CreateDesktopShortcut(string shortcut)
        {
            Shortcut.CreateDesktopShortcut(shortcut, String.Empty);
        }

        /// <summary>
        /// This method creates a shortcut on the user's <b>Desktop</b> using given 
        /// shortcut name and a description as well.
        /// </summary>
        /// <remarks>
        /// The current application's executable name is used if parameter 
        /// <paramref name="shortcut"/> is not set.
        /// </remarks>
        /// <param name="shortcut">
        /// The shortcut name to be used. This parameter is <b>optional</b>.
        /// </param>
        /// <param name="description">
        /// The description of the shortcut to create. This parameter is <b>optional</b>.
        /// </param>
        public static void CreateDesktopShortcut(string shortcut, string description)
        {
            Shortcut.CreateDesktopShortcut(shortcut, description, Application.ExecutablePath);
        }

        /// <summary>
        /// This method creates a shortcut on the user's <b>Desktop</b> using given 
        /// shortcut name, description as well as the fully qualified executable path.
        /// </summary>
        /// <remarks>
        /// The given executable's name is used if parameter <paramref name="shortcut"/> 
        /// is not set.
        /// </remarks>
        /// <param name="shortcut">
        /// The shortcut name to be used. This parameter is <b>optional</b>.
        /// </param>
        /// <param name="description">
        /// The description of the shortcut to create. This parameter is <b>optional</b>.
        /// </param>
        /// <param name="executable">
        /// The fully qualified executable path. This parameter is <b>mandatory</b>.
        /// </param>
        public static void CreateDesktopShortcut(string shortcut, string description, string executable)
        {
            Shortcut.CreateDesktopShortcut(shortcut, description, executable, String.Empty);
        }

        /// <summary>
        /// This method creates a shortcut on the user's <b>Desktop</b> using given 
        /// shortcut name, description, the fully qualified executable path and a 
        /// space separated list of additional arguments.
        /// </summary>
        /// <remarks>
        /// The given executable's name is used if parameter <paramref name="shortcut"/> 
        /// is not set.
        /// </remarks>
        /// <param name="shortcut">
        /// The shortcut name to be used. This parameter is <b>optional</b>.
        /// </param>
        /// <param name="description">
        /// The description of the shortcut to create. This parameter is <b>optional</b>.
        /// </param>
        /// <param name="executable">
        /// The fully qualified executable path. This parameter is <b>mandatory</b>.
        /// </param>
        /// <param name="arguments">
        /// The space separated list of additional arguments. This parameter is <b>optional</b>.
        /// </param>
        public static void CreateDesktopShortcut(string shortcut, string description, string executable, string arguments)
        {
            Shortcut.CreateDesktopShortcut(shortcut, description, executable, arguments, String.Empty);
        }

        /// <summary>
        /// This method creates a shortcut on the user's <b>Desktop</b> using given 
        /// shortcut name, description, the fully qualified executable path, a space 
        /// separated list of additional arguments as well as an application's working 
        /// directory.
        /// </summary>
        /// <remarks>
        /// The given executable's name is used if parameter <paramref name="shortcut"/> 
        /// is not set.
        /// </remarks>
        /// <param name="shortcut">
        /// The shortcut name to be used. This parameter is <b>optional</b>.
        /// </param>
        /// <param name="description">
        /// The description of the shortcut to create. This parameter is <b>optional</b>.
        /// </param>
        /// <param name="executable">
        /// The fully qualified executable path. This parameter is <b>mandatory</b>.
        /// </param>
        /// <param name="arguments">
        /// The space separated list of additional arguments. This parameter is <b>optional</b>.
        /// </param>
        /// <param name="working">
        /// The fully qualified working directory of the application. This parameter is 
        /// <b>optional</b>.
        /// </param>
        public static void CreateDesktopShortcut(string shortcut, string description, string executable, string arguments, string working)
        {
            Shortcut.CreateShortcut(Environment.SpecialFolder.DesktopDirectory, executable, shortcut, description, working, arguments);
        }

        /// <summary>
        /// This method removes the shortcut for the current application from the user's 
        /// <b>Desktop</b>.
        /// </summary>
        /// <remarks>
        /// The current application's executable name is used as default shortcut name.
        /// </remarks>
        public static void RemoveDesktopShortcut()
        {
            Shortcut.RemoveDesktopShortcut(Path.GetFileNameWithoutExtension(Application.ExecutablePath));
        }

        /// <summary>
        /// This method removes the shortcut for the given shortcut name from the user's 
        /// <b>Desktop</b>.
        /// </summary>
        /// <param name="shortcut">
        /// The shortcut name to be used. This parameter is <b>mandatory</b>.
        /// </param>
        public static void RemoveDesktopShortcut(string shortcut)
        {
            Shortcut.RemoveShortcut(Environment.SpecialFolder.DesktopDirectory, shortcut);
        }

        #endregion // Desktop related method implementation section.

        #region Private method implementation section.

        /// <summary>
        /// This method tries to determine if the given shortcut exists within the given 
        /// special folder.
        /// </summary>
        /// <param name="destination">
        /// The shortcut's destination. This parameter is <b>mandatory</b>.
        /// </param>
        /// <param name="shortcut">
        /// The shortcut name to remove. This parameter is <b>mandatory</b>.
        /// </param>
        /// <returns>
        /// This method returns <c>true</c> if a shortcut exists for the combination of 
        /// special folder and shortcut name and <c>false</c> otherwise.
        /// </returns>
        private static bool IsShortcut(Environment.SpecialFolder destination, string shortcut)
        {
            // Try get the fully qualified path of the shell 
            // shortcut file. This call may cause an exception.
            return File.Exists(Shortcut.GetLinkFilePath(destination, shortcut));
        }

        /// <summary>
        /// This method creates a shortcut for a given special folder using given shortcut 
        /// name, description, the fully qualified executable path and a space separated 
        /// list of additional arguments.
        /// </summary>
        /// <remarks>
        /// The given executable's name is used if parameter <paramref name="shortcut"/> 
        /// is not set.
        /// </remarks>
        /// <param name="destination">
        /// The shortcut's destination. This parameter is <b>mandatory</b>.
        /// </param>
        /// <param name="executable">
        /// The fully qualified executable path. This parameter is <b>mandatory</b>.
        /// </param>
        /// <param name="shortcut">
        /// The shortcut name to be used. This parameter is <b>optional</b>. 
        /// </param>
        /// <param name="description">
        /// The description of the shortcut to create. This parameter is <b>optional</b>.
        /// </param>
        /// <param name="working">
        /// The fully qualified working directory of the application. This parameter is 
        /// <b>optional</b>.
        /// </param>
        /// <param name="arguments">
        /// The space separated list of additional arguments. This parameter is <b>optional</b>.
        /// </param>
        private static void CreateShortcut(Environment.SpecialFolder destination, string executable, string shortcut, string description, string working, string arguments)
        {
            if (String.IsNullOrEmpty(executable))
            {
                throw new ArgumentNullException("executable");
            }

            if (!File.Exists(executable))
            {
                throw new FileNotFoundException((new FileNotFoundException()).Message, executable);
            }

            // Get shortcut name from executable, if necessary.
            if (String.IsNullOrEmpty(shortcut))
            {
                shortcut = Path.GetFileNameWithoutExtension(executable);
            }

            // Try get the fully qualified path of the shell shortcut file. 
            // This call may cause an exception. Therefore, get this path 
            // before anything else is done.
            string path = Shortcut.GetLinkFilePath(destination, shortcut);

            // Create and setup the shell shortcut information.
            IShellLink link = (IShellLink)new ShellLink();
            link.SetPath(executable);

            if (!String.IsNullOrEmpty(description)) { link.SetDescription(description); }
            if (!String.IsNullOrEmpty(working)) { link.SetWorkingDirectory(working); }
            if (!String.IsNullOrEmpty(arguments)) { link.SetArguments(arguments); }

            // Finally, save current shell shortcut information into its file.
            IPersistFile file = (IPersistFile)link;
            file.Save(path, false);
        }

        /// <summary>
        /// This method removes the shortcut for the given shortcut name from the given 
        /// special folder.
        /// </summary>
        /// <param name="destination">
        /// The shortcut's destination. This parameter is <b>mandatory</b>.
        /// </param>
        /// <param name="shortcut">
        /// The shortcut name to remove. This parameter is <b>mandatory</b>.
        /// </param>
        private static void RemoveShortcut(Environment.SpecialFolder destination, string shortcut)
        {
            // Try get the fully qualified path of the shell 
            // shortcut file. This call may cause an exception.
            File.Delete(Shortcut.GetLinkFilePath(destination, shortcut));
        }

        /// <summary>
        /// This method determines the fully qualified physical path for a combination 
        /// of given destination and shortcut.
        /// </summary>
        /// <param name="destination">
        /// The shortcut's destination. This parameter is <b>mandatory</b>.
        /// </param>
        /// <param name="shortcut">
        /// The shortcut name to remove. This parameter is <b>mandatory</b>.
        /// </param>
        /// <returns>
        /// The fully qualified physical path of the requested shortcut file.
        /// </returns>
        private static string GetLinkFilePath(Environment.SpecialFolder destination, string shortcut)
        {
            return Path.Combine(Environment.GetFolderPath(destination), String.Format("{0}.lnk", shortcut));
        }

        #endregion // Private method implementation section.

        #region External COM classes and interfaces.

        /// <exclude />
        [ComImport]
        [Guid("00021401-0000-0000-C000-000000000046")]
        private class ShellLink
        {
            // This is simply a wrapper class and thus 
            // nothing else has to be implemented!
        }

        /// <exclude />
        [ComImport]
        [Guid("000214F9-0000-0000-C000-000000000046")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        private interface IShellLink
        {
            void GetPath([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszFile, int cchMaxPath, out IntPtr pfd, int fFlags);
            void GetIDList(out IntPtr ppidl);
            void SetIDList(IntPtr pidl);
            void GetDescription([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszName, int cchMaxName);
            void SetDescription([MarshalAs(UnmanagedType.LPWStr)] string pszName);
            void GetWorkingDirectory([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszDir, int cchMaxPath);
            void SetWorkingDirectory([MarshalAs(UnmanagedType.LPWStr)] string pszDir);
            void GetArguments([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszArgs, int cchMaxPath);
            void SetArguments([MarshalAs(UnmanagedType.LPWStr)] string pszArgs);
            void GetHotkey(out short pwHotkey);
            void SetHotkey(short wHotkey);
            void GetShowCmd(out int piShowCmd);
            void SetShowCmd(int iShowCmd);
            void GetIconLocation([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszIconPath, int cchIconPath, out int piIcon);
            void SetIconLocation([MarshalAs(UnmanagedType.LPWStr)] string pszIconPath, int iIcon);
            void SetRelativePath([MarshalAs(UnmanagedType.LPWStr)] string pszPathRel, int dwReserved);
            void Resolve(IntPtr hwnd, int fFlags);
            void SetPath([MarshalAs(UnmanagedType.LPWStr)] string pszFile);
        }

        #endregion // External COM classes and interfaces.
    }
}
