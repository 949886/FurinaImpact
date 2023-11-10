namespace FurinaImpact.Security.Util;

internal class MT19937
{
    private const ulong N = 312;
    private const ulong M = 156;
    private const ulong MATRIX_A = 0xB5026F5AA96619E9L;
    private const ulong UPPER_MASK = 0xFFFFFFFF80000000;
    private const ulong LOWER_MASK = 0X7FFFFFFFUL;

    private readonly ulong[] _mt = new ulong[N + 1];
    private ulong _mti = N + 1;

    public MT19937(ulong seed)
    {
        this.Seed(seed);
    }

    public void Seed(ulong seed)
    {
        _mt[0] = seed;
        for (_mti = 1; _mti < N; _mti++)
        {
            _mt[_mti] = (6364136223846793005L * (_mt[_mti - 1] ^ (_mt[_mti - 1] >> 62)) + _mti);
        }
    }

    public ulong Int63()
    {
        ulong x = 0;
        ulong[] mag01 = new ulong[2] { 0x0UL, MATRIX_A };

        if (_mti >= N)
        {
            ulong kk;
            if (_mti == N + 1)
            {
                Seed(5489UL);
            }
            for (kk = 0; kk < (N - M); kk++)
            {
                x = (_mt[kk] & UPPER_MASK) | (_mt[kk + 1] & LOWER_MASK);
                _mt[kk] = _mt[kk + M] ^ (x >> 1) ^ mag01[x & 0x1UL];
            }
            for (; kk < N - 1; kk++)
            {
                x = (_mt[kk] & UPPER_MASK) | (_mt[kk + 1] & LOWER_MASK);
                _mt[kk] = _mt[kk - M] ^ (x >> 1) ^ mag01[x & 0x1UL];
            }
            x = (_mt[N - 1] & UPPER_MASK) | (_mt[0] & LOWER_MASK);
            _mt[N - 1] = _mt[M - 1] ^ (x >> 1) ^ mag01[x & 0x1UL];

            _mti = 0;
        }

        x = _mt[_mti++];
        x ^= (x >> 29) & 0x5555555555555555L;
        x ^= (x << 17) & 0x71D67FFFEDA60000L;
        x ^= (x << 37) & 0xFFF7EEE000000000L;
        x ^= (x >> 43);
        return x;
    }

    public ulong IntN(ulong value)
    {
        return Int63() % value;
    }
}