using System;

namespace EncryptedLegder.Abstractions
{
    public interface IPersonBalance
    {
        decimal Is();
        IPersonBalance From(DateTime date);
        IPersonBalance To(DateTime date);
    }
}
