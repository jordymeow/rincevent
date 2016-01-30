using Meow.FR.Rincevent.Core.Data;
using Meow.FR.Rincevent.Core.Extensibility;
using System;
using System.Threading;

namespace Meow.FR.Rincevent.Core.Extensibility
{
    public abstract class IOModule : BaseModule, IAsyncResult 
    {
        /* INFORMATION */
        abstract public IOModuleType Type { get; }

        /* IMPORT */
        abstract public ContentManager Import();
        abstract public IAsyncResult BeginImport();
        abstract public ContentManager EndImport(IAsyncResult asyncResult);

        /* EXPORT */
        abstract public void Export(ContentManager contentManager);
        abstract public IAsyncResult BeginExport(ContentManager contentManager);
        abstract public void EndExport(IAsyncResult asyncResult);

        /* ASYNC RESULT */
        abstract public object AsyncState { get; }
        abstract public WaitHandle AsyncWaitHandle { get;  }
        abstract public bool CompletedSynchronously { get;  }
        abstract public bool IsCompleted { get;  }
    }
}
