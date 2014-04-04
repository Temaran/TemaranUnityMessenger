// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY 
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
//
// Created by Fredrik Lindh (Temaran)
// Contact me at: temaran (at) gmail (dot) com
// Last changed: 2014-03-15
// You're free to do whatever you want with the code except claiming it's you who wrote it.
// I would also appreciate it if you kept this file header as a thank you for the code :)

/// <summary>
///   A marker interface for classes that subscribe to messages.
/// </summary>
public interface IHandle
{
}

/// <summary>
///   Denotes a class which can handle a particular type of message.
/// </summary>
/// <typeparam name = "TMessage">The type of message to handle.</typeparam>
public interface IHandle<TMessage> : IHandle
{ //don't use contravariance here
    /// <summary>
    ///   Handles the message.
    /// </summary>
    /// <param name = "message">The message.</param>
    void Handle(TMessage message);
}