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

using System;
using UnityEngine;

/// <summary>
///   Enables loosely-coupled publication of and subscription to events.
/// </summary>
public interface IEventAggregator
{
    /// <summary>
    /// Searches the subscribed handlers to check if we have a handler for
    /// the message type supplied.
    /// </summary>
    /// <param name="messageType">The message type to check with</param>
    /// <returns>True if any handler is found, false if not.</returns>
    bool HandlerExistsFor(Type messageType);

    /// <summary>
    ///   Subscribes an instance to all events declared through implementations of <see cref = "IHandle{T}" />
    /// </summary>
    /// <param name = "subscriber">The instance to subscribe for event publication.</param>
    void Subscribe(object subscriber);

    /// <summary>
    ///   Unsubscribes the instance from all events.
    /// </summary>
    /// <param name = "subscriber">The instance to unsubscribe.</param>
    void Unsubscribe(object subscriber);

    /// <summary>
    ///   Publishes a message.
    /// </summary>
    /// <param name = "message">The message instance.</param>
    /// <param name = "commonRoot">If the common root parameter is not null, the publisher requires that only subscribers who are a decendent of this particular transform can process the message.</param>
    void Publish(object message, Transform commonRoot);
    /// <summary>
    ///   Publishes a message.
    /// </summary>
    /// <param name = "message">The message instance.</param>
    void Publish(object message);
}
