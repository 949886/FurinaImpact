using System.Buffers.Binary;
using System.Security.Cryptography;
using System.Text;
using FurinaImpact.Security.Util;

namespace FurinaImpact.Common.Security;
public static class MhySecurity
{
    public static byte[] InitialKey { get; }
    public static byte[] InitialKeyEc2b { get; }
    public static byte[] RSAClientPublicKey { get; }

    static MhySecurity()
    {
        InitialKey = File.ReadAllBytes("assets/security/initial_key.bin");
        InitialKeyEc2b = File.ReadAllBytes("assets/security/initial_key.ec2b");
        RSAClientPublicKey = File.ReadAllBytes("assets/security/client_public_key.der");
    }

    public static byte[] GenerateSecretKey(ulong seed)
    {
        byte[] key = GC.AllocateUninitializedArray<byte>(0x1000);
        Span<byte> keySpan = key.AsSpan();

        MT19937 mt = new(seed);
        mt.Int63();

        for (int i = 0; i < 0x1000; i += 8)
        {
            BinaryPrimitives.WriteUInt64BigEndian(keySpan[i..], mt.Int63());
        }

        return key;
    }

    public static byte[] EncryptWithRSA(ReadOnlySpan<byte> data)
    {
        using RSA cipher = RSA.Create();
        cipher.ImportSubjectPublicKeyInfo(RSAClientPublicKey, out _);

        const int chunkSize = 256 - 11;
        int dataLength = data.Length;

        int numChunks = dataLength / chunkSize;
        if ((dataLength - (chunkSize * numChunks)) % chunkSize != 0) ++numChunks;

        if (numChunks < 2)
        {
            return cipher.Encrypt(data, RSAEncryptionPadding.Pkcs1);
        }

        using MemoryStream stream = new();
        for (int i = 0; i < numChunks; i++)
        {
            ReadOnlySpan<byte> chunk = data.Slice(i * chunkSize, Math.Min(dataLength, chunkSize));
            stream.Write(cipher.Encrypt(chunk, RSAEncryptionPadding.Pkcs1));

            dataLength -= chunkSize;
        }

        return stream.ToArray();
    }

    public static byte[] Xor(string data, ReadOnlySpan<byte> key)
    {
        byte[] result = Encoding.UTF8.GetBytes(data);
        Xor(result, key);

        return result;
    }

    public static void Xor(Span<byte> data, ReadOnlySpan<byte> key)
    {
        for (int i = 0; i < data.Length; i++)
        {
            data[i] ^= key[i % key.Length];
        }
    }
}
