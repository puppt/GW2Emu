using System;

namespace GameRevision.GW2Emu.Core.Types
{
    public struct WorldPosition
    {
        public Vector3 Vector;
        public int W;

        public WorldPosition(Vector3 vector, int w)
        {
            this.Vector = vector;
            this.W = w;
        }
    }
}
