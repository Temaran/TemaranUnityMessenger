TemaranUnityMessenger
=====================

Better messaging mechanism for Unity3D.

One of the biggest probems I've had with Unity3D so far is the annoying lack of a decent messanger / event aggregator.
I've looked at some of the solutions available. For example the one on http://wiki.unity3d.com/index.php?title=CSharpMessenger_Extended is decent,
and there are two paid solutions on the asset store right now, but all three are annoyingly enough string based and therefore infuriating to work with
when the projects are getting large due to a couple of reasons:

1. Since the message id's and sometimes callback function id's are strings, as soon as you want to change anything, you need to remember
to change EACH AND EVERY PLACE YOU USE THAT ID. ARRRRR! In some situations (such as the standard unity broadcast message) it just fails silently when you forget!

2. The systems are limited in usage, you cannot easily get return values and extending them is not pretty.

3. It is usually very clumsy coding-wise, requiring you not only to REMEMBER to subscribe and unsubscribe, but the code to do this usually takes up 6-12 lines of code PER CLASS.
ugh. In most implementations, since you need to specify your message names / types it is usually not refactorable to a base class either.



I tried to address these and some other issues when creating this small event aggregation framework:

1. My solution is very fast, faster than any solution I've tested against (Unity broadcasting and the Unify messenger atm)
2. It is type safe, if you forget something since we're using real types here, the compiler will throw complaints right away.
   It also makes it a breeze to refactor using Resharper or other extensions since the IDE has the type information to go on, yum!
3. It's easier, only one line of code to publish, and you don't have to explicitly remember to subscribe and unsubscribe for clients since it is all pushed to the base class.
   If you don't want to use the base class, it's one line of code in your subscriber class (plus the IHandle<> interface declaration of course)
4. It is more useful, since you can also use it for much more than just sending plain messages (see for example the tagging example or the request example)

Hopefully this will be useful for other people too, as it has been for me.
I've always wanted to make an open source project, but since I've been too lazy to clean and set up my other pet projects, this is the first one to see the light of day I'm afraid...

I'll be better in the future, promise.

/Temaran