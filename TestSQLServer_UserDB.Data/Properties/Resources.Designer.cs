﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestSQLServer_UserDB.Data.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("TestSQLServer_UserDB.Data.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to USE [master]
        ///GO
        ///
        ////****** Object:  Database [UserDB]    Script Date: 10/7/2022 13:39:25 ******/
        ///CREATE DATABASE [UserDB]
        /// CONTAINMENT = NONE
        /// ON  PRIMARY 
        ///( NAME = N&apos;UserDB&apos;, FILENAME = N&apos;C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\UserDB.mdf&apos; , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
        /// LOG ON 
        ///( NAME = N&apos;UserDB_log&apos;, FILENAME = N&apos;C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\UserDB_log.ldf&apos; , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGRO [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string CreateUserDB {
            get {
                return ResourceManager.GetString("CreateUserDB", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 
        ///USE UserDB
        ///GO
        /// IF NOT EXISTS(SELECT * FROM sys.schemas WHERE [name] = N&apos;db_accessadmin&apos;)      
        ///     EXEC (N&apos;CREATE SCHEMA db_accessadmin&apos;)                                   
        /// GO                                                               
        ///
        ///USE UserDB
        ///GO
        /// IF NOT EXISTS(SELECT * FROM sys.schemas WHERE [name] = N&apos;db_backupoperator&apos;)      
        ///     EXEC (N&apos;CREATE SCHEMA db_backupoperator&apos;)                                   
        /// GO                                                               
        ///
        ///USE User [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string CreateUserDBFull {
            get {
                return ResourceManager.GetString("CreateUserDBFull", resourceCulture);
            }
        }
    }
}
