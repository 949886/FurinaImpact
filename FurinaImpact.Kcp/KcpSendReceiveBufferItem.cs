namespace FurinaImpact.Kcp
{
    internal struct KcpSendReceiveBufferItem
    {
        public KcpBuffer Data;
        public KcpPacketHeader Segment;
        public KcpSendSegmentStats Stats;
    }
}
