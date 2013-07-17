using System;

namespace GameRevision.GW2Emu.Common.Math
{
    public struct WorldPosition
    {

        public Vector3 Vector;
        public int W;


        public WorldPosition(Vector3 vector, int w)
        {
            Vector = vector;
            W = w;
        }
    }
}
