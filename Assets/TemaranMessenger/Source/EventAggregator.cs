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
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

/// <summary>
/// Enables loosely-coupled publication of and subscription to events.
/// </summary>
public class EventAggregator : IEventAggregator
{
    /// <summary>
    /// For each type of message we have a list of handler wrappers that hold weak references to their handling classes
    /// </summary>
    readonly Dictionary<Type, List<Handler>> _handlers = new Dictionary<Type, List<Handler>>();

    /// <summary>
    /// Searches the subscribed handlers to check if we have a handler for
    /// the message type supplied.
    /// </summary>
    /// <param name="messageType">The message type to check with</param>
    /// <returns>True if any handler is found, false if not.</returns>
    public bool HandlerExistsFor(Type messageType)
    {
        return _handlers.ContainsKey(messageType) && _handlers[messageType].Any(handler => !handler.IsDead);
    }

    /// <summary>
    ///   Subscribes an instance to all events declared through implementations of <see cref = "IHandle{T}" />
    /// </summary>
    /// <param name = "subscriber">The instance to subscribe for event publication.</param>
    public virtual void Subscribe(object subscriber)
    {
        if (subscriber == null)
        {
            Debug.LogError("The argument 'subscriber' was null. Please specify a non-null MonoBehaviourEx to subscribe to.");
            return;
        }
        lock (_handlers)
        {
            foreach (var interfaceType in subscriber.GetType().GetInterfaces().Where(t => typeof(IHandle).IsAssignableFrom(t) && t.IsGenericType))
            {
                var genericArguments = interfaceType.GetGenericArguments();
                if (genericArguments.Length > 0)
                {
                    var handleType = genericArguments[0];

                    if (_handlers.ContainsKey(handleType) && _handlers[handleType] != null && _handlers[handleType].Any(x => x.Matches(subscriber)))
                        continue;

                    if (!_handlers.ContainsKey(handleType))
                        _handlers.Add(handleType, new List<Handler>());

                    _handlers[handleType].Add(new Handler(subscriber, interfaceType));
                }
            }
        }
    }

    /// <summary>
    ///   Unsubscribes the instance from all events.
    /// </summary>
    /// <param name = "subscriber">The instance to unsubscribe.</param>
    public virtual void Unsubscribe(object subscriber)
    {
        if (subscriber == null)
        {
            Debug.LogError("The argument 'subscriber' was null. Please specify a non-null MonoBehaviourEx to unsubscribe from.");
            return;
        }

        lock (_handlers)
        {
            foreach (var handlerList in _handlers)
            {
                var found = handlerList.Value.FirstOrDefault(x => x.Matches(subscriber));

                if (found != null)
                    handlerList.Value.Remove(found);
            }
        }
    }

    /// <summary>
    ///   Publishes a message.
    /// </summary>
    /// <param name = "message">The message instance.</param>
    public void Publish(object message) { Publish(message, null); }

    /// <summary>
    ///   Publishes a message.
    /// </summary>
    /// <param name = "message">The message instance.</param>
    /// <param name = "commonRoot">If the common root parameter is not null, the publisher requires that only subscribers who are a decendent of this particular transform can process the message.</param>
    public virtual void Publish(object message, Transform commonRoot)
    {
        //Remove this check if you want an itsy bit of more performance if feel comfortable enough with the code :)
        if (message is ValueType)
        {
            Debug.LogError("You're trying to send a message based on a value type, this does not make much sense since there is not context to a type like int or float. Please use reference types instead and name the type with a sensible name which reflects the meaning of the message");
            return;
        }
        //End of optional check

        if (message == null)
        {
            Debug.LogError("The argument 'message' was null. It is not possible to publish null messages, any other object should work however");
            return;
        }

        var messageType = message.GetType();
        if (!_handlers.ContainsKey(messageType))
            return;

        lock (_handlers)
        {
            var toNotify = _handlers[messageType];

            if (commonRoot != null)
            {
                for (var i = 0; i < toNotify.Count; i++)
                {
                    var handler = toNotify[i];

                    if (!handler.SharesCommonTransformRoot(commonRoot))
                        continue;

                    if (handler.Handle(message))
                        continue;

                    //Remove it if its link is broken
                    toNotify.RemoveAt(i);
                    i--;
                }
            }
            else
            {
                for (var i = 0; i < toNotify.Count; i++)
                {
                    if (toNotify[i].Handle(message))
                        continue;

                    //Remove it if its link is broken
                    toNotify.RemoveAt(i);
                    i--;
                }
            }
        }
    }

    /// <summary>
    /// This class is a helper for the messenger, and keeps track on which objects are interested in which message types
    /// </summary>
    private class Handler
    {
        /// <summary>
        /// This is true if our target was garbage collected. In this case, this handle is not needed anymore
        /// </summary>
        public bool IsDead { get { return _reference.Target == null; } }

        private readonly Action<object, object> _handleDelegate;
        private readonly WeakReference _reference;

        /// <summary>
        /// Create a new handler
        /// </summary>
        /// <param name="handler">The object that is interested in a message</param>
        /// <param name="interfaceType">The message type the object is interested in</param>
        public Handler(object handler, Type interfaceType)
        {
            _reference = new WeakReference(handler);

#if !UNITY_IPHONE
            var methodInfo = interfaceType.GetMethod("Handle");
            var genericHelper = typeof(Handler).GetMethod("CreateWeaklyTypedDelegate", BindingFlags.Static | BindingFlags.NonPublic);
            //Create a generic method from the open genericHelper method we requested from our own class. This lets us specify our generic arguments in runtime
            var constructedHelper = genericHelper.MakeGenericMethod(interfaceType, methodInfo.GetParameters()[0].ParameterType);
            //Create a weakly typed Action<object, object> delegate from the strongly typed we created at runtime. 
            //The "constructedHelper" here is actually a runtime version of the CreateWeaklyTypedDelegate method that we are going to invoke! 
            //The null argument is used since it's a static method, and we send the methodinfo as per the signature of CreateWeaklyTypedDelegate(MethodInfo method)
            _handleDelegate = (Action<object, object>)constructedHelper.Invoke(null, new object[] { methodInfo });
#endif
        }

        /// <summary>
        /// Checks to see if this handle is pointing towards a certain monobehaviour
        /// </summary>
        /// <param name="instance">Is this instance the same as the one this handle is handling requests for?</param>
        /// <returns>True if they are the same</returns>
        public bool Matches(object instance)
        {
            return _reference.Target == instance;
        }

        /// <summary>
        /// Checks to see if this handles reference target has the requested transform as ancestor
        /// </summary>
        /// <param name="commonRoot">The transform to check ancestry for</param>
        /// <returns>True if the commonRoot transform is an ancestor to our reference target</returns>
        public bool SharesCommonTransformRoot(Transform commonRoot)
        {
            if (_reference.Target == null)
                return false;

            var monoBehaviour = _reference.Target as MonoBehaviour;

            if (monoBehaviour == null)
                return true; //If our target isn't a monobehaviour, it is always eligable

            //The common root is the transform hierarchy root, this is probably the most common choice, so let's check this first :)
            //Otherwise, check recursively for a common root
            return monoBehaviour.transform.root == commonRoot || CheckParents(monoBehaviour.transform, commonRoot);
        }

        /// <summary>
        /// Send the message to the reference target
        /// </summary>
        /// <param name="message">The message to be sent</param>
        /// <returns>True if the message was sent without problems</returns>
        public bool Handle(object message)
        {
            if (_reference.Target == null)
                return false;

            var monoBehaviour = _reference.Target as MonoBehaviour;
            if (monoBehaviour != null && monoBehaviour.gameObject == null)
                return false;

#if UNITY_IPHONE
            //This is marginally slower than caching the delegate, maybe on average a couple of hundred ms for 10.000.000 runs, so not such a big deal :)
            var method = _reference.Target.GetType().GetMethod("Handle", new[] { message.GetType() });
            method.Invoke(_reference.Target, new[] { message });
#else
            _handleDelegate(monoBehaviour, message);
#endif

            return true;
        }

        /// <summary>
        /// Walks the transform hierarchy and tries to figure out if we can find the wanted transform there
        /// </summary>
        /// <param name="currentTransform">The current transform we are checking right now</param>
        /// <param name="wantedTransform">The ancestor we are looking for</param>
        /// <returns>True if the wanted transform was found in the hierarchy</returns>
        private static bool CheckParents(Transform currentTransform, Transform wantedTransform)
        {
            while (true)
            {
                if (currentTransform == null || wantedTransform == null)
                    return false;

                if (currentTransform == wantedTransform)
                    return true;

                currentTransform = currentTransform.parent;
            }
        }

        private static Action<object, object> CreateWeaklyTypedDelegate<TTarget, TParam>(MethodInfo method)
        {
            // Convert the slow MethodInfo into a fast, strongly typed, open delegate
            var func = (Action<TTarget, TParam>)Delegate.CreateDelegate(typeof(Action<TTarget, TParam>), method);

            // Now create a more weakly typed delegate which will call the strongly typed one
            Action<object, object> ret = (target, param) => func((TTarget)target, (TParam)param);
            return ret;
        }
    }
}