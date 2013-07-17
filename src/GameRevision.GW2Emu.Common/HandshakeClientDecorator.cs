using System;
//using GameRevision.GW2Emu.Core.Serializers;

//namespace GameRevision.GW2Emu.Network
//{
//    public abstract class HandshakeClientDecorator
//    {
//        public event Action HandshakeDone;
//
//        public Client Client { get; protected set; }
//        public uint Version { get; protected set; }
//        public byte[] RC4Key { get; protected set; }
//
//        public HandshakeClientDecorator(Client client)
//        {
//            this.Client = client;
//            this.Client.DataReceived += OnDataReceived;
//        }
//
//        public void SendKey(byte[] key)
//        {
//            Serializer serializer = new Serializer();
//            serializer.Write((byte)0x01); // RC4Seed Header
//            serializer.Write((byte)0x16); // RC4Seed Length
//            serializer.Write(key); // RC4Key
//            this.Client.Send(serializer.GetBytes());
//        }
//
//        protected void OnHandshakeDone()
//        {
//            if (this.HandshakeDone != null)
//            {
//                this.HandshakeDone();
//            }
//        }
//    }
//}
