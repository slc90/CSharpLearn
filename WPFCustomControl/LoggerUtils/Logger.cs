﻿using log4net;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace WPFCustomControl.LoggerUtils;

/// <summary>
/// 对log4net的封装，之后就能当做全局静态函数来调用
/// </summary>
public class Logger
{
    /// <summary>
    /// 初始化Logger
    /// </summary>
    static Logger()
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        var xml = assembly.GetManifestResourceStream("CaliburnMicroLearn.LoggerUtils.log4net.config");
        log4net.Config.XmlConfigurator.Configure(xml);
    }

    /// <summary>
    /// 记录Debug级别的信息
    /// </summary>
    /// <param name="msg">具体的log信息</param>
    /// <param name="callerMember">调用的函数名字</param>
    /// <param name="callerLineNumber">调用的具体行数</param>
    public static void Debug(string msg, [CallerFilePath] string callerFile = "",
        [CallerMemberName] string callerMember = "", [CallerLineNumber] int callerLineNumber = 0)
    {
        LogManager.GetLogger(callerFile.Split('.')[^2].Split('\\')[^1] + ":" + callerMember + ":" + callerLineNumber).Debug(msg);
    }

    /// <summary>
    /// 记录Info级别的信息
    /// </summary>
    /// <param name="msg">具体的log信息</param>
    /// <param name="callerMember">调用的函数名字</param>
    /// <param name="callerLineNumber">调用的具体行数</param>
    public static void Info(string msg, [CallerFilePath] string callerFile = "",
        [CallerMemberName] string callerMember = "", [CallerLineNumber] int callerLineNumber = 0)
    {
        LogManager.GetLogger(callerFile.Split('.')[^2].Split('\\')[^1] + ":" + callerMember + ":" + callerLineNumber).Info(msg);
    }

    /// <summary>
    /// 记录Warn级别的信息
    /// </summary>
    /// <param name="msg">具体的log信息</param>
    /// <param name="callerMember">调用的函数名字</param>
    /// <param name="callerLineNumber">调用的具体行数</param>
    public static void Warn(string msg, [CallerFilePath] string callerFile = "",
        [CallerMemberName] string callerMember = "", [CallerLineNumber] int callerLineNumber = 0)
    {
        LogManager.GetLogger(callerFile.Split('.')[^2].Split('\\')[^1] + ":" + callerMember + ":" + callerLineNumber).Warn(msg);
    }

    /// <summary>
    /// 记录Error级别的信息
    /// </summary>
    /// <param name="msg">具体的log信息</param>
    /// <param name="callerMember">调用的函数名字</param>
    /// <param name="callerLineNumber">调用的具体行数</param>
    public static void Error(string msg, [CallerFilePath] string callerFile = "",
        [CallerMemberName] string callerMember = "", [CallerLineNumber] int callerLineNumber = 0)
    {
        LogManager.GetLogger(callerFile.Split('.')[^2].Split('\\')[^1] + ":" + callerMember + ":" + callerLineNumber).Error(msg);
    }

    /// <summary>
    /// 记录Error级别的信息
    /// </summary>
    /// <param name="msg">具体的log信息</param>
    /// <param name="e">抛的异常</param>
    /// <param name="callerMember">调用的函数名字</param>
    /// <param name="callerLineNumber">调用的具体行数</param>
    public static void Error(string msg, Exception e, [CallerFilePath] string callerFile = "",
        [CallerMemberName] string callerMember = "", [CallerLineNumber] int callerLineNumber = 0)
    {
        LogManager.GetLogger(callerFile.Split('.')[^2].Split('\\')[^1] + ":" + callerMember + ":" + callerLineNumber).Error(msg, e);
    }
}