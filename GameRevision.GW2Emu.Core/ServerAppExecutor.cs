using System;
using GameRevision.GW2Emu.Core.Events;

namespace GameRevision.GW2Emu.Core
{
    public static class ServerAppExecutor
    {
        public static void RunConsole(IServerApp serverApp)
        {
            Console.Title = serverApp.Name;
            serverApp.RegisterHandlers();
            serverApp.EventAggregator.Trigger(new StartupEvent(serverApp));
            serverApp.Run();
        }
    }
}
